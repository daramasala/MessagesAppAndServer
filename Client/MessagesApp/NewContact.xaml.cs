using MessagesCore;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for NewContact.xaml
    /// </summary>
    public partial class NewContact : Window
    {
        public Contact Contact { get; set; }

        public NewContact()
        {
            InitializeComponent();
            DataContext = this;
            Contact = new Contact();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var s = new MessageService();
            Contact = s.SaveContact(Contact);
            DialogResult = true;
            Close();
        }
    }
}
