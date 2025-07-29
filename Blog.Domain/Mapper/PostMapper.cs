using Blog.Domain.DTOs.Post;
using Blog.Domain.Entity;

namespace Blog.Domain.Mapper;

public static class PostMapper
{
    public static Post ToEntity(this PostCreateDto dto)
    {
        return new Post(
            title: dto.Title,
            content: dto.Content
        );
    }

    public static Post ToEntity(this PostUpdateDto dto)
    {
        return new Post(
            id: dto.Id,
            title: dto.Title,
            content: dto.Content,
            authorId: dto.AuthorId
        );
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