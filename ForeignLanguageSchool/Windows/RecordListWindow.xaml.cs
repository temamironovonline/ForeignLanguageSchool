using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace ForeignLanguageSchool
{
    public partial class RecordListWindow : Window
    {
        public RecordListWindow()
        {
            InitializeComponent();
            DateTime dateToday = DateTime.Now; // Сегодняшний день
            DateTime dateTomorrow = dateToday.AddDays(2).Date; // Завтрашний день до 00:00
            recordList.ItemsSource = DataBaseConnection.schoolEntities.ClientService.Where(x => x.StartTime > dateToday && x.StartTime < dateTomorrow).OrderBy(x => x.StartTime).ToList(); // Сортировка записей от ближайшего времени
            DispatcherTimer timer = new DispatcherTimer(); // Таймер для обновления страницы каждые 30 секунд
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 30, 0);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e) // Событие таймера обновления страницы
        {
            recordList.Items.Refresh();
        }

        private void remainingTime_Loaded(object sender, RoutedEventArgs e) // Событие при загрузке, которое подсчитывает оставшееся время
        {
            TextBlock remainingTime = (TextBlock)sender;
            int indexRecord = Convert.ToInt32(remainingTime.Uid);

            ClientService clientService = DataBaseConnection.schoolEntities.ClientService.FirstOrDefault(x => x.ID == indexRecord);
            TimeSpan time = clientService.StartTime.Subtract(DateTime.Now); // Подсчет оставшегося времени, путем вычитания текущего времени из даты начала приема

            remainingTime.Text = $"До приема осталось {time.Hours+time.Days*24} час. {time.Minutes} мин.";

            if (time.Hours+time.Days*24 == 0)
            {
                remainingTime.Foreground = Brushes.Red;
            }
        }
    }
}
