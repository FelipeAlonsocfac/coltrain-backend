using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ColTrain.Shared.Contracts.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        Task Add(T entity);
        Task AddRange(List<T> entity);
        Task DeleteRange(List<T> entity);
        Task Update(T entity);
        Task UpdateRange(List<T> entity);
        Task Delete(T entity);
        IQueryable<T> GetAll();
        T GetSingle(Expression<Func<T, bool>> predicate);
        Task<T> GetFirst(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<T> MapperUpdate(T receiver, T sender);
    }
}
