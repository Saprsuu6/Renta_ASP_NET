using DataBaseContext.EF;
using DataBaseContext.Interfaces;
using DataBaseContext.Models;
using DataBaseContext.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseContext.Repositories
{
    public class ContactRepository : IRepository<Contact>
    {
        private Context DB;

        public ContactRepository(Context context)
        {
            DB = context;
        }

        public void Create(Contact item)
        {
            DB.Contacts.Add(item);
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            Contact concreteContact = DB.Contacts.ToList().
                Find(item => item.Id == id);

            if (concreteContact != null)
            {
                DB.Contacts.Remove(concreteContact);
                DB.SaveChanges();
            }
            else
                throw new ApplicationException("Contact has not found");
        }

        public Contact Read(int id)
        {
            Contact contact = DB.Contacts.Find(id);

            try
            {
                contact.Phones = DB.Phones.ToList().FindAll(x => x.Contact.Id == id);
            }
            catch (Exception) { }

            return contact;
        }

        public IEnumerable<Contact> ReadAll()
        {
            IEnumerable<Contact> contacts = DB.Contacts.ToList();

            foreach (Contact contact in contacts)
            {
                contact.Phones = DB.Phones.ToList().FindAll(x => x.Contact.Id == contact.Id);
            }

            return contacts;
        }

        public void Update(Contact item)
        {
            var concreteContact = Read(item.Id);

            if (concreteContact != null)
            {
                concreteContact.Email = item.Email;
                concreteContact.CountryTownStreet = item.CountryTownStreet;
                concreteContact.Phones = item.Phones;

                DB.Contacts.Update(concreteContact);
                DB.SaveChanges();
            }
            else
                throw new ApplicationException("Contact has not found");
        }
    }
}
