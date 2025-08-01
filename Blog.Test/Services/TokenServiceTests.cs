using Blog.Application.Services;
using Blog.Domain.DTOs.Login;
using Blog.Domain.DTOs.Token;
using Blog.Domain.DTOs;
using Blog.Domain.Interfaces.Services;
using Moq;

namespace Blog.Test.Services;

public class TokenServiceTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly JwtSettings _jwtOptions;
    private readonly TokenService _service;

    public TokenServiceTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _jwtOptions = new JwtSettings
        {
            Key = "H#s}#y5)t_h%0Ivd1s]TufhZia,UawCP",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            Expiration = "60"
        };
        _service = new TokenService(_userServiceMock.Object, _jwtOptions);
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ReturnsTokenDto()
    {
        // Arrange
        var login = new LoginDto { Email = "test@test.com", Password = "password" };
        var user = new UserGetDto { Id = 1, Email = login.Email, FirstName = "Test", LastName = "User" };
        _userServiceMock.Setup(s => s.GetByEmailPasswordAsync(login.Email, It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        TokenDto result = await _service.LoginAsync(login, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrEmpty(result.AccessToken));
        Assert.True(result.Expiration > DateTime.UtcNow);
    }

    [Fact]
    public async Task LoginAsync_InvalidCredentials_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        var login = new LoginDto { Email = "wrong@test.com", Password = "wrong" };
        _userServiceMock.Setup(s => s.GetByEmailPasswordAsync(login.Email, It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserGetDto?)null);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _service.LoginAsync(login, CancellationToken.None));
    }

    [Fact]
    public void GenerateAccessToken_ShouldContainCorrectClaims()
    {
        // Arrange
        var user = new UserGetDto { Id = 42, Email = "claims@test.com", FirstName = "Claims", LastName = "User" };
        var method = typeof(TokenService)
            .GetMethod("GenerateAccessToken", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(method); // Garante que o método existe
        var token = method.Invoke(_service, new object[] { user }) as string;
        Assert.False(string.IsNullOrEmpty(token));

        var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        // Assert
        Assert.Equal("TestIssuer", jwtToken.Issuer);
        Assert.Equal("TestAudience", jwtToken.Audiences.First());
        Assert.Contains(jwtToken.Claims, c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier && c.Value == "42");
        Assert.Contains(jwtToken.Claims, c => c.Type == System.Security.Claims.ClaimTypes.Email && c.Value == "claims@test.com");
        Assert.Contains(jwtToken.Claims, c => c.Type == System.Security.Claims.ClaimTypes.Name && c.Value == "Claims User");
    }
}