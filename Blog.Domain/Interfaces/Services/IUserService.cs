using Blog.Domain.DTOs;

namespace Blog.Domain.Interfaces.Services;

public interface IUserService
{
    Task CreateUserAsync(UserCreateDto user, CancellationToken cancellationToken);
    Task<UserGetDto> GetByEmailPasswordAsync(string email, string password, CancellationToken cancellationToken);
}