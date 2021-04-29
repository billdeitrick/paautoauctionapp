using Microsoft.Azure.Cosmos.Table;
using System;

namespace paautoauctionapp.Server.Entity
{
    public class SaleEntry : TableEntity
    {

        public SaleEntry() { }

        public SaleEntry(int saleNumber, int itemNumber)
        {
            ItemNumber = itemNumber;
            SaleNumber = saleNumber;
            PartitionKey = saleNumber.ToString();
            RowKey = itemNumber.ToString();
        }

        public DateTime SaleDate { get; set; }
        public int ItemNumber { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Mileage { get; set; }
        public int MileageBoundId { get; set; }
        public int DeptNumber { get; set; }
        public int SaleNumber { get; set; }
        public string Comments { get; set; }
        public int SalePrice { get; set; }

    }

}