using CookBookBE.Models;

namespace CookBookBE
{
    public static class Extensions
    {
        public static Recipe ToDtoModel(this DbRecipe dbRecipe)
        {
            return new Recipe
            {
                Id = dbRecipe.Id,
                Title = dbRecipe.Title,
                CreatedDate = dbRecipe.CreatedDate,
            };
        }

        public static DbRecipe ToDbModel(this Recipe recipe)
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
