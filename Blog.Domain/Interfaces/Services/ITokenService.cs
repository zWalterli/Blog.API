using Blog.Domain.DTOs.Login;
using Blog.Domain.DTOs.Token;

namespace Blog.Domain.Interfaces.Services;

public interface ITokenService
{
    Task<TokenDto> LoginAsync(LoginDto login, CancellationToken cancellationToken);
}