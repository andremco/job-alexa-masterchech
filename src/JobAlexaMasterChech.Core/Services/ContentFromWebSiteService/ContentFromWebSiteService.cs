using HtmlAgilityPack;
using JobAlexaMasterChech.Core.Models.AppSettings;
using JobAlexaMasterChech.Core.Models.ContentWebSite;
using JobAlexaMasterChech.Core.Models.DataTableEntities;
using JobAlexaMasterChech.Core.Util;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public ContentFromWebSiteService(HtmlWeb htmlWeb, 
            RecipeAppSettings recipesAppSettings,
            ILoggerFactory loggerFactory)
        {
            _htmlWeb = htmlWeb;
            _recipeAppSettings = recipesAppSettings;
            _logger = loggerFactory.CreateLogger("Content");
        }

        public async Task<ICollection<LinkWebSite>> GetLinksAsync()
        {
            var linksWebSite = new List<LinkWebSite>();
            var url = _recipeAppSettings.Url;
            var tagLinkForSearch = _recipeAppSettings.TagLinkForSearch;
            var doc = await _htmlWeb.LoadFromWebAsync(url);

            var nodes = doc.DocumentNode.SelectNodes(tagLinkForSearch);

            if(nodes != null)
            {
                var links = nodes
                                //Only valid links
                                .Where(p => !string.IsNullOrEmpty(p.InnerText) && p.Attributes["href"] != null && !string.IsNullOrEmpty(p.Attributes["href"].Value))
                                //Take random 5 links!!
                                .OrderBy(p => new Random().Next()).Take(5)
                                .Select(p => new LinkWebSite
                                {
                                    Title = p.InnerText,
                                    Url = p.Attributes["href"].Value
                                })
                                .ToList();
                return links;
            }

            return linksWebSite;
        }

        public async Task<ICollection<IngredientWebSite>> GetIngredientContentFromLink()
        {
            var ingredients = new List<IngredientWebSite>();
            var links = await GetLinksAsync();

            if (links == null || !links.Any()) throw new NullReferenceException("links");

            foreach (var link in links)
            {
                _logger.LogInformation($"Read url: {link.Url}");

                var doc = await _htmlWeb.LoadFromWebAsync(link.Url);

                var nodes = doc.DocumentNode.SelectNodes(_recipeAppSettings.TagIngredientForSearch);

                if (nodes != null)
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
                        var externCode = link.Url.GetExternCodeFromUrl();

                        if (!string.IsNullOrEmpty(description) && externCode > 0)
                        {
                            ingredients.Add(new IngredientWebSite()
                            {
                                ExternCode = externCode,
                                Description = description
                            });
                        }
                    }

                    return ingredients;
                }
            }

            return ingredients;
        }

        public async Task<ICollection<RecipeWebSite>> GetRecipeContentFromLink()
        {
            var recipes = new List<RecipeWebSite>();
            var links = await GetLinksAsync();

            if (links == null || !links.Any()) throw new NullReferenceException("recipes");

            foreach (var link in links)
            {
                _logger.LogInformation($"Read url: {link.Url}");

                var titleRecipe = link.Title;

                if (!string.IsNullOrEmpty(titleRecipe))
                {
                    recipes.Add(new RecipeWebSite()
                    {
                        Title = titleRecipe
                    });
                }
            }

            return recipes;
        }
    }
}
