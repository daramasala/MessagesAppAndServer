using MessagesCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MessagesApp
{
    /// <summary>
    /// Interaction logic for AddressBook.xaml
    /// </summary>
    public partial class AddressBook : Window
    {
        public Contact Contact { get; set; }
        public ObservableCollection<Contact> Contacts { get; set; }
        readonly MessageService service = new MessageService();
        public AddressBook()
        {
            InitializeComponent();
            DataContext = this;
            Contacts = new ObservableCollection<Contact>(service.LoadContacts());
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Contacts.Clear();
            var filtered = service.LoadContacts((sender as TextBox).Text);
            foreach (var c in filtered)
            {
                Contacts.Add(c);
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new NewContact();
            var result = w.ShowDialog();
            if (result.GetValueOrDefault())
            {
                Contacts.Add(w.Contact);
            }
        }
    }
}
