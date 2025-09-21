namespace OperativeService.Handlers.Commands.Play
{
    using System.Threading;

    using MediatR;

    using DiceRollingMicroservices.Common.Models.Response;
    using OperativeService.Data.Contracts;
    using OperativeService.Data.Models;
    using OperativeService.Handlers.Commands.Response;
    using OperativeService.Handlers.Commands.Rounds;
    using OperativeService.Handlers.Queries.Games;
    using OperativeService.Handlers.Queries.Rounds;
    using OperativeService.Handlers.Commands.Play.Strategies.RollDice;
    using OperativeService.Handlers.Commands.Play.Strategies.PlayRound;

    public class RollDiceCommandHandler : IRequestHandler<RollDiceCommand, RollDiceResponse>
    {
        private readonly IMediator mediator;
        private readonly IDiceRollerStrategy diceRollerStrategy;
        private readonly IEnumerable<IPlayRoundStrategy> playRoundStrategies;

        public RollDiceCommandHandler(IMediator mediator, IDiceRollerStrategy diceRollerStrategy, IEnumerable<IPlayRoundStrategy> playRoundStrategies)
        {
            this.mediator = mediator;
            this.diceRollerStrategy = diceRollerStrategy;
            this.playRoundStrategies = playRoundStrategies;
        }

        public async Task<RollDiceResponse> Handle(RollDiceCommand request, CancellationToken cancellationToken)
        {
            var query = new GetGamesQuery() { GameId = request.GameId };
            IEnumerable<Game> games = await mediator.Send(query);
            Game game = games.FirstOrDefault();

            if (game == null)
            {
                return new RollDiceResponse("Could not find the game!");
            }

            if (!game.UserIds.Contains(request.UserId))
            {
                return new RollDiceResponse("The payer has not joined the game!");
            }

            IEnumerable<Round> rounds = await mediator.Send(new GetRoundsQuery() { GameId = game.Id });

            if (rounds.Count(x => x.Results.Any(y => y.UserId == request.UserId)) >= game.MaxRounds)
            {
                return new RollDiceResponse("No more rounds to play!");
            }

            int[] results = RollDice(game.DicePerUser, game.DieType);
            
            Round notPlayedRound = rounds.FirstOrDefault(x => !x.Results.Select(y => y.UserId).Contains(request.UserId));

            var strategy = playRoundStrategies.First(x => x.CanHandle(rounds, request.UserId));
            var (updated, roundNumber) = await strategy.Play(game, rounds, request.UserId, results, cancellationToken);

            if (updated)
            {
                return new RollDiceResponse() { RoundNumber = roundNumber, DiceRolls = results };
            }

            return new RollDiceResponse("Cound not play a round!");
        }


        private int[] RollDice(int dicePerUser, DieType dieType)
        {
            int[] results = new int[dicePerUser];

            for (int i = 0; i < dicePerUser; i++)
            {
                results[i] = diceRollerStrategy.Roll(dieType);
            }

            return results;
        }
    }
}
