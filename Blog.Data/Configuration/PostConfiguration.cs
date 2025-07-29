using Blog.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Configuration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.Content)
               .IsRequired();

        builder.Property(p => p.CreatedAt)
               .IsRequired();

        builder.Property(p => p.UpdatedAt)
               .IsRequired();

        builder.HasOne(p => p.Author)
               .WithMany(u => u.Posts)
               .HasForeignKey(p => p.AuthorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}