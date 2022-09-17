using Azure.Data.Tables;
using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services.AzDataTableService
{
    public interface IAzDataTableService
    {
        TableClient TableClient { get; set; }
        Task AddAsync(ITableEntity model);
        Task<bool> ExistIngredientEntity(string description);
        Task<bool> ExistRecipeEntity(string title);
    }
}
