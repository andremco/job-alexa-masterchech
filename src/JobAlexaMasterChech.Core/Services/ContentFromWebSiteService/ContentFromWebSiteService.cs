using HtmlAgilityPack;
using JobAlexaMasterChech.Core.Models.AppSettings;
using System.Linq;
using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services.ContentFromWebSiteService
{
    public class ContentFromWebSiteService : IContentFromWebSiteService
    {
        public readonly HtmlWeb _htmlWeb;
        public readonly RecipeAppSettings _recipeAppSettings;

        public ContentFromWebSiteService(HtmlWeb htmlWeb, RecipeAppSettings recipesAppSettings)
        {
            _htmlWeb = htmlWeb;
            _recipeAppSettings = recipesAppSettings;
        }

        public async Task<string> GetLinksAsync()
        {
            var url = _recipeAppSettings.Url;
            var tagLinkForSearch = _recipeAppSettings.TagLinkForSearch;
            var doc = await _htmlWeb.LoadFromWebAsync(url);

            var itemList = doc.DocumentNode.SelectNodes(tagLinkForSearch)
                  .Select(p => p.Attributes)
                  .ToList();

            return url;
        }
    }
}
