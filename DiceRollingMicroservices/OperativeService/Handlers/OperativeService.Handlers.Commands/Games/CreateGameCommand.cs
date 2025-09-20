namespace OperativeService.Handlers.Commands.Games
{
    using MediatR;

    using DiceRollingMicroservices.Common.Models.Response;
    using OperativeService.Data.Models;
    
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
