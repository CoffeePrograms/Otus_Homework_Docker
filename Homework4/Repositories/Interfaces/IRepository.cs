using Homework4.Models;

namespace Homework4.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(Func<T, bool> condition = null);

        Task<T?> GetByIdAsync(Guid id);

        Task<T?> AddAsync(T entity);

        Task<T?> UpdateAsync(T entity);

        Task DeleteAsync(Guid id);
    }
}
