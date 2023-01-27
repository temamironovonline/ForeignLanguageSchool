using System.Windows;

namespace ForeignLanguageSchool
{
    public partial class ExitAdminModeWindow : Window
    {
        private bool _exitResult = false; // Переменная для проверки какую кнопку нажал пользователь
        public ExitAdminModeWindow()
        {
            InitializeComponent();
        }

        private void yesButton_Click(object sender, RoutedEventArgs e) // Событие, если пользователь нажал кнопку "Да"
        {
            _exitResult = true;
            this.DialogResult = true;
        }

        private void noButton_Click(object sender, RoutedEventArgs e) // Событие, если пользователь нажал кнопку "Нет"
        {
            this.DialogResult = true;
        }

        public bool GetExitResult // Свойство, возращающее значение результата выбора пользователя
        {
            get { return _exitResult; }
        }
    }
}
