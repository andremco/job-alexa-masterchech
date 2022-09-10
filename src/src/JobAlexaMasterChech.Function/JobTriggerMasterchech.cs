using System;
using System.Net.Http;
using System.Threading.Tasks;
using JobAlexaMasterChech.Core.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace JobAlexaMasterChech.Function
{
    public class JobTriggerMasterchech
    {
        private readonly IContentFromWebService _contentFromWebService;

        public JobTriggerMasterchech(IContentFromWebService contentFromWebService)
        {
            _contentFromWebService = contentFromWebService;
        }

        [FunctionName("JobTriggerMasterchech")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger logger)
        {
            if (myTimer.IsPastDue)
            {
                logger.LogInformation("Timer is running late!");
            }
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var url = await _contentFromWebService.GetLinksAsync();

            logger.LogInformation($"Recipes Url: {url}");
        }
    }
}
