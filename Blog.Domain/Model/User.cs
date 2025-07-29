using Blog.Domain.Model;

namespace Blog.Application.Domain;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    //
    public List<Post> Posts { get; set; }
}
