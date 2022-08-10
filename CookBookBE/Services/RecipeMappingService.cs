using CookBookBE.Models;

namespace CookBookBE.Services
{
    // TODO - consider what to do with this
    public class RecipeMappingService
    {
        public Recipe ToDtoModel(DbRecipe dbRecipe)
        {
            return new Recipe
            {
                Id = dbRecipe.Id,
                Title = dbRecipe.Title,
            };
        }

        public DbRecipe ToDbModel(Recipe recipe)
        {
            return new DbRecipe
            {
                Id = recipe.Id,
                Title = recipe.Title,
            };
        }
    }
}
