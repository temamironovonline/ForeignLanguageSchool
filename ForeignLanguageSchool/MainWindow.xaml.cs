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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool adminMode = false;
        public MainWindow()
        {
            InitializeComponent();
            DataBaseConnection.schoolEntities = new SchoolEntities();
            FrameClass.forFrameWindow = frameWindow;
            FrameClass.forFrameWindow.Navigate(new ServiceList());
        }

        private void adminModeButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordAdminModeWindow adminWindow = new PasswordAdminModeWindow();

            if (adminWindow.ShowDialog() == true)
            {
                if (adminWindow.Password == "0000")
                {
                    MessageBox.Show("Режим администратора активирован");
                    adminMode = true;
                    adminModeButton.Visibility = Visibility.Collapsed;
                    exitAdminModeButton.Visibility = Visibility.Visible;
                }
                else MessageBox.Show("Неверный пароль!");
            }
        }
        private void exitAdminModeButton_Click(object sender, RoutedEventArgs e)
        {
            ExitAdminModeWindow exitAdminMode = new ExitAdminModeWindow();

            if (exitAdminMode.ShowDialog() == true)
            {
                if (exitAdminMode.GetExitResult)
                {
                    MessageBox.Show("Выход из режима администратора");
                    adminMode = false;
                    exitAdminModeButton.Visibility = Visibility.Collapsed;
                    adminModeButton.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
