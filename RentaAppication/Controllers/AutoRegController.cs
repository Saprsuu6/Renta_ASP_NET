using Microsoft.AspNetCore.Mvc;
using RentaAppication.Models.Client;
using RentaAppication.Models.Contact;
using RentaAppication.Models.Good;
using RentaAppication.Services;
using RentaAppication.Services.AutoMapper;
using RentaAppication.Services.Converters;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RentaAppication.Controllers
{
    public class AutoRegController : Controller
    {
        private readonly GetClients getClients;
        private readonly ClientsConverter clientsConverter;
        private readonly Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment;
        private readonly Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapApartment;

        public AutoRegController(ClientsConverter clientsConverter,
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
        public IActionResult AutorisationView()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegistrationView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autorisation(Client client)
        {
            try
            {
                await getClients.CheckClient(client, clientsConverter, mapPayment, mapApartment);

                SetCokkie(client);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("AutorisationView");
            }

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public async Task<IActionResult> Registration(Client client)
        {
            try
            {
                await getClients.Create(client, clientsConverter, mapPayment, mapApartment);

                SetCokkie(client);
            }
            catch (Exception)
            {
                ViewBag.Message = "You were not succesfully registered";
                return View("RegistrationView");
            }

            return Redirect("/Home/Index");
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home/Index");
        }
    }
}
