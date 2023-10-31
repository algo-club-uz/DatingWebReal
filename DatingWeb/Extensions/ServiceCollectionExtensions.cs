﻿using DatingWeb.Managers;
using DatingWeb.Options;
using DatingWeb.Providers;
using DatingWeb.Repositories.Interfaces;
using DatingWeb.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DatingWeb.Extensions;

public static  class ServiceCollectionExtensions
{
    private static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(nameof(JwtOptions));
        services.Configure<JwtOptions>(section);
        JwtOptions jwtOptions = section.Get<JwtOptions>()!;
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var signingKey = System.Text.Encoding.UTF32.GetBytes(jwtOptions.SigningKey);
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = jwtOptions.ValidIssuer,
                    ValidAudience = jwtOptions.ValidAudience,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
    }

    public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwt(configuration);
        services.AddScoped<JwtTokenManager>();
        services.AddScoped<UserManager>();
        services.AddHttpContextAccessor();
        services.AddScoped<UserProvider>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<ChatManager>();
    }

}