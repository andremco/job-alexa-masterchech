using JobAlexaMasterChech.Core.Models.ContentWebSite;
using JobAlexaMasterChech.Core.Models.DataTableEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services.ContentFromWebSiteService
{
    public interface IContentFromWebSiteService
    {
        public Task<ICollection<LinkWebSite>> GetLinksAsync();
        public Task<ICollection<IngredientWebSite>> GetIngredientContentFromLink();
        public Task<ICollection<RecipeWebSite>> GetRecipeContentFromLink();
    }
}
