using HtmlAgilityPack;
using JobAlexaMasterChech.Core.Models;
using JobAlexaMasterChech.Core.Models.Settings;
using System.Linq;
using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services
{
    public class ContentFromWebService : IContentFromWebService
    {
        public readonly HtmlWeb _htmlWeb;
        public readonly RecipeSettings _recipeSettings;

        public ContentFromWebService(HtmlWeb htmlWeb, RecipeSettings recipesSettings)
        {
            _htmlWeb = htmlWeb;
            _recipeSettings = recipesSettings;
        }

        public async Task<string> GetLinksAsync()
        {
            var url = _recipeSettings.Url;
            var doc = await _htmlWeb.LoadFromWebAsync(url);

            var itemList = doc.DocumentNode.SelectNodes("//a[@class='titulo titulo--bloque']")
                  .Select(p => p.Attributes)
                  .ToList();

            return url;
        }
    }
}
