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
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    {
        public AutorizationWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTB.Text) || string.IsNullOrEmpty(PassPB.Password))
            {
                MessageBox.Show("Введите логин и/или пароль", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var LoginCheckClient = App.context.Client.FirstOrDefault(user => user.Login == LoginTB.Text && user.Password == PassPB.Password);
                var LoginCheckDogHandler = App.context.DogHandler.FirstOrDefault(user => user.Login == LoginTB.Text && user.Password == PassPB.Password);

                if (LoginCheckClient != null || LoginCheckDogHandler != null)
                {
                    MainWindow mainWindow = new MainWindow(LoginCheckClient, LoginCheckDogHandler);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {

                    MessageBox.Show("Неверные логин и/или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RegBtn_Click_1(object sender, RoutedEventArgs e)
        {
            RegistrationWindow mainWindow = new RegistrationWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
