using RouteLists.Model;
using RouteLists.ViewModel;
using System.Linq;
using System.Windows;

namespace RouteLists.View.Pages.EntityEditors
{
    public partial class PageEditDriver : EntityPage
    {
        private Driver _driver = null;

        public PageEditDriver()
        {
            InitializeComponent();
            cBoxMainVehicle.ItemsSource = DatabaseContext.Database.Vehicles.ToList();
        }

        public PageEditDriver(Driver driver) : this()
        {
            this.Title = "Изменения данных о водителе";
            _driver = driver;

            txtBoxSurname.Text = _driver.Surname;
            txtBoxName.Text = _driver.Name;
            txtBoxPatronymic.Text = _driver.Patronymic;
            datePickerBithday.SelectedDate = _driver.Bithday;
            txtBoxExperience.Text = _driver.DrivingExperience.ToString();

            if(_driver.Vehicle != null)
                cBoxMainVehicle.SelectedItem = _driver.Vehicle;
        }

        public override bool EntityRemoved()
        {
            MessageBoxResult result = MessageBox.Show($"Данное действие удалит маршрутные листы," +
                $"в которых встречается этот водитель.\n\n" +
                $"Вы действительно хотите удалить водителя: {_driver.Surname} {_driver.Name} {_driver.Patronymic}?" +
                $"\n\nДанное действие невозможно отменить!",
                "Удаление водителя", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                DatabaseContext.Database.Drivers.Remove(_driver);
                DatabaseContext.SaveDatabase();
                return true;
            }

            return false;
        }

        public override bool EntitySaved()
        {
            if (!EntityValidated())
            {
                return false;
            }

            if (_driver == null)
            {
                _driver = new Driver();
                DatabaseContext.Database.Drivers.Add(_driver);
            }

            AppendDriverData();

            DatabaseContext.SaveDatabase();
            return true;
        }

        private void AppendDriverData()
        {
            _driver.Surname = txtBoxSurname.Text;
            _driver.Name = txtBoxName.Text;
            _driver.Patronymic = txtBoxPatronymic.Text;
            _driver.Bithday = datePickerBithday.SelectedDate.Value;
            _driver.DrivingExperience = int.Parse(txtBoxExperience.Text);


            if (cBoxMainVehicle.SelectedItem != null)
            {
                _driver.Vehicle = (Vehicle)cBoxMainVehicle.SelectedItem;
            }
        }

        private bool EntityValidated()
        {
            if (txtBoxSurname.Text.Replace(" ", "") == "" ||
                txtBoxName.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Введите фамилию, имя!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!StringValidator.IsLettersOnly(txtBoxSurname.Text) ||
                !StringValidator.IsLettersOnly(txtBoxName.Text) ||
                (txtBoxPatronymic.Text != "" && !StringValidator.IsLettersOnly(txtBoxPatronymic.Text)))
            {
                MessageBox.Show("Фамилия, имя, отчество должны содержать только буквы!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (datePickerBithday.SelectedDate == null ||
                Driver.GetAgeFromBithday(datePickerBithday.SelectedDate.Value) < 18)
            {
                MessageBox.Show("Возраст водителя должен быть больше или равен 18 годам!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (txtBoxExperience.Text.Length <= 0 ||
                !StringValidator.IsDigitsOnly(txtBoxExperience.Text))
            {
                MessageBox.Show("Введите стаж вождения содержащий только цифры!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
