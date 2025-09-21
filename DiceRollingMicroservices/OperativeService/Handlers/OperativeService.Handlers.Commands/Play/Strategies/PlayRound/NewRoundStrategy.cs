namespace OperativeService.Handlers.Commands.Play.Strategies.PlayRound
{
    using MediatR;
    
    using DiceRollingMicroservices.Common.Models.Response;
    using OperativeService.Data.Contracts;
    using OperativeService.Data.Models;
    using OperativeService.Handlers.Commands.Rounds;

    public class NewRoundStrategy : IPlayRoundStrategy
    {
        private const int ROUND_START = 1;

        private readonly IMediator mediator;
        private readonly IRepository<Game> repository;

        public NewRoundStrategy(IMediator mediator, IRepository<Game> repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }

        public bool CanHandle(IEnumerable<Round> rounds, string userId) => 
            rounds.All(x => x.Results.Any(x => x.UserId == userId));

        public async Task<(bool Updated, int RoundNumber)> Play(Game game, IEnumerable<Round> rounds, string userId, int[] results, CancellationToken cancellationToken)
        {
            var roundCommand = new CreateRoundCommand()
            {
                GameId = game.Id,
                RoundNumber = rounds.LastOrDefault()?.RoundNumber + ROUND_START ?? ROUND_START,
                RollResult = new RollResult { UserId = userId, DiceRolls = results.ToList() }
            };

            EntityResponse createdRound = await mediator.Send(roundCommand);
            game.RoundIds.Add(createdRound.Id);

            bool updated = await repository.UpdateAsync(game, cancellationToken);
            return (updated, roundCommand.RoundNumber);
        }
    }
}
