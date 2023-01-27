using System.Windows;

namespace ForeignLanguageSchool
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataBaseConnection.schoolEntities = new SchoolEntities();
            FrameClass.forFrameWindow = frameWindow;
            FrameClass.forFrameWindow.Navigate(new ServiceList());
        }

        private static bool _adminMode = false; // Переменная для проверки входа в режим администратора

        public static bool GetAdminModeStatus // Получение статуса администратора для включения/выключения функций в приложении
        {
            get { return _adminMode; }
        }

        private void adminModeButton_Click(object sender, RoutedEventArgs e) // Событие при нажатии на кнопку входа в режим администратора
        {
            PasswordAdminModeWindow adminWindow = new PasswordAdminModeWindow(); // Окно для ввода пароля

            if (adminWindow.ShowDialog() == true)
            {
                // Если пароль совпадает, то перезагружаем страницу и выключаем кнопку "Войти в режим администратора"
                // и включаем кнопки "Добавить услугу", "Список записей" и "Выйти из режима администратора"
                if (adminWindow.Password == "0000")
                {
                    _adminMode = true; // Администратор мод включен
                    FrameClass.forFrameWindow.Navigate(new ServiceList());
                    adminModeButton.Visibility = Visibility.Collapsed;
                    exitAdminModeButton.Visibility = Visibility.Visible;
                    addServiceButton.Visibility = Visibility.Visible;
                    recordList.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Неверный пароль!");
                }
            }
        }
        private void exitAdminModeButton_Click(object sender, RoutedEventArgs e) // Событие при нажатии на кнопку выхода из режима администратора
        {
            ExitAdminModeWindow exitAdminMode = new ExitAdminModeWindow(); // Окно для выхода из режима администратора

            if (exitAdminMode.ShowDialog() == true)
            {
                // Если пользователь вышел и режима администратора, то перезагружаем страницу и выключаем кнопки
                // "Добавить услугу", "Список записей" и "Выйти из режима администратора"
                // и включаем кнопку "Войти в режим администратора"
                if (exitAdminMode.GetExitResult)
                {
                    _adminMode = false; // Админ мод выключен
                    FrameClass.forFrameWindow.Navigate(new ServiceList());
                    exitAdminModeButton.Visibility = Visibility.Collapsed;
                    addServiceButton.Visibility = Visibility.Collapsed;
                    recordList.Visibility = Visibility.Collapsed;
                    adminModeButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void addServiceButton_Click(object sender, RoutedEventArgs e) // Событие при нажатии на кнопку "Добавить услугу"
        {
            AddUpdateService addServiceWindow = new AddUpdateService(); // Окно с формой добавления услуги
            addServiceWindow.ShowDialog();
            FrameClass.forFrameWindow.Navigate(new ServiceList()); // Обновление страницы сразу после закрытия окна с добавлением
            
        }

        private void recordList_Click(object sender, RoutedEventArgs e) // Событие при нажатии на кнопку "Список записей"
        {
            RecordListWindow recordListWindow = new RecordListWindow(); // Окно со списком ближайших записей
            recordListWindow.Show(); // Позволяет работать с приложением дальше при открытом новом окне
        }
    }
}
