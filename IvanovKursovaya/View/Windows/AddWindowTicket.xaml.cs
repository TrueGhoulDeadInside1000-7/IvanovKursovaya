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
    /// Логика взаимодействия для AddWindowTicket.xaml
    /// </summary>
    public partial class AddWindowTicket : Window
    {

        public Ticket NewTicket { get; set; }
        public AddWindowTicket()
        {
            InitializeComponent();

            // Заполняем ComboBox данными из базы
            TrainingTitle.ItemsSource = App.context.Training.ToList();
            Client.ItemsSource = App.context.Client.ToList();
            DogHandler.ItemsSource = App.context.DogHandler.ToList();

            // Устанавливаем значения по умолчанию
            if (TrainingTitle.Items.Count > 0) TrainingTitle.SelectedIndex = 0;
            if (Client.Items.Count > 0) Client.SelectedIndex = 0;
            if (DogHandler.Items.Count > 0) DogHandler.SelectedIndex = 0;
            DataDP.SelectedDate = DateTime.Now;

            TimeCB.Items.Add("10:00");
            TimeCB.Items.Add("12:00");
            TimeCB.Items.Add("14:00");
            TimeCB.Items.Add("16:00");
            TimeCB.Items.Add("18:00");
            TimeCB.SelectedIndex = 0;
        }

        private void TrainingTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var select = TrainingTitle.SelectedItem;
            if (select != null)
            {
                Training selectedTraining = (Training)select;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataDP.SelectedDate == null
        || TrainingTitle.SelectedItem == null
        || Client.SelectedItem == null
        || DogHandler.SelectedItem == null
        || TimeCB.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            DateTime selectedDate = DataDP.SelectedDate.Value;

            TimeSpan selectedTime =
                TimeSpan.Parse(TimeCB.SelectedItem.ToString());

            DateTime fullDateTime =
                selectedDate.Date + selectedTime;

            Ticket newTicket = new Ticket()
            {
                DateTime = fullDateTime,

                Id_Training =
                    ((Training)TrainingTitle.SelectedItem).Id,

                Id_Client =
                    ((Client)Client.SelectedItem).Id,

                Id_DogHandler =
                    ((DogHandler)DogHandler.SelectedItem).Id
            };

            App.context.Ticket.Add(newTicket);

            App.context.SaveChanges();

            MessageBox.Show("Запись успешно добавлена");

            DialogResult = true;

            this.Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Client_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DogHandler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
