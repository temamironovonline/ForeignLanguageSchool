using System.Windows;

namespace ForeignLanguageSchool
{
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
