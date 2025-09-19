namespace OperativeService.Handlers.Commands.Games
{
    using AutoMapper;
    
    using MediatR;

    using DiceRollingMicroservices.Common.Models.Response;
    using OperativeService.Data.Contracts;
    using OperativeService.Data.Models;
    using OperativeService.Handlers.Commands.Common;
    using OperativeService.Handlers.Queries.Games;

    public class JoinGameCommandHandler : IRequestHandler<GameCommand, BaseResponse>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IRepository<Game> repository;

        public JoinGameCommandHandler(IMapper mapper, IMediator mediator, IRepository<Game> repository)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.repository = repository;
        }

        public async Task<BaseResponse> Handle(GameCommand request, CancellationToken cancellationToken)
        {
            var query = mapper.Map<GetAvailableGamesQuery>(request);
            IEnumerable<Game> games = await mediator.Send(query);
            Game game = games.FirstOrDefault();

            if (game != null)
            {
                game.UserIds.Add(request.UserId);
                bool updated = await repository.UpdateAsync(game, cancellationToken);

                if (updated)
                {
                    return new BaseResponse();
                }
            }

            return new BaseResponse("Could not join the game!");
        }
    }
}
