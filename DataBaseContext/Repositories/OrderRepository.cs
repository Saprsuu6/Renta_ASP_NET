using DataBaseContext.EF;
using DataBaseContext.Interfaces;
using DataBaseContext.Models;
using DataBaseContext.Models.Client;
using DataBaseContext.Models.Good;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private Context DB;

        public OrderRepository(Context context)
        {
            DB = context;
        }

        public void Create(Order item)
        {
            Client client = DB.Clients.ToList().Find(x => x.Id == item.Client.Id);
            item.Client = client;

            Good good = DB.Goods.ToList().Find(x => x.Id == item.Good.Id);
            good.Status = "Sale";
            item.Good = good;

            DB.Orders.Add(item);
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            Order concreteOrder = DB.Orders.ToList().
                Find(item => item.Id == id);

            if (concreteOrder != null)
            {
                DB.Orders.Remove(concreteOrder);
                DB.SaveChanges();
            }
            else
                throw new ApplicationException("Order has not found");
        }

        public Order Read(int id)
        {
            Order concreteOrder = DB.Orders.ToList().
                Find(item => item.Id == id);

            concreteOrder.Client = DB.Clients.ToList().Find(x=>x.Id == concreteOrder.Client.Id);
            concreteOrder.Good = DB.Goods.ToList().Find(x => x.Id == concreteOrder.Good.Id);

            concreteOrder.Good.Apartment = DB.Apartments.ToList()
                .Find(x => x.GoodId == concreteOrder.Good.Id);
            concreteOrder.Good.Rieltor = DB.Clients.ToList()
                .Find(x => x.Id == concreteOrder.Good.Rieltor.Id);

            return concreteOrder;
        }

        public IEnumerable<Order> ReadAll()
        {
            IEnumerable<Order> orders = DB.Orders.ToList();

            foreach (Order order in orders)
            {
                order.Client = DB.Clients.ToList().Find(x => x.Id == order.Client.Id);
                order.Good = DB.Goods.ToList().Find(x => x.Id == order.Good.Id);

                order.Good.Apartment = DB.Apartments.ToList()
                    .Find(x => x.GoodId == order.Good.Id);
                order.Good.Rieltor = DB.Clients.ToList()
                    .Find(x => x.Id == order.Good.Rieltor.Id);
            }

            return orders;
        }

        public void Update(Order item)
        {
            var concreteOrder = Read(item.Id);

            if (concreteOrder != null)
            {
                concreteOrder.Good = item.Good;
                concreteOrder.Client = item.Client;
                concreteOrder.Date = item.Date;

                DB.Orders.Update(concreteOrder);
                DB.SaveChanges();
            }
            else
                throw new ApplicationException("Order has not found");
        }
    }
}
