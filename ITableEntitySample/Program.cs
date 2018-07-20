using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITableEntitySample
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount StorageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            CloudTableClient TableClient = StorageAccount.CreateCloudTableClient();
            CloudTable PersonTable = TableClient.GetTableReference("Person");
            PersonTable.CreateIfNotExists();

            TableResult Result = PersonTable.Execute(TableOperation.InsertOrReplace(new PersonEntity()
            {
                PartitionKey = "Partition2",
                RowKey = "Row2",
                FirstName = "FirstName2",
                LastName = "LastName2"
            }));
        }
    }

    public class PersonEntity : ITableEntity
    {
        
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string ETag { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            TableEntity.ReadUserObject(this, properties, operationContext);
        }

        public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            return TableEntity.WriteUserObject(this, operationContext);
        }
    }

}
