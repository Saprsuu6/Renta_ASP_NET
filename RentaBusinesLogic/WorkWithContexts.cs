using RentaBusinesLogic.Interfaces;
using RentaBusinesLogic.Models;
using RentaBusinesLogic.Models.Client;
using RentaBusinesLogic.Models.Contact;
using RentaBusinesLogic.Models.Good;
using RentaBusinesLogic.WorkWithDB;

namespace RentaBusinesLogic
{
    public class WorkWithContexts : IUnitOfWork
    {
        private IRepositoryClient<Client> clients;
        private IRepository<Contact> contacts;
        private IRepository<Good> goods;
        private IRepository<Order> orders;

        public IRepository<Contact> Contacts
        {
            get
            {
                if (contacts == null)
                    contacts = new ContactsDB();
                return contacts;
            }
        }

        public IRepositoryClient<Client> Clients
        {
            get
            {
                if (clients == null)
                    clients = new ClientsDB();
                return clients;
            }
        }

        public IRepository<Good> Goods
        {
            get
            {
                if (goods == null)
                    goods = new GoodsDB();
                return goods;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orders == null)
                    orders = new OrdersDB();
                return orders;
            }
        }
    }
}
