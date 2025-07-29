using Blog.Application.Domain;
using Blog.Data.Context;
using Blog.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repositories;

public class UserRepository(ContextAPI _contextAPI) : IUserRepository
{
    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _contextAPI.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User> GetByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken)
    {
        return await _contextAPI.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password, cancellationToken);
    }

    public async Task InsertAsync(User user, CancellationToken cancellationToken)
    {
        await _contextAPI.Users.AddAsync(user, cancellationToken);
        await _contextAPI.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _contextAPI.Users.Update(user);
        await _contextAPI.SaveChangesAsync(cancellationToken);
    }
}