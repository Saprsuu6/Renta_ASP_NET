using DataBaseContext.Models;
using DataBaseContext.Models.Client;
using DataBaseContext.Models.Contact;
using DataBaseContext.Models.Good;
using System;
using System.Collections.Generic;

namespace DataBaseContext
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Tests
            using (WorkWith workWith = new WorkWith())
            {

                #region Contacts

                #region Create
                //Contact contact = new Contact()
                //{
                //    Email = "mr.asd.2002@li.ru",
                //    CountryTownStreet = @"Ukrain\Youzne\Chimikov2"
                //};
                //
                //List<Phone> contacts = new List<Phone>()
                //{
                //    new Phone()
                //    {
                //        Number = "123456789",
                //        Contact = contact
                //    },
                //    new Phone()
                //    {
                //        Number = "123456788",
                //        Contact = contact
                //    },
                //};
                //
                //contact.Phones = contacts;
                //workWith.Contacts.Create(contact);
                #endregion

                #region Get
                //IEnumerable<Contact> contacts = workWith.Contacts.ReadAll();
                //Contact contact = workWith.Contacts.Read(4);
                #endregion

                #region Update
                //Contact contact = new Contact()
                //{
                //    Email = "mr.asdasd.2002@li.ru",
                //    CountryTownStreet = @"Ukrain\Youzne\Chimikov2"
                //};

                //List<Phone> contacts = new List<Phone>()
                //{
                //    new Phone()
                //    {
                //        Number = "12312312389",
                //        Contact = contact
                //    },
                //    new Phone()
                //    {
                //        Number = "23423456788",
                //        Contact = contact
                //    },
                //};

                //contact.Id = 1;
                //contact.Phones = contacts;

                //workWith.Contacts.Update(contact);
                #endregion

                #region Remove
                //workWith.Contacts.Delete(2);
                #endregion

                #endregion

                #region Clients

                #region Create
                //Client client = new Client()
                //{
                //    Firstname = "Andry",
                //    Lastname = "Saprigin",
                //    Phone = "+380639830317",
                //    Email = "coffee.2002@gmail.com",
                //    Password = "Cormax25524",
                //};

                //List<Payment> payments = new List<Payment>()
                //{
                //    new Payment()
                //    {
                //        Client = client,
                //        Provider = "MonoBank",
                //        Number = "111111111111111"
                //    }
                //};

                //client.Payments = payments;
                //workWith.Clients.Create(client);
                #endregion

                #region Get
                //IEnumerable<Client> clients = workWith.Clients.ReadAll();
                //Client client = workWith.Clients.Read(31);
                #endregion

                #region Update
                //Client client = new Client()
                //{
                //    Firstname = "Andry",
                //    Lastname = "Saprigin",
                //    Phone = "+380639830317",
                //    Email = "coffee.2002@gmail.com",
                //    Password = "Cormax25524",
                //};

                //List<Payment> payments = new List<Payment>()
                //{
                //    new Payment()
                //    {
                //        Client = client,
                //        Provider = "MonoBank",
                //        Number = "121212121212121"
                //    }
                //};

                //client.Id = 2;
                //client.Payments = payments;

                //workWith.Clients.Update(client);
                #endregion

                #region Remove
                //workWith.Clients.Delete(2);
                #endregion

                #endregion

                #region Goods

                #region Create
                //Apartment apartment = new Apartment()
                //{
                    //Town = "Kiev",
                    //Street = "Street",
                    //Type = "House",
                    //Floors = 2,
                    //Rooms = 3,
                    //Layout = "Layout",
                    //Condition = "Good",
                    //Length = 10,
                    //Width = 10,
                    //Height = 10,
                //};
                
                //Good good = new Good()
                //{
                    //Apartment = apartment,
                    //DateOfAdd = DateTime.Now,
                    //DateOfUpdate = DateTime.Now,
                    //Watchings = 0,
                    //Link = @"https://mystat.itstep.org/en/main/dashboard/page/index",
                    //Describe = "Describe",
                    //Rieltor = client,
                    //Price = 10000,
                    //Status = "Status"
                //};
                
                //workWith.Goods.Create(good);
                #endregion

                #region Get
                //IEnumerable<Good> goods = workWith.Goods.ReadAll();
                //Good good = workWith.Goods.Read(9);
                #endregion

                #region Update
                //good.Describe = "NewDecribe";
                //workWith.Goods.Update(good);
                #endregion

                #region Remove
                //workWith.Goods.Delete(2);
                #endregion

                #endregion

                #region Orders

                #region Create
                //Order order = new Order()
                //{
                //    Client = client,
                //    Good = good,
                //    Date = DateTime.Now,
                //};
                //
                //workWith.Orders.Create(order);
                #endregion

                #region Get
                //IEnumerable<Order> orders = workWith.Orders.ReadAll();
                //Order order = workWith.Orders.Read(1);
                #endregion

                #region Update
                //order.Client = client;
                //workWith.Orders.Update(order);
                #endregion

                #region Remove
                //workWith.Orders.Delete(1);
                #endregion

                #endregion
            };
            #endregion  
        }
    }
}
