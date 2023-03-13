using CookBookBE.Data.Models;
using CookBookBE.Api.Services.Interfaces;
using CookBookBE.Data.DbModels;
using CookBookBE.Data.Repositories.Interfaces;

namespace CookBookBE.Api.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
                
        public async Task<IEnumerable<DbRecipe>> GetRecipesAsync()
        {
            return await _recipeRepository.GetAllAsync();
        }

        public async Task<DbRecipe?> GetRecipeAsync(Guid id)
        {
            return await _recipeRepository.GetByIdAsync(id);
        }

        public async Task<DbRecipe?> CreateRecipeAsync(Recipe recipe)
        {
            var dbRecipe = new DbRecipe()
            {
                Title = recipe.Title,
                Description = recipe.Description ?? "",
                Ingredients = recipe.Ingredients.Select(i => i?.ToDbModel()).ToList(),
                Tags = recipe.Tags.Select(t => t?.ToDbModel()).ToList(),
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            await _recipeRepository.AddAsync(dbRecipe);

            return dbRecipe;
        }

        public async Task<DbRecipe?> UpdateRecipeByIdAsync(Guid id, Recipe recipeUpdate)
        {
            var dbRecipe = await GetRecipeAsync(id);
            var updatedRecipe = dbRecipe with
            {
                Title = recipeUpdate.Title,
                Description = recipeUpdate.Description,
                Ingredients = recipeUpdate.Ingredients.Select(i => i?.ToDbModel()).ToList(),
                Tags = recipeUpdate.Tags.Select(t => t?.ToDbModel()).ToList(),
                DateUpdated = DateTime.Now,
            };

            await _recipeRepository.UpdateAsync(dbRecipe);

            return updatedRecipe;
        }

        public async Task<DbRecipe?> DeleteRecipeAsync(Guid id)
        {
            var dbRecipe = await GetRecipeAsync(id);
            if (dbRecipe is null)
            {
                return null;
            }

            await _recipeRepository.DeleteAsync(dbRecipe);

            return dbRecipe;
        }

        // TMP - Until sql script prepared
        public async Task PopulateDbWithData()
        {
            await _recipeRepository.PopulateDbWithData();
        }
    }
}
