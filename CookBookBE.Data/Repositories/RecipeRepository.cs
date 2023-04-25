using CookBookBE.Data.DbModels;
using CookBookBE.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookBookBE.Data.Repositories
{
    public class RecipeRepository : BaseContextRepository<DbRecipe>, IRecipeRepository
    {
        public RecipeRepository(IDbContextFactory<RecipeContext> dbContextFactory)
            : base(dbContextFactory) { }
        
        public async Task PopulateDbWithData()
        {
            Random rnd = new();

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
                    new DbRecipe()
                    {
                        Id = Guid.NewGuid(),
                        Title = $"Recipe{i}",
                        Description = i.ToString(),
                        Ingredients = newIngredients.OrderBy(i => rnd.Next()).Take(2).ToList(),
                        Tags = newTags.OrderBy(i => rnd.Next()).Take(2).ToList()
                    }
                );
            }

            using var context = _dbContextFactory.CreateDbContext();
            await context.AddRangeAsync(newRecipes);
            await context.SaveChangesAsync();
        }
    }
}
