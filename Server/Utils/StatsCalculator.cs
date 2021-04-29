using Microsoft.Azure.Cosmos.Table;
using paautoauctionapp.Server.Entity;
using paautoauctionapp.Shared.Utils;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace paautoauctionapp.Shared.Utils
{

    public static class StatsCalculator
    {

        public static void UpdateStats(CloudTable EntriesTable, CloudTable StatsTable)
        {

            foreach (StatsEntry entry in StatsTable.ExecuteQuery<StatsEntry>(new TableQuery<StatsEntry>()))
            {
                TableOperation op = TableOperation.Delete(entry);
                StatsTable.Execute(op);
            }

            var statsDict = new Dictionary<string, StatsContainer>();

            foreach (SaleEntry entry in EntriesTable.ExecuteQuery<SaleEntry>(new TableQuery<SaleEntry>()))
            {
                // make, model, year, mileageboundid
                string key = $"{entry.Make}_{entry.Model}_{entry.Year}_{entry.MileageBoundId}";

                if (statsDict.ContainsKey(key))
                {
                    statsDict[key].SaleEntries.Add(entry);
                }
                else
                {
                    statsDict[key] = new StatsContainer
                    {
                        Key = key,
                        Make = entry.Make,
                        Model = entry.Model,
                        Year = entry.Year,
                        MileageBoundId = entry.MileageBoundId,
                        SaleEntries = new List<SaleEntry> {
                            entry
                        }
                    };
                }

            }

            foreach (var sc in statsDict)
            {
                var entry = new StatsEntry(sc.Value.Make, $"{sc.Value.Model}_{sc.Value.Year}_{sc.Value.MileageBoundId}");

                var priceSorted = sc.Value.SaleEntries.OrderBy(e => e.SalePrice).ToList();

                entry.Model = sc.Value.Model;
                entry.Year = sc.Value.Year;
                entry.SampleSize = sc.Value.SaleEntries.Count;
                entry.MinSalePrice = priceSorted.First().SalePrice;
                entry.MaxSalePrice = priceSorted.Last().SalePrice;
                entry.MeanSalePrice = priceSorted.Sum(e => e.SalePrice) / entry.SampleSize;
                entry.MileageBoundId = sc.Value.MileageBoundId;
                entry.MileageBoundDescription = MileageBounds.Bounds[entry.MileageBoundId].ToString();

                double arithmeticMean = (double)priceSorted.Sum(e => e.SalePrice) / (double)entry.SampleSize;
                double diffSum = 0;

                foreach (var sale in sc.Value.SaleEntries)
                {
                    diffSum += Pow((sale.SalePrice - arithmeticMean), 2);
                }

                double variance = diffSum / entry.SampleSize;
                int stdev = (int)Round(Sqrt(variance), 0);

                entry.LowStandardDev = entry.MeanSalePrice - stdev;
                entry.HighStandardDev = entry.MeanSalePrice + stdev;
                entry.StandardDevHigh50 = entry.MeanSalePrice + (stdev / 2);

                var op = TableOperation.InsertOrMerge(entry);

                StatsTable.Execute(op);

            }

        }

        private class StatsContainer
        {

            public string Key { get; set; }

            public string Make { get; set; }

            public string Model { get; set; }

            public int Year { get; set; }

            public int MileageBoundId { get; set; }

            public List<SaleEntry> SaleEntries { get; set; }

        }

    }

}