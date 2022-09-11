using Azure.Data.Tables;
using HtmlAgilityPack;
using JobAlexaMasterChech.Core.Models.AppSettings;
using JobAlexaMasterChech.Core.Services.AzDataTableService;
using JobAlexaMasterChech.Core.Services.ContentFromWebSiteService;
using JobAlexaMasterChech.Core.Services.WorkContentService;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

[assembly: FunctionsStartup(typeof(JobAlexaMasterChech.Function.Startup))]
namespace JobAlexaMasterChech.Function
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            string recipeUrl = Environment.GetEnvironmentVariable("RecipeUrl");
            string tagLinkForSearch = Environment.GetEnvironmentVariable("TagLinkForSearch");
            string tagIngredientForSearch = Environment.GetEnvironmentVariable("TagIngredientForSearch");
            string azConnectionDataTable = Environment.GetEnvironmentVariable("AzConnectionDataTable");
            string azNameDataTable = Environment.GetEnvironmentVariable("AzNameDataTable");

            var recipeSettings = new RecipeAppSettings
            {
                Url = recipeUrl,
                TagLinkForSearch = tagLinkForSearch,
                TagIngredientForSearch = tagIngredientForSearch
            };

            var tableClient = new TableClient(azConnectionDataTable, azNameDataTable);

            builder.Services.AddSingleton<IContentFromWebSiteService, ContentFromWebSiteService>();
            builder.Services.AddSingleton(recipeSettings);
            builder.Services.AddSingleton<HtmlWeb>();
            builder.Services.AddSingleton(tableClient);
            builder.Services.AddSingleton<IAzDataTableService, AzDataTableService>();
            builder.Services.AddSingleton<IWorkContentService>(s =>
            {
                var factory = s.GetService<ILoggerFactory>();
                return new WorkContentService(s.GetService<IAzDataTableService>(), s.GetService<IContentFromWebSiteService>(), factory);
            });
        }
    }
}
