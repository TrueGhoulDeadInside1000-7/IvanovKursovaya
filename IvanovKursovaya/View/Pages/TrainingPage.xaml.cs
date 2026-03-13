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
        public TrainingPage(Client client, DogHandler dogHandler)
        {
            InitializeComponent();
            InfoLV.ItemsSource = training;
            CostFilter.Items.Insert(0, "Больше 2000");
            CostFilter.Items.Insert(1, "Меньше 2000");
            if (client != null)
            {
                AddBtnTrainings.Visibility = Visibility.Collapsed;
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
                decimal PriceM= 2000 ;
            if(CostFilter.SelectedIndex == 0) 
            {
                
                InfoLV.ItemsSource = 
                    training.Where(u => 
                    u.Price>=PriceM );
            }
            if (CostFilter.SelectedIndex == 1)
            {
                InfoLV.ItemsSource =
    training.Where(u =>
    u.Price <= PriceM);
            }
        }

        private void AddBtnTrainings_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.ShowDialog();
        }
    }
}
