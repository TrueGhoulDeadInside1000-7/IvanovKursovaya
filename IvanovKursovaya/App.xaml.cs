using IvanovKursovaya.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IvanovKursovaya
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IvanovKinologNormEntities context = new IvanovKinologNormEntities();
        public static Client client = new Client();
        public static DogHandler dogHandler = new DogHandler();
    }
}
