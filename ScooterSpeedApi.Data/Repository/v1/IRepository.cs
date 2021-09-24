using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScooterSpeedApi.Data.Repository.v1
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        public IEnumerable<TEntity> GetAll();

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}