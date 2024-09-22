using Azure.Identity;
using clean_keycloak_pkce.Application.Common.Interfaces;
using clean_keycloak_pkce.Infrastructure.Data;
using clean_keycloak_pkce.Web.Services;
using Microsoft.AspNetCore.Mvc;
using NSwag;


namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        // services.AddOpenApiDocument((configure, sp) =>
        // {
        //     configure.Title = "clean_keycloak_pkce API";
        //
        // });
        services.AddSwaggerDocument(config => {
            config.AddSecurity("JWT token", new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                Description = "Copy 'Bearer ' + valid JWT token into field",
                In = OpenApiSecurityApiKeyLocation.Header
            });
            config.PostProcess = (document) =>
            {
                document.Info.Version = "v1";
                document.Info.Title = "HSF-Rest-API";
                document.Info.Description = "ASP.NET 8 HSF-Rest-API";
            };
        });
        return services;
    }

    public static IServiceCollection AddKeyVaultIfConfigured(this IServiceCollection services, ConfigurationManager configuration)
    {
        var keyVaultUri = configuration["KeyVaultUri"];
        if (!string.IsNullOrWhiteSpace(keyVaultUri))
        {
            configuration.AddAzureKeyVault(
                new Uri(keyVaultUri),
                new DefaultAzureCredential());
        }

        return services;
    }
}
