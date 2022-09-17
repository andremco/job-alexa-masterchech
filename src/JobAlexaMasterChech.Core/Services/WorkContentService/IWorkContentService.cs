using System.Threading.Tasks;

namespace JobAlexaMasterChech.Core.Services.WorkContentService
{
    public interface IWorkContentService
    {
        Task SaveContent();
        Task SaveIngredients();
        Task SaveRecipes();
    }
}
