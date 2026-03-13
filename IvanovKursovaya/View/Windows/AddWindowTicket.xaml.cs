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

        public AddWindowTicket()
        {
            InitializeComponent();
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

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
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
