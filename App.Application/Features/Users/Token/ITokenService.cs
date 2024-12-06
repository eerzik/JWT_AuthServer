using App.Application.Features.Users.Authentication.Dtos;
using App.Domain.Entities.Identity;
using App.Persistence.Authentication.Configurations;

namespace App.Application.Features.Users.Token;

public interface ITokenService
{
    TokenDto CreateToken(UserApp userApp);

    ClientTokenDto CreateTokenByClient(Client client);
}
