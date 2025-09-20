namespace OperativeService.Infrastructure.IoC.Packages
{
    using AutoMapper;

    using MediatR;

    using Microsoft.Extensions.DependencyInjection;

    using DiceRollingMicroservices.Common.Models.IoC;
    using OperativeService.Handlers.Commands.Games;
    using OperativeService.Handlers.Queries.Games;
    using OperativeService.Infrastructure.Mapping;

    public sealed class ApiPackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfiles(new List<Profile>() { new ApiProfile(), new HandlersProfile() }));
            services.AddMediatR(typeof(GetAvailableGamesQuery).Assembly, typeof(CreateGameCommand).Assembly);
        }
    }
}
