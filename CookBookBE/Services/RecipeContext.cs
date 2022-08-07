using CookBookBE.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBookBE.Services
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options) { }

        public DbSet<DbRecipe> Recipes { get; set; }
    }
}
