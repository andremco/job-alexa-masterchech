using Azure;
using Azure.Data.Tables;
using JobAlexaMasterChech.Core.Models.DataTableEntities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services.AzDataTableService
{
    public class AzDataTableService : IAzDataTableService
    {
        public TableClient TableClient { get; set; }

        public AzDataTableService()
        {
        }

        public async Task AddAsync(ITableEntity model)
        {
            if (TableClient == null) throw new NullReferenceException("TableClient");

            await TableClient.AddEntityAsync(model);
        }

        public async Task<bool> ExistIngredientEntity(string description)
        {
            if (TableClient == null) throw new NullReferenceException("TableClient");

            var queryResults = TableClient.QueryAsync<IngredientEntity>(q => q.Description == description, 1);

            await foreach (Page<IngredientEntity> page in queryResults.AsPages())
            {
                var result = page.Values.Any();

                return result;
            }

            return false;
        }

        public async Task<bool> ExistRecipeEntity(string title)
        {
            if (TableClient == null) throw new NullReferenceException("TableClient");

            var queryResults = TableClient.QueryAsync<RecipeEntity>(q => q.Title == title, 1);

            await foreach (Page<RecipeEntity> page in queryResults.AsPages())
            {
                var result = page.Values.Any();

                return result;
            }

            return false;
        }
    }
}
