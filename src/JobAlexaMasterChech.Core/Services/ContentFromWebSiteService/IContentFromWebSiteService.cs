using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services.ContentFromWebSiteService
{
    public interface IContentFromWebSiteService
    {
        public Task<string> GetLinksAsync();
    }
}
