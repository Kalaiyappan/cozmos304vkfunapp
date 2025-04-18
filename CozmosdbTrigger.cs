using System;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs;

namespace Cozmos.Function
{
    public class CozmosdbTrigger
    {
        private readonly ILogger _logger;

        public CozmosdbTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CozmosdbTrigger>();
        }

        [FunctionName("CozmosdbTrigger")]
        public void Run([CosmosDBTrigger(
            databaseName: "BookStore",
            containerName: "Documents",
            Connection = "az204vkcosmosdb_DOCUMENTDB",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)] IReadOnlyList<MyDocument> input)
        {
            if (input != null && input.Count > 0)
            {
                _logger.LogInformation("Documents modified: " + input.Count);
                _logger.LogInformation("First document Id: " + input[0].id);
            }
        }
    }

    public class MyDocument
    {
        public string id { get; set; }
        public string Description { get; set; }
        public string Title {  get; set; }
        public string Documents {  get; set; }
    }
}
