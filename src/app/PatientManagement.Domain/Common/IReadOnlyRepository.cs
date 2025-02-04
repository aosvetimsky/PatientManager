using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientManagement.Domain.Common
{
   public interface IReadOnlyRepository<TEntity, in TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task<TEntity?> FindAsync(TKey id);
        Task<List<TEntity>> GetListAsync();
        Task<IQueryable<TEntity>> GetQueryableAsync();
        IQueryable<TEntity> GetQueryable();
    }
}
