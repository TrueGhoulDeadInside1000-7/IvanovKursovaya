using IvanovKursovaya.Model;
using IvanovKursovaya.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
    /// Логика взаимодействия для TrainingPage.xaml
    /// </summary>
    public partial class TrainingPage : Page
    {
        private List<Training> training = App.context.Training.ToList();
        private Client currentClient;

        public TrainingPage(Client client, DogHandler dogHandler)
        {
            InitializeComponent();
            InfoLV.ItemsSource = training;
            currentClient = client;
            CostFilter.Items.Insert(0, "Больше 2000");
            CostFilter.Items.Insert(1, "Меньше 2000");
            CostFilter.Items.Insert(2, "Все");
            CostFilter.SelectedIndex = 2;
            if (client != null)
            {
                AddBtnTrainings.Visibility = Visibility.Collapsed;
                EditBtnTrainings.Visibility = Visibility.Collapsed;
                DeleteBtnTrainings.Visibility = Visibility.Collapsed;
            }
            else
            {
                BookBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SearchTB.Text))
            {
                InfoLV.ItemsSource =
                    training.Where(u =>
                    u.Title.ToLower().Contains(SearchTB.Text.ToLower())
                    || u.Description.ToLower().Contains(SearchTB.Text.ToLower()));
            }
        }

        private void CostFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            decimal PriceM = 2000;
            if (CostFilter.SelectedIndex == 0)
            {

                InfoLV.ItemsSource =
                    training.Where(u =>
                    u.Price >= PriceM);
            }
            if (CostFilter.SelectedIndex == 1)
            {
                InfoLV.ItemsSource =
    training.Where(u =>
    u.Price <= PriceM);
            }
            if (CostFilter.SelectedIndex == 2)
            {
                InfoLV.ItemsSource = training;
            }
        }

        private void AddBtnTrainings_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            if (addWindow.ShowDialog() == true)
            {
                InfoLV.ItemsSource = null;
                InfoLV.ItemsSource = App.context.Training.ToList();
            }
        }
        private void EditBtnTrainings_Click(object sender, RoutedEventArgs e)
        {
            Training selectedTraining = InfoLV.SelectedItem as Training;

            if (selectedTraining != null)
            {
                AddWindow addWindow = new AddWindow(selectedTraining);
                addWindow.ShowDialog();

                InfoLV.ItemsSource = null;
                InfoLV.ItemsSource = App.context.Training.ToList();
            }
            else
            {
                MessageBox.Show("Выберите тренировку");
            }
        }
        private void DeleteBtnTrainings_Click(object sender, RoutedEventArgs e)
        {
            Training selectedTraining = InfoLV.SelectedItem as Training;

            if (selectedTraining != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Удалить тренировку?",
                    "Подтверждение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    App.context.Training.Remove(selectedTraining);

                    App.context.SaveChanges();

                    InfoLV.ItemsSource = null;
                    InfoLV.ItemsSource = App.context.Training.ToList();

                    MessageBox.Show("Тренировка удалена");
                }
            }
            else
            {
                MessageBox.Show("Выберите тренировку");
            }
        }

        private void BookBtn_Click(object sender, RoutedEventArgs e)
        {
            Training selectedTraining = InfoLV.SelectedItem as Training;

            if (selectedTraining == null)
            {
                MessageBox.Show("Выберите тренировку");
                return;
            }

            BookingWindow bookingWindow =
                new BookingWindow(currentClient, selectedTraining);

            bookingWindow.ShowDialog();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SearchTB.Text))
            {
                InfoLV.ItemsSource =
                    training.Where(u =>
                    u.Title.ToLower().Contains(SearchTB.Text.ToLower())
                    || u.Description.ToLower().Contains(SearchTB.Text.ToLower()));
            }
        }
    }
}
