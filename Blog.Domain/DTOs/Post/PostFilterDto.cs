namespace Blog.Domain.DTOs.Post;

public class PostFilterDto
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int UserId { get; set; }

    public void SetUserId(int userId)
    {
        UserId = userId;
    }
}