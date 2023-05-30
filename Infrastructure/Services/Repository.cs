using System.Linq.Expressions;
using ManageCollections.Application.Abstractions;
using ManageCollections.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IManageCollectionDbContext _collectionDb;
        public Repository(IManageCollectionDbContext collectionDb)
        {
            _collectionDb = collectionDb;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            Log.Information("Trying to create entity: {0}", entity);
            _collectionDb.Set<T>().Add(entity);
            await _collectionDb.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<bool> DeleteAsync(Guid Id)
        {
            var entity = _collectionDb.Set<T>().Find(Id);
            if (entity != null)
            {
                _collectionDb.Set<T>().Remove(entity);
                await _collectionDb.SaveChangesAsync();

                Log.Information("Entity with the {0} id deleted", Id);
                return true;
            }
            Log.Fatal("Entity with {id} could not found", Id);
            return false;

        }
        public virtual Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
            IQueryable<T> source = _collectionDb.Set<T>();
            if (includes is not null)
                foreach (var item in includes)
                    source = source.Include(item);

            return Task.FromResult(source.Where(expression));
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _collectionDb.Set<T>().FindAsync(id);
        }

        public virtual async Task<T?> UpdateAsync(T entity)
        {
            if (entity != null)
            {
                _collectionDb.Set<T>().Update(entity);
                await _collectionDb.SaveChangesAsync();

                Log.Information("Entity {0} updated", entity);

                return entity;
            }
            Log.Fatal("Entity {0} could not found for updating!", entity);

            return null;
        }
    }
}
