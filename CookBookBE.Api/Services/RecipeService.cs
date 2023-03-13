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
                
        // TODO Include all entities / maybe separate method
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
                Description = recipe.Description ?? String.Empty,
                Ingredients = recipe.Ingredients.Select(i => i.ToDbModel()).ToList(),
                Tags = recipe.Tags?.Where(i => i != null).Select(t => t.ToDbModel()).ToList(),
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
            };

            await _recipeRepository.AddAsync(dbRecipe);

            return dbRecipe;
        }

        public async Task<DbRecipe?> UpdateRecipeByIdAsync(Guid id, Recipe recipeUpdate)
        {
            var dbRecipe = await GetRecipeAsync(id);
            if (dbRecipe is null)
            {
                // TODO Shot some error here
                return null;
            }

            var updatedRecipe = dbRecipe with
            {
                Title = recipeUpdate.Title,
                Description = recipeUpdate.Description ?? string.Empty,
                Ingredients = recipeUpdate.Ingredients.Where(i => i != null).Select(i => i.ToDbModel()).ToList(),
                Tags = recipeUpdate.Tags?.Where(i => i != null).Select(t => t.ToDbModel()).ToList(),
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
