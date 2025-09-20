namespace OperativeService.Infrastructure.IoC.Packages
{
    using DiceRollingMicroservices.Common.Models.IoC;
    using DiceRollingMicroservices.Common.Models.Response;
   
    using MediatR;
    
    using Microsoft.Extensions.DependencyInjection;

    using OperativeService.Data.Models;
    using OperativeService.Handlers.Commands.Common;
    using OperativeService.Handlers.Commands.Games;
    using OperativeService.Handlers.Commands.Play;
    using OperativeService.Handlers.Commands.Play.Strategies;
    using OperativeService.Handlers.Commands.Response;
    using OperativeService.Handlers.Commands.Rounds;

    public sealed class HandlersPackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateGameCommand, EntityResponse>, CreateEntityCommandHandler<CreateGameCommand, Game>>();
            services.AddScoped<IRequestHandler<CreateRoundCommand, EntityResponse>, CreateEntityCommandHandler<CreateRoundCommand, Round>>();
            services.AddScoped<IRequestHandler<GameCommand, BaseResponse>, JoinGameCommandHandler>();
            services.AddScoped<IRequestHandler<GameCommand, RollDiceResponse>, RollDiceCommandHandler>();
            services.AddScoped<IDiceRollerStrategy, SecureDiceRollerStrategy>();
        }
    }
}
