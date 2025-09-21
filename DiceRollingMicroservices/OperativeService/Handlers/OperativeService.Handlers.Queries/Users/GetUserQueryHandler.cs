namespace OperativeService.Handlers.Queries.Users
{
    using MediatR;

    using DiceRollingMicroservices.Common.Models.Response;
    using OperativeService.Data.Contracts;
    using OperativeService.Data.Models;

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, EntityResponse>
    {
        private readonly IRepository<User> repository;

        public GetUserQueryHandler(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public async Task<EntityResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<User> users = await repository.FindAsync(x => x.ExternalId == request.ExternalId, cancellationToken);
            User user = users.FirstOrDefault();

            if (user != null)
            {
                return new EntityResponse() { Id = user.Id };
            }

            return new EntityResponse("Cound not find user!");
        }
    }
}
