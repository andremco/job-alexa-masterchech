using Azure.Data.Tables;
using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services.AzDataTableService
{
    public interface IAzDataTableService
    {
        string TableName { get; set; }
        Task AddAsync(ITableEntity model);
        Task<bool> ExistIngredientEntity(string description);
        Task<bool> ExistRecipeEntity(string title);
    }
}
