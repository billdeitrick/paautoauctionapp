using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using paautoauctionapp.Shared.ViewModels;
using paautoauctionapp.Shared.Utils;
using paautoauctionapp.Server.Services;
using paautoauctionapp.Server.Entity;
using Microsoft.Azure.Cosmos.Table;

namespace paautoauctionapp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatasetController : ControllerBase
    {
        private readonly ILogger<DatasetController> _logger;
        private readonly CloudTable _saleEntryTable;
        private readonly CloudTable _statsEntryTable;

        public DatasetController(ILogger<DatasetController> logger, DatasetStorageService storageService)
        {
            _logger = logger;
            _saleEntryTable = storageService.SaleEntryTable;
            _statsEntryTable = storageService.StatsEntryTable;
        }

        [HttpPut]
        public IActionResult Upload(DatasetUploadViewModel dataset)
        {

            //This could be chunked into 100 operations and batched
            foreach (var model in dataset.SaleEntries)
            {
                var entity = new SaleEntry(model.SaleNumber, model.ItemNumber);
                entity.SaleDate = dataset.SaleDate;
                entity.Year = model.Year;
                entity.Make = model.Make;
                entity.Model = model.Model;
                entity.Mileage = model.Mileage;
                entity.DeptNumber = model.DeptNumber;
                entity.SaleNumber = model.SaleNumber;
                entity.Comments = model.Comments;
                entity.SalePrice = model.SalePrice;
                entity.MileageBoundId = MileageBounds.GetMileageBoundId(model.Mileage);

                var op = TableOperation.InsertOrMerge(entity);

                _saleEntryTable.Execute(op);
            }

            StatsCalculator.UpdateStats(_saleEntryTable, _statsEntryTable);

            return Ok();
        }

        /// <summary>
        /// Refreshes the statistics table from the current data.
        /// </summary>
        /// <returns>Result of the operation.</returns>
        [HttpPatch]
        public IActionResult UpdateStats()
        {

            StatsCalculator.UpdateStats(_saleEntryTable, _statsEntryTable);

            return Ok();

        }

    }
}
