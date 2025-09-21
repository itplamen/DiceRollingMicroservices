namespace OperativeService.Handlers.Queries.Users.FilterDecorators.Common
{
    using System.Linq.Expressions;
    
    using DiceRollingMicroservices.Common.Utils.Extensions;
    using OperativeService.Data.Models;

    public class YearFilter<TEntity> : IEntityFilter<TEntity>
        where TEntity : BaseModel
    {
        public Expression<Func<TEntity, bool>> Filter(FilterQuery request)
        {
            Expression<Func<TEntity, bool>> filter = PredicateBuilder.True<TEntity>();

            if (request.Year > 0)
            {
                filter = filter.And(x => x.CreatedOn.Year == request.Year);
            }

            return filter;
        }
    }
}
