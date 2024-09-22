
using System.Net;
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
            document.OperationProcessors.Add(new OperationSecurityScopeProcessor("Bearer"));
            document.AddSecurity("Bearer", Enumerable.Empty<string>(),
                new OpenApiSecurityScheme()
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = nameof(Authorization),
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Copy this into the value field: Bearer {token}"
                }
            );
        });
//             document.AddSecurity("bearer", Enumerable.Empty<string>(),
//                 new OpenApiSecurityScheme
//                 {
//                     Type = OpenApiSecuritySchemeType.OAuth2,
//                     Description = "My PKCE Authentication",
//                     // Flow = OpenApiOAuth2Flow.Password,
//                     // Flows = new OpenApiOAuthFlows()
//                     // {
//                     //     Implicit = new OpenApiOAuthFlow()
//                     //     {
//                     //         Scopes = new Dictionary<string, string>
//                     //         {
//                     //             { "read", "Read access to protected resources" },
//                     //             { "write", "Write access to protected resources" }
//                     //         },
//                     //         AuthorizationUrl = $"{configuration["Keycloak:ServerRealm"]}",
//                     //         TokenUrl = $"{configuration["Keycloak:ServerRealm"]}"
//                     //     },
//                     // }
//                 });
//
//             document.OperationProcessors.Add(
//                 new AspNetCoreOperationSecurityScopeProcessor("bearer"));
// //      new OperationSecurityScopeProcessor("bearer"));
//         });
        return services;
    }
}
