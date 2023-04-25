using CookBookBE.Api.Services.Interfaces;
using CookBookBE.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using CookBookBE.Data;
using CookBookBE.Data.Repositories.Interfaces;
using CookBookBE.Data.Repositories;

namespace CookBookBE.Api;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContextFactory<RecipeContext>(o =>
        {
            o.UseSqlServer(Configuration.GetConnectionString("DockerSQLServerConnection"));
        });

        services.AddControllers()
        .AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        });

        services.AddScoped<IRecipeService, RecipeService>();
        services.AddSingleton<IRecipeRepository, RecipeRepository>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        //{
        //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CookBookWebApp", Version = "v1" });
        //});


        //if (isDevelopment)
        //{
        //    // Register CORS with named policy
        //    builder.Services.AddCors(options =>
        //    {
        //        options.AddPolicy(name: "AllowAllOrigin",
        //            builder =>
        //            {
        //                builder.AllowAnyOrigin()
        //                    .AllowAnyHeader()
        //                    .AllowAnyMethod();
        //            });
        //    });
        //}
        services.AddCors();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors(o => o.AllowAnyOrigin());

        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(e =>
        {
            e.MapControllers();
        });
    }
}
