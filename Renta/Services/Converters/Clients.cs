using Renta.Services.AutoMapper;
using RentaAPI.Models.Client;
using RentaAPI.Models.Good;
using System.Collections.Generic;

namespace RentaAPI.Services.Converters
{
    public class Clients
    {
        public Client ConvertClientFrom(DataBaseContext.Models.Client.Client client,
            Mapper<Payment, DataBaseContext.Models.Client.Payment> mapPayment,
            Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapAppartment)
        {
            IEnumerable<Good> goods = new List<Good>();

            foreach (var item in client.Goods)
            {
                Good good = new Good()
                {
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

            Client convertedClient = new Client()
            {
                Id = client.Id,
                Firstname = client.Firstname,
                Lastname = client.Lastname,
                Phone = client.Phone,
                Password = client.Password,
                Email = client.Email,
                Payments = mapPayment.ToResource.Map<IEnumerable<Payment>>(client.Payments),
                Goods = goods,
            };

            return convertedClient;
        }

        public IEnumerable<Client> ConvertClientsFrom(
            IEnumerable<DataBaseContext.Models.Client.Client> clients,
            Mapper<Payment, DataBaseContext.Models.Client.Payment> mapPayment,
            Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapAppartment)
        {
            IEnumerable<Client> convertedCliensList = new List<Client>();

            foreach (var item in clients)
            {
                (convertedCliensList as List<Client>)
                    .Add(ConvertClientFrom(item, mapPayment, mapAppartment));
            }

            return convertedCliensList;
        }

        public DataBaseContext.Models.Client.Client ConvertClientTo(Client client,
            Mapper<Payment, DataBaseContext.Models.Client.Payment> mapPayment,
            Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapAppartment)
        {
            IEnumerable<DataBaseContext.Models.Good.Good> goods =
                new List<DataBaseContext.Models.Good.Good>();

            foreach (var item in client.Goods)
            {
                DataBaseContext.Models.Good.Good good =
                    new DataBaseContext.Models.Good.Good()
                    {
                        DateOfAdd = item.DateOfAdd,
                        DateOfUpdate = item.DateOfUpdate,
                        Watchings = item.Watchings,
                        Link = item.Link,
                        Describe = item.Describe,
                        Price = item.Price,
                        Status = item.Status,
                        Apartment = mapAppartment.ToDestiantion
                    .Map<DataBaseContext.Models.Good.Apartment>(item.Apartment)
                    };

                (goods as List<DataBaseContext.Models.Good.Good>).Add(good);
            }

            DataBaseContext.Models.Client.Client convertedClient =
                new DataBaseContext.Models.Client.Client()
                {
                    Id = client.Id,
                    Firstname = client.Firstname,
                    Lastname = client.Lastname,
                    Phone = client.Phone,
                    Password = client.Password,
                    Email = client.Email,
                    Payments = mapPayment.ToDestiantion
                .Map<IEnumerable<DataBaseContext.Models.Client.Payment>>(client.Payments),
                    Goods = goods
                };

            return convertedClient;
        }
    }
}
