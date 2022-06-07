using Microsoft.AspNetCore.Mvc;
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
    public class GoodController : Controller
    {
        private readonly GetClients getClients;
        private readonly ClientsConverter clientsConverter;
        private readonly Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment;
        private readonly Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapApartment;

        public GoodController(ClientsConverter clientsConverter,
            Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment,
            Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapApartment)
        {
            getClients = new GetClients();
            this.clientsConverter = clientsConverter;
            this.mapPayment = mapPayment;
            this.mapApartment = mapApartment;
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

        public IActionResult CreateGood()
        {
            GetSession();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateGood(int goodId)
        {
            if (goodId != 0)
                SetCokkie(goodId);

            GetSession();

            IEnumerable<Client> clients
                = await getClients.ReadAll(clientsConverter, mapPayment, mapApartment);

            Client client = (clients as List<Client>).Find(x => x.Firstname.ToLower() == ViewBag.Session);

            Good good = (client.Goods as List<Good>).Find(x => x.Id == ViewBag.GoodId);

            return View(good);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int goodId)
        {
            GetSession();

            IEnumerable<Client> clients
                = await getClients.ReadAll(clientsConverter, mapPayment, mapApartment);

            Client client = (clients as List<Client>).Find(x => x.Firstname.ToLower() == ViewBag.Session);

            (client.Goods as List<Good>).Remove((client.Goods as List<Good>).Find(x => x.Id == goodId));

            try
            {
                await getClients.Update(client, clientsConverter, mapPayment, mapApartment);
            }
            catch (Exception) 
            {
                ViewBag.Message = "Good was not succesfully removed";
                return RedirectToAction("UpdateGood", goodId);
            }

            return Redirect("/Account/ShowAccount");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Good good)
        {
            GetSession();

            good.DateOfAdd = DateTime.Now;
            good.DateOfUpdate = DateTime.Now;

            IEnumerable<Client> clients
            = await getClients.ReadAll(clientsConverter, mapPayment, mapApartment);

            Client client = (clients as List<Client>).Find(x => x.Firstname.ToLower() == ViewBag.Session);

            (client.Goods as List<Good>).Add(good);

            try
            {
                await getClients.Update(client, clientsConverter, mapPayment, mapApartment);
            }
            catch (Exception)
            {
                ViewBag.Message = "Good was not succesully added";
                return View("CreateGood");
            }

            return Redirect("/Account/ShowAccount");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Good good)
        {
            GetSession();

            good.DateOfUpdate = DateTime.Now;

            IEnumerable<Client> clients
                = await getClients.ReadAll(clientsConverter, mapPayment, mapApartment);

            Client client = (clients as List<Client>).Find(x => x.Firstname.ToLower() == ViewBag.Session);

            Good currentGood = (client.Goods as List<Good>).Find(x => x.Id == good.Id);

            #region Copping
            currentGood.DateOfUpdate = good.DateOfUpdate;
            currentGood.Watchings = good.Watchings;
            currentGood.Link = good.Link;
            currentGood.Describe = good.Describe;
            currentGood.Price = good.Price;
            currentGood.Status = good.Status;

            currentGood.Apartment.Street = good.Apartment.Street;
            currentGood.Apartment.Town = good.Apartment.Town;
            currentGood.Apartment.Type = good.Apartment.Type;
            currentGood.Apartment.Floors = good.Apartment.Floors;
            currentGood.Apartment.Rooms = good.Apartment.Rooms;
            currentGood.Apartment.Layout = good.Apartment.Layout;
            currentGood.Apartment.Condition = good.Apartment.Condition;
            currentGood.Apartment.Length = good.Apartment.Length;
            currentGood.Apartment.Width = good.Apartment.Width;
            currentGood.Apartment.Height = good.Apartment.Height;
            currentGood.Apartment.GoodId = good.Id;
            #endregion

            try
            {
                await getClients.Update(client, clientsConverter, mapPayment, mapApartment);
            }
            catch (Exception) { }

            return Redirect("/Account/ShowAccount");
        }
    }
}
