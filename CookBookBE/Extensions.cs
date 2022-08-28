using CookBookBE.DbModels;
using CookBookBE.Models;

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
                Id = dbIngredient.Id,
                Name = dbIngredient.Name,
            };

        public static DbIngredient ToDbModel(this Ingredient ingredient) =>
            new ()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
            };

        public static Tag ToDtoModel(this DbTag dbTag) =>
            new ()
            {
                Id = dbTag.Id,
                Name = dbTag.Name,
            };

        public static DbTag ToDbModel(this Tag tag) =>
            new ()
            {
                Id = tag.Id,
                Name = tag.Name,
            };
    }
}
