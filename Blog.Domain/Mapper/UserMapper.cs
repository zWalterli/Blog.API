using Blog.Application.Domain;
using Blog.Domain.DTOs;

namespace Blog.Domain.Mapper;

public static class UserMapper
{
    public static string HashPassword(string password)
    {
        return password;
    }

    public static User ToEntity(this UserCreateDto dto)
    {
        return new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = dto.Password
        };
    }

    public static UserGetDto ToDto(this User user)
    {
        return new UserGetDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }
}