using Microsoft.AspNetCore.Mvc;
using RentaAppication.Models.Contact;
using RentaAppication.Services;
using RentaAppication.Services.AutoMapper;
using RentaAppication.Services.Converters;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentaAppication.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactsConverter contactsConverter;
        private readonly GetContacts getContacts;
        private readonly Mapper<Phone, RentaBusinesLogic.Models.Contact.Phone> mapPhone;

        public ContactsController(ContactsConverter contactsConverter,
            Mapper<Phone, RentaBusinesLogic.Models.Contact.Phone> mapPhone)
        {
            getContacts = new GetContacts();
            this.contactsConverter = contactsConverter;
            this.mapPhone = mapPhone;
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

        [HttpGet]
        public async Task<IActionResult> ShowContacts()
        {
            GetSession();

            IEnumerable<Contact> contacts 
                = await getContacts.ReadAll(contactsConverter, mapPhone);

            return View(contacts);
        }
    }
}
