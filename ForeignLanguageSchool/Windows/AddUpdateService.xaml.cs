using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            services = DataBaseConnection.schoolEntities.Service.ToList();
        }

        Service service;
        List<Service> services;

        public AddUpdateService(Service service)
        {

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
                                    foreach(Service serviceSearch in services)
                                    {
                                        if (serviceSearch.Title == nameService.Text)
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
                                        DataBaseConnection.schoolEntities.SaveChanges();
                                        MessageBox.Show("Успешно добавлено!");
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
