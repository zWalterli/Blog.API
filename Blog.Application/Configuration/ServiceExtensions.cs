using Blog.Application.Services;
using Blog.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application.Configuration;

public static class ServiceExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}