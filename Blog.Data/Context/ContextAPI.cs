using Blog.Application.Domain;
using Blog.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Context;

public class ContextAPI : DbContext
{
    public ContextAPI(DbContextOptions<ContextAPI> options)
        : base(options) { }

    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
}