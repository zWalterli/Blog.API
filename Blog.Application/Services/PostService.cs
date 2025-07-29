using Blog.Application.Hubs;
using Blog.Domain.DTOs.Post;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using Blog.Domain.Mapper;
using Blog.Domain.Model;
using Microsoft.AspNetCore.SignalR;

namespace Blog.Application.Services;

public class PostService(IPostRepository _postRepository, IHubContext<PostNotificationHub> _hubContext) : IPostService
{
    public async Task CreatePostAsync(PostCreateDto postDto, int userId, CancellationToken cancellationToken)
    {
        Post entity = postDto.ToEntity();
        entity.AuthorId = userId;
        await _postRepository.InsertAsync(entity, cancellationToken);

        string message = $"Novo post criado: {entity.Title}";
        await _hubContext.Clients.All.SendAsync("NewPost", entity.Id, message, cancellationToken);
    }

    public async Task DeletePostAsync(int id, int userId, CancellationToken cancellationToken)
    {
        await _postRepository.DeleteAsync(id, userId, cancellationToken);
    }

    public async Task<(IEnumerable<PostGetDto>, int)> GetAllPostsAsync(PostFilterDto filter, CancellationToken cancellationToken)
    {
        (int count, IEnumerable<Post>? posts) = await _postRepository.GetAllAsync(filter.UserId, filter.Page, filter.PageSize, cancellationToken);
        IEnumerable<PostGetDto>? postsDto = posts.Select(post => post.ToDto());

        return (postsDto, count);
    }

    public async Task UpdatePostAsync(PostUpdateDto postDto, int userId, CancellationToken cancellationToken)
    {
        var entity = postDto.ToEntity();
        entity.AuthorId = userId;
        
        await _postRepository.UpdateAsync(entity, cancellationToken);
    }
}
