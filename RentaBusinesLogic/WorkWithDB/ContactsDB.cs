using DataBaseContext;
using RentaBusinesLogic.Interfaces;
using RentaBusinesLogic.Models.Contact;
using RentaBusinesLogic.Services.AutoMapper;
using RentaBusinesLogic.Services.Converters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentaBusinesLogic.WorkWithDB
{
    public class ContactsDB : IRepository<Contact>
    {
        readonly ContactsConverter contactsConverters;
        readonly Mapper<Phone, DataBaseContext.Models.Contact.Phone> mapPhone;

        public ContactsDB()
        {
            mapPhone = new Mapper<Phone, DataBaseContext.Models.Contact.Phone>();
            contactsConverters = new ContactsConverter();
        }

        public async Task<Contact> Read(int id)
        {
            using (WorkWith contacts = new WorkWith())
            {
                DataBaseContext.Models.Contact.Contact contact =
                    await Task.Run(() => contacts.Contacts.Read(id));

                Contact convertedContact = contactsConverters.ConvertContactFrom(contact, mapPhone);

                return convertedContact;
            }
        }

        public async Task<IEnumerable<Contact>> ReadAll()
        {
            using (WorkWith contacts = new WorkWith())
            {
                IEnumerable<DataBaseContext.Models.Contact.Contact> contactsList =
                    await Task.Run(() => contacts.Contacts.ReadAll());

                IEnumerable<Contact> convertedContacts =
                    contactsConverters.ConvertContactsFrom(contactsList, mapPhone);

                return convertedContacts;
            }
        }


        public async Task<Contact> Create(Contact item)
        {
            using (WorkWith contacts = new WorkWith())
            {
                DataBaseContext.Models.Contact.Contact convertedContact =
                    contactsConverters.ConvertContactTo(item, mapPhone);

                await Task.Run(() => contacts.Contacts.Create(convertedContact));

                Contact convertedCorrectContact =
                    contactsConverters.ConvertContactFrom(convertedContact, mapPhone);

                return convertedCorrectContact;
            }
        }
        public async Task<Contact> Update(Contact item)
        {
            using (WorkWith contacts = new WorkWith())
            {
                DataBaseContext.Models.Contact.Contact convertedContact =
                    contactsConverters.ConvertContactTo(item, mapPhone);

                await Task.Run(() => contacts.Contacts.Update(convertedContact));

                Contact convertedCorrectContact =
                    contactsConverters.ConvertContactFrom(convertedContact, mapPhone);

                return convertedCorrectContact;
            }
        }

        public async Task Delete(int id)
        {
            using (WorkWith contacts = new WorkWith())
            {
                await Task.Run(() => contacts.Contacts.Delete(id));
            }
        }
    }
}
