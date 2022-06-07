using DataBaseContext;
using Microsoft.AspNetCore.Mvc;
using Renta.Services.AutoMapper;
using RentaAPI.Models.Contact;
using RentaAPI.Services.Converters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentaAPI.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class ContactsController : ControllerBase
    {
        readonly Contacts contactsConverters;
        readonly Mapper<Phone, DataBaseContext.Models.Contact.Phone> mapPhone;

        public ContactsController(Mapper<Phone, DataBaseContext.Models.Contact.Phone> mapPhone,
            Contacts contactsConverters)
        {
            this.mapPhone = mapPhone;
            this.contactsConverters = contactsConverters;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1)
                ModelState.AddModelError("Identity", "Invalid identificator");

            using (WorkWith contacts = new WorkWith())
            {
                DataBaseContext.Models.Contact.Contact contact =
                    await Task.Run(() => contacts.Contacts.Read(id));

                Contact convertedContact = contactsConverters.ConvertContactFrom(contact, mapPhone);

                return new ObjectResult(convertedContact);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (WorkWith contacts = new WorkWith())
            {
                IEnumerable<DataBaseContext.Models.Contact.Contact> contactsList =
                    await Task.Run(() => contacts.Contacts.ReadAll());

                IEnumerable<Contact> convertedContacts =
                    contactsConverters.ConvertContactsFrom(contactsList, mapPhone);

                return new ObjectResult(convertedContacts);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (WorkWith contacts = new WorkWith())
            {
                DataBaseContext.Models.Contact.Contact convertedContact =
                    contactsConverters.ConvertContactTo(contact, mapPhone);

                await Task.Run(() => contacts.Contacts.Create(convertedContact));

                Contact convertedCorrectContact = 
                    contactsConverters.ConvertContactFrom(convertedContact, mapPhone);

                return new ObjectResult(convertedCorrectContact);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (WorkWith contacts = new WorkWith())
            {
                DataBaseContext.Models.Contact.Contact convertedContact =
                    contactsConverters.ConvertContactTo(contact, mapPhone);

                await Task.Run(() => contacts.Contacts.Update(convertedContact));

                Contact convertedCorrectContact =
                    contactsConverters.ConvertContactFrom(convertedContact, mapPhone);

                return new ObjectResult(convertedCorrectContact);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
                ModelState.AddModelError("Identity", "Invalid identificator");

            using (WorkWith contacts = new WorkWith())
            {
                await Task.Run(() => contacts.Contacts.Delete(id));

                return new ObjectResult(id);
            }
        }
    }
}
