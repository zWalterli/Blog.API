using System.Net;
using Blog.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Application.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "Ocorreu um erro inesperado. Tente novamente mais tarde.";

        // Tratar exceções específicas
        if (context.Exception is UnauthorizedAccessException)
        {
            statusCode = HttpStatusCode.Unauthorized;
            message = "Acesso não autorizado.";
        }
        else if (context.Exception is ArgumentException argEx)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = argEx.Message;
        }
        else if (context.Exception is ApplicationException appEx)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = appEx.Message;
        }

        var response = new BaseResponse<object>
        {
            Success = false,
            Erros = new List<string> { message },
            Message = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
            Data = null
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = (int)statusCode
        };

        context.ExceptionHandled = true;
    }
}