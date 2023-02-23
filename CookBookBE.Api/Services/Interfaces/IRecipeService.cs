using CookBookBE.Data.DbModels;
using CookBookBE.Data.Models;

namespace CookBookBE.Api.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<DbRecipe>> GetRecipesAsync();
        Task<DbRecipe?> GetRecipeAsync(Guid id);
        Task<DbRecipe?> CreateRecipeAsync(Recipe newRecipe);
        Task<DbRecipe?> UpdateRecipeByIdAsync(Guid id, Recipe updateRecipe);
        Task<DbRecipe?> DeleteRecipeAsync(Guid id);

        // TMP - Untill sql script
        Task PopulateDbWithData();
    }
}
