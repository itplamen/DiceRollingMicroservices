namespace OperativeService.Handlers.Commands.Rounds
{
    using MediatR;

    using DiceRollingMicroservices.Common.Models.Response;
    using OperativeService.Data.Models;

    public class CreateRoundCommand : IRequest<EntityResponse>
    {
        public int RoundNumber { get; set; }

        public string GameId { get; set; }

        public RollResult RollResult { get; set; }
    }
}
