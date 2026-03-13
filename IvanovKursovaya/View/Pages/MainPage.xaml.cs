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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IvanovKursovaya.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(Client client, DogHandler dogHandler)
        {
            InitializeComponent();
            if (client != null)
            {
                LoginTB.Text = $"Логин: {client.Login}";
                EmailTB.Text = $"Почта: {client.Email}";
                PhoneTB.Text = $"Номер телефона: {client.Phone} ";
                NameTB.Text = $"Имя: {client.Name}";
                SurenameTB.Text = $"Фамилия: {client.Surename}";
                PatronymicTB.Text = $"Отчество: {client.Patronymic}";
            }
                if (dogHandler != null)
                {
                LoginTB.Text = $"Логин: {dogHandler.Login}";
                EmailTB.Text = $"Почта: {dogHandler.Email}";
                PhoneTB.Text = $"Номер телефона: {dogHandler.Phone} ";
                NameTB.Text = $"Имя: {dogHandler.Name}";
                SurenameTB.Text = $"Фамилия: {dogHandler.Surename}";
                PatronymicTB.Text = $"Отчество: {dogHandler.Patronymic}";
            }
            }
        }
    }

