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
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TitleTB.Text) || string.IsNullOrEmpty(PriceTB.Text) || string.IsNullOrEmpty(DescriptionTB.Text))
            {
                MessageBox.Show("Введите данные", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (App.context.Training.FirstOrDefault(u=>u.Title == TitleTB.Text) != null) 
                {
                    MessageBox.Show("Такое название уже существует");
                }    
                else 
                {
                    Training training = new Training() 
                    {
                        Title = TitleTB.Text,
                        Description = DescriptionTB.Text,
                        Price = Convert.ToDecimal(PriceTB.Text)
                    };
                    App.context.Training.Add(training);
                    App.context.SaveChanges();
                    MessageBox.Show("Курс успешно добавлен");
                }
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
