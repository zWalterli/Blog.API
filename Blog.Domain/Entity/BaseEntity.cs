using Blog.Domain.Enums;

namespace Blog.Application.Domain;

public class BaseEntity
{
    public int Id { get; set; }
    public StatusModelEnum Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
