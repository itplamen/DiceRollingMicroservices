namespace OperativeService.Handlers.Queries.Users.FilterDecorators.Common
{
    using System.Linq.Expressions;

    using OperativeService.Data.Models;
    using OperativeService.Infrastructure.Utils.Extensions;

    public class MonthFilter<TEntity> : IEntityFilter<TEntity>
        where TEntity : BaseModel
    {
        private readonly IEntityFilter<TEntity> decoratee;

        public MonthFilter(IEntityFilter<TEntity> decoratee)
        {
            this.decoratee = decoratee;
        }

        public Expression<Func<TEntity, bool>> Filter(FilterQuery request)
        {
            Expression<Func<TEntity, bool>> filter = decoratee.Filter(request);

            if (request.Month > 0)
            {
                filter = filter.And(x => x.CreatedOn.Month == request.Month);
            }

            return filter;
        }
    }
}
