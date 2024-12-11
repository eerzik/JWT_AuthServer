using App.Application.Features.Users.Authentication.Dtos;
using App.Application.Features.Users.Users.Dtos;

namespace App.Application.Features.Users.Users;

public interface IUserService
{

    Task<ServiceResult<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);

    Task<ServiceResult<UserAppDto>> GetUserByNameAsync(string userName);
}
