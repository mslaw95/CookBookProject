using CookBookBE.Data.Models;
using CookBookBE.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CookBookBE.Data;
using CookBookBE.Data.DbModels;

namespace CookBookBE.Api.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeContext recipeContext;

        public RecipeService(RecipeContext recipeContext)
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

            recipeContext.Add(dbRecipe);
            await recipeContext.SaveChangesAsync();

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
            
            recipeContext.ChangeTracker.Clear();
            recipeContext.Update(updatedRecipe);
            await recipeContext.SaveChangesAsync();

            return updatedRecipe;
        }

        public async Task<DbRecipe?> DeleteRecipeAsync(Guid id)
        {
            var dbRecipe = await GetRecipeAsync(id);
            if (dbRecipe is null)
            {
                return null;
            }

            recipeContext.Remove(dbRecipe);
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
                new () { Name = "Egg", Amount = 2, Unit="" },
                new () { Name = "Tuna", Amount = 1, Unit="can" },
                new () { Name = "Salad", Amount = 1, Unit="" },
                new () { Name = "Ketchup", Amount = 3, Unit="TbSp" },
                new () { Name = "Tortilla", Amount = 2, Unit="" },
                new () { Name = "Ham", Amount = 400, Unit="g" },
                new () { Name = "Cheese", Amount = 250, Unit="g" },
            };

            var newRecipes = new List<DbRecipe>();
            for (int i = 1; i <= 10; i++)
            {
                newRecipes.Add(
                    new DbRecipe() {
                        Id = Guid.NewGuid(),
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
