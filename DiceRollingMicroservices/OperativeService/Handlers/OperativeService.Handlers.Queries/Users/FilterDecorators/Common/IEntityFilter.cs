namespace OperativeService.Handlers.Queries.Users.FilterDecorators.Common
{
    using System.Linq.Expressions;

    using OperativeService.Data.Models;

    public interface IEntityFilter<TEntity>
        where TEntity : BaseModel
    {
        Expression<Func<TEntity, bool>> Filter(FilterQuery request);
    }
}
