using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services
{
    public interface IContentFromWebService
    {
        public Task<string> GetLinksAsync();
    }
}
