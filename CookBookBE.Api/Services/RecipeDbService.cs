using CookBookBE.Data.Models;
using CookBookBE.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CookBookBE.Data;
using CookBookBE.Data.DbModels;

namespace CookBookBE.Api.Services
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
            var recipes = await recipeContext.Recipes.ToListAsync();
            if (recipes.Any())
            {
                return recipes;
            }

            await PopulateDbWithData();
            return await recipeContext.Recipes.ToListAsync();
        }

        public async Task<DbRecipe?> GetRecipeAsync(Guid id)
        {
            return await recipeContext.Recipes.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<DbRecipe?> CreateRecipeAsync(Recipe recipe)
        {
            var dbRecipe = new DbRecipe()
            {
                Id = Guid.NewGuid(),
                Title = recipe.Title,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients?.Select(i => i.ToDbModel()).ToList(),
                Tags = recipe.Tags?.Select(t => t.ToDbModel()).ToList(),
                CreatedDate = DateTime.Now,
            };

            recipeContext.Add(dbRecipe);
            await recipeContext.SaveChangesAsync();

            return dbRecipe;
        }

        public async Task<DbRecipe?> UpdateRecipeAsync(DbRecipe existingRecipe, Recipe recipeUpdate)
        {
            var recipe = existingRecipe with
            {
                Title = recipeUpdate.Title,
            };
            
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

        // TMP - untill I write sql script for some random data
        public async Task PopulateDbWithData()
        {
            Random rnd = new ();

            var newTags = new List<DbTag>() {
                new () { Name = "Breakfast" },
                new () { Name = "Dinner" },
                new () { Name = "Lunch" },
                new () { Name = "Supper" },
                new () { Name = "Easy" },
                new () { Name = "Hard" },
            };

            var newIngredients = new List<DbIngredient>() {
                new () { Name = "Egg" },
                new () { Name = "Tuna" },
                new () { Name = "Salad" },
                new () { Name = "Ketchup" },
                new () { Name = "Tortilla" },
                new () { Name = "Ham" },
                new () { Name = "Cheese" },
            };

            var newRecipes = new List<DbRecipe>();
            for (int i = 1; i <= 10; i++)
            {
                newRecipes.Add(
                    new DbRecipe() {
                        Title = $"Recipe{i}",
                        Description = i.ToString(),
                        Ingredients = newIngredients.OrderBy(i => rnd.Next()).Take(2).ToList(),
                        Tags = newTags.OrderBy(i => rnd.Next()).Take(2).ToList()
                    }
                );
            }

            await recipeContext.AddRangeAsync(newRecipes);
            await recipeContext.SaveChangesAsync();
        }
    }
}
