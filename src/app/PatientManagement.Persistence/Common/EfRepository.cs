using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientManagement.Domain.Common;

namespace PatientManagement.Persistence.Common
{
    internal class EfRepository<TContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TContext : DbContext
    {
        private readonly TContext _dbContext;

        public EfRepository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected TContext Context => _dbContext;

        public IUnitOfWork UnitOfWork => (IUnitOfWork)_dbContext;

        public virtual async Task<TEntity?> FindAsync(TKey id) => 
            await Context.Set<TEntity>().AsQueryable()
                .FirstOrDefaultAsync(e => e.Id!.Equals(id))
                ?? Context.ChangeTracker
                    .Entries<TEntity>()
                    .Where(x => x.State == EntityState.Added)
                    .FirstOrDefault(t => t.Entity.Id!.Equals(id))
                    ?.Entity;

        public Task<List<TEntity>> GetListAsync() => Context.Set<TEntity>().AsQueryable().ToListAsync();

        public Task<IQueryable<TEntity>> GetQueryableAsync() => Task.FromResult(GetQueryable());

        public IQueryable<TEntity> GetQueryable() => Context.Set<TEntity>().AsQueryable().AsNoTracking();

        public Task DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.Attach(entity);

            return Task.FromResult(Context.Update(entity).Entity);

        }

        public async Task<TEntity> AddAsync(TEntity entity) => (await Context.Set<TEntity>().AddAsync(entity)).Entity;
    }
}
