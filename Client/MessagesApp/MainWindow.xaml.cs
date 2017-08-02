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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MessagesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public Message Message { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoadMessages();
        }

        private void LoadMessages()
        {
            var s = new MessageService();
            s.LoadMessages(Messages);
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new NewMessageWindow();
            w.ShowDialog();
            if (w.DialogResult.GetValueOrDefault())
            {
                Messages.Insert(0, w.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Delete the message?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var s = new MessageService();
                s.DeleteMessage(Message.Id);
                Messages.Remove(Message);
            }
        }
    }
}
