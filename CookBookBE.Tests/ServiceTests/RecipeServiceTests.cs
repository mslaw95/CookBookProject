using Xunit;
using CookBookBE.Data.DbModels;
using CookBookBE.Data.Models;
using CookBookBE.Api.Services;
using FluentAssertions;
using CookBookBE.Data.Repositories.Interfaces;
using Moq;

namespace CookBookBE.Tests.ServiceTests
{
    public class RecipeServiceTests
    {
        private readonly RecipeService _recipeService;
        private readonly Mock<IRecipeRepository> _recipeRepositoryMock = new();

        public RecipeServiceTests()
        {
            _recipeService = new RecipeService(_recipeRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateRecipe_ShouldCreateRecipe()
        {
            var recipe = new Recipe() { Title = "TestCase1", Ingredients = new List<Ingredient>() };

            var result = await _recipeService.CreateRecipeAsync(recipe);

            result.Should().NotBeNull();
        }

        [Fact]
        public async void GetRecipes_ShouldGetAllRecipes()
        {
            var recipes = new List<DbRecipe>()
            {
                new() { Title = "TestCase1", Ingredients = new List<DbIngredient>() },
                new() { Title = "TestCase2", Ingredients = new List<DbIngredient>() },
                new() { Title = "TestCase3", Ingredients = new List<DbIngredient>() }
            };

            _recipeRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(recipes);

            var result = await _recipeService.GetRecipesAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().Contain(recipes);
        }

        // TODO: May be usefull in the Repository tests, if not: remove
        //public static Fixture CreateFixture()
        //{
        //    var fixture = new Fixture();
        //    fixture.Customize<DbRecipe>(x => x.With(y => y.Id, default(Guid)));
        //    fixture.Customize<DbIngredient>(x => x.With(y => y.Name, fixture.Create<string>));
        //    fixture.Customize<DbTag>(x => x.With(y => y.Name, fixture.Create<string>));
        //    return fixture;
        //}

        //public static IEnumerable<DbRecipe> CreateManyRecipes(RecipeContext context)
        //{
        //    var fixture = CreateFixture();
        //    var recipes = fixture.CreateMany<DbRecipe>().ToList();
        //    context.Recipes.AddRange(recipes);
        //    context.SaveChanges();
        //    return recipes;
        //}

        //private static Recipe BuildSingleRecipe()
        //{
        //    var fixture = new Fixture();
        //    fixture.Customize<Recipe>(x => x.With(y => y.Title, fixture.Create<string>()));
        //    fixture.Customize<Ingredient>(x => x.With(y => y.Name, fixture.Create<string>));
        //    fixture.Customize<Tag>(x => x.With(y => y.Name, fixture.Create<string>));
        //    return fixture.Create<Recipe>();
        //}
    }
}