namespace OperativeService.Handlers.Commands.Rounds
{
    using MediatR;
    
    using OperativeService.Data.Models;
    using OperativeService.Handlers.Commands.Response;

    public class CreateRoundCommand : IRequest<EntityResponse>
    {
        public int RoundNumber { get; set; }

        public string GameId { get; set; }

        public RollResult RollResult { get; set; }
    }
}
