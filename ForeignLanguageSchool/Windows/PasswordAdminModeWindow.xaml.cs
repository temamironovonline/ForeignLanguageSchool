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
using System.Windows.Shapes;

namespace ForeignLanguageSchool
{
    /// <summary>
    /// Логика взаимодействия для PasswordAdminModeWindow.xaml
    /// </summary>
    public partial class PasswordAdminModeWindow : Window
    {
        public PasswordAdminModeWindow()
        {
            InitializeComponent();
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public string Password
        {
            get { return passwordText.Password; }
        }
    }
}
