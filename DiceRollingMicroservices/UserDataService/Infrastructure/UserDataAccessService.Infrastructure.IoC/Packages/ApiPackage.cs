namespace UserDataAccessService.Infrastructure.IoC.Packages
{
    using System.Collections.Generic;

    using AutoMapper;
    
    using MediatR;
    
    using Microsoft.Extensions.DependencyInjection;

    using DiceRollingMicroservices.Common.Models.IoC;
    using UserDataAccessService.Handlers.Commands.Login;
    using UserDataAccessService.Handlers.Queries.Token;
    using UserDataAccessService.Infrastructure.Mapping;

    public sealed class ApiPackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfiles(new List<Profile>() { new ApiProfile(), new HandlersProfile() }));
            services.AddMediatR(typeof(GetTokenQuery).Assembly, typeof(LoginUserCommand).Assembly);
        }
    }
}
