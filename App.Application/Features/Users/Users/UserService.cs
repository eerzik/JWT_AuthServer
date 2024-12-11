using App.Application.Features.Users.Users.Dtos;
using App.Domain.Entities.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace App.Application.Features.Users.Users;

public class UserService(UserManager<UserApp> userManager, IMapper mapper) : IUserService
{
    public async Task<ServiceResult<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = new UserApp { Email = createUserDto.Email, UserName = createUserDto.UserName };

        var result = await userManager.CreateAsync(user, createUserDto.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToList();

            return ServiceResult<UserAppDto>.Fail(errors, System.Net.HttpStatusCode.BadRequest);

            //return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
        }


        return ServiceResult<UserAppDto>.Success(mapper.Map<UserAppDto>(user), System.Net.HttpStatusCode.OK);
    
    }

    public async Task<ServiceResult<UserAppDto>> GetUserByNameAsync(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return ServiceResult<UserAppDto>.Fail("UserName not found", System.Net.HttpStatusCode.BadRequest);

        }
        return ServiceResult<UserAppDto>.Success(mapper.Map<UserAppDto>(user), System.Net.HttpStatusCode.OK);

    }
}
