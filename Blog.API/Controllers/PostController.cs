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
    public async Task<IActionResult> CreateAsync([FromBody] PostCreateDto post)
    {
        await _postService.CreatePostAsync(post, UserId, CancellationToken.None);
        return CreatedResponse();
    }

    /// <summary>
    /// Atualiza um post
    /// </summary>
    /// <param name="post">Post a ser atualizado</param>
    /// <returns></returns>
    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int postId, [FromBody] PostUpdateDto post)
    {
        await _postService.UpdatePostAsync(post, postId, UserId, CancellationToken.None);
        return OkResponse();
    }

    /// <summary>
    /// Deleta um post pelo ID
    /// </summary>
    /// <param name="id">Identificador do post</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _postService.DeletePostAsync(id, UserId, CancellationToken.None);
        return OkResponse();
    }

    /// <summary>
    /// Obtem todos os posts com paginação
    /// </summary>
    /// <param name="filter">Filtros de paginação</param>
    /// <returns></returns>
    [HttpGet("paginated")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPaginatedAsync(
        [FromQuery] PostFilterDto filter
    )
    {
        (IEnumerable<PostGetDto> posts, int count) = await _postService.GetAllPostsAsync(filter, userId: null, CancellationToken.None);
        if (count == 0) return NoContent();
        return OkPaginatedResponse(filter.Page, filter.PageSize, count, posts);
    }

    /// <summary>
    /// Obtem todos os posts com paginação do usuario
    /// </summary>
    /// <param name="filter">Filtros de paginação</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetPaginatedByUserAsync(
        [FromQuery] PostFilterDto filter
    )
    {
        (IEnumerable<PostGetDto> posts, int count) = await _postService.GetAllPostsAsync(filter, UserId, CancellationToken.None);
        if (count == 0) return NoContent();
        return OkPaginatedResponse(filter.Page, filter.PageSize, count, posts);
    }
}