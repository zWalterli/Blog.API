using System.Security.Claims;
using Blog.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers;

public abstract class BaseController : ControllerBase
{
    protected int UserId
    {
        get
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : throw new UnauthorizedAccessException("Token inválido. Favor autenticar novamente.");
        }
    }

    protected IActionResult OkPaginatedResponse<T>(int page, int pageSize, int count, IEnumerable<T> data, string? message = null)
    {
        var response = new BasePaginatedResponse<T>
        {
            Data = data,
            Message = message ?? "Operação realizada com sucesso.",
            Page = page,
            PageSize = pageSize,
            TotalItem = count,
            Success = true
        };

        return Ok(response);
    }
    protected IActionResult OkResponse<T>(T data, string? message = null)
    {
        var response = new BaseResponse<T>
        {
            Data = data,
            Message = message,
            Success = true
        };

        return Ok(response);
    }
    protected IActionResult OkResponse()
    {
        var response = new BaseResponse<object>
        {
            Data = new { },
            Success = true
        };

        return Ok(response);
    }

    protected IActionResult CreatedResponse()
    {
        return Created(string.Empty, new BaseResponse<object>
        {
            Success = true,
            Data = new { }
        });
    }

    protected IActionResult ErrorResponse(string message, int statusCode = 400)
    {
        var response = new BaseResponse<string>
        {
            Success = false,
            Message = message,
            Data = null
        };

        return StatusCode(statusCode, response);
    }
}