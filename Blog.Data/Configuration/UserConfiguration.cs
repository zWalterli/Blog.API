using Blog.Application.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
       public void Configure(EntityTypeBuilder<User> builder)
       {
              builder.ToTable("Users");

              builder.HasKey(u => u.Id);

              builder.Property(u => u.FirstName)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(u => u.LastName)
                     .IsRequired()
                     .HasMaxLength(100);

              builder.Property(u => u.Email)
                     .IsRequired()
                     .HasMaxLength(150);

              builder.HasIndex(u => u.Email)
                     .IsUnique();

              builder.Property(u => u.PasswordHash)
                     .IsRequired();
       }
}