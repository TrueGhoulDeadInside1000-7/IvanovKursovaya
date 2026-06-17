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
        private List<Ticket> tickets;
        private Client thisClient;
        private DogHandler thisDogHandler;

        public TicketPage(Client client, DogHandler dogHandler)
        {
            InitializeComponent();

            thisClient = client;
            thisDogHandler = dogHandler;

            if (thisClient != null)
            {
                // Страницу открыл клиент
                AddBtn.Visibility = Visibility.Collapsed;
                ChangeBtn.Visibility = Visibility.Collapsed;
            }
            else if (thisDogHandler != null)
            {
                // Страницу открыл кинолог
                AddBtn.Visibility = Visibility.Visible;
                ChangeBtn.Visibility = Visibility.Visible;
            }

            loadTickets();
        }

        private void loadTickets()
        {
            if (thisClient != null)
            {
                // Клиент видит только свои записи
                tickets = App.context.Ticket
                    .Where(x => x.Id_Client == thisClient.Id)
                    .OrderByDescending(x => x.DateTime)
                    .ToList();
            }
            else if (thisDogHandler != null)
            {
                // Кинолог видит весь список записей
                tickets = App.context.Ticket
                    .OrderByDescending(x => x.DateTime)
                    .ToList();
            }
            else
            {
                tickets = new List<Ticket>();
            }

            InfoLV.ItemsSource = null;
            InfoLV.ItemsSource = tickets;
        }
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            Ticket selectedTicket = InfoLV.SelectedItem as Ticket;

            if (selectedTicket != null)
            {
                EditBookingWindow editWindow =
                    new EditBookingWindow(selectedTicket);

                editWindow.ShowDialog();

                loadTickets();
            }
            else
            {
                MessageBox.Show("Выберите запись");
            }
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

                    loadTickets();

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
            if (addWindowTicket.ShowDialog() == true)
            {
                
                
                   loadTickets();

            }
        }
    }
}
