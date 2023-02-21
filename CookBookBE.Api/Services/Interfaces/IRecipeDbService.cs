using CookBookBE.Data.DbModels;
using CookBookBE.Data.Models;

namespace CookBookBE.Api.Services.Interfaces
{
    public interface IRecipeDbService
    {
        Task<IEnumerable<DbRecipe>> GetRecipesAsync();
        Task<DbRecipe?> GetRecipeAsync(Guid id);
        Task<DbRecipe?> CreateRecipeAsync(Recipe newRecipe);
        Task<DbRecipe?> UpdateRecipeAsync(DbRecipe existingRecipe, Recipe updateRecipe);
        Task<DbRecipe?> DeleteRecipeAsync(Guid id);

        // TMP - Untill sql script
        Task PopulateDbWithData();
    }
}
