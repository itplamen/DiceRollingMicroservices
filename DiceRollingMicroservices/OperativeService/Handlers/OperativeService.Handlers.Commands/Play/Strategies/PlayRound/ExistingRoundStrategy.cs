namespace OperativeService.Handlers.Commands.Play.Strategies.PlayRound
{
    using OperativeService.Data.Contracts;
    using OperativeService.Data.Models;

    public class ExistingRoundStrategy : IPlayRoundStrategy
    {
        private readonly IRepository<Round> repository;

        public ExistingRoundStrategy(IRepository<Round> repository)
        {
            this.repository = repository;
        }

        public bool CanHandle(IEnumerable<Round> rounds, string userId) =>
            rounds.Any(x => !x.Results.Select(y => y.UserId).Contains(userId));

        public async Task<(bool Updated, int RoundNumber)> Play(Game game, IEnumerable<Round> rounds, string userId, int[] results, CancellationToken cancellationToken)
        {
            Round notPlayedRound = rounds.First(x => !x.Results.Select(y => y.UserId).Contains(userId));
            notPlayedRound.Results.Add(new RollResult
            {
                UserId = userId,
                DiceRolls = results
            });

            bool updated = await repository.UpdateAsync(notPlayedRound, cancellationToken);
            return (updated, notPlayedRound.RoundNumber);
        }
    }
}
