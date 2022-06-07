using DataBaseContext.Models;
using DataBaseContext.Models.Client;
using DataBaseContext.Models.Contact;
using DataBaseContext.Models.Good;
using System;

namespace DataBaseContext.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Contact> Contacts { get; }
        IRepository<Client> Clients { get; }
        IRepository<Good> Goods { get; }
        IRepository<Order> Orders { get; }
    }
}
