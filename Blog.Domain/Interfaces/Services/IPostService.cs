using Blog.Domain.DTOs.Post;

namespace Blog.Domain.Interfaces.Services;

public interface IPostService
{
    Task<(IEnumerable<PostGetDto>, int)> GetAllPostsAsync(PostFilterDto filter, CancellationToken cancellationToken);
    Task CreatePostAsync(PostCreateDto postDto, CancellationToken cancellationToken);
    Task UpdatePostAsync(PostUpdateDto postDto, CancellationToken cancellationToken);
    Task DeletePostAsync(int id, CancellationToken cancellationToken);
}