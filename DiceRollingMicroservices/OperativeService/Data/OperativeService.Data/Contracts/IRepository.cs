namespace OperativeService.Data.Contracts
{
    using System.Linq.Expressions;

    using OperativeService.Data.Models;

    public interface IRepository<TEntity>
       where TEntity : BaseModel
    {
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);

        Task AddAsync(TEntity item, CancellationToken cancellationToken);

        Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
