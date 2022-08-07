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

            await recipeDbService.CreateRecipeAsync(dbRecipe);

            return CreatedAtAction(nameof(GetRecipeAsync), new { id = dbRecipe.Id }, dbRecipe.ToDtoModel());
        }

        // Put /recipes/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRecipeAsync(Guid id, Recipe recipe)
        {
            var existingRecipe = await recipeDbService.GetRecipeAsync(id);
            if (existingRecipe is null)
            {
                return NotFound();
            }

            var updatedRecipe = existingRecipe with
            {
                Title = recipe.Title,
            };

            await recipeDbService.UpdateRecipeAsync(updatedRecipe);

            return NoContent();
        }

        // Delete /recipes/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecipeAsync(Guid id)
        {
            var existingRecipe = await recipeDbService.GetRecipeAsync(id);
            if (existingRecipe is null)
            {
                return NotFound();
            }

            await recipeDbService.DeleteRecipeAsync(id);

            return NoContent();
        }
    }
}
