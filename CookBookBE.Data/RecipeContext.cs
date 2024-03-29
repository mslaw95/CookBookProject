﻿using CookBookBE.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace CookBookBE.Data
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options) { }

        public DbSet<DbRecipe> Recipes { get; set; }
        public DbSet<DbRecipe> Ingredients { get; set; }
        public DbSet<DbRecipe> Tags { get; set; }
    }
}
