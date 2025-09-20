namespace OperativeService.Handlers.Queries.Users
{
    using MediatR;

    using DiceRollingMicroservices.Common.Models.Response;

    public class GetUserQuery : IRequest<EntityResponse>
    {
        public int ExternalId { get; set; }
    }
}
