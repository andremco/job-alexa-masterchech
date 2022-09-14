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
        public readonly TableClient _tableClient;

        public AzDataTableService(TableClient tableClient)
        {
            _tableClient = tableClient;
        }

        public async Task AddAsync(ITableEntity model)
        {
            await _tableClient.AddEntityAsync(model);
        }

        public async Task<bool> ExistIngredientEntity(string description)
        {
            var queryResults = _tableClient.QueryAsync<IngredientEntity>(q => q.Description == description, 10);

            await foreach (Page<IngredientEntity> page in queryResults.AsPages())
            {
                var result = page.Values.Any();

                return result;
            }

            return false;
        }
    }
}
