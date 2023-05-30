using System.Linq.Expressions;

namespace ManageCollections.Application.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> expression, params string[] includes);
        Task<T?> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid Id);
    }
}
