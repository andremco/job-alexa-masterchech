using HtmlAgilityPack;
using JobAlexaMasterChech.Core.Models;
using JobAlexaMasterChech.Core.Models.Settings;
using JobAlexaMasterChech.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;

[assembly: FunctionsStartup(typeof(JobAlexaMasterChech.Function.Startup))]
namespace JobAlexaMasterChech.Function
{
    public class Startup : FunctionsStartup
    {
        private IConfigurationBuilder _configuration;
        private IHostingEnvironment _env;
        public Startup()
        {
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            string recipeUrl = Environment.GetEnvironmentVariable("RecipeUrl");
            var recipeSettings = new RecipeSettings
            {
                Url = recipeUrl
            };

            builder.Services.AddSingleton<IContentFromWebService, ContentFromWebService>();
            builder.Services.AddSingleton(recipeSettings);
            builder.Services.AddSingleton<HtmlWeb>();
        }
    }
}
