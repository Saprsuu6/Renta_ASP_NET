using DataBaseContext;
using RentaBusinesLogic.Interfaces;
using RentaBusinesLogic.Models;
using RentaBusinesLogic.Models.Client;
using RentaBusinesLogic.Models.Good;
using RentaBusinesLogic.Services.AutoMapper;
using RentaBusinesLogic.Services.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentaBusinesLogic.WorkWithDB
{
    public class OrdersDB : IRepository<Order>
    {
        readonly OrdersConverter ordersConverters;
        readonly Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapAppartment;

        public OrdersDB()
        {
            mapAppartment = new Mapper<Apartment, DataBaseContext.Models.Good.Apartment>();
            ordersConverters = new OrdersConverter();
        }

        public async Task<Order> Read(int id)
        {
            using (WorkWith oreders = new WorkWith())
            {
                DataBaseContext.Models.Order order =
                    await Task.Run(() => oreders.Orders.Read(id));

                Order convertedOrder = ordersConverters
                    .ConvertOrederFrom(order, mapAppartment);

                return convertedOrder;
            }
        }

        public async Task<IEnumerable<Order>> ReadAll()
        {
            using (WorkWith oreders = new WorkWith())
            {
                IEnumerable<DataBaseContext.Models.Order> orders =
                    await Task.Run(() => oreders.Orders.ReadAll());

                IEnumerable<Order> convertedOrders = ordersConverters
                    .ConvertOrdersFrom(orders, mapAppartment);

                return convertedOrders;
            }
        }


        public async Task<Order> Create(Order item)
        {
            using (WorkWith oreders = new WorkWith())
            {
                DataBaseContext.Models.Order convertedOrder =
                    ordersConverters.ConvertOrederTo(item, mapAppartment);

                await Task.Run(() => oreders.Orders.Create(convertedOrder));

                Order convertedCorrectOreder =
                    ordersConverters.ConvertOrederFrom(convertedOrder, mapAppartment);

                return item;
            }
        }

        public async Task<Order> Update(Order item)
        {
            using (WorkWith oreders = new WorkWith())
            {
                DataBaseContext.Models.Order convertedOrder =
                    ordersConverters.ConvertOrederTo(item, mapAppartment);

                await Task.Run(() => oreders.Orders.Update(convertedOrder));

                Order convertedCorrectOreder =
                    ordersConverters.ConvertOrederFrom(convertedOrder, mapAppartment);

                return item;
            }
        }

        public async Task Delete(int id)
        {
            using (WorkWith oreders = new WorkWith())
            {
                await Task.Run(() => oreders.Orders.Delete(id));
            }
        }
    }
}
