using Azure;
using Azure.Data.Tables;
using JobAlexaMasterChech.Core.Models.AppSettings;
using JobAlexaMasterChech.Core.Models.DataTableEntities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services.AzDataTableService
{
    public class AzDataTableService : IAzDataTableService
    {
        public readonly RecipeAppSettings _recipeAppSettings;
        public string TableName { get; set; }
        public AzDataTableService(RecipeAppSettings recipeAppSettings)
        {
            _recipeAppSettings = recipeAppSettings;
        }

        public async Task AddAsync(ITableEntity model)
        {
            var tableClient = new TableClient(_recipeAppSettings.AzConnectionDataTable, TableName);
            await tableClient.AddEntityAsync(model);
        }

        public async Task<bool> ExistIngredientEntity(string description)
        {
            var tableClient = new TableClient(_recipeAppSettings.AzConnectionDataTable, TableName);

            var queryResults = tableClient.QueryAsync<IngredientEntity>(q => q.Description == description, 1);

            await foreach (Page<IngredientEntity> page in queryResults.AsPages())
            {
                var result = page.Values.Any();

                return result;
            }

            return false;
        }

        public async Task<bool> ExistRecipeEntity(string title)
        {
            var tableClient = new TableClient(_recipeAppSettings.AzConnectionDataTable, TableName);

            var queryResults = tableClient.QueryAsync<RecipeEntity>(q => q.Title == title, 1);

            await foreach (Page<RecipeEntity> page in queryResults.AsPages())
            {
                var result = page.Values.Any();

                return result;
            }

            return false;
        }
    }
}
