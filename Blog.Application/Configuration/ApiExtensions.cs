using System.Text;
using Blog.Application.Filters;
using Blog.Domain.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Blog.Application.Configuration;

public static class ApiExtensions
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Token JWT no formato: Bearer {token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
            });
        });

        services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionFilter>();
            options.Filters.Add<ModelStateToBaseResponseFilter>();
        });

        services.AddControllers();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });


        return services;
    }
}