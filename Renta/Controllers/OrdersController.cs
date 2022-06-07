using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Renta.Services.AutoMapper;
using RentaAPI.Models;
using RentaAPI.Models.Client;
using RentaAPI.Models.Good;
using RentaAPI.Services.Converters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentaAPI.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class OrdersController : ControllerBase
    {
        readonly Orders ordersConverters;
        readonly Mapper<Payment, DataBaseContext.Models.Client.Payment> mapPayment;
        readonly Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapAppartment;

        public OrdersController(Mapper<Payment, DataBaseContext.Models.Client.Payment> mapPayment,
            Mapper<Apartment, DataBaseContext.Models.Good.Apartment> mapAppartment, Orders ordersConverters)
        {
            this.mapPayment = mapPayment;
            this.mapAppartment = mapAppartment;
            this.ordersConverters = ordersConverters;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                ModelState.AddModelError("Identity", "Invalid identificator");

            using (WorkWith oreders = new WorkWith())
            {
                DataBaseContext.Models.Order order =
                    await Task.Run(() => oreders.Orders.Read(id));

                Order convertedOrder = ordersConverters
                    .ConvertOrederFrom(order, mapAppartment);

                return new ObjectResult(convertedOrder);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (WorkWith oreders = new WorkWith())
            {
                IEnumerable<DataBaseContext.Models.Order> orders =
                    await Task.Run(() => oreders.Orders.ReadAll());

                IEnumerable<Order> convertedOrders = ordersConverters
                    .ConvertOrdersFrom(orders, mapAppartment);

                return new ObjectResult(convertedOrders);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (WorkWith oreders = new WorkWith())
            {
                DataBaseContext.Models.Order convertedOrder =
                    ordersConverters.ConvertOrederTo(order, mapAppartment);

                await Task.Run(() => oreders.Orders.Create(convertedOrder));

                Order convertedCorrectOreder =
                    ordersConverters.ConvertOrederFrom(convertedOrder, mapAppartment);

                return new ObjectResult(convertedCorrectOreder);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (WorkWith oreders = new WorkWith())
            {
                DataBaseContext.Models.Order convertedOrder =
                    ordersConverters.ConvertOrederTo(order, mapAppartment);

                await Task.Run(() => oreders.Orders.Update(convertedOrder));

                Order convertedCorrectOreder =
                    ordersConverters.ConvertOrederFrom(convertedOrder, mapAppartment);

                return new ObjectResult(convertedCorrectOreder);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                ModelState.AddModelError("Identity", "Invalid identificator");

            using (WorkWith oreders = new WorkWith())
            {
                await Task.Run(() => oreders.Orders.Delete(id));

                return new ObjectResult(id);
            }
        }
    }
}
