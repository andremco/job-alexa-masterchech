using JobAlexaMasterChech.Core.Services.AzDataTableService;
using JobAlexaMasterChech.Core.Services.ContentFromWebSiteService;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services.WorkContentService
{
    public class WorkContentService : IWorkContentService
    {
        private readonly IAzDataTableService _azDataTableService;
        private readonly IContentFromWebSiteService _contentFromWebSiteService;
        private readonly ILogger _logger;

        public WorkContentService(IAzDataTableService azDataTableService, 
            IContentFromWebSiteService contentFromWebSiteService,
            ILoggerFactory loggerFactory)
        {
            _azDataTableService = azDataTableService;
            _contentFromWebSiteService = contentFromWebSiteService;
            _logger = loggerFactory.CreateLogger("logger");
        }

        public async Task SaveRecipes()
        {
            _logger.LogInformation($"Hello from my service!! {DateTime.Now}");
        }
    }
}
