using DataBaseContext.EF;
using DataBaseContext.Interfaces;
using DataBaseContext.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBaseContext.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private Context DB;

        public ClientRepository(Context context)
        {
            DB = context;
        }

        public void Create(Client item)
        {
            if (item.Phone == "" && item.Email == "")
            {
                item.Phone = null;
                item.Email = null;
            }

            DB.Clients.Add(item);
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            Client concreteClient = DB.Clients.ToList().
                Find(item => item.Id == id);

            if (concreteClient != null)
            {
                DB.Clients.Remove(concreteClient);
                DB.SaveChanges();
            }
            else
                throw new ApplicationException("Client has not found");
        }

        public Client Read(int id)
        {
            Client client = DB.Clients.Find(id);

            try
            {
                client.Payments = DB.Payments.ToList().FindAll(x => x.Client.Id == id);
            }
            catch (Exception) { }

            try
            {
                client.Goods = DB.Goods.ToList().FindAll(x => x.Rieltor.Id == id);

                if (client.Goods.Any())
                {
                    foreach (var good in client.Goods)
                    {
                        good.Apartment = DB.Apartments.FirstOrDefault(x => x.GoodId == good.Id);
                    }
                }
            }
            catch (Exception) { }

            return client;
        }

        public IEnumerable<Client> ReadAll()
        {
            IEnumerable<Client> clients = DB.Clients.ToList();

            foreach (Client client in clients)
            {
                try
                {
                    client.Payments = DB.Payments.ToList().FindAll(x => x.Client.Id == client.Id);
                }
                catch (Exception) { }

                try
                {
                    client.Goods = DB.Goods.ToList().FindAll(x => x.Rieltor.Id == client.Id);

                    if (client.Goods.Any())
                    {
                        foreach (var good in client.Goods)
                        {
                            good.Apartment = DB.Apartments.FirstOrDefault(x => x.GoodId == good.Id);
                        }
                    }
                }
                catch (Exception) { }
            }

            return clients;
        }

        public void Update(Client item)
        {
            var concreteClient = Read(item.Id);

            if (concreteClient != null)
            {
                concreteClient.Firstname = item.Firstname;
                concreteClient.Lastname = item.Lastname;
                concreteClient.Phone = item.Phone;
                concreteClient.Password = item.Password;
                concreteClient.Email = item.Email;
                concreteClient.Payments = item.Payments;
                concreteClient.Goods = item.Goods;

                DB.Clients.Update(concreteClient);
                DB.SaveChanges();
            }
            else
                throw new ApplicationException("Client has not found");
        }
    }
}
