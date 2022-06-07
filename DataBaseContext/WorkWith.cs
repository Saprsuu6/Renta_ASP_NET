using DataBaseContext.EF;
using DataBaseContext.Interfaces;
using DataBaseContext.Models;
using DataBaseContext.Models.Client;
using DataBaseContext.Models.Contact;
using DataBaseContext.Models.Good;
using DataBaseContext.Repositories;

namespace DataBaseContext
{
    public class WorkWith : IUnitOfWork
    {
        private Context DataBase { get; }
        private ContactRepository contatcRepository;
        private ClientRepository clientRepository;
        private GoodRepository goodRepository;
        private OrderRepository orderRepository;

        public WorkWith()
        {
            DataBase = new Context();
        }

        public IRepository<Contact> Contacts
        {
            get
            {
                if (contatcRepository == null)
                    contatcRepository = new ContactRepository(DataBase);
                return contatcRepository;
            }
        }

        public IRepository<Client> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepository(DataBase);
                return clientRepository;
            }
        }

        public IRepository<Good> Goods
        {
            get
            {
                if (goodRepository == null)
                    goodRepository = new GoodRepository(DataBase);
                return goodRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(DataBase);
                return orderRepository;
            }
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
