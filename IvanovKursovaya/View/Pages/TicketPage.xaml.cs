using IvanovKursovaya.Model;
using IvanovKursovaya.View.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IvanovKursovaya.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для TicketPage.xaml
    /// </summary>
    public partial class TicketPage : Page
    {
        private List<Ticket> ticket = App.context.Ticket.ToList();

        public TicketPage(Client client, DogHandler dogHandler)
        {
            InitializeComponent();
            InfoLV.ItemsSource = ticket;
            if (client != null)
            { 
                AddBtn.Visibility  = Visibility.Collapsed;
                
                ChangeBtn.Visibility  = Visibility.Collapsed;
                var ticketclient = ticket.Where(u=>u.Id_Client == client.Id);
                InfoLV.ItemsSource = ticketclient;
            }
        }


        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            Ticket selectedTicket = InfoLV.SelectedItem as Ticket;

            if (selectedTicket != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Отменить запись?",
                    "Подтверждение",
                    MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    App.context.Ticket.Remove(selectedTicket);

                    App.context.SaveChanges();

                    // Проверяем остались ли записи у клиента
                    var clientTickets = App.context.Ticket
                        .Where(u => u.Id_Client == selectedTicket.Id_Client)
                        .ToList();

                    if (clientTickets.Count == 0)
                    {
                        Client client = App.context.Client
                            .FirstOrDefault(u => u.Id == selectedTicket.Id_Client);

                        if (client != null)
                        {
                            client.Recording_status = false.ToString();

                            App.context.SaveChanges();
                        }
                    }

                    InfoLV.ItemsSource = null;
                    InfoLV.ItemsSource = App.context.Ticket.ToList();

                    MessageBox.Show("Запись отменена");
                }
            }
            else
            {
                MessageBox.Show("Выберите запись");
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddWindowTicket addWindowTicket = new AddWindowTicket();
            addWindowTicket.ShowDialog();
        }
    }
}
