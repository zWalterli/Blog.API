using System.ComponentModel;

namespace Blog.Domain.DTOs.Post;

public class PostFilterDto
{
    [DefaultValue(1)]
    public int Page { get; set; } = 1;
    [DefaultValue(10)]
    public int PageSize { get; set; } = 10;
    public int UserId { get; set; }

    public void SetUserId(int userId)
    {
        UserId = userId;
    }
}