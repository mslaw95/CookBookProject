using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using System.Net.Mime;
using CookBookBE.Services;
using CookBookBE.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var isDevelopment = builder.Environment.IsDevelopment();

/// <summary>
// Configure Services
/// </summary>
/// 
builder.Services
    .AddScoped<IRecipeDbService, RecipeDbService>()
    .AddDbContext<RecipeContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    })
    .AddControllers(options =>
    {
        options.SuppressAsyncSuffixInActionNames = false;
    });

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CookBookWebApp", Version = "v1" });
    })
    .AddHealthChecks();

if (isDevelopment)
{
    // Register CORS with named policy
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "AllowAllOrigin",
            builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });
}

/// <summary>
// Configure App
/// </summary>
/// 
var app = builder.Build();

if (isDevelopment)
{
    app.UseDeveloperExceptionPage()
        .UseSwagger()
        .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CookBookWebApp v1"))
        .UseHttpsRedirection();

    RecipeContext dbContext = app.Services.GetRequiredService<RecipeContext>();
    dbContext.Database.Migrate();
}

app.UseRouting();
// Enable CORS with named policy, must be before UserAuthorization
app.UseCors("AllowAllOrigin");
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
    {
        Predicate = check => check.Tags.Contains("ready"),
        ResponseWriter = async (context, report) =>
        {
            var result = JsonSerializer.Serialize(
                new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(entry => new
                    {
                        name = entry.Key,
                        status = entry.Value.Status.ToString(),
                        exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                        duration = entry.Value.Duration.ToString()
                    })
                }
            );

            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsync(result);
        }
    });

    endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
    {
        Predicate = _ => false
    });
});

app.Run();
