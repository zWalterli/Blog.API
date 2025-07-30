using Blog.Domain.DTOs.Post;
using Blog.Domain.Util;
using FluentValidation;

namespace Blog.Domain.Validators;

public class PostFilterDtoValidator : AbstractValidator<PostFilterDto>
{
    public PostFilterDtoValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page deve ser maior que zero.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, Constants.MaxPageSize).WithMessage("PageSize deve estar entre 1 e 100.");
    }
}