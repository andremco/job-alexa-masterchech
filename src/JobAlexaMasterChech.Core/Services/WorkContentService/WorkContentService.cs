using Azure.Data.Tables;
using JobAlexaMasterChech.Core.Services.AzDataTableService;
using JobAlexaMasterChech.Core.Services.ContentFromWebSiteService;
using JobAlexaMasterChech.Core.Util;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _logger = loggerFactory.CreateLogger("Work");
        }

        public async Task SaveRecipes()
        {
            var links = await _contentFromWebSiteService.GetLinksAsync();

            foreach (var link in links)
            {
                _logger.LogInformation($"Read url: {link}");

                var content = await _contentFromWebSiteService.GetContentFromLink(link);

                foreach (var ingredient in content)
                {
                    var entity = new TableEntity("ingredients", Guid.NewGuid().ToString())
                        {
                            { "ExternCode", ingredient.ExternCode },
                            { "Description", ingredient.Description }
                        };
                    //save az data table
                    await _azDataTableService.AddAsync(entity);
                }
            }
        }
    }
}
