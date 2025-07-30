using Blog.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Application.Filters;

public class ModelStateToBaseResponseFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(kvp => kvp.Value!.Errors.Count > 0)
                .SelectMany(kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage))
                .ToList();

            var body = new BaseResponse<object>(
                "Erro de validação",
                false)
            {
                Erros = errors.Where(e => !string.IsNullOrEmpty(e)).ToList()
            };
            context.Result = new BadRequestObjectResult(body);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}