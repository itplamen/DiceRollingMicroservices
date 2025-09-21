namespace OperativeService.Handlers.Queries.Users.SorterDecorators.Games
{
    using DiceRollingMicroservices.Common.Models.Request;
    using OperativeService.Handlers.Queries.Response;

    public interface IGameSorter
    {
        IOrderedEnumerable<GameResponse> Sort(IEnumerable<GameResponse> games, IEnumerable<SortOptions> sortBy, bool desc);
    }
}
