using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
            
            priceSorting.SelectedIndex = 0;
            discountFiltrating.SelectedIndex = 0;

            serviceList.ItemsSource = DataBaseConnection.schoolEntities.Service.ToList();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private int indexForAllLoadedMethods = 0;

        private void oldPrice_Loaded(object sender, RoutedEventArgs e) // Событие для вывода старой цены, если есть скидка
        {
            TextBlock oldPriceText = (TextBlock)sender;
            indexForAllLoadedMethods = Convert.ToInt32(oldPriceText.Uid);
            Service serviceItem = DataBaseConnection.schoolEntities.Service.FirstOrDefault(x => x.ID == indexForAllLoadedMethods);

            if (serviceItem.Discount != 0)
            {
                oldPriceText.Visibility = Visibility.Visible;
            }
        }

        private void informationService_Loaded(object sender, RoutedEventArgs e) // Событие для вывода цены и времени работы
        {
            TextBlock informationText = (TextBlock)sender;

            indexForAllLoadedMethods = Convert.ToInt32(informationText.Uid);
            Service serviceItem = DataBaseConnection.schoolEntities.Service.FirstOrDefault(x => x.ID == indexForAllLoadedMethods);

            if (serviceItem.Discount != 0)
            {
                informationText.Text = $"{Convert.ToInt32(serviceItem.Cost - (serviceItem.Cost * serviceItem.Discount))} рублей за {serviceItem.DurationInSeconds / 60} минут";
            }
            else
            {
                informationText.Text = $"{serviceItem.Cost} рублей за {serviceItem.DurationInSeconds / 60} минут";
            }


        }

        private void discount_Loaded(object sender, RoutedEventArgs e) // Событие для вывода размера скидки в процентах, если она есть
        {
            TextBlock discountSender = (TextBlock)sender;
            indexForAllLoadedMethods = Convert.ToInt32(discountSender.Uid);

            Service serviceItem = DataBaseConnection.schoolEntities.Service.FirstOrDefault(x => x.ID == indexForAllLoadedMethods);

            int discountPercent = Convert.ToInt32(serviceItem.Discount * 100);
            if (discountPercent != 0)
            {
                discountSender.Text = $"* скидка {discountPercent}%";
                discountSender.Visibility = Visibility.Visible;
            }
        }

        private void updateButton_Loaded(object sender, RoutedEventArgs e) // Событие для показа кнопки редактирования (режим администратора)
        {
            Button updateButtonSender = (Button)sender;
            indexForAllLoadedMethods = Convert.ToInt32(updateButtonSender.Uid);

            if (MainWindow.GetAdminModeStatus)
            {
                updateButtonSender.Visibility = Visibility.Visible;
            }
            else
            {
                updateButtonSender.Visibility = Visibility.Collapsed;
            }
        }

        private void deleteButton_Loaded(object sender, RoutedEventArgs e) // Событие для показа кнопки удаления (режим администратора)
        {
            Button deleteButtonSender = (Button)sender;
            indexForAllLoadedMethods = Convert.ToInt32(deleteButtonSender.Uid);

            if (MainWindow.GetAdminModeStatus)
            {
                deleteButtonSender.Visibility = Visibility.Visible;
            }
            else
            {
                deleteButtonSender.Visibility = Visibility.Collapsed;
            }
        }

        private void DataFiltrationAndSorting()
        {

            List<Service> services;

            // Сортировка по цене
            if (priceSorting.SelectedIndex == 1)
            {
                services = DataBaseConnection.schoolEntities.Service.OrderBy(x => x.Cost).ToList();
            }
            else if (priceSorting.SelectedIndex == 2)
            {
                services = DataBaseConnection.schoolEntities.Service.OrderByDescending(x => x.Cost).ToList();
            }
            else
            {
                services = DataBaseConnection.schoolEntities.Service.ToList();
            }

            //Фильтрация по скидке
            if (discountFiltrating.SelectedIndex == 1)
            {
                services = services.Where(x => (x.Discount*100) < 5).ToList();
            }
            else if (discountFiltrating.SelectedIndex == 2)
            {
                services = services.Where(x => (x.Discount*100) >= 5 && (x.Discount*100) < 15).ToList();
            }
            else if (discountFiltrating.SelectedIndex == 3)
            {
                services = services.Where(x => (x.Discount * 100) >= 15 && (x.Discount * 100) < 30).ToList();
            }
            else if (discountFiltrating.SelectedIndex == 4)
            {
                services = services.Where(x => (x.Discount * 100) >= 30 && (x.Discount * 100) < 70).ToList();
            }
            else if (discountFiltrating.SelectedIndex == 5)
            {
                services = services.Where(x => (x.Discount * 100) >= 70 && (x.Discount * 100) < 100).ToList();
            }

            Regex regexName = new Regex($@".*{nameService.Text.ToLower()}.*");

            services = services.Where(x => regexName.IsMatch(x.Title.ToLower())).ToList();

            Regex regexDescription = new Regex($@".*{descriptionService.Text.ToLower()}.*");

            services = services.Where(x => regexDescription.IsMatch(x.Description.ToLower())).ToList();

            serviceList.ItemsSource = services;
        }


        private void priceSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataFiltrationAndSorting();
        }

        private void discountFiltrating_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataFiltrationAndSorting();
        }

        private void nameService_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataFiltrationAndSorting();
        }

        private void descriptionService_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataFiltrationAndSorting();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            priceSorting.SelectedIndex = 0;
            discountFiltrating.SelectedIndex = 0;
            nameService.Text = "";
            descriptionService.Text = "";
        }

        private void imageService_Loaded(object sender, RoutedEventArgs e) // Загружаем изображения из папки Resources в проекте
        {
            Image image = (Image)sender;
            int index = Convert.ToInt32(image.Uid);

            Service serviceItem = DataBaseConnection.schoolEntities.Service.FirstOrDefault(x => x.ID == index);

            string path = Directory.GetCurrentDirectory(); // Берем путь проекта

            path = path.Replace("bin\\Debug", $"{serviceItem.MainImagePath}"); //Меняем его на путь к Resources
            
            try 
            {
                image.Source = new BitmapImage(new Uri(path)); // Устанавливаем картинку
            }
            catch
            {
                image.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/defaultImage.png")); // Если картинка не найдена, то устанавливаем заглушку
            }
            
        }
    }
}
