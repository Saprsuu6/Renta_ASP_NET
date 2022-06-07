using RentaAdmin.Command;
using RentaAdmin.Models;
using RentaAdmin.Models.Client;
using RentaAdmin.Models.Good;
using RentaAdmin.Services;
using RentaAdmin.Services.AutoMapper;
using RentaAppication.Services.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RentaAdmin.ViewModels
{
    public class ViewModelOrders : INotifyPropertyChanged
    {
        private Client client;
        private Good good;
        private Order order;
        private ObservableCollection<Order> orders;
        private IEnumerable<Order> allOrders;
        private RelayCommand command;
        private GetOrders getOrders;
        private OrdersConverter ordersConverter;
        private Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment> mapAppartment;

        public ViewModelOrders()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            order = new Order();
            getOrders = new GetOrders();
            ordersConverter = new OrdersConverter();
            mapAppartment = new Mapper<Apartment, RentaBusinesLogic.Models.Good.Apartment>();

            ViewModelAutorisation.update += new EventHandler<EventArgs>(Update);
            MainWindow.updateOreders += new EventHandler<EventArgs>(UpdateOredrs);
            Update(null, new EventArgs());
        }

        private async void Update(object? sender, EventArgs e)
        {
            IEnumerable<Order> orders = await getOrders.ReadAll(ordersConverter, mapAppartment);
            allOrders = orders;
            Orders = new ObservableCollection<Order>(orders.ToList());
        }

        private void UpdateOredrs(object? sender, EventArgs e)
        {
            string template = ((string)sender).ToLower();

            if (template.Trim() != "")
            {
                Orders = new ObservableCollection<Order>(
                    allOrders.ToList().FindAll(x => x.Date.ToString().Contains(template)));
            }
            else
            {
                Orders = new ObservableCollection<Order>(allOrders.ToList());
            }
        }


        private void SetInfo(Order order)
        {
            if (order != null)
            {
                Client = order.Client;
                Good = order.Good;
            }
            else
            {
                Client = null;
                Good = null;
            }
        }

        public Client Client
        {
            get { return client; }
            set
            {
                client = value;
                OnPropertyChanged(nameof(Client));
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

        public Order Order
        {
            get { return order; }
            set
            {
                order = value;
                SetInfo(order);
                OnPropertyChanged(nameof(Order));
            }
        }

        public ObservableCollection<Order> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        public RelayCommand RemoveOrder
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
                            await getOrders.Delete(order.Id);

                            MessageBox.Show("Order was succesfully removed.", "Message",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                            List<Order> orders = allOrders.ToList();
                            orders.Remove(order);
                            Orders = new ObservableCollection<Order>(orders);
                            allOrders = Orders;

                            Good = null;
                            Client = null;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Order was not succesfully removed.", "Message",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Operation was succesfully denied", "Message",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }, obj => CanOnClickRemoveOreder());
            }
        }

        private bool CanOnClickRemoveOreder()
        {
            if (order != null && order.Good != null && order.Client != null)
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
