using RentaAdmin.Models.Client;
using RentaAdmin.Models.Good;
using RentaAdmin.Services.AutoMapper;
using System.Collections.Generic;

namespace RentaAdmin.Services.Converters
{
    public class ClientsConverter
    {
        public Client ConvertClientFrom(RentaBusinesLogic.Models.Client.Client client,
            Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment)
        {
            IEnumerable<Good> goods = new List<Good>();

            if (client.Goods != null)
            {
                foreach (var item in client.Goods)
                {
                    Good good = new Good()
                    {
                        Id = item.Id,
                        DateOfAdd = item.DateOfAdd,
                        DateOfUpdate = item.DateOfUpdate,
                        Watchings = item.Watchings,
                        Link = item.Link,
                        Describe = item.Describe,
                        Price = item.Price,
                        Status = item.Status,
                        Apartment = mapAppartment.ToResource.Map<Apartment>(item.Apartment)
                    };

                    (goods as List<Good>).Add(good);
                }
            }

            Client convertedClient = new Client()
            {
                Id = client.Id,
                Firstname = client.Firstname,
                Lastname = client.Lastname,
                Phone = client.Phone,
                Email = client.Email,
                Password = client.Password,
                Payments = mapPayment.ToResource.Map<IEnumerable<Payment>>(client.Payments),
                Goods = goods,
            };

            return convertedClient;
        }

        public IEnumerable<Client> ConvertClientsFrom(
            IEnumerable<RentaBusinesLogic.Models.Client.Client> clients,
            Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment)
        {
            IEnumerable<Client> convertedCliensList = new List<Client>();

            foreach (var item in clients)
            {
                (convertedCliensList as List<Client>)
                    .Add(ConvertClientFrom(item, mapPayment, mapAppartment));
            }

            return convertedCliensList;
        }

        public RentaBusinesLogic.Models.Client.Client ConvertClientTo(Client client,
            Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment)
        {
            IEnumerable<RentaBusinesLogic.Models.Good.Good> goods =
                new List<RentaBusinesLogic.Models.Good.Good>();

            if (client.Goods != null)
            {
                foreach (var item in client.Goods)
                {
                    RentaBusinesLogic.Models.Good.Good good =
                        new RentaBusinesLogic.Models.Good.Good()
                        {
                            Id = item.Id,
                            DateOfAdd = item.DateOfAdd,
                            DateOfUpdate = item.DateOfUpdate,
                            Watchings = item.Watchings,
                            Link = item.Link,
                            Describe = item.Describe,
                            Price = item.Price,
                            Status = item.Status,
                            Apartment = mapAppartment.ToDestiantion
                        .Map<RentaBusinesLogic.Models.Good.Apartment>(item.Apartment)
                        };

                    (goods as List<RentaBusinesLogic.Models.Good.Good>).Add(good);
                }
            }

            client.Phone = "";
            client.Email = "";

            RentaBusinesLogic.Models.Client.Client convertedClient =
                new RentaBusinesLogic.Models.Client.Client()
                {
                    Id = client.Id,
                    Firstname = client.Firstname,
                    Lastname = client.Lastname,
                    Phone = client.Phone,
                    Password = client.Password,
                    Email = client.Email,
                    Payments = mapPayment.ToDestiantion
                .Map<IEnumerable<RentaBusinesLogic.Models.Client.Payment>>(client.Payments),
                    Goods = goods
                };

            return convertedClient;
        }
    }
}
