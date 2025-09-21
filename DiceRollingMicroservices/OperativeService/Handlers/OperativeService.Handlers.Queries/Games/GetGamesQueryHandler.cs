namespace OperativeService.Handlers.Queries.Games
{
    using System.Linq.Expressions;
   
    using MediatR;

    using DiceRollingMicroservices.Common.Utils.Extensions;
    using OperativeService.Data.Contracts;
    using OperativeService.Data.Models;

    public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IEnumerable<Game>>
    {
        private readonly IRepository<Game> repository;

        public GetGamesQueryHandler(IRepository<Game> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Game>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Game, bool>> filter = PredicateBuilder.True<Game>();

            if (!string.IsNullOrEmpty(request.GameId))
            {
                filter = filter.And(x => x.Id == request.GameId);
            }

            if (!string.IsNullOrEmpty(request.UserId))
            {
                filter = filter.And(x => x.UserIds.Contains(request.UserId));
            }

            IEnumerable<Game> games = await repository.FindAsync(filter, cancellationToken);

            return games;
        }
    }
}
