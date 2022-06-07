using RentaAppication.Models;
using RentaAppication.Models.Client;
using RentaAppication.Models.Good;
using RentaAppication.Services.AutoMapper;
using RentaAppication.Services.Converters;
using RentaBusinesLogic;
using RentaBusinesLogic.WorkWithDB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentaAppication.Services
{
    public class GetClients
    {
        private WorkWithContexts contexts;

        public GetClients()
        {
            contexts = new WorkWithContexts();
        }

        public async Task<IEnumerable<Client>> ReadAll(ClientsConverter clientsConverter,
            Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapApartment)
        { 
            IEnumerable<RentaBusinesLogic.Models.Client.Client> clientsList 
                = await contexts.Clients.ReadAll();

            IEnumerable<Client> converedClients 
                = clientsConverter.ConvertClientsFrom(clientsList, mapPayment, mapApartment);

            IEnumerable<Client> temp = new List<Client>();

            foreach (var client in converedClients) 
            {
                if (client.Firstname != "admin")
                {
                    (temp as List<Client>).Add(client);
                }
            }

            return temp;
        }

        public async Task<Client> Read(int id, 
            ClientsConverter clientsConverter,
            Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapApartment)
        {
            RentaBusinesLogic.Models.Client.Client client
                = await contexts.Clients.Read(id);

            Client converedClient
                = clientsConverter.ConvertClientFrom(client, mapPayment, mapApartment);

            return converedClient;
        }

        public async Task<Client> Update(Client client, ClientsConverter clientsConverter,
            Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment)
        {
            RentaBusinesLogic.Models.Client.Client convertedClient 
                = clientsConverter.ConvertClientTo(client, mapPayment, mapAppartment);

            if (await contexts.Clients.Check(convertedClient) == null)
            {
                await contexts.Clients.Update(convertedClient);
            }
            else
                throw new ApplicationException("User with the same email or phone is already exists");

            return client;
        }

        public async Task<Client> Create(Client client, ClientsConverter clientsConverter,
            Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment)
        {
            ClientsDB clients = new ClientsDB();

            RentaBusinesLogic.Models.Client.Client convertedClient
                = clientsConverter.ConvertClientTo(client, mapPayment, mapAppartment);

            if (await contexts.Clients.Check(convertedClient) == null)
            {
                await contexts.Clients.Create(convertedClient);
            }
            else
                throw new ApplicationException("User with the same email or phone is already exists");

            return client;
        }

        public async Task Delete(int id, ClientsConverter clientsConverter,
            Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment)
        {
            await contexts.Clients.Delete(id);
        }

        public async Task CheckClient(Client client, ClientsConverter clientsConverter,
            Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment)
        {
            RentaBusinesLogic.Models.Client.Client convertedClient
                = clientsConverter.ConvertClientTo(client, mapPayment, mapAppartment);

            if (await contexts.Clients.Check(convertedClient) == null)
            {
                throw new ApplicationException("You have to registrate");
            }
        }
    }
}
