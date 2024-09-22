using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace clean_keycloak_pkce.Web.ConfigService;

public static class KeycloakConfigService
{

    public static IServiceCollection AddKeycloakConfig(this IServiceCollection services, IConfiguration configuration)
    {
        // https://dev.to/kayesislam/integrating-openid-connect-to-your-application-stack-25ch
        services
            .AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Authority = $"{configuration["Keycloak:ServerRealm"]}";
                x.RequireHttpsMetadata = false;
                //x.MetadataAddress = $"{configuration["Keycloak:ServerRealm"]}/.well-known/openid-configuration";
                x.Audience = $"{configuration["Keycloak:aud"]}";
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = $"{configuration["Keycloak:name_claim"]}",
                    ValidAudience = $"{configuration["Keycloak:aud"]}",
                    ValidateAudience = false,
                    // https://stackoverflow.com/questions/60306175/bearer-error-invalid-token-error-description-the-issuer-is-invalid
                    ValidateIssuer = Convert.ToBoolean($"{configuration["Keycloak:validate-issuer"]}"),
                    ValidIssuer = $"{configuration["Keycloak:iss"]}",
                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
