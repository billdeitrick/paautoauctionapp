using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;

namespace paautoauctionapp.Server.Services
{

    public class DatasetStorageService
    {

        public CloudStorageAccount StorageAccount { get; }
        public CloudTableClient TableClient { get; }
        public CloudTable SaleEntryTable { get; }

        public CloudTable StatsEntryTable { get; }

        public DatasetStorageService(IConfiguration configuration)
        {
            StorageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("StorageConnectionString"));
            TableClient = StorageAccount.CreateCloudTableClient(new TableClientConfiguration());
            SaleEntryTable = TableClient.GetTableReference("saleentries");
            StatsEntryTable = TableClient.GetTableReference("statsentries");
        }

    }

}