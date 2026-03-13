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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTB.Text) || string.IsNullOrEmpty(EmailTB.Text) || string.IsNullOrEmpty(PhoneTB.Text) || string.IsNullOrEmpty(NameTB.Text) || string.IsNullOrEmpty(SurenameTB.Text) || string.IsNullOrEmpty(PatronymicTB.Text)
                           || string.IsNullOrEmpty(PassRepPB.Password) || string.IsNullOrEmpty(PassFstPB.Password))
            {
                MessageBox.Show("Введите данные");
            }
            else
            {

                if (App.context.Client.Any(u => u.Login == LoginTB.Text || u.Email == EmailTB.Text || u.Phone == PhoneTB.Text) == false)
                {
                    if (PassFstPB.Password == PassRepPB.Password)
                    {
                        Client clientN = new Client()
                        {
                            Login = LoginTB.Text,
                            Email = EmailTB.Text,
                            Password = PassFstPB.Password,
                            Phone = PhoneTB.Text,

                            Name = NameTB.Text,
                            Surename = SurenameTB.Text,
                            Patronymic = PatronymicTB.Text,
                        };

                        App.context.Client.Add(clientN);
                        App.context.SaveChanges();
                        MessageBox.Show("Аккаун создан успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        AutorizationWindow authorization = new AutorizationWindow();
                        authorization.Show();
                        this.Close();
                    }
                    else { MessageBox.Show("Пароли не совпадают"); }

                }
                else { MessageBox.Show("Логин или телефон или почта уже зарегестрированны", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }
    }
}