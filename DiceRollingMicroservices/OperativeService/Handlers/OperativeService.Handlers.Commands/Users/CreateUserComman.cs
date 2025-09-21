namespace OperativeService.Handlers.Commands.Users
{
    using MediatR;

    using DiceRollingMicroservices.Common.Models.Response;

    public class CreateUserComman : IRequest<EntityResponse>
    {
        public int ExternalId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }
    }
}
