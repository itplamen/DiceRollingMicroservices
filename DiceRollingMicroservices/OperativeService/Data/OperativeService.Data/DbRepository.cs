namespace OperativeService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using MongoDB.Driver;
    
    using OperativeService.Data.Contracts;
    using OperativeService.Data.Models;
    
    public class DbRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseModel
    {
        private readonly OperativeServiceDbContext dbContext;

        public DbRepository(OperativeServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await dbContext.GetCollection<TEntity>()
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
        {
            return await dbContext.GetCollection<TEntity>()
                .Find(filter)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(TEntity item, CancellationToken cancellationToken)
        {
            item.CreatedOn = DateTime.UtcNow;

            await dbContext.GetCollection<TEntity>()
                .InsertOneAsync(item, cancellationToken: cancellationToken);
        }

        public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            entity.ModifiedOn = DateTime.UtcNow;

            ReplaceOneResult result = await dbContext.GetCollection<TEntity>()
                .ReplaceOneAsync(
                    x => x.Id == entity.Id,
                    entity,                 
                    new ReplaceOptions { IsUpsert = false },
                    cancellationToken);

            return result.ModifiedCount > 0;
        }
    }
}
