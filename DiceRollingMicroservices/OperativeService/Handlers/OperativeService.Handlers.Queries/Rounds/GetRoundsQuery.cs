namespace OperativeService.Handlers.Queries.Rounds
{
    using MediatR;
    
    using OperativeService.Data.Models;

    public class GetRoundsQuery : IRequest<IEnumerable<Round>>
    {
        public string GameId { get; set; }
    }
}
