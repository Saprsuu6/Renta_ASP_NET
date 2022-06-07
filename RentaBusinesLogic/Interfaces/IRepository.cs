using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentaBusinesLogic.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ReadAll();
        Task<T> Read(int id);
        Task<T> Create(T item);
        Task<T> Update(T item);
        Task Delete(int id);
    }
}
