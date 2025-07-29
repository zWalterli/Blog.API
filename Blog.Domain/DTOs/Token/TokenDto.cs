namespace Blog.Domain.DTOs.Token;

public class TokenDto
{
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }

    public TokenDto(string accessToken, DateTime expiration)
    {
        AccessToken = accessToken;
        Expiration = expiration;
    }
}