using Blog.Domain.DTOs.Login;
using Blog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class TokenController(ITokenService _tokenService) : BaseController
{
    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    /// <param name="user">Usuário a ser criado</param>
    /// <returns></returns>
    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto login, CancellationToken cancellationToken)
    {
        var token = await _tokenService.LoginAsync(login, cancellationToken);
        return OkResponse(token);
    }
}