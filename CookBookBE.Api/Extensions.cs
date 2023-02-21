using CookBookBE.Data.DbModels;
using CookBookBE.Data.Models;

namespace CookBookBE
{
    public static class Extensions
    {
        public static Recipe ToDtoModel(this DbRecipe dbRecipe) =>
            new ()
            {
                Title = dbRecipe.Title,
                Description = dbRecipe.Description,
                Ingredients = dbRecipe.Ingredients?.Select(i => i.ToDtoModel()).ToList(),
                Tags = dbRecipe.Tags?.Select(t => t.ToDtoModel()).ToList(),
            };

        public static DbRecipe ToDbModel(this Recipe recipe) =>
            new ()
            {
                Title = recipe.Title,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients?.Select(i => i.ToDbModel()).ToList(),
                Tags = recipe.Tags?.Select(t => t.ToDbModel()).ToList(),
            };

        public static Ingredient ToDtoModel(this DbIngredient dbIngredient) =>
            new ()
            {
                Name = dbIngredient.Name,
            };

        public static DbIngredient ToDbModel(this Ingredient ingredient) =>
            new ()
            {
                Name = ingredient.Name,
            };

        public static Tag ToDtoModel(this DbTag dbTag) =>
            new ()
            {
                Name = dbTag.Name,
            };

        public static DbTag ToDbModel(this Tag tag) =>
            new ()
            {
                Name = tag.Name,
            };
    }
}
