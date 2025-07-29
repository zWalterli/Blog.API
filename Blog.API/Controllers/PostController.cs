using Blog.Domain.DTOs.Post;
using Blog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PostController(IPostService _postService) : BaseController
{
    /// <summary>
    /// Cria um novo post
    /// </summary>
    /// <param name="post">Post a ser criado</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] PostCreateDto post)
    {
        await _postService.CreatePostAsync(post, UserId, CancellationToken.None);
        return OkResponse();
    }

    /// <summary>
    /// Atualiza um post
    /// </summary>
    /// <param name="post">Post a ser atualizado</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] PostUpdateDto post)
    {
        post.Id = id;
        await _postService.UpdatePostAsync(post, UserId, CancellationToken.None);
        return OkResponse();
    }

    /// <summary>
    /// Deleta um post pelo ID
    /// </summary>
    /// <param name="id">Identificador do post</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        await _postService.DeletePostAsync(id, UserId, CancellationToken.None);
        return OkResponse();
    }

    /// <summary>
    /// Obtem todos os posts com paginação
    /// </summary>
    /// <param name="page">Número da página</param>
    /// <param name="pageSize">Tamanho da página</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetPosts(
        [FromQuery] PostFilterDto filter
    )
    {
        filter.SetUserId(UserId);
        (IEnumerable<PostGetDto> posts, int count) = await _postService.GetAllPostsAsync(filter, CancellationToken.None);
        return OkPaginatedResponse(filter.Page, filter.PageSize, count, posts);
    }
}