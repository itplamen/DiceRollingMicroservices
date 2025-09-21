namespace OperativeService.Handlers.Commands.Games
{
    using MediatR;

    using DiceRollingMicroservices.Common.Models.Response;
    using OperativeService.Data.Contracts;
    using OperativeService.Data.Models;
    using OperativeService.Handlers.Queries.Games;

    public class JoinGameCommandHandler : IRequestHandler<JoinGameCommand, BaseResponse>
    {
        private readonly IMediator mediator;
        private readonly IRepository<Game> repository;

        public JoinGameCommandHandler(IMediator mediator, IRepository<Game> repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }

        public async Task<BaseResponse> Handle(JoinGameCommand request, CancellationToken cancellationToken)
        {
            var query = new GetGamesQuery() { GameId = request.GameId };
            IEnumerable<Game> games = await mediator.Send(query);
            Game game = games.FirstOrDefault();

            if (game == null)
            {
                return new BaseResponse("Could not find the game!");
            }

            if (game.UserIds.Contains(request.UserId))
            {
                return new BaseResponse("The player has already joined the game!");
            }

            if (game.MaxUsers > game.UserIds.Count)
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
