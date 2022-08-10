using CookBookBE.Models;

namespace CookBookBE.Services.Interfaces
{
    public interface IRecipeDbService
    {
        Task<IEnumerable<DbRecipe>> GetRecipesAsync();
        Task<DbRecipe?> GetRecipeAsync(Guid id);
        Task<DbRecipe?> CreateRecipeAsync(DbRecipe recipe);
        Task<DbRecipe?> UpdateRecipeAsync(DbRecipe recipe);
        Task<DbRecipe?> DeleteRecipeAsync(Guid id);

        // TMP - Untill sql script
        Task PopulateDbWithData();
    }
}
