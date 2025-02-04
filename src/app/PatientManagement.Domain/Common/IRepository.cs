using System.Threading.Tasks;

namespace PatientManagement.Domain.Common
{
    public interface IRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task DeleteAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        IUnitOfWork UnitOfWork { get; }
    }
}
