using JobAlexaMasterChech.Core.Models.DataTableEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services.ContentFromWebSiteService
{
    public interface IContentFromWebSiteService
    {
        public Task<ICollection<string>> GetLinksAsync();
        public Task<ICollection<IngredientEntity>> GetContentFromLink(string link);
    }
}
