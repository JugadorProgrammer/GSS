using GSS.Domain.Auth;
using GSS.Domain.ConfigurationOptions;
using GSS.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GSS.Application.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddScoped<ITokenAuthService, JWTTokenAuthService>();
        }

        public static void AddOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection(JWTTokenOptions.JWT));
        }

        public static void AddAuth(this WebApplicationBuilder builder)
        {
            var jwtOptions = builder.Configuration.GetSection(JWTTokenOptions.JWT).Get<JWTTokenOptions>()!;

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                };
            });

            builder.Services.AddAuthorization();
        }
    }
}
