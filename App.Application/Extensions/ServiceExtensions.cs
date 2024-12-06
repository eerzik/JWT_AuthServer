using App.Application.Features.Users.Authentication;
using App.Application.Features.Users.Token;
using App.Domain.Entities.Identity;
using App.Domain.Options;
using App.Persistence.Authentication.Configurations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace App.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        //.net in default olarak ürettiği hata mesajını kapattık.
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenService, TokenService>();


        //Bussiness Validation 3. yolu kullanmak için otomatik validasyonu kapatmamız gerekiyor. Bunun yerine kendimiz manuel yazacağız.
         services.AddFluentValidationAutoValidation();
        //Çalıştığı assembly i verebiliriz.
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());



        




        return services;
    }
}
