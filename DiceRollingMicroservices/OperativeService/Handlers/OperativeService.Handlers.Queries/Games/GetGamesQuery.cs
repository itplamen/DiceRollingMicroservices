namespace OperativeService.Handlers.Queries.Games
{
    using MediatR;

    using OperativeService.Data.Models;

    public class GetGamesQuery : IRequest<IEnumerable<Game>>
    {
        public string UserId { get; set; }

        public string GameId { get; set; }
    }
}
