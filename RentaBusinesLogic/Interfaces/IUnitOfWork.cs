using RentaBusinesLogic.Models;
using RentaBusinesLogic.Models.Client;
using RentaBusinesLogic.Models.Contact;
using RentaBusinesLogic.Models.Good;

namespace RentaBusinesLogic.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Contact> Contacts { get; }
        IRepositoryClient<Client> Clients { get; }
        IRepository<Good> Goods { get; }
        IRepository<Order> Orders { get; }
    }
}
