using DataBaseContext;
using DataBaseContext.Services;
using RentaBusinesLogic.Interfaces;
using RentaBusinesLogic.Models.Client;
using RentaBusinesLogic.Models.Good;
using RentaBusinesLogic.Services.AutoMapper;
using RentaBusinesLogic.Services.Converters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentaBusinesLogic.WorkWithDB
{
    public class ClientsDB : IRepositoryClient<Client>
    {
        readonly ClientsConverter clientsConverters;
        readonly Mapper<Payment, DataBaseContext.Models.Client.Payment> mapPayment;
        readonly Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapAppartment;

        public ClientsDB()
        {
            mapPayment = new Mapper<Payment, DataBaseContext.Models.Client.Payment>();
            mapAppartment = new Mapper<Apartment, DataBaseContext.Models.Good.Apartment>();
            clientsConverters = new ClientsConverter();
        }

        public async Task<Client> Check(Client client)
        {
            string password = client.Password;
            byte[] hash = Hash.CreateHash(password);
            password = Hash.ByteArrayToString(hash);

            using (WorkWith clients = new WorkWith())
            {
                IEnumerable<DataBaseContext.Models.Client.Client> clientsList =
                    await Task.Run(() => clients.Clients.ReadAll());

                foreach (DataBaseContext.Models.Client.Client item in clientsList)
                {
                    if (item.Password == password
                        && item.Firstname == client.Firstname)
                    {
                        Client convertedClient = clientsConverters
                            .ConvertClientFrom(item, mapPayment, mapAppartment);

                        return convertedClient;
                    }
                }
            }

            return null;
        }

        public async Task<Client> Read(int id)
        {
            using (WorkWith clients = new WorkWith())
            {
                DataBaseContext.Models.Client.Client client =
                    await Task.Run(() => clients.Clients.Read(id));

                Client convertedClient = clientsConverters
                    .ConvertClientFrom(client, mapPayment, mapAppartment);

                return convertedClient;
            }
        }

        public async Task<IEnumerable<Client>> ReadAll()
        {
            using (WorkWith clients = new WorkWith())
            {
                IEnumerable<DataBaseContext.Models.Client.Client> clientsList =
                    await Task.Run(() => clients.Clients.ReadAll());

                IEnumerable<Client> convertedClients =
                    clientsConverters.ConvertClientsFrom(clientsList, mapPayment, mapAppartment);

                return convertedClients;
            }
        }

        public async Task<Client> Create(Client item)
        {
            byte[] hash = Hash.CreateHash(item.Password);
            item.Password = Hash.ByteArrayToString(hash);

            using (WorkWith clients = new WorkWith())
            {
                DataBaseContext.Models.Client.Client convertedClient =
                    clientsConverters.ConvertClientTo(item, mapPayment, mapAppartment);

                await Task.Run(() => clients.Clients.Create(convertedClient));

                Client convertedCurrentClient = clientsConverters
                    .ConvertClientFrom(convertedClient, mapPayment, mapAppartment);

                return item;
            }
        }

        public async Task<Client> Update(Client item)
        {
            using (WorkWith clients = new WorkWith())
            {
                DataBaseContext.Models.Client.Client convertedClient =
                    clientsConverters.ConvertClientTo(item, mapPayment, mapAppartment);

                await Task.Run(() => clients.Clients.Update(convertedClient));

                Client convertedCurrentClient = clientsConverters
                    .ConvertClientFrom(convertedClient, mapPayment, mapAppartment);

                return item;
            }
        }

        public async Task Delete(int id)
        {
            using (WorkWith clients = new WorkWith())
            {
                await Task.Run(() => clients.Clients.Delete(id));
            }
        }
    }
}
