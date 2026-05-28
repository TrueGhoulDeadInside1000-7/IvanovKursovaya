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
    /// Логика взаимодействия для EditBookingWindow.xaml
    /// </summary>
    public partial class EditBookingWindow : Window
    {
        private Ticket currentTicket;
        public EditBookingWindow(Ticket ticket)
        {

            InitializeComponent();

            currentTicket = ticket;

            // Заполнение ComboBox
            TrainingTitle.ItemsSource = App.context.Training.ToList();
            Client.ItemsSource = App.context.Client.ToList();
            DogHandler.ItemsSource = App.context.DogHandler.ToList();

            // Заполняем данные
            DataDP.SelectedDate = currentTicket.DateTime;

            TrainingTitle.SelectedValue = currentTicket.Id_Training;
            Client.SelectedValue = currentTicket.Id_Client;
            DogHandler.SelectedValue = currentTicket.Id_DogHandler;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataDP.SelectedDate == null
                || TrainingTitle.SelectedItem == null
                || Client.SelectedItem == null
                || DogHandler.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            currentTicket.DateTime = DataDP.SelectedDate.Value;

            currentTicket.Id_Training =
                ((Training)TrainingTitle.SelectedItem).Id;

            currentTicket.Id_Client =
                ((Client)Client.SelectedItem).Id;

            currentTicket.Id_DogHandler =
                ((DogHandler)DogHandler.SelectedItem).Id;

            App.context.SaveChanges();

            MessageBox.Show("Запись успешно изменена");

            Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TrainingTitle_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void Client_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void DogHandler_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}