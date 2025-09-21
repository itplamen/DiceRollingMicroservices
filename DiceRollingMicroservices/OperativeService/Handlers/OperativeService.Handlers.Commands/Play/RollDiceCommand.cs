namespace OperativeService.Handlers.Commands.Play
{
    using MediatR;

    using OperativeService.Handlers.Commands.Response;

    public class RollDiceCommand : IRequest<RollDiceResponse>
    {
        public string UserId { get; set; }

        public string GameId { get; set; }
    }
}
