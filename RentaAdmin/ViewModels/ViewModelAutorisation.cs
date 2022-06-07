using RentaAdmin.Command;
using RentaAdmin.Models.Client;
using RentaAdmin.Models.Good;
using RentaAdmin.Services.AutoMapper;
using RentaAdmin.Services.Converters;
using RentaAdmin.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;

namespace RentaAdmin.ViewModels
{
    public class ViewModelAutorisation : INotifyPropertyChanged
    {
        private Client admin;
        private RelayCommand command;
        private ClientsConverter clientsConverter;
        private GetClients getClients;
        private Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment;
        private Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment;

        private string namePattern;
        private string passwordPattern;

        public static event EventHandler<EventArgs>? enter = null;
        public static event EventHandler<EventArgs>? update = null;

        public ViewModelAutorisation()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            admin = new Client();
            getClients = new GetClients();
            clientsConverter = new ClientsConverter();
            mapAppartment = new Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment>();
            mapPayment = new Mapper<Payment, RentaBusinesLogic.Models.Client.Payment>();

            SetPatterns();
        }

        public void SetPatterns()
        {
            IEnumerable<PropertyInfo> properties = admin.GetType().GetProperties().ToList();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Firstname" || property.Name == "Password")
                {
                    IEnumerable<Attribute> attributes = property.GetCustomAttributes();
                    string pattern = (attributes.ToList()
                        .Find(x => x.TypeId
                        == typeof(RegularExpressionAttribute)) as RegularExpressionAttribute).Pattern;

                    switch (property.Name)
                    {
                        case "Firstname":
                            namePattern = pattern;
                            break;
                        case "Password":
                            passwordPattern = pattern;
                            break;
                    }
                }
            }
        }

        public Client Admin
        {
            get { return admin; }
            set
            {
                admin = value;
                OnPropertyChanged(nameof(Admin));
            }
        }

        public RelayCommand SingUp
        {
            get
            {
                return command = new RelayCommand(async obj =>
                {
                    MessageBox.Show("Loading...", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                    Client admin = new Client()
                    {
                        Firstname = this.admin.Firstname.ToLower(),
                        Lastname = this.admin.Lastname.ToLower(),
                        Password = this.admin.Password.ToLower(),
                    };

                    try
                    {
                        await getClients.Create(admin, clientsConverter, mapPayment, mapAppartment);

                        MessageBox.Show("You was succesfully registred.", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        update?.Invoke(null, EventArgs.Empty);
                        enter?.Invoke(null, EventArgs.Empty);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("You was not succesfully registred.", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }, obj => CanOnClick());
            }
        }

        public RelayCommand LogIn
        {
            get
            {
                return command = new RelayCommand(async obj =>
                {
                    MessageBox.Show("Loading...", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Information);

                    Client admin = new Client()
                    {
                        Firstname = this.admin.Firstname.ToLower(),
                        Lastname = this.admin.Lastname.ToLower(),
                        Password = this.admin.Password.ToLower(),
                    };

                    try
                    {
                        await getClients.Check(admin, clientsConverter, mapPayment, mapAppartment);

                        MessageBox.Show("You was succesfully loged in.", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        update?.Invoke(null, EventArgs.Empty);
                        enter?.Invoke(null, EventArgs.Empty);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("You was not succesfully loged in.", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }, obj => CanOnClick());
            }
        }

        private bool CanOnClick()
        {
            if (admin != null && admin.Firstname != null
                && admin.Lastname != null && admin.Password != null)
            {
                if (Regex.IsMatch(admin.Firstname.Trim(), namePattern)
                && Regex.IsMatch(admin.Lastname.Trim(), namePattern)
                && Regex.IsMatch(admin.Password.Trim(), passwordPattern))
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
