using RentaAdmin.Models.Contact;
using RentaAdmin.Services.AutoMapper;
using RentaAdmin.Services.Converters;
using RentaBusinesLogic;
using RentaBusinesLogic.WorkWithDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentaAdmin.Services
{
    public class GetContacts
    {
        private WorkWithContexts contexts;

        public GetContacts()
        {
            contexts = new WorkWithContexts();
        }

        public async Task<Contact> Read(int id, ContactsConverter contactsConverter,
            Mapper<Phone, RentaBusinesLogic.Models.Contact.Phone> mapPhone)
        {
            RentaBusinesLogic.Models.Contact.Contact contact =
                    await Task.Run(() => contexts.Contacts.Read(id));

            Contact convertedContact = contactsConverter.ConvertContactFrom(contact, mapPhone);

            return convertedContact;
        }

        public async Task<IEnumerable<Contact>> ReadAll(ContactsConverter contactsConverter,
            Mapper<Phone, RentaBusinesLogic.Models.Contact.Phone> mapPhone)
        {
            IEnumerable<RentaBusinesLogic.Models.Contact.Contact> contactsList =
                    await Task.Run(() => contexts.Contacts.ReadAll());

            IEnumerable<Contact> convertedContacts =
                contactsConverter.ConvertContactsFrom(contactsList, mapPhone);

            return convertedContacts;
        }

        public async Task<Contact> Create(Contact contact, ContactsConverter contactsConverter,
            Mapper<Phone, RentaBusinesLogic.Models.Contact.Phone> mapPhone)
        {
            RentaBusinesLogic.Models.Contact.Contact convertedContact =
                    contactsConverter.ConvertContactTo(contact, mapPhone);

            await Task.Run(() => contexts.Contacts.Create(convertedContact));

            Contact convertedCorrectContact =
                contactsConverter.ConvertContactFrom(convertedContact, mapPhone);

            return convertedCorrectContact;
        }

        public async Task<Contact> Update(Contact contact, ContactsConverter contactsConverter,
            Mapper<Phone, RentaBusinesLogic.Models.Contact.Phone> mapPhone)
        {
            RentaBusinesLogic.Models.Contact.Contact convertedContact =
                    contactsConverter.ConvertContactTo(contact, mapPhone);

            await Task.Run(() => contexts.Contacts.Update(convertedContact));

            Contact convertedCorrectContact =
                contactsConverter.ConvertContactFrom(convertedContact, mapPhone);

            return convertedCorrectContact;
        }

        public async Task Delete(int id)
        {
            await Task.Run(() => contexts.Contacts.Delete(id));
        }
    }
}
