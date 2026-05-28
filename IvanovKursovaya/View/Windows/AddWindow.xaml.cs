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
        private Training currentTraining;

        public AddWindow()
        {
            InitializeComponent();
        }

        public AddWindow(Training training)
        {
            InitializeComponent();

            currentTraining = training;

            TitleTB.Text = training.Title;
            DescriptionTB.Text = training.Description;
            PriceTB.Text = training.Price.ToString();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TitleTB.Text) ||
                string.IsNullOrEmpty(PriceTB.Text) ||
                string.IsNullOrEmpty(DescriptionTB.Text))
            {
                MessageBox.Show("Введите данные",
                    "Информация",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                decimal price;

                if (!decimal.TryParse(PriceTB.Text, out price))
                {
                    MessageBox.Show("Введите корректную цену");
                    return;
                }

                // ДОБАВЛЕНИЕ
                if (currentTraining == null)
                {
                    if (App.context.Training.FirstOrDefault(u => u.Title == TitleTB.Text) != null)
                    {
                        MessageBox.Show("Такое название уже существует");
                    }
                    else
                    {
                        Training training = new Training()
                        {
                            Title = TitleTB.Text,
                            Description = DescriptionTB.Text,
                            Price = price
                        };

                        App.context.Training.Add(training);
                        App.context.SaveChanges();

                        MessageBox.Show("Курс успешно добавлен");
                        DialogResult = true;
                        this.Close();

                    }
                }

                // РЕДАКТИРОВАНИЕ
                else
                {
                    currentTraining.Title = TitleTB.Text;
                    currentTraining.Description = DescriptionTB.Text;
                    currentTraining.Price = price;

                    App.context.SaveChanges();

                    MessageBox.Show("Данные успешно изменены");

                    this.Close();
                }
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}