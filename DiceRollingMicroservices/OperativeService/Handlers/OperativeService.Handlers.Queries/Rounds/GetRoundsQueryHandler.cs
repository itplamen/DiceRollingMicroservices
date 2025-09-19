namespace OperativeService.Handlers.Queries.Rounds
{
    using MediatR;
    
    using OperativeService.Data.Contracts;
    using OperativeService.Data.Models;

    public class GetRoundsQueryHandler : IRequestHandler<GetRoundsQuery, IEnumerable<Round>>
    {
        private readonly IRepository<Round> repository;

        public GetRoundsQueryHandler(IRepository<Round> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Round>> Handle(GetRoundsQuery request, CancellationToken cancellationToken) =>
            await repository.FindAsync(x => x.GameId == request.GameId && x.DeletedOn == null, cancellationToken);
    }
}
