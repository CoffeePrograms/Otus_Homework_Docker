using Homework4.Models;

namespace Homework4.Services.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
    }
}
