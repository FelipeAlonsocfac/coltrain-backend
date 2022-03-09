using ColTrain.Shared.Contracts.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ColTrain.Shared.Infrastructure.DataAccess
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public ColTrainDbContext DatabaseContext { get; set; }
        public BaseRepository(ColTrainDbContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public virtual IQueryable<T> GetAll()
        {
            var entitySet = DatabaseContext.Set<T>();
            return entitySet.AsQueryable();
        }
        public virtual T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public virtual Task<T> GetFirst(Expression<Func<T, bool>> predicate)
        {
            return GetAll().FirstOrDefaultAsync(predicate);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public async Task Add(T entity)
        {
            var UpdatedAt = entity.GetType().GetProperty("UpdatedAt");
            if (UpdatedAt != null) entity.GetType().GetProperty("UpdatedAt").SetValue(entity, DateTime.Now);

            var CreatedAt = entity.GetType().GetProperty("CreatedAt");
            if (CreatedAt != null) entity.GetType().GetProperty("CreatedAt").SetValue(entity, DateTime.Now);

            await DatabaseContext.AddAsync(entity);
            DatabaseContext.Entry(entity).State = EntityState.Added;
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task AddRange(List<T> entity)
        {
            for (int i = 0; i < entity.Count; i++)
            {
                entity[i].GetType().GetProperty("UpdatedAt").SetValue(entity[i], DateTime.Now);
                entity[i].GetType().GetProperty("CreatedAt").SetValue(entity[i], DateTime.Now);
            }
            DatabaseContext.AddRange(entity);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            DatabaseContext.Remove(entity);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task DeleteRange(List<T> entity)
        {
            DatabaseContext.RemoveRange(entity);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            var UpdatedAt = entity.GetType().GetProperty("UpdatedAt");
            if (UpdatedAt != null) entity.GetType().GetProperty("UpdatedAt").SetValue(entity, DateTime.Now);
            DatabaseContext.Update(entity);
            await DatabaseContext.SaveChangesAsync();
        }


        public async Task UpdateRange(List<T> entity)
        {
            for (int i = 0; i < entity.Count; i++)
            {
                entity[i].GetType().GetProperty("UpdatedAt").SetValue(entity[i], DateTime.Now);
            }
            DatabaseContext.UpdateRange(entity);
            await DatabaseContext.SaveChangesAsync();
        }
        public Task<T> MapperUpdate(T fromDB, T fromRequest)
        {
            // copy fields
            var typeOfSender = fromRequest.GetType();
            var typeOfReceiver = fromDB.GetType();
            foreach (var fieldOfReceiver in typeOfSender.GetFields())
            {
                var fieldOfB = typeOfReceiver.GetField(fieldOfReceiver.Name);
                fieldOfB.SetValue(fromDB, fieldOfReceiver.GetValue(fromRequest));
            }
            // copy properties
            foreach (var propertyOfReceiver in typeOfSender.GetProperties())
            {
                var propertyOfB = typeOfReceiver.GetProperty(propertyOfReceiver.Name);
                propertyOfB.SetValue(fromDB, propertyOfReceiver.GetValue(fromRequest));
            }
            return Task.FromResult(fromDB);
        }
    }
}
