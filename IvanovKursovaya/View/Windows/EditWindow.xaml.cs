using IvanovKursovaya.Model;
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

namespace IvanovKursovaya.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Ticket editingTicket;
        public event Action<Ticket> TicketChanged;
        public EditWindow(Ticket ticket)
        {
            InitializeComponent();
            editingTicket = ticket;

            TrainingTitle.ItemsSource = App.context.Training.ToList();
            Client.ItemsSource = App.context.Client.ToList();
            DogHandler.ItemsSource = App.context.DogHandler.ToList();


            NameTB.Text = ticket.DateTime.ToString();
            TrainingTitle.SelectedItem = App.context.Training.FirstOrDefault(t => t.Id == ticket.Id_Training);
            Client.SelectedItem = App.context.Client.FirstOrDefault(c => c.Id == ticket.Id_Client);
            DogHandler.SelectedItem = App.context.DogHandler.FirstOrDefault(d => d.Id == ticket.Id_DogHandler);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(NameTB.Text, out DateTime dt))
            {
                editingTicket.DateTime = dt;
            }

            App.context.SaveChanges(); // сохраняем изменения в базе
            this.DialogResult = true;
            this.Close();
        }

        private void Client_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (editingTicket != null && Client.SelectedItem != null)
            {
                var selectedClient = (Client)Client.SelectedItem;
                editingTicket.Id_Client = selectedClient.Id;
                editingTicket.Client = selectedClient; // обновляем навигационное свойство
                TicketChanged?.Invoke(editingTicket);
            }
        }

        private void TrainingTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (editingTicket != null && TrainingTitle.SelectedItem != null)
            {
                var selectedTraining = (Training)TrainingTitle.SelectedItem;
                editingTicket.Id_Training = selectedTraining.Id;
                editingTicket.Training = selectedTraining; // обновляем навигационное свойство
                TicketChanged?.Invoke(editingTicket);
            }
        }

        private void DogHandler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (editingTicket != null && DogHandler.SelectedItem != null)
            {
                var selectedHandler = (DogHandler)DogHandler.SelectedItem;
                editingTicket.Id_DogHandler = selectedHandler.Id;
                editingTicket.DogHandler = selectedHandler; // обновляем навигационное свойство
                TicketChanged?.Invoke(editingTicket);
            }
        }
    }
}
