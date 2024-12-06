using App.Application.Features.Users.Authentication.Dtos;

namespace App.Application.Features.Users.Authentication;

public interface IAuthenticationService
{
    Task<ServiceResult<TokenDto>> CreateTokenAsync(LoginDto loginDto);

    Task<ServiceResult<TokenDto>> CreateTokenByRefreshToken(string refreshToken);

    Task<ServiceResult> RevokeRefreshToken(string refreshToken);

    ServiceResult<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);
}
