namespace OperativeService.Handlers.Queries.Users
{
    using MediatR;

    using OperativeService.Handlers.Queries.Response;

    public class GetUserProfileQuery : FilterQuery, IRequest<ProfileResponse>
    {
        public int ExternalId { get; set; }
    }
}
