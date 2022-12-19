using System.Windows;

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
