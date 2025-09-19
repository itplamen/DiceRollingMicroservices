namespace OperativeService.Handlers.Queries.Games
{
    using System.Linq.Expressions;

    using MediatR;
    
    using OperativeService.Data.Contracts;
    using OperativeService.Data.Models;
    using OperativeService.Infrastructure.Utils.Extensions;

    public class GetAvailableGamesQueryHandler : IRequestHandler<GetAvailableGamesQuery, IEnumerable<Game>>
    {
        private readonly IRepository<Game> repository;

        public GetAvailableGamesQueryHandler(IRepository<Game> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Game>> Handle(GetAvailableGamesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Game, bool>> filter = PredicateBuilder.True<Game>();

            if (!string.IsNullOrEmpty(request.GameId))
            {
                filter = filter.And(x => x.Id == request.GameId);
            }

            if (!string.IsNullOrEmpty(request.UserId))
            {
                filter = filter.And(x => !x.UserIds.Contains(request.UserId));
            }

            filter = filter.And(x => x.MaxUsers < x.UserIds.Count && x.DeletedOn == null);

            IEnumerable<Game> games = await repository.FindAsync(filter, cancellationToken);

            return games;
        }
    }
}
