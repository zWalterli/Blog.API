using Blog.Data.Context;
using Blog.Data.Repositories;
using Blog.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyApi.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        var connStr = configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"Using connection string: {connStr}");
        services.AddDbContext<ContextAPI>(options =>
            options.UseMySql(connStr, ServerVersion.AutoDetect(connStr)));

        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddSignalR();

        return services;
    }

    public static void RunMigrate(this WebApplication app)
    {
        try
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ContextAPI>();
                db.Database.Migrate();
            }
        }
        catch (Exception ex)
        { }
    }
}
