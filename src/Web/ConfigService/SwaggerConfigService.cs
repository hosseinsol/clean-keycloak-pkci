using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace clean_keycloak_pkce.Web.ConfigService;

public static class SwaggerConfigService
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services, IConfiguration configuration)
    {
        // Add security definition and scopes to document
        services.AddOpenApiDocument(document =>
        {
            // document.OperationProcessors.Add(
            //     new OperationSecurityScopeProcessor(JwtBearerDefaults.AuthenticationScheme));
            // document.AddSecurity(JwtBearerDefaults.AuthenticationScheme, Enumerable.Empty<string>(),
            //     new OpenApiSecurityScheme()
            //     {
            //         Type = OpenApiSecuritySchemeType.ApiKey,
            //         Name = nameof(Authorization),
            //         In = OpenApiSecurityApiKeyLocation.Header,
            //         Description = "Copy this into the value field: Bearer {token}"
            //     }
            // );
            document.AddSecurity(JwtBearerDefaults.AuthenticationScheme, Enumerable.Empty<string>(),
                new OpenApiSecurityScheme
                {
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Name = "Authorization",
                    Type = OpenApiSecuritySchemeType.OAuth2, 
                    Description = "My PKCE Authentication",
                    Flow = OpenApiOAuth2Flow.AccessCode,
                    Flows = new OpenApiOAuthFlows()
                    {
                        AuthorizationCode = new OpenApiOAuthFlow()
                        {
                            Scopes = new Dictionary<string, string>
                            {
                                { "read", "Read access to protected resources" },
                                { "write", "Write access to protected resources" }
                            },
                            AuthorizationUrl = $"{configuration["Keycloak:AuthorizationUrl"]}",
                            TokenUrl = $"{configuration["Keycloak:TokenUrl"]}"
                        },
                    }
                });

            document.OperationProcessors.Add(
                new AspNetCoreOperationSecurityScopeProcessor(JwtBearerDefaults.AuthenticationScheme));
//      new OperationSecurityScopeProcessor("bearer"));
        });
        return services;
    }
}
