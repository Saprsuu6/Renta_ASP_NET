using System.Threading.Tasks;

namespace RentaBusinesLogic.Interfaces
{
    public interface IRepositoryClient<T> : IRepository<T> where T : class
    {
        Task<T> Check(T client);
    }
}
