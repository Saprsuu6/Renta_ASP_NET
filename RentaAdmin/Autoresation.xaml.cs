using RentaAdmin.ViewModels;
using System;
using System.Windows;

namespace RentaAdmin
{
    /// <summary>
    /// Interaction logic for Autoresation.xaml
    /// </summary>
    public partial class Autoresation : Window
    {
        public Autoresation()
        {
            ViewModelAutorisation.enter += new EventHandler<EventArgs>(Enter);
            InitializeComponent();
        }

        private void Enter(object? sender, EventArgs e)
        {
            Owner.Effect = null;
            Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Owner.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }
    }
}
