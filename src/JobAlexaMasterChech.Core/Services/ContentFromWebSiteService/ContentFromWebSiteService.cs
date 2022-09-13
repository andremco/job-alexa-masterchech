using HtmlAgilityPack;
using JobAlexaMasterChech.Core.Models.AppSettings;
using JobAlexaMasterChech.Core.Models.DataTableEntities;
using JobAlexaMasterChech.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public async Task<ICollection<string>> GetLinksAsync()
        {
            var url = _recipeAppSettings.Url;
            var tagLinkForSearch = _recipeAppSettings.TagLinkForSearch;
            var doc = await _htmlWeb.LoadFromWebAsync(url);

            var nodes = doc.DocumentNode.SelectNodes(tagLinkForSearch);

            if(nodes != null)
            {
                var links = nodes.Select(p => p.Attributes)
                                  //Only valid links
                                  .Where(x => x["href"] != null && !string.IsNullOrEmpty(x["href"].Value))
                                  //Take random 5 links!!
                                  .OrderBy(x => new Random().Next()).Take(5)
                                  .Select(x => x["href"].Value)
                                  .ToList();

                return links;
            }

            return new List<string>();
        }

        public async Task<ICollection<IngredientEntity>> GetContentFromLink(string link)
        {
            var ingredients = new List<IngredientEntity>();
            if (string.IsNullOrEmpty(link)) throw new NullReferenceException("link");

            var doc = await _htmlWeb.LoadFromWebAsync(link);

            var nodes = doc.DocumentNode.SelectNodes("//label[@for]");

            if(nodes != null)
            {
                var content = nodes
                                    //Only valid links
                                    .Where(p => !string.IsNullOrEmpty(p.InnerText))
                                    //Take random 5 links!!
                                    .OrderBy(x => new Random().Next()).Take(5)
                                    .Select(p => Regex.Unescape(p.InnerText))
                                    .ToList();

                foreach (var item in content)
                {
                    var description = item.RemoveInvalidSentenceFromString();
                    var externCode = link.GetExternCodeFromUrl();

                    if (!string.IsNullOrEmpty(description) && externCode > 0)
                    {
                        ingredients.Add(new IngredientEntity()
                        {
                            PartitionKey = "ingredient",
                            RowKey = Guid.NewGuid().ToString(),
                            ExternCode = externCode,
                            Description = description
                        });
                    }
                }

                return ingredients;
            }

            return ingredients;
        }
    }
}
