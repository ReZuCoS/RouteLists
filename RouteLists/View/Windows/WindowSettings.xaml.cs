using RouteLists.ViewModel;
using System.Windows;

namespace RouteLists.View.Windows
{
    public partial class WindowSettings : Window
    {
        public WindowSettings()
        {
            InitializeComponent();

            checkBoxDisableSQLService.IsChecked = AppSettings.DisableSQLServerOnExit;
            txtBoxSurname.Text = AppSettings.UserSurname;
            txtBoxName.Text = AppSettings.UserName;
            txtBoxPatronymic.Text = AppSettings.UserPatronymic;
            txtBoxCompanyName.Text = AppSettings.UserCompanyName;
            txtBoxPosition.Text = AppSettings.UserPosition;
        }

        private void SaveSettings(object sender, RoutedEventArgs e)
        {
            if (!SettingsValidated())
                return;

            AppSettings.DisableSQLServerOnExit = checkBoxDisableSQLService.IsChecked.Value;
            AppSettings.UserSurname = txtBoxSurname.Text;
            AppSettings.UserName = txtBoxName.Text;
            AppSettings.UserPatronymic = txtBoxPatronymic.Text;
            AppSettings.UserCompanyName = txtBoxCompanyName.Text;
            AppSettings.UserPosition = txtBoxPosition.Text;

            this.DialogResult = true;
        }

        private bool SettingsValidated()
        {
            if (txtBoxSurname.Text.Replace(" ", "") == "" ||
                txtBoxName.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Введите фамилию, имя!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (txtBoxCompanyName.Text.Replace(" ", "") == "" ||
                txtBoxPosition.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Введите название компании, должность!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
