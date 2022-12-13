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

namespace ForeignLanguageSchool
{
    /// <summary>
    /// Логика взаимодействия для ServiceList.xaml
    /// </summary>
    public partial class ServiceList : Page
    {
        public ServiceList()
        {
            InitializeComponent();
            serviceList.ItemsSource = DataBaseConnection.schoolEntities.Service.ToList();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void oldPrice_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock oldPriceText = (TextBlock)sender;
            int indexService = Convert.ToInt32(oldPriceText.Uid);
            Service serviceItem = DataBaseConnection.schoolEntities.Service.FirstOrDefault(x => x.ID == indexService);

            if (serviceItem.Discount != 0)
            {
                oldPriceText.Visibility = Visibility.Visible;
            }
        }

        private void discount_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
