using Azure.Data.Tables;
using System;
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

        public async Task AddAsync(TableEntity model)
        {
            await _tableClient.AddEntityAsync(model);
        }
    }
}
