using Blog.Domain.DTOs;
using Blog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController(IUserService _userService) : BaseController
{
    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    /// <param name="user">Usuário a ser criado</param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreatePost([FromBody] UserCreateDto user, CancellationToken cancellationToken)
    {
        await _userService.CreateUserAsync(user, cancellationToken);
        return OkResponse();
    }
}