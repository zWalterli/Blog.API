using System.ComponentModel;
using Blog.Domain.Util;

namespace Blog.Domain.DTOs.Post;

public class PostFilterDto
{
    [DefaultValue(1)]
    public int Page { get; set; } = 1;
    [DefaultValue(10)]
    public int PageSize { get; set; } = 10;

    public void ValidateMaxPageSize()
    {
        if (PageSize > Constants.MaxPageSize)
        {
            PageSize = Constants.MaxPageSize;
        }
    }
}