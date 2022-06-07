using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using RentaAppication.Models;
using RentaAppication.Models.Client;
using RentaAppication.Models.Good;
using RentaAppication.Services;
using RentaAppication.Services.AutoMapper;
using RentaAppication.Services.Comparers;
using RentaAppication.Services.Converters;
using RentaBusinesLogic.WorkWithDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentaAppication.Controllers
{
    public class HomeController : Controller
    {
        private readonly GetOrders getOrders;
        private readonly GetClients getClients;
        private readonly GetGood getGood;
        private readonly GoodsConverter goodsConverter;
        private readonly ClientsConverter clientsConverter;
        private readonly OrdersConverter ordersConverter;
        private readonly Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment;
        private readonly Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapApartment;

        public HomeController(ClientsConverter clientsConverter,
            OrdersConverter ordersConverter, GoodsConverter goodsConverter,
            Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapApartment)
        {
            getOrders = new GetOrders();
            getClients = new GetClients();
            getGood = new GetGood();
            this.clientsConverter = clientsConverter;
            this.mapPayment = mapPayment;
            this.mapApartment = mapApartment;
            this.goodsConverter = goodsConverter;
            this.ordersConverter = ordersConverter;
        }

        private void GetSession()
        {
            byte[] dataName;
            byte[] goodId;
            string resultName;
            string resultId;

            HttpContext.Session.TryGetValue("Name", out dataName);
            HttpContext.Session.TryGetValue("GoodId", out goodId);

            if (dataName != null)
            {
                resultName = Encoding.UTF8.GetString(dataName);
                ViewBag.Session = resultName;
            }
            if (goodId != null)
            {
                resultId = Encoding.UTF8.GetString(goodId);
                ViewBag.GoodId = int.Parse(resultId);
            }
        }

        public void SetCokkie(int goodId)
        {
            string id = goodId.ToString();
            byte[] bytesName = Encoding.UTF8.GetBytes(id);
            HttpContext.Session.Set("GoodId", bytesName);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string filter = "date")
        {
            GetSession();

            IEnumerable<Client> temp 
                = await getClients.ReadAll(clientsConverter, mapPayment, mapApartment);

            int isNotEmpty = 0;
            foreach (var item in temp)
            {
                if ((item.Goods as List<Good>).Count != 0) isNotEmpty++;
            }

            if (isNotEmpty == 0)
                return View("IndexEmpty");

            switch (filter)
            {
                case "date":
                    foreach (var item in temp)
                    {
                        DateComparer comparer = new DateComparer();
                        (item.Goods as List<Good>).Sort(comparer);
                    }
                    break;

                case "populariry":
                    foreach (var item in temp)
                    {
                        WatchngsComparer comparer = new WatchngsComparer();
                        (item.Goods as List<Good>).Sort(comparer);
                    }
                    break;

                case "price":
                    foreach (var item in temp)
                    {
                        PriceComparer comparer = new PriceComparer();
                        (item.Goods as List<Good>).Sort(comparer);
                    }
                    break;
            }

            return View(temp);
        }

        [HttpGet]
        public async Task<IActionResult> ReadMore(int goodId)
        {
            if (goodId != 0)
                SetCokkie(goodId);

            GetSession();

            Good good = await getGood.Read(ViewBag.GoodId, goodsConverter, mapApartment);

            if (ViewBag.Session != null)
            {
                IEnumerable<Client> clients = await getClients.ReadAll(clientsConverter, mapPayment, mapApartment);

                Client client = (clients as List<Client>)
                    .Find(x => (x.Goods as List<Good>)
                    .Find(x => x.Id == ViewBag.GoodId) != null);

                if(client.Firstname != ViewBag.Session)
                {
                    good.Watchings++;
                }
            }

            return View(good);
        }

        [HttpGet]
        public async Task<IActionResult> Buy(Good good)
        {
            GetSession();

            Good newGood = await getGood.Read(good.Id, goodsConverter, mapApartment);
            IEnumerable<Client> clients = await getClients.ReadAll(clientsConverter, mapPayment, mapApartment);
            Client client = (clients as List<Client>).Find(x => x.Firstname.ToLower() == ViewBag.Session);

            Order order = new Order()
            {
                Client = client,
                Good = newGood,
                Date = DateTime.Now
            };

            await getOrders.Put(order, ordersConverter, mapApartment);

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
