using CookBookBE.Models;
using CookBookBE.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CookBookBE.Controllers
{
    [ApiController]
    [Route("recipes")]
    public class RecipeController: ControllerBase
    {
        private readonly IRecipeDbService recipeDbService;

        public RecipeController(IRecipeDbService recipeDbService)
        {
            this.recipeDbService = recipeDbService;
        }

        // Get /recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipesAsync()
        {
            return Ok((await recipeDbService.GetRecipesAsync()).Select(recipe => recipe.ToDtoModel()));
        }

        // Get /recipes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeAsync(Guid id)
        {
            var recipe = await recipeDbService.GetRecipeAsync(id);
            return recipe is null ? NotFound() : recipe.ToDtoModel();
        }

        // Post /recipes
        [HttpPost]
        public async Task<ActionResult<Recipe>> CreateRecipeAsync(Recipe recipe)
        {
            DbRecipe dbRecipe = new()
            {
                Id = Guid.NewGuid(),
                Title = recipe.Title,
                CreatedDate = DateTimeOffset.UtcNow,
            };

            var createdRecipe = await recipeDbService.CreateRecipeAsync(dbRecipe);

            //return CreatedAtAction(nameof(GetRecipeAsync), new { id = dbRecipe.Id }, dbRecipe.ToDtoModel());
            return createdRecipe is null ? NotFound() : createdRecipe.ToDtoModel();
        }

        // Put /recipes/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Recipe>> UpdateRecipeAsync(Guid id, Recipe recipe)
        {
            var existingRecipe = await recipeDbService.GetRecipeAsync(id);
            if (existingRecipe is null)
            {
                return NotFound();
            }

            var recipeUpdate = existingRecipe with
            {
                Title = recipe.Title,
            };

            var updatedRecipe = await recipeDbService.UpdateRecipeAsync(recipeUpdate);

            return updatedRecipe is null ? NotFound() : updatedRecipe.ToDtoModel();

        }

        // Delete /recipes/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recipe>> DeleteRecipeAsync(Guid id)
        {
            var existingRecipe = await recipeDbService.GetRecipeAsync(id);
            if (existingRecipe is null)
            {
                return NotFound();
            }

            var deletedRecipe = await recipeDbService.DeleteRecipeAsync(id);

            //return NoContent();
            return deletedRecipe is null ? NotFound() : deletedRecipe.ToDtoModel();
        }
    }
}
