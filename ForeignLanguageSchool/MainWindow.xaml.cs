using System.Windows;

namespace ForeignLanguageSchool
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            DataBaseConnection.schoolEntities = new SchoolEntities();
            FrameClass.forFrameWindow = frameWindow;
            FrameClass.forFrameWindow.Navigate(new ServiceList());
        }

        public static bool adminMode = false;

        public static bool GetAdminModeStatus
        {
            get { return adminMode; }
        }

        private void adminModeButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordAdminModeWindow adminWindow = new PasswordAdminModeWindow();

            if (adminWindow.ShowDialog() == true)
            {
                if (adminWindow.Password == "0000")
                {
                    adminMode = true;
                    FrameClass.forFrameWindow.Refresh();
                    adminModeButton.Visibility = Visibility.Collapsed;
                    exitAdminModeButton.Visibility = Visibility.Visible;
                    addServiceButton.Visibility = Visibility.Visible;
                }
                else MessageBox.Show("Неверный пароль!");
            }
            else
            {
                MessageBox.Show("Ошибка при входе в режим администратора!");
            }
        }
        private void exitAdminModeButton_Click(object sender, RoutedEventArgs e)
        {
            ExitAdminModeWindow exitAdminMode = new ExitAdminModeWindow();

            if (exitAdminMode.ShowDialog() == true)
            {
                if (exitAdminMode.GetExitResult)
                {
                    adminMode = false;
                    FrameClass.forFrameWindow.Refresh();
                    exitAdminModeButton.Visibility = Visibility.Collapsed;
                    adminModeButton.Visibility = Visibility.Visible;
                    addServiceButton.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Ошибка при выходе из режима администратора!");
            }
        }

        private void addServiceButton_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateService addUpdateWindow = new AddUpdateService();

            if (addUpdateWindow.ShowDialog() == true)
            {
                FrameClass.forFrameWindow.Refresh();
            }
            else
            {
                MessageBox.Show("При открытии окна с добавлением услуги произошла ошибка!");
            }
        }
    }
}
