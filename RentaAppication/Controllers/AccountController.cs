using DataBaseContext.Services;
using Microsoft.AspNetCore.Mvc;
using RentaAppication.Models;
using RentaAppication.Models.Client;
using RentaAppication.Models.Good;
using RentaAppication.Services;
using RentaAppication.Services.AutoMapper;
using RentaAppication.Services.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentaAppication.Controllers
{
    public class AccountController : Controller
    {
        private readonly GetOrders getOrders;
        private readonly GetClients getClients;
        private readonly GoodsConverter goodsConverter;
        private readonly ClientsConverter clientsConverter;
        private readonly OrdersConverter ordersConverter;
        private readonly Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment;
        private readonly Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapApartment;

        public AccountController(ClientsConverter clientsConverter,
            GoodsConverter goodsConverter, OrdersConverter ordersConverter,
            Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapApartment)
        {
            getOrders = new GetOrders();
            getClients = new GetClients();
            this.clientsConverter = clientsConverter;
            this.mapPayment = mapPayment;
            this.mapApartment = mapApartment;
            this.goodsConverter = goodsConverter;
            this.ordersConverter = ordersConverter;
        }

        private void GetSession()
        {
            byte[] dataName;
            string resultName;

            HttpContext.Session.TryGetValue("Name", out dataName);

            if (dataName != null)
            {
                resultName = Encoding.UTF8.GetString(dataName);
                ViewBag.Session = resultName;
            }
        }

        public void SetCokkie(Client client)
        {
            string name = client.Firstname.ToLower();
            byte[] bytesName = Encoding.UTF8.GetBytes(name);
            HttpContext.Session.Set("Name", bytesName);
        }

        [HttpGet]
        public async Task<IActionResult> ShowAccount()
        {
            GetSession();

            IEnumerable<Client> clients = await getClients.ReadAll(clientsConverter, mapPayment, mapApartment);
            Client client = (clients as List<Client>).Find(x => x.Firstname.ToLower() == ViewBag.Session);

            if ((client.Goods as List<Good>).Count == 0)
            {
                ViewBag.Message = "No orders";
            }

            return View(client);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveAccount()
        {
            GetSession();

            IEnumerable<Client> clients = await getClients.ReadAll(clientsConverter, mapPayment, mapApartment);
            Client client = (clients as List<Client>).Find(x => x.Firstname.ToLower() == ViewBag.Session);

            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int clientId)
        {
            GetSession();

            await getClients.Delete(clientId, clientsConverter, mapPayment, mapApartment);

            HttpContext.Session.Clear();

            return Redirect("/Home/Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAccount()
        {
            GetSession();

            IEnumerable<Client> clients = await getClients.ReadAll(clientsConverter, mapPayment, mapApartment);
            Client client = (clients as List<Client>).Find(x => x.Firstname.ToLower() == ViewBag.Session);

            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Client client)
        {
            GetSession();

            IEnumerable<Client> clients = await getClients.ReadAll(clientsConverter, mapPayment, mapApartment);
            Client currentClient = (clients as List<Client>).Find(x => x.Firstname.ToLower() == ViewBag.Session);

            currentClient.Firstname = client.Firstname;
            currentClient.Lastname = client.Lastname;
            currentClient.Phone = client.Phone;
            currentClient.Password = client.Password;
            currentClient.Email = client.Email;

            byte[] hash = Hash.CreateHash(client.Password);
            currentClient.Password = Hash.ByteArrayToString(hash);

            try
            {
                await getClients.Update(currentClient, clientsConverter, mapPayment, mapApartment);

                SetCokkie(currentClient);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("UpdateAccount");
            }

            return Redirect("ShowAccount");
        }

        [HttpGet]
        public async Task<IActionResult> Orders(int clientId)
        {
            GetSession();

            IEnumerable<Order> orders = await getOrders.ReadAll(ordersConverter, mapApartment);
            IEnumerable<Order> myOrders = new List<Order>();

            foreach (var item in orders)
            {
                if (item.Client.Id == clientId)
                {
                    (myOrders as List<Order>).Add(item);
                }
            }

            if ((myOrders as List<Order>).Count == 0)
            {
                ViewBag.Message = "No orders. Go home and make a deals!";
            }

            return View(myOrders);
        }

        [HttpGet]
        public async Task<IActionResult> Payments()
        {
            GetSession();

            IEnumerable<Client> clients
                = await getClients.ReadAll(clientsConverter, mapPayment, mapApartment);

            Client client = (clients as List<Client>).Find(x => x.Firstname.ToLower() == ViewBag.Session);

            KeyValuePair<Client, Payment> pair = new KeyValuePair<Client, Payment>(client, null);

            return View(pair);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(KeyValuePair<Client, Payment> pair)
        {
            GetSession();

            IEnumerable<Client> orders
                = await getClients.ReadAll(clientsConverter, mapPayment, mapApartment);

            Client client = (orders as List<Client>).Find(x => x.Id == pair.Key.Id);
            (client.Payments as List<Payment>).Add(pair.Value);

            try
            {
                await getClients.Update(client, clientsConverter, mapPayment, mapApartment);
            }
            catch (Exception) { }

            return RedirectToAction("Payments");
        }
    }
}
