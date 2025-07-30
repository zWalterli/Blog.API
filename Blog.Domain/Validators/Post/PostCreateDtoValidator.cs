using Blog.Domain.DTOs.Post;
using FluentValidation;

namespace Blog.Domain.Validators;

public class PostCreateDtoValidator : AbstractValidator<PostCreateDto>
{
    public PostCreateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Título é obrigatório.")
            .MaximumLength(100).WithMessage("Título não pode exceder 100 caracteres.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Conteúdo é obrigatório.")
            .MinimumLength(10).WithMessage("Conteúdo deve ter pelo menos 10 caracteres.");
    }
}