using MessagesCore;
using System.Windows;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MessagesApp
{
    /// <summary>
    /// Interaction logic for NewMessageWindow.xaml
    /// </summary>
    public partial class NewMessageWindow : Window
    {
        public Message Message { get; set; } = new Message();

        public ObservableCollection<Contact> Recipients { get; set; } = 
            new ObservableCollection<Contact>();

        public NewMessageWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Message.Subject))
            {
                MessageBox.Show("Subject is empty");
                return;
            }
            //if (!Message.Recipients.Any())
            //{
            //    MessageBox.Show("Recipients are empty");
            //    return;
            //}
            Save();
            DialogResult = true;
            Close();
        }

        private void Save()
        {
            var s = new MessageService();
            Message.Recipients = new List<Contact>(Recipients);
            Message = s.SaveMessage(Message);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (DialogResult.GetValueOrDefault())
            { return; }
            var result = MessageBox.Show("Discard message?", "Close new message", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new AddressBook();
            w.ShowDialog();
            if (w.DialogResult.GetValueOrDefault())
            {
                Recipients.Add(w.Contact);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
