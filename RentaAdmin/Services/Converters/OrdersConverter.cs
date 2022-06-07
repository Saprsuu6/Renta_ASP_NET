using RentaAdmin.Models;
using RentaAdmin.Models.Client;
using RentaAdmin.Models.Good;
using RentaAdmin.Services.AutoMapper;
using System.Collections.Generic;

namespace RentaAppication.Services.Converters
{
    public class OrdersConverter
    {
        public Order ConvertOrederFrom(RentaBusinesLogic.Models.Order order,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment)
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
                Status = order.Good.Status,
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
            IEnumerable<RentaBusinesLogic.Models.Order> orders,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment)
        {
            IEnumerable<Order> convertedOredersList = new List<Order>();

            foreach (var item in orders)
            {
                (convertedOredersList as List<Order>)
                    .Add(ConvertOrederFrom(item, mapAppartment));
            }

            return convertedOredersList;
        }

        public RentaBusinesLogic.Models.Order ConvertOrederTo(Order order,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment)
        {
            RentaBusinesLogic.Models.Client.Client convertedClient
                = new RentaBusinesLogic.Models.Client.Client()
                {
                    Id = order.Client.Id,
                    Firstname = order.Client.Firstname,
                    Lastname = order.Client.Lastname,
                    Phone = order.Client.Phone,
                    Password = order.Client.Password,
                    Email = order.Client.Email,
                };

            RentaBusinesLogic.Models.Good.Good convertedGood
                = new RentaBusinesLogic.Models.Good.Good()
                {
                    Id = order.Good.Id,
                    DateOfAdd = order.Good.DateOfAdd,
                    DateOfUpdate = order.Good.DateOfUpdate,
                    Watchings = order.Good.Watchings,
                    Link = order.Good.Link,
                    Describe = order.Good.Describe,
                    Status = order.Good.Status,
                    Apartment = mapAppartment.ToResource
                    .Map<RentaBusinesLogic.Models.Good.Apartment>(order.Good.Apartment)
                };

            RentaBusinesLogic.Models.Order convertedOreder = new RentaBusinesLogic.Models.Order()
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
