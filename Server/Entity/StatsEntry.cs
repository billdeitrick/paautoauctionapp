using Microsoft.Azure.Cosmos.Table;

namespace paautoauctionapp.Server.Entity
{

    public class StatsEntry : TableEntity
    {

        public StatsEntry() { }

        public StatsEntry(string make, string modelYearMileageBoundId)
        {

            PartitionKey = make;
            RowKey = modelYearMileageBoundId;
            Make = make;

        }

        public string Make { get; }

        public string Model { get; set; }

        public int Year { get; set; }

        public int MileageBoundId { get; set; }

        public string MileageBoundDescription { get; set; }

        public int SampleSize { get; set; }

        public int MinSalePrice { get; set; }

        public int MaxSalePrice { get; set; }

        public int MeanSalePrice { get; set; }

        public int LowStandardDev { get; set; }

        public int HighStandardDev { get; set; }

        public int StandardDevHigh50 { get; set; }

    }

}