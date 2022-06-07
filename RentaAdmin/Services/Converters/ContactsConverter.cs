using RentaAdmin.Models.Contact;
using RentaAdmin.Services.AutoMapper;
using System.Collections.Generic;

namespace RentaAdmin.Services.Converters
{
    public class ContactsConverter
    {
        public Contact ConvertContactFrom(RentaBusinesLogic.Models.Contact.Contact contact,
            Mapper<Phone, RentaBusinesLogic.Models.Contact.Phone> mapPhone)
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
            IEnumerable<RentaBusinesLogic.Models.Contact.Contact> contacts,
            Mapper<Phone, RentaBusinesLogic.Models.Contact.Phone> mapPhone)
        {
            IEnumerable<Contact> convertedContactsList = new List<Contact>();

            foreach (var item in contacts)
            {
                (convertedContactsList as List<Contact>)
                    .Add(ConvertContactFrom(item, mapPhone));
            }

            return convertedContactsList;
        }

        public RentaBusinesLogic.Models.Contact.Contact ConvertContactTo(Contact contact,
            Mapper<Phone, RentaBusinesLogic.Models.Contact.Phone> mapPhone)
        {
            RentaBusinesLogic.Models.Contact.Contact convertedContact =
                new RentaBusinesLogic.Models.Contact.Contact()
                {
                    Id = contact.Id,
                    Email = contact.Email,
                    CountryTownStreet = contact.CountryTownStreet,
                    Phones = mapPhone.ToDestiantion
                .Map<IEnumerable<RentaBusinesLogic.Models.Contact.Phone>>(contact.Phones)
                };

            return convertedContact;
        }
    }
}
