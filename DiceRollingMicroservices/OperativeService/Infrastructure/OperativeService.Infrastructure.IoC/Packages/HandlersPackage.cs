namespace OperativeService.Infrastructure.IoC.Packages
{  
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    using DiceRollingMicroservices.Common.Models.IoC;
    using DiceRollingMicroservices.Common.Models.Response;
    using OperativeService.Data.Models;
    using OperativeService.Handlers.Commands.Common;
    using OperativeService.Handlers.Commands.Games;
    using OperativeService.Handlers.Commands.Play;
    using OperativeService.Handlers.Commands.Play.Strategies.PlayRound;
    using OperativeService.Handlers.Commands.Play.Strategies.RollDice;
    using OperativeService.Handlers.Commands.Response;
    using OperativeService.Handlers.Commands.Rounds;
    using OperativeService.Handlers.Commands.Users;
    using OperativeService.Handlers.Queries.Games;
    using OperativeService.Handlers.Queries.Response;
    using OperativeService.Handlers.Queries.Rounds;
    using OperativeService.Handlers.Queries.Users;
    using OperativeService.Handlers.Queries.Users.FilterDecorators.Common;
    using OperativeService.Handlers.Queries.Users.FilterDecorators.Games;
    using OperativeService.Handlers.Queries.Users.SorterDecorators.Games;

    public sealed class HandlersPackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateGameCommand, EntityResponse>, CreateEntityCommandHandler<CreateGameCommand, Game>>();
            services.AddScoped<IRequestHandler<CreateRoundCommand, EntityResponse>, CreateEntityCommandHandler<CreateRoundCommand, Round>>();
            services.AddScoped<IRequestHandler<CreateUserComman, EntityResponse>, CreateEntityCommandHandler<CreateUserComman, User>>();
            services.AddScoped<IRequestHandler<JoinGameCommand, BaseResponse>, JoinGameCommandHandler>();
            services.AddScoped<IRequestHandler<RollDiceCommand, RollDiceResponse>, RollDiceCommandHandler>();
            services.AddScoped<IPlayRoundStrategy, ExistingRoundStrategy>();
            services.AddScoped<IPlayRoundStrategy, NewRoundStrategy>();
            services.AddScoped<IDiceRollerStrategy, SecureDiceRollerStrategy>();
            services.AddScoped<IRequestHandler<GetUserProfileQuery, ProfileResponse>, GetUserProfileQueryHandler>();
            services.AddScoped<IRequestHandler<GetGamesQuery, IEnumerable<Game>>, GetGamesQueryHandler>();
            services.AddScoped<IRequestHandler<GetRoundsQuery, IEnumerable<Round>>, GetRoundsQueryHandler>();
            services.AddScoped<IRequestHandler<GetUserQuery, EntityResponse>, GetUserQueryHandler>();
            services.AddScoped<YearFilter<Game>>();
            services.AddScoped(x => new MonthFilter<Game>(x.GetRequiredService<YearFilter<Game>>()));
            services.AddScoped(x => new DayFilter<Game>(x.GetRequiredService<MonthFilter<Game>>()));
            services.AddScoped(x => new GameByUserFilter(x.GetRequiredService<DayFilter<Game>>()));
            services.AddScoped<IEntityFilter<Game>>(x => x.GetRequiredService<GameByUserFilter>());
            services.AddScoped<DiceSumSorter>();
            services.AddScoped(x => new DateTimeSorter(x.GetRequiredService<DiceSumSorter>()));
            services.AddScoped<IGameSorter>(x => x.GetRequiredService<DateTimeSorter>());
        }
    }
}
