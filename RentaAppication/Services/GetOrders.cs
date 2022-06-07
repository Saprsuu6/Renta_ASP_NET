using RentaAppication.Models;
using RentaAppication.Models.Good;
using RentaAppication.Services.AutoMapper;
using RentaAppication.Services.Converters;
using RentaBusinesLogic;
using RentaBusinesLogic.WorkWithDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentaAppication.Services
{
    public class GetOrders
    {
        private WorkWithContexts contexts;

        public GetOrders()
        {
            contexts = new WorkWithContexts();
        }

        public async Task<IEnumerable<Order>> ReadAll(OrdersConverter ordersConverter,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment)
        {
            IEnumerable<RentaBusinesLogic.Models.Order> ordersList 
                = await contexts.Orders.ReadAll();

            IEnumerable<Order> convertedOrdersList
                = ordersConverter.ConvertOrdersFrom(ordersList, mapAppartment);

            return convertedOrdersList;
        }

        public async Task<Order> Put(Order order, OrdersConverter ordersConverter,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment)
        {
            RentaBusinesLogic.Models.Order convertedOrder
                = ordersConverter.ConvertOrederTo(order, mapAppartment);

            await contexts.Orders.Create(convertedOrder);

            return order;
        }
    }
}
