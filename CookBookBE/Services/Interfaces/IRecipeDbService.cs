using CookBookBE.Models;

namespace CookBookBE.Services.Interfaces
{
    public interface IRecipeDbService
    {
        Task<DbRecipe> GetRecipeAsync(Guid id);
        Task<IEnumerable<DbRecipe>> GetRecipesAsync();
        Task CreateRecipeAsync(DbRecipe recipe);
        Task UpdateRecipeAsync(DbRecipe recipe);
        Task DeleteRecipeAsync(Guid id);
    }
}
