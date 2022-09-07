using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace JobAlexaMasterChech.Function
{
    public static class JobTriggerMasterchech
    {
        [FunctionName("JobTriggerMasterchech")]
        public static void Run([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            if (myTimer.IsPastDue)
            {
                log.LogInformation("Timer is running late!");
            }
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
