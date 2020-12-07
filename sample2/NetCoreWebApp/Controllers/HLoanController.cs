using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using NetCoreWebApp.Utility;
using Newtonsoft.Json;
using RestSharp.Extensions;
using RestSharp.Serialization.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCoreWebApp.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]


    public class HLoanController : ControllerBase
    {
        private readonly APIConfiguration _envvars;
        // GET: api/<TestController>
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{

        //}
        public HLoanController(IOptions<APIConfiguration> EnvVars)
        {
            _envvars = EnvVars.Value ?? throw new ArgumentNullException(nameof(EnvVars));
        }
        // GET api/<TestController>/5
        [HttpGet("{appid}/{email}")]
        public async Task<ActionResult<HomeLoanClass>> GetTodoItem(string appid,string email)
        {
            // string storageConnectionString = AppSettings.LoadAppSettings().StorageConnectionString;
            string storageConnectionString = _envvars.STORAGE_ENDPOINT;
            string homeloantablenameString = _envvars.STORAGE_TABLE_NAME;
            // string homeloantablenameString = AppSettings.LoadAppSettings().HomeLoanTableName;
            StorageHelpers.HomeLoanApplicationTableEntity loanapplication = new StorageHelpers.HomeLoanApplicationTableEntity();
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = StorageHelpers.CreateStorageAccountFromConnectionString(storageConnectionString);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            // Console.WriteLine("Create a Table for the demo");

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(homeloantablenameString);

            loanapplication = await StorageHelpers.RetrieveEntityUsingPointQueryAsync(table, email, appid);
            HomeLoanClass applicationdetails = new HomeLoanClass();
            if (loanapplication != null)
            {
                applicationdetails = JsonConvert.DeserializeObject<HomeLoanClass>(loanapplication.JsonBody);
                return Ok(applicationdetails);
            }
            else
                return Ok();
            
        }
    

        //// POST api/<TestController>
        //[HttpPost]
        //public JsonResult Post([FromBody] string value)
        //{
        //    //HomeLoanClass ho = JsonConvert.DeserializeObject<HomeLoanClass>(value);
        //    return JsonResult(value);
        //}

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<LoanSubmittedinfo> CreateAsync(HomeLoanClass loan)
        {
            //if (product.Description.Contains("XYZ Widget"))
            //{
            //    return BadRequest();
            //}

            //await _repository.AddProductAsync(product);

            LoanSubmittedinfo info = new LoanSubmittedinfo();
            //Insert the details to Azure Storate Table
            Guid g = Guid.NewGuid();
            StorageHelpers.HomeLoanApplicationTableEntity loanapplication = new StorageHelpers.HomeLoanApplicationTableEntity()
            { PartitionKey = loan.email,
                JsonBody = JsonConvert.SerializeObject(loan),
                Timestamp = DateTime.UtcNow,
                RowKey = g.ToString()
            } ;

            string storageConnectionString = _envvars.STORAGE_ENDPOINT;
            string homeloantablenameString = _envvars.STORAGE_TABLE_NAME;
            //string storageConnectionString = AppSettings.LoadAppSettings().StorageConnectionString;
            //string homeloantablenameString = AppSettings.LoadAppSettings().HomeLoanTableName;
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = StorageHelpers.CreateStorageAccountFromConnectionString(storageConnectionString);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            // Console.WriteLine("Create a Table for the demo");

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(homeloantablenameString);

            //CloudTable table = new CloudTable()
            await StorageHelpers.InsertOrMergeEntityAsync(table, loanapplication);

            //Later On encrypt before storing

            info.assignedloanofficer = "test";
            info.datesubmitted = DateTime.Now;
            info.appid = g.ToString();
            return info;
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
