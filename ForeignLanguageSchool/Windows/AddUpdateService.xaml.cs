using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Path = System.IO.Path;

namespace ForeignLanguageSchool
{
    /// <summary>
    /// Логика взаимодействия для AddUpdateService.xaml
    /// </summary>
    public partial class AddUpdateService : Window
    {
        
        public AddUpdateService()
        {
            InitializeComponent();
        }

        Service service;
        List<Service> services = DataBaseConnection.schoolEntities.Service.ToList();

        public AddUpdateService(Service service)
        {
            InitializeComponent();
            this.service = service;

            string path = Directory.GetCurrentDirectory(); // Берем путь проекта
            path = path.Replace("bin\\Debug", $"{service.MainImagePath}"); //Меняем его на путь к Resources

            try
            {
                imageService.Source = new BitmapImage(new Uri(path)); // Устанавливаем картинку
            }
            catch
            {
                imageService.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/defaultImage.png")); // Если картинка не найдена, то устанавливаем заглушку
            }

            nameService.Text = service.Title;
            priceService.Text = Convert.ToString(service.Cost);
            durationService.Text = Convert.ToString(service.DurationInSeconds / 60);
            discountService.Text = Convert.ToString(service.Discount*100);
            descriptionService.Text = Convert.ToString(service.Description);

            this.Title = "Изменение услуги";
            addImage.Content = "Изменить изображение";
            addUpdateButton.Content = "Изменить";
        }

        private void addUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameService.Text != "")
            {
                if (priceService.Text != "")
                {
                    if (durationService.Text != "")
                    {
                        if (Convert.ToInt32(durationService.Text) <= 240 && Convert.ToInt32(durationService.Text) > 0)
                        {
                            if (discountService.Text != "")
                            {
                                if (Convert.ToInt32(discountService.Text) <= 100 && Convert.ToInt32(discountService.Text) >= 0)
                                {
                                    bool checkSame = false;
                                    nameService.Text = nameService.Text.Trim();
                                    foreach(Service serviceSearch in services) // Проверка на уже существующую услугу с таким названием
                                    {
                                        if (serviceSearch.Title == nameService.Text && serviceSearch.ID != service.ID)
                                        {
                                            checkSame = true;
                                        }
                                        
                                    }
                                    if (!checkSame)
                                    {
                                        string pathPhoto = "\\Resources\\" + Path.GetFileName(Convert.ToString(imageService.Source));
                                        MessageBox.Show(pathPhoto);
                                        if (pathPhoto == "pack://application:,,,/Resources/defaultImage.png")
                                        {
                                            pathPhoto = null;
                                        }
                                        
                                        if (service != null)
                                        {
                                            service.Title = nameService.Text;
                                            service.Cost = Convert.ToInt32(priceService.Text);
                                            service.DurationInSeconds = Convert.ToInt32(durationService.Text) * 60;
                                            service.Description = descriptionService.Text;
                                            service.Discount = Convert.ToDouble(discountService.Text) / 100;
                                            service.MainImagePath = pathPhoto;
                                        }
                                        else
                                        {
                                            service = new Service()
                                            {
                                                Title = nameService.Text,
                                                Cost = Convert.ToInt32(priceService.Text),
                                                DurationInSeconds = Convert.ToInt32(durationService.Text) * 60,
                                                Description = descriptionService.Text,
                                                Discount = Convert.ToDouble(discountService.Text) / 100,
                                                MainImagePath = pathPhoto
                                            };
                                            DataBaseConnection.schoolEntities.Service.Add(service);
                                        }

                                        DataBaseConnection.schoolEntities.SaveChanges();
                                        MessageBox.Show("Операция выполнена успешно");
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Услуга с таким наименованием уже существует!");
                                    }
                                    
                                }
                                else
                                {
                                    MessageBox.Show("Скидка не может первышать 100 и быть меньше 0");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Длительность услуги не может превышать 4 часов (240 минут)");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Поле с длительностью услуги не может быть пустым!");
                    }
                }
                else
                {
                    MessageBox.Show("Поле со стоимостью услуги не может быть пустым!");
                }
            }
            else
            {
                MessageBox.Show("Поле с наименованием услуги не может быть пустым");
            }
        }

        private void priceService_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex($"[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void durationService_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex($"[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void discountService_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex($"[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void addImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                string nameImage = openFileDialog.FileName; // Полный путь до изображение
                string nameCurrentImage = Path.GetFileName(nameImage); // Получение только названия изображения с расширением
                string path = Directory.GetCurrentDirectory(); // Путь конкретной директории, в которой находится проект
                path = path.Replace("bin\\Debug", "");
                path = path + "Resources\\"+nameCurrentImage; // итоговый путь, по которому перемещать изображение
                File.Copy(nameImage, path, true); // копирование изображения из выбранного места в каталог проекта с возможностью замены уже существующей картинки
                imageService.Source = new BitmapImage(new Uri(path)); // установка изображения
            }
        }
    }
}
