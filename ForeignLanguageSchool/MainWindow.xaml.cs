using System.Linq;
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
                    FrameClass.forFrameWindow.Navigate(new ServiceList());
                    adminModeButton.Visibility = Visibility.Collapsed;
                    exitAdminModeButton.Visibility = Visibility.Visible;
                    addServiceButton.Visibility = Visibility.Visible;
                    recordList.Visibility = Visibility.Visible;
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
                    FrameClass.forFrameWindow.Navigate(new ServiceList());
                    exitAdminModeButton.Visibility = Visibility.Collapsed;
                    addServiceButton.Visibility = Visibility.Collapsed;
                    recordList.Visibility = Visibility.Collapsed;
                    adminModeButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void addServiceButton_Click(object sender, RoutedEventArgs e)
        {
            AddUpdateService addServiceWindow = new AddUpdateService();
            addServiceWindow.ShowDialog();
            FrameClass.forFrameWindow.Navigate(new ServiceList());
            
        }

        private void recordList_Click(object sender, RoutedEventArgs e)
        {
            RecordListWindow recordListWindow = new RecordListWindow();
            recordListWindow.Show();
        }
    }
}
