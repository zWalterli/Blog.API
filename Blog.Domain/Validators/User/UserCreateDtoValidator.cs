using Blog.Domain.DTOs;
using FluentValidation;

namespace Blog.Domain.Validators;

public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .MaximumLength(50).WithMessage("Email não pode exceder 50 caracteres.")
            .EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é obrigatório.")
            .MinimumLength(6).WithMessage("Senha deve ter pelo menos 6 caracteres.")
            .MaximumLength(50).WithMessage("Senha não pode exceder 50 caracteres.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9])\S{8,}$")
            .WithMessage("Senha deve ter ao menos 8 caracteres, incluindo maiúscula, minúscula, número e caractere especial, sem espaços.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Primeiro nome é obrigatório.")
            .MaximumLength(50).WithMessage("Primeiro nome não pode exceder 50 caracteres.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Ultimo nome é obrigatório.")
            .MaximumLength(50).WithMessage("Ultimo nome não pode exceder 50 caracteres.");
    }
}