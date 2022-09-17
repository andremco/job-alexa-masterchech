using Azure.Data.Tables;
using JobAlexaMasterChech.Core.Models.DataTableEntities;
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

        public async Task SaveContent()
        {
            await SaveIngredients();
            await SaveRecipes();
        }

        public async Task SaveIngredients()
        {
            _azDataTableService.TableName = "Ingredients";
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
                }
            }
        }

        public async Task SaveRecipes()
        {
            _azDataTableService.TableName = "Recipes";
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
                }
            }
        }
    }
}
