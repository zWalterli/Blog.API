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
        const int maxRetries = 10;
        const int delaySeconds = 5;
        int attempt = 0;

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ContextAPI>();

        while (true)
        {
            try
            {
                db.Database.Migrate();
                break;
            }
            catch (Exception ex)
            {
                attempt++;
                if (attempt >= maxRetries)
                {
                    Console.WriteLine($"[ERROR] Migration failed after {maxRetries} attempts: {ex.Message}");
                    throw;
                }

                Console.WriteLine($"[WARN] DB not ready yet. Retry {attempt}/{maxRetries} in {delaySeconds}s...");
                Thread.Sleep(TimeSpan.FromSeconds(delaySeconds));
            }
        }
    }
}
