using System.Windows;

namespace ForeignLanguageSchool
{
   
    public partial class ConfirmDeleteService : Window
    {
        public ConfirmDeleteService()
        {
            InitializeComponent();
        }

        private bool _exitResult = false;

        private void confirmDelete_Click(object sender, RoutedEventArgs e)
        {
            _exitResult = true;
            this.Close();
        }

        private void denyDelete_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public bool GetExitResult
        {
            get { return _exitResult; }
        }
    }
}
