using Renta.Services.AutoMapper;
using RentaAPI.Models.Contact;
using System.Collections.Generic;

namespace RentaAPI.Services.Converters
{
    public class Contacts
    {
        public Contact ConvertContactFrom(DataBaseContext.Models.Contact.Contact contact,
            Mapper<Phone, DataBaseContext.Models.Contact.Phone> mapPhone)
        {
            Contact convertedContacts = new Contact()
            {
                Id = contact.Id,
                Email = contact.Email,
                CountryTownStreet = contact.CountryTownStreet,
                Phones = mapPhone.ToResource.Map<IEnumerable<Phone>>(contact.Phones)
            };

            return convertedContacts;
        }

        public IEnumerable<Contact> ConvertContactsFrom(
            IEnumerable<DataBaseContext.Models.Contact.Contact> contacts,
            Mapper<Phone, DataBaseContext.Models.Contact.Phone> mapPhone)
        {
            IEnumerable<Contact> convertedContactsList = new List<Contact>();

            foreach (var item in contacts)
            {
                (convertedContactsList as List<Contact>)
                    .Add(ConvertContactFrom(item, mapPhone));
            }

            return convertedContactsList;
        }

        public DataBaseContext.Models.Contact.Contact ConvertContactTo(Contact contact,
            Mapper<Phone, DataBaseContext.Models.Contact.Phone> mapPhone)
        {
            DataBaseContext.Models.Contact.Contact convertedContact =
                new DataBaseContext.Models.Contact.Contact()
                {
                    Id = contact.Id,
                    Email = contact.Email,
                    CountryTownStreet = contact.CountryTownStreet,
                    Phones = mapPhone.ToDestiantion
                .Map<IEnumerable<DataBaseContext.Models.Contact.Phone>>(contact.Phones)
                };

            return convertedContact;
        }
    }
}
