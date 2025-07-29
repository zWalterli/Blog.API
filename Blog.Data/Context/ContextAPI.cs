using Blog.Application.Domain;
using Blog.Data.Configuration;
using Blog.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Context;

public class ContextAPI : DbContext
{
    public ContextAPI(DbContextOptions<ContextAPI> options)
        : base(options) { }

    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();
        var utcNow = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = utcNow;
                    entry.Entity.UpdatedAt = utcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = utcNow;
                    break;
                default:
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}