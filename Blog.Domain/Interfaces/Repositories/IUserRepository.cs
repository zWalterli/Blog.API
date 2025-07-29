using Blog.Application.Domain;

namespace Blog.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User> GetByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken);
    Task InsertAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
}
