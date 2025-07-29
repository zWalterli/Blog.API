using Blog.Application.Hubs;
using Blog.Domain.DTOs.Post;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using Blog.Domain.Mapper;
using Blog.Domain.Entity;
using Microsoft.AspNetCore.SignalR;

namespace Blog.Application.Services;

public class PostService(IPostRepository _postRepository, IHubContext<PostNotificationHub> _hubContext) : IPostService
{
    public async Task CreatePostAsync(PostCreateDto postDto, int userId, CancellationToken cancellationToken)
    {
        Post entity = postDto.ToEntity();
        entity.SetAuthor(userId);
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
        var postDb = await _postRepository.ObterPorIdAsync(postDto.Id, cancellationToken);
        if (postDb is null || postDb.AuthorId != userId)
        {
            throw new InvalidOperationException("Post não foi encontrado ou você não tem permissão para atualizar.");
        }

        postDb.UpdateTitle(postDto.Title);
        postDb.UpdateContent(postDto.Content);

        await _postRepository.UpdateAsync(postDb, cancellationToken);
    }
}
