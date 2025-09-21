namespace OperativeService.Handlers.Commands.Play
{
    using AutoMapper;
   
    using MediatR;

    using DiceRollingMicroservices.Common.Models.Response;
    using OperativeService.Data.Contracts;
    using OperativeService.Data.Models;
    using OperativeService.Handlers.Commands.Play.Strategies;
    using OperativeService.Handlers.Commands.Response;
    using OperativeService.Handlers.Commands.Rounds;
    using OperativeService.Handlers.Queries.Games;
    using OperativeService.Handlers.Queries.Rounds;

    public class RollDiceCommandHandler : IRequestHandler<RollDiceCommand, RollDiceResponse>
    {
        private const int ROUND_START = 1;

        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IRepository<Game> repository;
        private readonly IDiceRollerStrategy diceRollerStrategy;

        public RollDiceCommandHandler(IMapper mapper, IMediator mediator, IRepository<Game> repository, IDiceRollerStrategy diceRollerStrategy)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.repository = repository;
            this.diceRollerStrategy = diceRollerStrategy;
        }

        public async Task<RollDiceResponse> Handle(RollDiceCommand request, CancellationToken cancellationToken)
        {
            var query = mapper.Map<GetAvailableGamesQuery>(request);
            IEnumerable<Game> games = await mediator.Send(query);
            Game game = games.FirstOrDefault();

            if (game == null)
            {
                return new RollDiceResponse("Could not find the game!");
            }

            if (game.RoundIds.Count >= game.MaxRounds)
            {
                return new RollDiceResponse("No more rounds to play!");
            }

            int[] results = RollDice(game.DicePerUser, game.DieType);
            IEnumerable<Round> rounds = await mediator.Send(new GetRoundsQuery() { GameId = game.Id });

            var roundCommand = new CreateRoundCommand()
            {
                GameId = game.Id,
                RoundNumber = rounds.LastOrDefault()?.RoundNumber + ROUND_START ?? ROUND_START,
                RollResult = new RollResult() { UserId = request.UserId, DiceRolls = results.ToList() }
            };

            EntityResponse createdRound = await mediator.Send(roundCommand);
            game.RoundIds.Add(createdRound.Id);

            bool updated = await repository.UpdateAsync(game, cancellationToken);

            if (updated)
            {
                return new RollDiceResponse() { RoundNumber = roundCommand.RoundNumber, DiceRolls = results };
            }

            return new RollDiceResponse("Cound not play a round!");
        }

        private int[] RollDice(int dicePerUser, DieType dieType)
        {
            int[] results = new int[dicePerUser];

            for (int i = 1; i <= dicePerUser; i++)
            {
                results[i] = diceRollerStrategy.Roll(dieType);
            }

            return results;
        }
    }
}
