namespace OperativeService.Handlers.Queries.Users.SorterDecorators.Games
{
    using DiceRollingMicroservices.Common.Models.Request;
    using OperativeService.Handlers.Queries.Response;

    public class DiceSumSorter : IGameSorter
    {
        public IOrderedEnumerable<GameResponse> Sort(IEnumerable<GameResponse> games, IEnumerable<SortOptions> sortBy, bool desc)
        {
            if (sortBy.Contains(SortOptions.SumOfDice))
            {
                Func<GameResponse, int> filter = x => x.Rounds.SelectMany(y => y.DiceRolls).Sum();

                if (desc)
                {
                    return games.OrderByDescending(filter);
                }

                return games.OrderBy(filter);
            }

            return games.OrderBy(x => 0);
        }
    }
}
