using Renta.Services.AutoMapper;
using RentaAPI.Models;
using RentaAPI.Models.Client;
using RentaAPI.Models.Good;
using System.Collections.Generic;

namespace RentaAPI.Services.Converters
{
    public class Orders
    {
        public Order ConvertOrederFrom(DataBaseContext.Models.Order order,
            Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapAppartment)
        {
            Client convertedClient = new Client()
            {
                Id = order.Client.Id,
                Firstname = order.Client.Firstname,
                Lastname = order.Client.Lastname,
                Phone = order.Client.Phone,
                Password = order.Client.Password,
                Email = order.Client.Email,
            };

            Good convertedGood = new Good()
            {
                Id = order.Good.Id,
                DateOfAdd = order.Good.DateOfAdd,
                DateOfUpdate = order.Good.DateOfUpdate,
                Watchings = order.Good.Watchings,
                Link = order.Good.Link,
                Describe = order.Good.Describe,
                Apartment = mapAppartment.ToResource.Map<Apartment>(order.Good.Apartment)
            };

            Order convertedOreder = new Order()
            {
                Id = order.Id,
                Date = order.Date,
                Client = convertedClient,
                Good = convertedGood
            };

            return convertedOreder;
        }

        public IEnumerable<Order> ConvertOrdersFrom(
            IEnumerable<DataBaseContext.Models.Order> orders,
            Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapAppartment)
        {
            IEnumerable<Order> convertedOredersList = new List<Order>();

            foreach (var item in orders)
            {
                (convertedOredersList as List<Order>)
                    .Add(ConvertOrederFrom(item, mapAppartment));
            }

            return convertedOredersList;
        }

        public DataBaseContext.Models.Order ConvertOrederTo(Order order,
            Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapAppartment)
        {
            DataBaseContext.Models.Client.Client convertedClient
                = new DataBaseContext.Models.Client.Client()
                {
                    Id = order.Client.Id,
                    Firstname = order.Client.Firstname,
                    Lastname = order.Client.Lastname,
                    Phone = order.Client.Phone,
                    Password = order.Client.Password,
                    Email = order.Client.Email,
                };

            DataBaseContext.Models.Good.Good convertedGood
                = new DataBaseContext.Models.Good.Good()
                {
                    Id = order.Good.Id,
                    DateOfAdd = order.Good.DateOfAdd,
                    DateOfUpdate = order.Good.DateOfUpdate,
                    Watchings = order.Good.Watchings,
                    Link = order.Good.Link,
                    Describe = order.Good.Describe,
                    Apartment = mapAppartment.ToResource
                .Map<DataBaseContext.Models.Good.Apartment>(order.Good.Apartment)
                };

            DataBaseContext.Models.Order convertedOreder = new DataBaseContext.Models.Order()
            {
                Id = order.Id,
                Date = order.Date,
                Client = convertedClient,
                Good = convertedGood
            };

            return convertedOreder;
        }
    }
}
