using P79.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace P79.Api.Admin.Extensions
{
    public static class StartupJWTExtension
    {
        public const string APP_SETTINGS_JWT_BEARER_TOKEN_SECTION = "JwtBearerToken";
        public static IServiceCollection AddJWTExtension(this IServiceCollection services, IConfiguration Configuration)
        {
            JWTSettings jwtSettings = new JWTSettings();
            var provider = services
                .BuildServiceProvider()
                .GetService<JWTSettings>();
            Configuration.Bind(APP_SETTINGS_JWT_BEARER_TOKEN_SECTION, jwtSettings);
            if (!string.IsNullOrWhiteSpace(jwtSettings.SecretKey)) {
                services.AddSingleton(jwtSettings);
                var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = jwtSettings.Issuer,
                            ValidateAudience = true,
                            ValidAudience = jwtSettings.Audience,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero
                        };
                    });
                //services.AddScoped<IUserClaimsPrincipalFactory<User>, AppClaimsPrincipalFactory>();
            }
            return services;
        }
    }
}
