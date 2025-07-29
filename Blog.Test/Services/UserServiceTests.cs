using Blog.Application.Domain;
using Blog.Application.Services;
using Blog.Domain.DTOs;
using Blog.Domain.Interfaces.Repositories;
using Moq;

namespace Blog.Test.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _service;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _service = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateUserAsync_EmailNotRegistered_CreatesUser()
    {
        // Arrange
        var userDto = new UserCreateDto { Email = "new@test.com", Password = "123" };
        _userRepositoryMock.Setup(r => r.GetByEmailAsync(userDto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null);
        _userRepositoryMock.Setup(r => r.InsertAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.CreateUserAsync(userDto, CancellationToken.None);

        // Assert
        _userRepositoryMock.Verify(r => r.InsertAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateUserAsync_EmailAlreadyRegistered_ThrowsException()
    {
        // Arrange
        var userDto = new UserCreateDto { Email = "exists@test.com", Password = "123" };
        _userRepositoryMock.Setup(r => r.GetByEmailAsync(userDto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new User { Email = userDto.Email });

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateUserAsync(userDto, CancellationToken.None));
    }

    [Fact]
    public async Task GetByEmailPasswordAsync_UserExists_ReturnsDto()
    {
        // Arrange
        string email = "user@test.com", password = "hash";
        var user = new User { Id = 1, Email = email, FirstName = "Test", LastName = "User" };
        _userRepositoryMock.Setup(r => r.GetByEmailAndPasswordAsync(email, password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var result = await _service.GetByEmailPasswordAsync(email, password, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(email, result.Email);
    }

    [Fact]
    public async Task GetByEmailPasswordAsync_UserNotExists_ThrowsException()
    {
        // Arrange
        string email = "notfound@test.com", password = "hash";
        _userRepositoryMock.Setup(r => r.GetByEmailAndPasswordAsync(email, password, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.GetByEmailPasswordAsync(email, password, CancellationToken.None));
    }
}