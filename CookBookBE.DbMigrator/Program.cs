using CookBookBE.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CookBookBE.DbMigrator
{
    internal class Program : IDesignTimeDbContextFactory<RecipeContext>
    {
        static void Main(string[] args)
        {
            var program = new Program();
            using var context = program.CreateDbContext(args);
            context.Database.Migrate();
        }

        public RecipeContext CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RecipeContext>()
               .UseSqlServer(configuration.GetConnectionString("DockerSQLServerConnection"));

            return new RecipeContext(optionsBuilder.Options);
        }
    }
}