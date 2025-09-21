namespace OperativeService.Handlers.Commands.Common
{
    using MediatR;

    using DiceRollingMicroservices.Common.Models.Response;

    public class JoinGameCommand : IRequest<BaseResponse>
    {
        public string UserId { get; set; }

        public string GameId { get; set; }
    }
}
