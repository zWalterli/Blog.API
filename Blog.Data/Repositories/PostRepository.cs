using Blog.Data.Context;
using Blog.Domain.Enums;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories;

public class PostRepository(ContextAPI _contextAPI) : IPostRepository
{
    public async Task DeleteAsync(int id, int userId, CancellationToken cancellationToken)
    {
        var post = await _contextAPI.Posts.FirstOrDefaultAsync(p => p.Id == id && p.AuthorId == userId, cancellationToken);
        post!.Status = StatusModelEnum.Deleted;
        await _contextAPI.SaveChangesAsync(cancellationToken);
    }

    public async Task<(int, IEnumerable<Post>)> GetAllAsync(int userId, int page, int pageSize, CancellationToken cancellationToken)
    {
        var query = _contextAPI.Posts
            .Include(p => p.Author)
            .Where(p => p.Status != StatusModelEnum.Deleted && p.AuthorId == userId);

        var count = await query.CountAsync();
        var result = await query
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