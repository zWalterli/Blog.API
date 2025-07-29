using Blog.Data.Context;
using Blog.Data.Repositories;
using Blog.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyApi.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        var connStr = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ContextAPI>(options =>
            options.UseMySql(connStr, ServerVersion.AutoDetect(connStr)));

        services.AddScoped<IPostRepository, PostRepository>();

        services.AddSignalR();

        return services;
    }
}
