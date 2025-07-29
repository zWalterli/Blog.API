using Blog.Application.Domain;
using Blog.Domain.DTOs;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using Blog.Domain.Mapper;

namespace Blog.Application.Services;

public class UserService(IUserRepository _userRepository) : IUserService
{
    public async Task CreateUserAsync(UserCreateDto user, CancellationToken cancellationToken)
    {
        User userDataBase = await _userRepository.GetByEmailAsync(user.Email, cancellationToken);
        if (userDataBase != null)
        {
            throw new ArgumentException("Usuário já cadastrado!");
        }

        User newUser = user.ToEntity();
        newUser.PasswordHash = UserMapper.HashPassword(user.Password);

        await _userRepository.InsertAsync(newUser, cancellationToken);
    }

    public async Task<UserGetDto> GetByEmailPasswordAsync(string email, string password, CancellationToken cancellationToken)
    {
        User userDataBase = await _userRepository.GetByEmailAndPasswordAsync(email, password, cancellationToken);
        if (userDataBase != null)
        {
            throw new ArgumentException("Não existe usuário cadastrado com essa combinação!");
        }

        return userDataBase!.ToDto();
    }
}