using RentaAdmin.Command;
using RentaAdmin.Models.Contact;
using RentaAdmin.Services;
using RentaAdmin.Services.AutoMapper;
using RentaAdmin.Services.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;

namespace RentaAdmin.ViewModels
{
    public class ViewModelContacts : INotifyPropertyChanged
    {
        private Contact contact;
        private Phone phone;
        private ContactsConverter contactsConverter;
        private ObservableCollection<Contact> contacts;
        private ObservableCollection<Phone> phones;
        private RelayCommand command;
        private IEnumerable<Contact> allContacts;
        private GetContacts getContacts;
        private Mapper<Phone, RentaBusinesLogic.Models.Contact.Phone> mapPhone;
        string numberPattern;
        string emailPattern;

        public ViewModelContacts()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            contact = new Contact();
            phone = new Phone();
            getContacts = new GetContacts();
            contactsConverter = new ContactsConverter();
            mapPhone = new Mapper<Phone, RentaBusinesLogic.Models.Contact.Phone>();

            ViewModelAutorisation.update += new EventHandler<EventArgs>(Update);
            MainWindow.updateContacts += new EventHandler<EventArgs>(UpdateContacts);
            //Update(null, new EventArgs());
            SetPatternsContacts();
            SetPatternsPhone();
        }

        private async void Update(object? sender, EventArgs e)
        {
            IEnumerable<Contact> contacts =
                await getContacts.ReadAll(contactsConverter, mapPhone);

            allContacts = contacts;

            Contacts = new ObservableCollection<Contact>(contacts.ToList());
        }

        private void UpdateContacts(object? sender, EventArgs e)
        {
            string template = ((string)sender).ToLower();

            if (template.Trim() != "")
            {
                Contacts = new ObservableCollection<Contact>(
                    allContacts.ToList().FindAll(x => x.CountryTownStreet.ToString().Contains(template)));
            }
            else
            {
                Contacts = new ObservableCollection<Contact>(allContacts.ToList());
            }
        }

        public void LoadOther(Contact contact)
        {
            if (contact != null && contact.Email != null)
            {
                Phones = new ObservableCollection<Phone>(contact.Phones.ToList());
            }
            else
            {
                Phones = null;
            }
        }

        public void SetPatternsContacts()
        {
            IEnumerable<PropertyInfo> properties = Contact.GetType().GetProperties().ToList();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Email")
                {
                    IEnumerable<Attribute> attributes = property.GetCustomAttributes();
                    string pattern = (attributes.ToList()
                        .Find(x => x.TypeId
                        == typeof(RegularExpressionAttribute)) as RegularExpressionAttribute).Pattern;

                    emailPattern = pattern;
                }
            }
        }

        public void SetPatternsPhone()
        {
            IEnumerable<PropertyInfo> properties = Phone.GetType().GetProperties().ToList();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Number")
                {
                    IEnumerable<Attribute> attributes = property.GetCustomAttributes();
                    string pattern = (attributes.ToList()
                        .Find(x => x.TypeId
                        == typeof(RegularExpressionAttribute)) as RegularExpressionAttribute).Pattern;

                    numberPattern = pattern;
                }
            }
        }

        public Contact Contact
        {
            get { return contact; }
            set
            {
                contact = value;
                LoadOther(contact);
                OnPropertyChanged(nameof(Contact));
            }
        }

        public Phone Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public ObservableCollection<Contact> Contacts
        {
            get { return contacts; }
            set
            {
                contacts = value;
                OnPropertyChanged(nameof(Contacts));
            }
        }

        public ObservableCollection<Phone> Phones
        {
            get { return phones; }
            set
            {
                phones = value;
                OnPropertyChanged(nameof(Phones));
            }
        }

        public RelayCommand AddContact
        {
            get
            {
                return command = new RelayCommand(async obj =>
                {
                    MessageBox.Show("Loading...", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                    try
                    {
                        await getContacts.Create(contact, contactsConverter, mapPhone);

                        MessageBox.Show("Contacts was succesfully removed.", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                        List<Contact> contacts = allContacts.ToList();
                        Contact.Phones = new List<Phone>();
                        contacts.Add(Contact);
                        allContacts = contacts;
                        Contacts = new ObservableCollection<Contact>(allContacts);

                        Contact = new Contact();
                        Phones = null;

                        Update(null, new EventArgs());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Contacts was not succesfully removed.", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }, obj => CanOnClickAddContacts());
            }
        }

        public RelayCommand AddPhone
        {
            get
            {
                return command = new RelayCommand(async obj =>
                {
                    MessageBox.Show("Loading...", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                    List<Phone> phones = contact.Phones.ToList();
                    phones.Add(Phone);
                    Contact.Phones = phones;

                    try
                    {
                        await getContacts.Update(contact, contactsConverter, mapPhone);

                        MessageBox.Show("Phone was succesfully add.", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                        Phones = new ObservableCollection<Phone>(phones);
                        Phone = new Phone();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Phone was not succesfully add.", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }, obj => CanOnClickAddPhone());
            }
        }

        public RelayCommand RemoveContact
        {
            get
            {
                return command = new RelayCommand(async obj =>
                {
                    if (MessageBox.Show("Do you really want to remove?", "Message",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("Loading...", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                        try
                        {
                            await getContacts.Delete(contact.Id);

                            List<Contact> contacts = allContacts.ToList();
                            contacts.Remove(contact);
                            Contacts = new ObservableCollection<Contact>(contacts);
                            allContacts = Contacts;

                            MessageBox.Show("Contacts was succesfully removed.", "Message",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                            Phone = null;
                            Phones = null;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Contacts was not succesfully removed.", "Message",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Operation was succesfully denied", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }, obj => CanOnClickRemoveContact());
            }
        }

        public RelayCommand RemovePhone
        {
            get
            {
                return command = new RelayCommand(async obj =>
                {
                    if (MessageBox.Show("Do you really want to remove?", "Message",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("Loading...", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                        try
                        {
                            await getContacts.Update(contact, contactsConverter, mapPhone);

                            MessageBox.Show("Phone was succesfully removed.", "Message",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                            List<Phone> phones = contact.Phones.ToList();
                            phones.Remove(phone);
                            Contact.Phones = phones;

                            Phone = null;
                            Phones = new ObservableCollection<Phone>(phones);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Phone was not succesfully removed.", "Message",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Operation was succesfully denied", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }, obj => CanOnClickRemovePhone());
            }
        }

        public RelayCommand ClearContacts
        {
            get
            {
                return command = new RelayCommand(obj =>
                {
                    Contact = new Contact();
                }, obj => CanOnClickClearContacts());
            }
        }

        public RelayCommand ClearPhone
        {
            get
            {
                return command = new RelayCommand(obj =>
                {
                    Phone = new Phone();
                }, obj => CanOnClickClearPhone());
            }
        }

        private bool CanOnClickClearPhone()
        {
            if (phone != null && phone.Number != null)
            {
                return true;
            }

            return false;
        }

        private bool CanOnClickClearContacts()
        {
            if (contact != null && contact.Email != null
                && contact.CountryTownStreet != null)
            {
                return true;
            }

            return false;
        }

        private bool CanOnClickAddContacts()
        {
            if (contact != null && contact.Email != null
                && contact.CountryTownStreet != null)
            {
                if (Regex.IsMatch(contact.Email.Trim(), emailPattern)
                    && contact.CountryTownStreet.Trim() != "")
                    return true;
            }

            return false;
        }

        private bool CanOnClickAddPhone()
        {
            if (contact != null && phone != null && phone.Number != null)
            {
                if (Regex.IsMatch(phone.Number.Trim(), numberPattern))
                    return true;
            }

            return false;
        }

        private bool CanOnClickRemovePhone()
        {
            if (phone != null && phone.Number != null)
            {
                return true;
            }

            return false;
        }

        private bool CanOnClickRemoveContact()
        {
            if (contact != null && contact.CountryTownStreet != null
                 && contact.Email != null)
            {
                return true;
            }

            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
