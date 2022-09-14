using Azure.Data.Tables;
using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services.AzDataTableService
{
    public interface IAzDataTableService
    {
        Task AddAsync(ITableEntity model);
        Task<bool> ExistIngredientEntity(string description);
    }
}
