namespace OperativeService.Handlers.Queries.Users.SorterDecorators.Games
{
    using DiceRollingMicroservices.Common.Models.Request;
    using OperativeService.Handlers.Queries.Response;

    public class DateTimeSorter : IGameSorter
    {
        private readonly IGameSorter decoratee;

        public DateTimeSorter(IGameSorter decoratee)
        {
            this.decoratee = decoratee;
        }

        public IOrderedEnumerable<GameResponse> Sort(IEnumerable<GameResponse> games, IEnumerable<SortOptions> sortBy, bool desc)
        {
            IOrderedEnumerable<GameResponse> sortedGames = decoratee.Sort(games, sortBy, desc);

            if (sortBy.Contains(SortOptions.Datetime))
            {
                Func<GameResponse, DateTime> filter = x => x.CreatedOn;

                if (desc)
                {
                    return sortedGames.ThenByDescending(x => x.CreatedOn);
                }

                return sortedGames.ThenBy(x => x.CreatedOn);
            }

            return sortedGames;
        }
    }
}
