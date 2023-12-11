using Catalog.Host.Configurations;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Infrastructure.Filters;
using Infrastructure.Extentions;

var configuration = GetConfiguration();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "eShop- Catalog HTTP API",
    Version = "v1",
    Description = "The Catalog Service HTTP API"
});

    var authority = configuration["Authorization:Authority"];
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            Implicit = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = new Uri($"{authority}/connect/authorize"),
                TokenUrl = new Uri($"{authority}/connect/token"),
                Scopes = new Dictionary<string, string>()
                {   
                    { "mvc", "website" },
                    { "catalog.catalogbff", "catalog.catalogbff" },
                    { "catalog.cataloggame", "catalog.catalogame" },
                    {"catalog.catalogplatform", "catalog.catalogplatform" },
                    {"catalog.cataloggenre", "catalog.cataloggenre"},
                    {"catalog.catalogpublisher", "catalog.catalogpublisher" }
                }
            }
        }
    });

    options.OperationFilter<AuthorizeCheckOperationFilter>();
});
builder.AddConfiguration();
builder.Services.Configure<CatalogConfig>(configuration);
builder.Services.AddAuthorization(configuration);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole(options => options.IncludeScopes = true);
    loggingBuilder.SetMinimumLevel(LogLevel.Information);
});

builder.Services.AddTransient<ICatalogGameRepository, CatalogGameRepository>();
builder.Services.AddTransient<IBaseCatalogRepository<CatalogGenre>, CatalogGenreRepository>();
builder.Services.AddTransient<IBaseCatalogRepository<CatalogPlatform>, CatalogPlatformRepository>();
builder.Services.AddTransient<IBaseCatalogRepository<CatalogPublisher>, CatalogPublisherRepository>();
builder.Services.AddTransient<ICatalogService, CatalogService>();
builder.Services.AddTransient<ICatalogGameService, CatalogGameService>();
builder.Services.AddTransient<IBaseCatalogServices<CatalogGenre>, CatalogGenreService>();
builder.Services.AddTransient<IBaseCatalogServices<CatalogPlatform>, CatalogPlatformService>();
builder.Services.AddTransient<IBaseCatalogServices<CatalogPublisher>, CatalogPublisherService>();

builder.Services.AddDbContextFactory<ApplicationDbContext>(opts => opts.UseNpgsql(configuration["ConnectionString"]));
builder.Services.AddScoped<IDbContextWrapper<ApplicationDbContext>, DbContextWrapper<ApplicationDbContext>>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "CorsPolicy",
        builder => builder
            .SetIsOriginAllowed((host) => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();
app.UseSwagger()
    .UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", "Catalog.API V1");
        setup.OAuthClientId("catalogswaggerui");
        setup.OAuthAppName("Catalog Swagger UI");
    });

app.UseRouting();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllers();
});

CreateDbIfNotExists(app);
app.Run();

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}

void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            Seed.SeedData(context).Wait();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}
