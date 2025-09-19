namespace OperativeService.Handlers.Commands.Games
{
    using MediatR;

    using OperativeService.Data.Models;
    using OperativeService.Handlers.Commands.Response;

    public class CreateGameCommand : IRequest<EntityResponse>
    {
        public string Name { get; set; }

        public DieType DieType { get; set; }

        public int MaxUsers { get; set; }

        public int MaxRounds { get; set; }

        public int DicePerUser { get; set; }

        public string UserId { get; set; }
    }
}
