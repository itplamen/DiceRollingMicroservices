namespace OperativeService.Infrastructure.IoC.Packages
{
    using MediatR;
    
    using Microsoft.Extensions.DependencyInjection;

    using DiceRollingMicroservices.Common.Models.IoC;
    using DiceRollingMicroservices.Common.Models.Response;
    using OperativeService.Handlers.Commands.Common;
    using OperativeService.Handlers.Commands.Games;
    using OperativeService.Handlers.Commands.Play;
    using OperativeService.Handlers.Commands.Response;

    public sealed class HandlersPackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRequestHandler<,>), typeof(CreateEntityCommandHandler<,>));
            services.AddScoped<IRequestHandler<GameCommand, BaseResponse>, JoinGameCommandHandler>();
            services.AddScoped<IRequestHandler<GameCommand, RollDiceResponse>, RollDiceCommandHandler>();
        }
    }
}
