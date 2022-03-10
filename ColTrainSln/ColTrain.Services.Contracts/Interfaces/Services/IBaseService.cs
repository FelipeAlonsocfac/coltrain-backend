using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColTrain.Services.Contracts.Interfaces.Services
{
    public interface IBaseService<T, K>
    {
        Task<T> AddAsync(K entity);
        Task<T> AddRangeAsync(List<K> entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetSingleByIdAsync(int entityId);
        Task<T> UpdateAsync(K entity, int entityId);
        Task<IEnumerable<T>> UpdateRangeAsync(List<T> entity);
        Task<T> DeleteAsync(int entityId);
        Task<T> DeleteRangeAsync(List<int> entitiesIds);
    }
}
