namespace OperativeService.Handlers.Queries.Users.FilterDecorators.Games
{
    using System.Linq.Expressions;
   
    using DiceRollingMicroservices.Common.Utils.Extensions;
    using OperativeService.Data.Models;
    using OperativeService.Handlers.Queries.Users.FilterDecorators.Common;

    public class GameByUserFilter : IEntityFilter<Game>
    {
        private readonly IEntityFilter<Game> decoratee;

        public GameByUserFilter(IEntityFilter<Game> decoratee)
        {
            this.decoratee = decoratee;
        }

        public Expression<Func<Game, bool>> Filter(FilterQuery request)
        {
            Expression<Func<Game, bool>> filter = decoratee.Filter(request);

            return filter = filter.And(x => x.UserIds.Contains(request.Id));
        }
    }
}
