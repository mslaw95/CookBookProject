using Xunit;
using AutoFixture;
using CookBookBE.Data.DbModels;
using CookBookBE.Data.Models;
using CookBookBE.Api.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using CookBookBE.Data;

namespace CookBookBE.Tests
{
    public class RecipeDbServiceTests
    {
        private const string ConnectionString = "Server=localhost\\sqlexpress;Database=recipedb;Trusted_Connection=True";
        private readonly RecipeService sut;
        private readonly RecipeContext context;

        public RecipeDbServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RecipeContext>()
                .UseSqlServer(ConnectionString);

            context = new RecipeContext(optionsBuilder.Options);
            context.Database.Migrate();

            sut = new RecipeService(context);
        }

        [Fact]
        public async void CreateRecipe_ShouldCreateRecipe()
        {
            var recipe = BuildSingleRecipe();
            await sut.CreateRecipeAsync(recipe);

            var dbRecipe = await context.Recipes.SingleAsync(r => r.Title == recipe.Title);

            dbRecipe.Should().NotBeNull();
        }

        [Fact]
        public async void UpdateRecipe_ShouldUpdateRecipe()
        {
            var recipes = CreateManyRecipes(context);
            var recipe = recipes.First();
            var recipeUpdate = BuildSingleRecipe();
            await sut.UpdateRecipeByIdAsync(recipe.Id, recipeUpdate);

            recipe.Should().BeEquivalentTo(recipeUpdate);
        }

        public static Fixture CreateFixture()
        {
            var fixture = new Fixture();
            fixture.Customize<DbRecipe>(x => x.With(y => y.Id, default(Guid)));
            fixture.Customize<DbIngredient>(x => x.With(y => y.Name, fixture.Create<string>));
            fixture.Customize<DbTag>(x => x.With(y => y.Name, fixture.Create<string>));
            return fixture;
        }

        public static IEnumerable<DbRecipe> CreateManyRecipes(RecipeContext context)
        {
            var fixture = CreateFixture();
            var recipes = fixture.CreateMany<DbRecipe>().ToList();
            context.Recipes.AddRange(recipes);
            context.SaveChanges();
            return recipes;
        }

        private static Recipe BuildSingleRecipe()
        {
            var fixture = new Fixture();
            fixture.Customize<Recipe>(x => x.With(y => y.Title, fixture.Create<string>()));
            fixture.Customize<Ingredient>(x => x.With(y => y.Name, fixture.Create<string>));
            fixture.Customize<Tag>(x => x.With(y => y.Name, fixture.Create<string>));
            return fixture.Create<Recipe>();
        }
    }
}