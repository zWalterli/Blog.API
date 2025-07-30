using Blog.Domain.Entity;

namespace Blog.Domain.Interfaces.Repositories;

public interface IPostRepository
{
    Task<Post> ObterPorIdAsync(int id, CancellationToken cancellationToken);
    Task<(int, IEnumerable<Post>)> GetAllAsync(int page, int pageSize, int? userId, CancellationToken cancellationToken);
    Task InsertAsync(Post post, CancellationToken cancellationToken);
    Task UpdateAsync(Post post, CancellationToken cancellationToken);
    Task DeleteAsync(int id, int userId, CancellationToken cancellationToken);
}