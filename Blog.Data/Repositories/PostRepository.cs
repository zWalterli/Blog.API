using Blog.Data.Context;
using Blog.Domain.Enums;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories;

public class PostRepository(ContextAPI _contextAPI) : IPostRepository
{
    public async Task<Post> ObterPorIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _contextAPI.Posts.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task DeleteAsync(int id, int userId, CancellationToken cancellationToken)
    {
        Post post = await _contextAPI.Posts.FirstOrDefaultAsync(p => p.Id == id && p.AuthorId == userId, cancellationToken);
        post!.Status = StatusModelEnum.Deleted;
        await _contextAPI.SaveChangesAsync(cancellationToken);
    }

    public async Task<(int, IEnumerable<Post>)> GetAllAsync(int page, int pageSize, int? userId, CancellationToken cancellationToken)
    {
        IQueryable<Post> query = _contextAPI.Posts
            .Where(p => p.Status != StatusModelEnum.Deleted)
            .AsNoTracking();

        if (userId.HasValue)
        {
            query = query.Where(p => p.AuthorId == userId.Value);
        }

        int count = await query.CountAsync();
        List<Post> result = await query
            .Include(x => x.Author)
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (count, result);
    }

    public async Task InsertAsync(Post post, CancellationToken cancellationToken)
    {
        _contextAPI.Posts.Add(post);
        await _contextAPI.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Post post, CancellationToken cancellationToken)
    {
        _contextAPI.Posts.Update(post);
        await _contextAPI.SaveChangesAsync(cancellationToken);
    }
}