using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using sgc.Application.Services;
using sgc.Domain.Interfaces.Repositories;
using sgc.Domain.Interfaces.Services;
using sgc.Infra.Repositories;
using sgc.Infra.Security;
using System.Text;

namespace sgc.Configs;

public static class Configurations
{
    public static IServiceCollection AddConfigurationDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringLocal = configuration.GetSection("ConnectionString:DefaultConnection").Value;
        var currentConnectionString = configuration["DATABASE_URL"] ?? connectionStringLocal;

        services.Configure<Database>(options =>
        {
            options.DefaultConnection = currentConnectionString ?? string.Empty;
        });

        return services;
    }

    public static IServiceCollection AddConfigurationToken(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));

        var tokenSettings = configuration.GetSection("TokenSettings").Get<TokenSettings>() ?? throw new InvalidOperationException("TokenSettings não configurado no appsettings.json");
        var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("Token validated successfully");
                    return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    Console.WriteLine($"OnChallenge: {context.Error}, {context.ErrorDescription}");
                    return Task.CompletedTask;
                }
            };
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = tokenSettings.Issuer,
                ValidAudience = tokenSettings.Audience,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();
        return services;
    }

    public static void AddMappingServices(this IServiceCollection services)
    {
        services.AddSingleton<ConnectionFactory>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ICollaboratorService, CollaboratorService>();
    }

    public static void AddMappingRepositories(this IServiceCollection services)
    {
        //services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<ITokenHandlers, TokenHandlers>();
        services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
    }
}
