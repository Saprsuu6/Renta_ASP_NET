using RentaAdmin.Command;
using RentaAdmin.Models.Client;
using RentaAdmin.Models.Good;
using RentaAdmin.Services;
using RentaAdmin.Services.AutoMapper;
using RentaAdmin.Services.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace RentaAdmin.ViewModels
{
    public class ViewModelClients : INotifyPropertyChanged
    {
        private Client client;
        private Good good;
        private Payment payment;
        private ObservableCollection<Client> clients;
        private ObservableCollection<Good> goods;
        private IEnumerable<Client> allClients;
        private ObservableCollection<Payment> payments;
        private RelayCommand command;
        private ClientsConverter clientsConverter;
        private GetClients getClients;
        private Mapper<Payment, RentaBusinesLogic.Models.Client.Payment> mapPayment;
        private Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment;

        public ViewModelClients()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            client = new Client();
            getClients = new GetClients();
            clientsConverter = new ClientsConverter();
            mapAppartment = new Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment>();
            mapPayment = new Mapper<Payment, RentaBusinesLogic.Models.Client.Payment>();

            ViewModelAutorisation.update += new EventHandler<EventArgs>(Update);
            MainWindow.updateClients += new EventHandler<EventArgs>(UpdateClents);
            //Update(null, new EventArgs());
        }

        private async void Update(object? sender, EventArgs e)
        {
            IEnumerable<Client> clients
                = await getClients.ReadAll(clientsConverter, mapPayment, mapAppartment);
            IEnumerable<Client> withoutAdmins
                = clients.ToList().FindAll(x => x.Phone != null && x.Email != null);

            allClients = withoutAdmins;

            Clients = new ObservableCollection<Client>(withoutAdmins);
        }

        private void UpdateClents(object? sender, EventArgs e)
        {
            string template = ((string)sender).ToLower();

            if (template.Trim() != "")
            {
                Clients = new ObservableCollection<Client>(
                    allClients.ToList().FindAll(x => x.Firstname.ToLower().Contains(template)));
            }
            else
            {
                Clients = new ObservableCollection<Client>(
                    allClients.ToList());
            }
        }

        public void LoadOther(Client client)
        {
            if (client != null)
            {
                Payments = new ObservableCollection<Payment>(client.Payments.ToList());
                Goods = new ObservableCollection<Good>(client.Goods.ToList());
            }
            else
            {
                Goods = null;
                Payments = null;
            }
        }

        public Client Client
        {
            get { return client; }
            set
            {
                client = value;
                LoadOther(client);
                OnPropertyChanged(nameof(Client));
            }
        }

        public Payment Payment
        {
            get { return payment; }
            set
            {
                payment = value;
                OnPropertyChanged(nameof(Payment));
            }
        }

        public Good Good
        {
            get { return good; }
            set
            {
                good = value;
                OnPropertyChanged(nameof(Good));
            }
        }

        public ObservableCollection<Client> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }

        public ObservableCollection<Payment> Payments
        {
            get { return payments; }
            set
            {
                payments = value;
                OnPropertyChanged(nameof(Payments));
            }
        }

        public ObservableCollection<Good> Goods
        {
            get { return goods; }
            set
            {
                goods = value;
                OnPropertyChanged(nameof(Goods));
            }
        }

        public RelayCommand RemoveClient
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
                            await getClients.Delete(client.Id, clientsConverter, mapPayment, mapAppartment);

                            MessageBox.Show("Client was succesfully removed.", "Message",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                            List<Client> clients = allClients.ToList();
                            clients.Remove(client);
                            Clients = new ObservableCollection<Client>(clients);
                            allClients = Clients;

                            Goods = null;
                            Payments = null;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Client was not succesfully removed.", "Message",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Operation was succesfully denied", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }, obj => CanOnClickRemoveClient());
            }
        }

        public RelayCommand RemoveGood
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

                        List<Good> goods = client.Goods.ToList();
                        goods.Remove(good);
                        Client.Goods = goods;

                        try
                        {
                            await getClients.Update(client, clientsConverter, mapPayment, mapAppartment);

                            MessageBox.Show("Client was succesfully udated.", "Message",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                            Good = null;
                            Goods = new ObservableCollection<Good>(goods);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Good was not succesfully removed.", "Message",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Operation was succesfully denied", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }, obj => CanOnClickRemoveGood());
            }
        }

        private bool CanOnClickRemoveClient()
        {
            if (client != null && client.Firstname != null
                && client.Lastname != null && client.Password != null
                 && client.Phone != null && client.Email != null)
            {
                return true;
            }

            return false;
        }

        private bool CanOnClickRemoveGood()
        {
            if (good != null && good.Link != null)
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
