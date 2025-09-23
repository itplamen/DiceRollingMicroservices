namespace OperativeService.Handlers.Queries.Users.SorterDecorators.Games
{
    using DiceRollingMicroservices.Common.Models.Request;
    using OperativeService.Handlers.Queries.Response;

    public class DiceSumSorter : IGameSorter
    {
        public IOrderedEnumerable<GameResponse> Sort(IEnumerable<GameResponse> games, IEnumerable<SortOptions> sortBy, bool desc)
        {
            foreach (var game in games)
            {
                if (sortBy.Contains(SortOptions.SumOfDice) && game.Rounds != null)
                {
                    game.Rounds = desc
                        ? game.Rounds.OrderByDescending(x => x.Total).ToList()
                        : game.Rounds.OrderBy(x => x.Total).ToList();
                }
            }

            return games.OrderBy(g => g.Id);
        }
    }
}
