using CookBookBE.Models;
using CookBookBE.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookBookBE.Services
{
    public class RecipeDbService : IRecipeDbService
    {
        private readonly RecipeContext recipeContext;

        public RecipeDbService(RecipeContext recipeContext)
        {
            this.recipeContext = recipeContext;
        }
                
        public async Task<IEnumerable<DbRecipe>> GetRecipesAsync()
        {
            return await recipeContext.Recipes.ToListAsync();
        }
        public async Task<DbRecipe> GetRecipeAsync(Guid id)
        {
            return await recipeContext.Recipes.FindAsync(id);
        }

        public async Task CreateRecipeAsync(DbRecipe recipe)
        {
            recipeContext.Add(recipe);
            await recipeContext.SaveChangesAsync();
        }

        public async Task UpdateRecipeAsync(DbRecipe recipe)
        {
            var dbRecipe = await recipeContext.Recipes.UpdateAsync(recipe);

            await recipeContext.SaveChangesAsync();
        }

        public async Task DeleteRecipeAsync(Guid id)
        {
            var dbRecipe = await recipeContext.Recipes.FindAsync(id);
            if (dbRecipe != null)
                recipeContext.Remove(dbRecipe);

            await recipeContext.SaveChangesAsync();
        }
    }
}
