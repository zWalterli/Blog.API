using Blog.Application.Domain;

namespace Blog.Domain.Entity;

public class Post : BaseEntity
{
    public Post()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public Post(string title, string content, int? id = null, int? authorId = null) : this()
    {
        Id = id ?? 0;
        Title = title;
        Content = content;
        AuthorId = authorId ?? 0;
    }

    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public int AuthorId { get; private set; }
    //
    public User Author { get; set; }

    public void UpdateContent(string content)
    {
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateTitle(string title)
    {
        Title = title;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetAuthor(int authorId)
    {
        AuthorId = authorId;
        UpdatedAt = DateTime.UtcNow;
    }
}