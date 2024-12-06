using App.Application.Contracts.Persistence;
using App.Application.Features.Users.Authentication.Dtos;
using App.Application.Features.Users.Token;
using App.Domain.Entities.Identity;
using App.Persistence.Authentication.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace App.Application.Features.Users.Authentication;

public class AuthenticationService(
    IOptions<List<Client>> optionsClient,
    ITokenService tokenService,
    UserManager<UserApp> userManager,
    IUnitOfWork unitOfWork,
    IGenericRepository<UserRefreshToken, int> userRefreshTokenService
    ) : IAuthenticationService
{
    public async Task<ServiceResult<TokenDto>> CreateTokenAsync(LoginDto loginDto)
    {
        if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

        var user = await userManager.FindByEmailAsync(loginDto.Email);

        if (user == null) return ServiceResult<TokenDto>.Fail("Email or Password is wrong", System.Net.HttpStatusCode.BadRequest);


        if (!await userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return ServiceResult<TokenDto>.Fail("Email or Password is wrong", System.Net.HttpStatusCode.BadRequest);
        }

        var token = tokenService.CreateToken(user);

        var userRefreshToken = await userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

        if (userRefreshToken == null)
        {
            await userRefreshTokenService.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
        }
        else
        {
            userRefreshToken.Code = token.RefreshToken;
            userRefreshToken.Expiration = token.RefreshTokenExpiration;
        }

        await unitOfWork.SaveChangeAsync();

        return ServiceResult<TokenDto>.Success(token);
    }

    public  ServiceResult<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
    {
        var client =  optionsClient.Value.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

        if (client == null)
        {
            return ServiceResult<ClientTokenDto>.Fail("ClientId or ClientSecret not found", System.Net.HttpStatusCode.NotFound);

        }

        var token =  tokenService.CreateTokenByClient(client);
        return ServiceResult<ClientTokenDto>.Success(token);

    }

    public async Task<ServiceResult<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
    {
        var existRefreshToken = await userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

        if (existRefreshToken == null)
        {

            return ServiceResult<TokenDto>.Fail("Refresh token not found", System.Net.HttpStatusCode.NotFound);
        }

        var user = await userManager.FindByIdAsync(existRefreshToken.UserId);

        if (user == null)
        {
            return ServiceResult<TokenDto>.Fail("User Id not found", System.Net.HttpStatusCode.NotFound);
        }

        var tokenDto = tokenService.CreateToken(user);

        existRefreshToken.Code = tokenDto.RefreshToken;
        existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

        await unitOfWork.SaveChangeAsync();

        return ServiceResult<TokenDto>.Success(tokenDto);
    }

    public async Task<ServiceResult> RevokeRefreshToken(string refreshToken)
    {
        var existRefreshToken = await userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
        if (existRefreshToken == null)
        {
            return ServiceResult.Fail("Refresh token not found", System.Net.HttpStatusCode.NotFound);
        }

        userRefreshTokenService.Delete(existRefreshToken);

        await unitOfWork.SaveChangeAsync();

        return ServiceResult.Success();
    }
}
