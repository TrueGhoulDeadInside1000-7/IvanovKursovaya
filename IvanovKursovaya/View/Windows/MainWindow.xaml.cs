using IvanovKursovaya.Model;
using IvanovKursovaya.View.Pages;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Client thisclient;
        private DogHandler thisdoghandler;
        public MainWindow(Client client, DogHandler dogHandler)
        {
            InitializeComponent();


            thisclient = client;
            thisdoghandler = dogHandler;
            MainFrm.Navigate(new MainPage(thisclient, thisdoghandler));
            ClientsBtn.Visibility = Visibility.Collapsed;
            if (dogHandler != null)
            {
                ClientsBtn.Visibility = Visibility.Visible;
            }


        }

        private void TrainingBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrm.Navigate(new TrainingPage(thisclient, thisdoghandler));
        }

        private void TicketBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrm.Navigate(new TicketPage(thisclient, thisdoghandler));
        }

        private void MainBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrm.Navigate(new MainPage(thisclient, thisdoghandler));
        }

        private void ClientsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrm.Navigate(new ClientPage());

            
        }
    }
}
