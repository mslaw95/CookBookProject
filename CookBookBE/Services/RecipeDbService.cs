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

        public async Task<DbRecipe?> GetRecipeAsync(Guid id)
        {
            return await recipeContext.Recipes.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<DbRecipe?> CreateRecipeAsync(DbRecipe recipe)
        {
            recipeContext.Add(recipe);
            await recipeContext.SaveChangesAsync();

            return recipe;
        }

        public async Task<DbRecipe?> UpdateRecipeAsync(DbRecipe recipe)
        {
            recipeContext.ChangeTracker.Clear();

            recipeContext.Update(recipe);
            await recipeContext.SaveChangesAsync();

            return recipe;
        }

        public async Task<DbRecipe?> DeleteRecipeAsync(Guid id)
        {
            var dbRecipe = await recipeContext.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            if (dbRecipe != null)
            {
                recipeContext.Remove(dbRecipe);
            }

            await recipeContext.SaveChangesAsync();

            return dbRecipe;
        }
    }
}
