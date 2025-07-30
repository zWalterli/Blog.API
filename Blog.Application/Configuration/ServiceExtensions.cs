using Blog.Application.Services;
using Blog.Domain.DTOs;
using Blog.Domain.DTOs.Post;
using Blog.Domain.Interfaces.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Blog.Domain.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Application.Configuration;

public static class ServiceExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddValidators();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        services.Configure<ApiBehaviorOptions>(opt =>
        {
            opt.SuppressModelStateInvalidFilter = true;
        });

        services.AddTransient<IValidator<PostCreateDto>, PostCreateDtoValidator>();
        services.AddTransient<IValidator<PostUpdateDto>, PostUpdateDtoValidator>();
        services.AddTransient<IValidator<PostFilterDto>, PostFilterDtoValidator>();
        services.AddTransient<IValidator<UserCreateDto>, UserCreateDtoValidator>();

        return services;
    }
}