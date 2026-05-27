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
    /// Логика взаимодействия для BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        private Client currentClient;
        private Training currentTraining;

        public BookingWindow(Client client, Training training)
        {
            InitializeComponent();

            currentClient = client;
            currentTraining = training;

            TimeCB.Items.Add("10:00");
            TimeCB.Items.Add("12:00");
            TimeCB.Items.Add("14:00");
            TimeCB.Items.Add("16:00");
            TimeCB.Items.Add("18:00");

            TimeCB.SelectedIndex = 0;
        }

        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerTraining.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату");
                return;
            }

            DateTime selectedDate = DatePickerTraining.SelectedDate.Value;

            string selectedTime = TimeCB.SelectedItem.ToString();

            TimeSpan time = TimeSpan.Parse(selectedTime);

            DateTime fullDate = selectedDate.Date + time;

            // Проверка записи
            Ticket existingTicket = App.context.Ticket.FirstOrDefault(u =>
                u.Id_Client == currentClient.Id &&
                u.DateTime == fullDate);

            if (existingTicket != null)
            {
                MessageBox.Show("У вас уже есть запись на это время");
                return;
            }

            DogHandler dogHandler = App.context.DogHandler.FirstOrDefault();

            Ticket ticket = new Ticket()
            {
                Id_Client = currentClient.Id,
                Id_Training = currentTraining.Id,
                Id_DogHandler = dogHandler.Id,
                DateTime = fullDate
            };

            App.context.Ticket.Add(ticket);

            currentClient.Recording_status = true.ToString();

            App.context.SaveChanges();

            MessageBox.Show("Запись успешно оформлена");

            Close();
        }
    }
}