using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Domain.DTOs;
using Blog.Domain.DTOs.Login;
using Blog.Domain.DTOs.Token;
using Blog.Domain.Interfaces.Services;
using Blog.Domain.Mapper;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Application.Services;

public class TokenService(IUserService _userService, JwtSettings _jwtSettings) : ITokenService
{
    public async Task<TokenDto> LoginAsync(LoginDto login, CancellationToken cancellationToken)
    {
        string passwordHash = UserMapper.HashPassword(login.Password);
        UserGetDto user = await _userService.GetByEmailPasswordAsync(login.Email, passwordHash, cancellationToken);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Email ou senha inválido!");
        }

        string accessToken = GenerateAccessToken(user);
        DateTime expiration = DateTime.UtcNow.AddMinutes(int.Parse(_jwtSettings.Expiration));
        return new TokenDto(accessToken, expiration);
    }

    private string GenerateAccessToken(UserGetDto user)
    {
        List<Claim> claims = new()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtSettings.Expiration)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}