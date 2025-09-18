namespace UserDataAccessService.Data.Contracts
{
    using UserDataAccessService.Data.Models.Contracts;

    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> Filter();

        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task AddAsync(TEntity item, CancellationToken cancellationToken);

        void Update(TEntity item);

        void Delete(TEntity item);

        void HardDelete(TEntity item);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
