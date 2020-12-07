using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents;

namespace NetCoreWebApp
{
    public class StorageHelpers
    {
        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
               // Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
               // Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
               // Console.ReadLine();
                throw;
            }

            return storageAccount;
        }

        public static async Task<CloudTable> CreateTableAsync(string tableName)
        {
            string storageConnectionString = AppSettings.LoadAppSettings().StorageConnectionString;

            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(storageConnectionString);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

           // Console.WriteLine("Create a Table for the demo");

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(tableName);
            if (await table.CreateIfNotExistsAsync())
            //{
            //    Console.WriteLine("Created Table named: {0}", tableName);
            //}
            //else
            //{
            //    Console.WriteLine("Table {0} already exists", tableName);
            //}

            Console.WriteLine();
            return table;
        }

        public class HomeLoanApplicationTableEntity : TableEntity
        {
            public HomeLoanApplicationTableEntity()
            {
            }

            public HomeLoanApplicationTableEntity(string applicationbody)
            {
                
                JsonBody = applicationbody;
            }

           // public string applicationbody { get; set; }
          //  public string applicantssn { get; set; }
            public string JsonBody { get; set; }
        }

        public static async Task<HomeLoanApplicationTableEntity> InsertOrMergeEntityAsync(CloudTable table, HomeLoanApplicationTableEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                HomeLoanApplicationTableEntity insertedCustomer = result.Result as HomeLoanApplicationTableEntity;

                // Get the request units consumed by the current operation. RequestCharge of a TableResult is only applied to Azure Cosmos DB
                //if (result.RequestCharge.HasValue)
                //{
                //    Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                //}

                return insertedCustomer;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }

        public static async Task<HomeLoanApplicationTableEntity> RetrieveEntityUsingPointQueryAsync(CloudTable table, string partitionKey, string rowKey)
        {
            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<HomeLoanApplicationTableEntity>(partitionKey, rowKey);
                TableResult result = await table.ExecuteAsync(retrieveOperation);
                HomeLoanApplicationTableEntity customer = result.Result as HomeLoanApplicationTableEntity;
                if (customer != null)
                {
                    //Console.WriteLine("\t{0}\t{1}\t{2}\t{3}", customer.PartitionKey, customer.RowKey, customer.Email, customer.PhoneNumber);
                }

                // Get the request units consumed by the current operation. RequestCharge of a TableResult is only applied to Azure CosmoS DB 
                if (result.RequestCharge.HasValue)
                {
                    //Console.WriteLine("Request Charge of Retrieve Operation: " + result.RequestCharge);
                }

                return customer;
            }
            catch (StorageException e)
            {
                // Console.WriteLine(e.Message);
                //  Console.ReadLine();
                throw;
            }
        }

    }



}
