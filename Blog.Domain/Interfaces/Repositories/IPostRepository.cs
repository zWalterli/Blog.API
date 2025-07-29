using Blog.Domain.Model;

namespace Blog.Domain.Interfaces.Repositories;

public interface IPostRepository
{
    Task<(int, IEnumerable<Post>)> GetAllAsync(int userId, int page, int pageSize, CancellationToken cancellationToken);
    Task InsertAsync(Post post, CancellationToken cancellationToken);
    Task UpdateAsync(Post post, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}