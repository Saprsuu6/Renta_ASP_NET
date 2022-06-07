using System;
using System.Windows;
using System.Windows.Media.Effects;

namespace RentaAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static event EventHandler<EventArgs> updateClients = null;
        public static event EventHandler<EventArgs> updateOreders = null;
        public static event EventHandler<EventArgs> updateContacts = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Effect = new BlurEffect();

            var autorisation = new Autoresation();
            autorisation.Owner = this;
            autorisation.ShowDialog();
        }

        private void NameTemplate_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string template = NameTemplate.Text;
            updateClients?.Invoke(template, new EventArgs());
        }

        private void DateTemplate_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string template = DateTemplate.Text;
            updateOreders?.Invoke(template, new EventArgs());
        }

        private void ContactsTemplate_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string template = ContactsTemplate.Text;
            updateContacts?.Invoke(template, new EventArgs());
        }
    }
}
