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
    /// Логика взаимодействия для ExitAdminModeWindow.xaml
    /// </summary>
    public partial class ExitAdminModeWindow : Window
    {
        private bool exitResult = false;
        public ExitAdminModeWindow()
        {
            InitializeComponent();
        }

        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            exitResult = true;
            this.DialogResult = true;
        }

        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public bool GetExitResult
        {
            get { return exitResult; }
        }
    }
}
