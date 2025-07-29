using Blog.Domain.DTOs.Post;

namespace Blog.Domain.Interfaces.Services;

public interface IPostService
{
    Task<(IEnumerable<PostGetDto>, int)> GetAllPostsAsync(PostFilterDto filter, int userId, CancellationToken cancellationToken);
    Task CreatePostAsync(PostCreateDto postDto, int userId, CancellationToken cancellationToken);
    Task UpdatePostAsync(PostUpdateDto postDto, int postId, int userId, CancellationToken cancellationToken);
    Task DeletePostAsync(int id, int userId, CancellationToken cancellationToken);
}