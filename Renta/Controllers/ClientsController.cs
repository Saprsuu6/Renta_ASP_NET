using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Renta.Services.AutoMapper;
using RentaAPI.Models.Client;
using RentaAPI.Models.Good;
using RentaAPI.Services.Converters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentaAPI.Controllers
{
    [Route("api/[controller]"), ApiController] 
    public class ClientsController : ControllerBase
    {
        readonly Clients clientsConverters;
        readonly Mapper<Payment, DataBaseContext.Models.Client.Payment> mapPayment;
        readonly Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapAppartment;

        public ClientsController(Mapper<Payment, DataBaseContext.Models.Client.Payment> mapPayment,
            Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapAppartment, Clients clientsConverters)
        {
            this.mapPayment = mapPayment;
            this.mapAppartment = mapAppartment;
            this.clientsConverters = clientsConverters;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                ModelState.AddModelError("Identity", "Invalid identificator");

            using (WorkWith clients = new WorkWith())
            {
                DataBaseContext.Models.Client.Client client =
                    await Task.Run(() => clients.Clients.Read(id));

                Client convertedClient = clientsConverters
                    .ConvertClientFrom(client, mapPayment, mapAppartment);

                return new ObjectResult(convertedClient);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (WorkWith clients = new WorkWith())
            {
                IEnumerable<DataBaseContext.Models.Client.Client> clientsList =
                    await Task.Run(() => clients.Clients.ReadAll());

                IEnumerable<Client> convertedCliens =
                    clientsConverters.ConvertClientsFrom(clientsList, mapPayment, mapAppartment);

                return new ObjectResult(convertedCliens);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Client client)
        {
            if (client.Firstname.ToLower() == "admin"
                || client.Lastname.ToLower() == "admin"
                || client.Password.ToLower() == "admin")
                ModelState.AddModelError("Admin", "Invalid user - admin");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (WorkWith clients = new WorkWith())
            {
                DataBaseContext.Models.Client.Client convertedClient =
                    clientsConverters.ConvertClientTo(client, mapPayment, mapAppartment);

                await Task.Run(() => clients.Clients.Create(convertedClient));

                Client convertedCurrentClient = clientsConverters
                    .ConvertClientFrom(convertedClient, mapPayment, mapAppartment);

                return new ObjectResult(convertedCurrentClient);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Client client)
        {
            if (client.Firstname.ToLower() == "admin"
                || client.Lastname.ToLower() == "admin"
                || client.Password.ToLower() == "admin")
                ModelState.AddModelError("Admin", "Invalid user - admin");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (WorkWith clients = new WorkWith())
            {
                DataBaseContext.Models.Client.Client convertedClient =
                    clientsConverters.ConvertClientTo(client, mapPayment, mapAppartment);

                await Task.Run(() => clients.Clients.Update(convertedClient));

                Client convertedCurrentClient = clientsConverters
                    .ConvertClientFrom(convertedClient, mapPayment, mapAppartment);

                return new ObjectResult(convertedCurrentClient);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                ModelState.AddModelError("Identity", "Invalid identificator");

            using (WorkWith clients = new WorkWith())
            {
                await Task.Run(() => clients.Clients.Delete(id));

                return new ObjectResult(id);
            }
        }
    }
}
