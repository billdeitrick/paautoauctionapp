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
    public class StatsController : ControllerBase
    {
        private readonly ILogger<StatsController> _logger;
        private readonly CloudTable _saleEntryTable;
        private readonly CloudTable _statsEntryTable;

        public StatsController(ILogger<StatsController> logger, DatasetStorageService storageService)
        {
            _logger = logger;
            _saleEntryTable = storageService.SaleEntryTable;
            _statsEntryTable = storageService.StatsEntryTable;
        }

        [HttpGet]
        public StatsViewModel Get()
        {

            var vm = new StatsViewModel();
            vm.stats = new List<StatViewModel>();

            foreach (StatsEntry entry in _statsEntryTable.ExecuteQuery<StatsEntry>(new TableQuery<StatsEntry>()))
            {
                vm.stats.Add(
                    new StatViewModel
                    {
                        Make = entry.PartitionKey,
                        Model = entry.Model,
                        Year = entry.Year,
                        MileageBoundDescription = entry.MileageBoundDescription,
                        SampleSize = entry.SampleSize,
                        MinSalePrice = entry.MinSalePrice,
                        MaxSalePrice = entry.MaxSalePrice,
                        MeanSalePrice = entry.MeanSalePrice,
                        HighStandardDev = entry.HighStandardDev,
                        LowStandardDev = entry.LowStandardDev,
                        StandardDevHigh50 = entry.StandardDevHigh50
                    }
                );
            }

            return vm;
        }

    }
}
