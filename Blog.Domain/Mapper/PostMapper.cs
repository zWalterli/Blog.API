using Blog.Domain.DTOs.Post;
using Blog.Domain.Model;

namespace Blog.Domain.Mapper;

public static class PostMapper
{
    public static Post ToEntity(this PostCreateDto dto)
    {
        return new Post
        {
            Title = dto.Title,
            Content = dto.Content,
            AuthorId = dto.AuthorId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Post ToEntity(this PostUpdateDto dto)
    {
        return new Post
        {
            Id = dto.Id,
            Title = dto.Title,
            Content = dto.Content,
            AuthorId = dto.AuthorId,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static PostGetDto ToDto(this Post post)
    {
        return new PostGetDto
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            AuthorId = post.AuthorId,
            Author = post.Author.ToDto()
        };
    }
}