using System;
using System.Threading.Tasks;
using JobAlexaMasterChech.Core.Services.WorkContentService;
using Microsoft.AspNetCore.Mvc;
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
        public async Task Run([TimerTrigger("0 */15 * * * *")] TimerInfo myTimer, ILogger logger)
        {
            try
            {
                logger.LogInformation($"Job Alexa MasterChech executed at: {DateTime.Now}");

                await _workContentService.SaveRecipes();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error Job Alexa MasterChech");

                throw ex;
            }
        }
    }
}
