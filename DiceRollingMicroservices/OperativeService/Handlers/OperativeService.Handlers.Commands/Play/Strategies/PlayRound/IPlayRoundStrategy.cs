namespace OperativeService.Handlers.Commands.Play.Strategies.PlayRound
{
    using OperativeService.Data.Models;

    public interface IPlayRoundStrategy
    {
        bool CanHandle(IEnumerable<Round> rounds, string userId);

        Task<(bool Updated, int RoundNumber)> Play(
            Game game,
            IEnumerable<Round> rounds,
            string userId,
            int[] results,
            CancellationToken cancellationToken);
    }
}
