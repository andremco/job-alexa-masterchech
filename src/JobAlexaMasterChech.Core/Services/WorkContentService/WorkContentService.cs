using Azure.Data.Tables;
using JobAlexaMasterChech.Core.Models.AppSettings;
using JobAlexaMasterChech.Core.Models.DataTableEntities;
using JobAlexaMasterChech.Core.Services.AzDataTableService;
using JobAlexaMasterChech.Core.Services.ContentFromWebSiteService;
using JobAlexaMasterChech.Core.Util;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private readonly RecipeAppSettings _recipeAppSettings;
        private readonly ILogger _logger;

        public WorkContentService(IAzDataTableService azDataTableService, 
            IContentFromWebSiteService contentFromWebSiteService,
            RecipeAppSettings recipeAppSettings,
            ILoggerFactory loggerFactory)
        {
            _azDataTableService = azDataTableService;
            _contentFromWebSiteService = contentFromWebSiteService;
            _recipeAppSettings = recipeAppSettings;
            _logger = loggerFactory.CreateLogger("Work");
        }

        public async Task SaveContent()
        {
            await SaveIngredients();
            await SaveRecipes();
        }

        public async Task SaveIngredients()
        {
            var tableClient = new TableClient(_recipeAppSettings.AzConnectionDataTable, "Ingredients");
            _azDataTableService.TableClient = tableClient;

            var ingredients = await _contentFromWebSiteService.GetIngredientContentFromLink();

            foreach (var ingredient in ingredients)
            {
                var ingredientEntity = new IngredientEntity
                {
                    PartitionKey = "ingredient",
                    RowKey = Guid.NewGuid().ToString(),
                    ExternCode = ingredient.ExternCode,
                    Description = ingredient.Description
                };

                var exist = await _azDataTableService.ExistIngredientEntity(ingredientEntity.Description);
                if (!exist)
                {
                    //save az data table
                    await _azDataTableService.AddAsync(ingredientEntity);
                    _logger.LogInformation($"Save entity [{JsonConvert.SerializeObject(ingredientEntity)}] at: {DateTime.Now}");
                }
                else
                {
                    _logger.LogInformation($"Exist entity [{JsonConvert.SerializeObject(ingredientEntity)}] at: {DateTime.Now}");
                }
            }
        }

        public async Task SaveRecipes()
        {
            var tableClient = new TableClient(_recipeAppSettings.AzConnectionDataTable, "Recipes");
            _azDataTableService.TableClient = tableClient;

            var recipes = await _contentFromWebSiteService.GetRecipeContentFromLink();

            foreach (var recipe in recipes)
            {
                var recipeEntity = new RecipeEntity
                {
                    PartitionKey = "recipe",
                    RowKey = Guid.NewGuid().ToString(),
                    Title = recipe.Title
                };

                var exist = await _azDataTableService.ExistRecipeEntity(recipeEntity.Title);
                if (!exist)
                {
                    //save az data table
                    await _azDataTableService.AddAsync(recipeEntity);
                    _logger.LogInformation($"Save entity [{JsonConvert.SerializeObject(recipeEntity)}] at: {DateTime.Now}");
                }
                else
                {
                    _logger.LogInformation($"Exist entity [{JsonConvert.SerializeObject(recipeEntity)}] at: {DateTime.Now}");
                }
            }
        }
    }
}
