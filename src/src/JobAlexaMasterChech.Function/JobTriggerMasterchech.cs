using System;
using System.Threading.Tasks;
using JobAlexaMasterChech.Core.Services.WorkContentService;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace JobAlexaMasterChech.Function
{
    public class JobTriggerMasterchech
    {
        private readonly IWorkContentService _workContentService;

        public JobTriggerMasterchech(IWorkContentService workContentService)
        {
            _workContentService = workContentService;
        }

        [FunctionName("JobTriggerMasterchech")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger logger)
        {
            //if (myTimer.IsPastDue)
            //{
            //    logger.LogInformation("Timer is running late!");
            //}
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            //await _azDataTableService.AddRecipeAsync();

           await _workContentService.SaveRecipes();
        }
    }
}
