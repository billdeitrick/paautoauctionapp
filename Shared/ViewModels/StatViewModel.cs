namespace paautoauctionapp.Shared.ViewModels
{

    public class StatViewModel
    {

        public string Make { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

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