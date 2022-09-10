using Azure.Data.Tables;
using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services.AzDataTableService
{
    public interface IAzDataTableService
    {
        Task AddAsync(TableEntity model);
    }
}
