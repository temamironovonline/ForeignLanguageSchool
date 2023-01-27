using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace ForeignLanguageSchool
{

    public partial class RecordClientWindow : Window
    {
        Service service;
        public RecordClientWindow(Service service)
        {
            InitializeComponent();

            this.service = service;

            nameService.Text = $"{service.Title} за {service.DurationInSeconds/60} минут";

            clients.Items.Add("Не выбран");
            clients.SelectedIndex = 0;

            List<Client> clientsList = DataBaseConnection.schoolEntities.Client.ToList();

            foreach (Client client in clientsList)
            {
                clients.Items.Add($"{client.LastName} {client.FirstName} {client.Patronymic}");
            }
        }

        private Regex regexTime = new Regex("(^[0-9]:[0-5][0-9]$)|(^[1][0-9]:[0-5][0-9]$)|(^[2][0-3]:[0-5][0-9]$)"); // Регулярное выражение на проверку вводимого времени

        private void recordClient_Click(object sender, RoutedEventArgs e)
        {
            if (clients.SelectedIndex != 0)
            {
                if (dateRecord.Text != "")
                {
                    if (regexTime.IsMatch(timeRecord.Text))
                    {
                        string time = $"{dateRecord.Text} {timeRecord.Text}";
                        DateTime dateTime = DateTime.Parse(time);
                        if (dateTime > DateTime.Now)
                        {
                            ClientService clientService = new ClientService()
                            {
                                ClientID = clients.SelectedIndex,
                                ServiceID = service.ID,
                                StartTime = dateTime,
                                Comment = ""
                            };
                            DataBaseConnection.schoolEntities.ClientService.Add(clientService);
                            DataBaseConnection.schoolEntities.SaveChanges();
                            MessageBox.Show("Клиент успешно записан на услугу!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Укажите время, которое больше нынешнего!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Формат времени введен неправильно (ЧЧ:ММ) или поле для ввода пустое");
                    }
                }
                else
                {
                    MessageBox.Show("Укажите дату!");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберете клиента!");
            }
        }

        private void timeRecord_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            SetTextToEndtimeService();
            
        }

        private void dateRecord_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SetTextToEndtimeService();
        }

        private void SetTextToEndtimeService()
        {
            endtimeService.Text = "";

            if (dateRecord.Text != "")
            {
                if (regexTime.IsMatch(timeRecord.Text))
                {
                    string time = $"{dateRecord.Text} {timeRecord.Text}";
                    DateTime dateTime = DateTime.Parse(time);
                    dateTime = dateTime.AddSeconds(Convert.ToInt32(service.DurationInSeconds));
                    if (dateTime > DateTime.Now)
                    {
                        endtimeService.Text = $"Дата окончания услуги {dateTime}";
                    }   
                }
            }
        }
    }
}
