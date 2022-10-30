using RouteLists.Model;
using RouteLists.ViewModel;
using System.Linq;
using System.Windows;

namespace RouteLists.View.Pages.EntityEditors
{
    public partial class PageEditManager : EntityPage
    {
        private Manager _manager = null;

        public PageEditManager()
        {
            InitializeComponent();
            cBoxCompany.ItemsSource = DatabaseContext.Database.Companies.ToList();
        }

        public PageEditManager(Manager manager) : this()
        {
            this.Title = "Изменение данных менеджера";
            _manager = manager;

            txtBoxName.Text = manager.Name;
            txtBoxSurname.Text = manager.Surname;
            txtBoxPatronymic.Text = manager.Patronymic;
            txtBoxPhone.Text = manager.Phone;
            cBoxCompany.SelectedItem = manager.Company;
        }

        public override bool EntityRemoved()
        {
            MessageBoxResult result = MessageBox.Show($"Данное действие удалит все маршрутные точки в которых" +
                $"встречается этот менеджер.\n\n" +
                $"Вы действительно хотите удалить менеджера: {_manager.Name} из компании " +
                $"{_manager.Company.Title}?\n\nДанное действие невозможно отменить!",
                "Удаление менджера", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                DatabaseContext.Database.Managers.Remove(_manager);
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

            if (_manager == null)
            {
                _manager = new Manager();
                DatabaseContext.Database.Managers.Add(_manager);
            }

            AppendManagerData();

            DatabaseContext.SaveDatabase();
            return true;
        }

        private void AppendManagerData()
        {
            _manager.Surname = txtBoxSurname.Text;
            _manager.Name = txtBoxName.Text;
            _manager.Patronymic = txtBoxPatronymic.Text;
            _manager.Phone = Manager.GetPhoneFromString(txtBoxPhone.Text);
            _manager.Company = (Company)cBoxCompany.SelectedItem;
        }

        private bool EntityValidated()
        {
            if (txtBoxName.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Введите имя менеджера!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!StringValidator.IsLettersOnly(txtBoxName.Text) ||
                (txtBoxSurname.Text != "" && !StringValidator.IsLettersOnly(txtBoxSurname.Text)) ||
                (txtBoxPatronymic.Text != "" && !StringValidator.IsLettersOnly(txtBoxPatronymic.Text)))
            {
                MessageBox.Show("Фамилия, имя, отчество должны содержать только буквы!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!StringValidator.IsCorrectPhoneNumber(txtBoxPhone.Text))
            {
                MessageBox.Show("Телефонный номер должен иметь формат +7 *** *** ** **!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (cBoxCompany.SelectedItem == null)
            {
                MessageBox.Show("Выберите компанию менеджера!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
