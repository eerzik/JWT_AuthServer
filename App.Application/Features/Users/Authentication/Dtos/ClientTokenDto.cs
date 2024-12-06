namespace App.Application.Features.Users.Authentication.Dtos;

public class ClientTokenDto
{
    public string? AccessToken { get; set; }

    public DateTime AccessTokenExpiration { get; set; }
}
