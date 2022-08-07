using CookBookBE.Models;

namespace CookBookBE.Services
{
    public class RecipeMappingService
    {
        public Recipe ToDtoModel(DbRecipe dbRecipe)
        {
            return new Recipe
            {
                Id = dbRecipe.Id,
                Title = dbRecipe.Title,
                CreatedDate = dbRecipe.CreatedDate,
            };
        }

        public DbRecipe ToDbModel(Recipe recipe)
        {
            return new DbRecipe
            {
                Id = recipe.Id,
                Title = recipe.Title,
                CreatedDate = recipe.CreatedDate,
            };
        }
    }
}
