using RouteLists.Model;
using RouteLists.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RouteLists.View.Pages.EntityEditors
{
    public partial class PageEditCompany : EntityPage
    {
        private Company _company;

        public PageEditCompany()
        {
            InitializeComponent();
        }

        public PageEditCompany(Company company) : this()
        {
            this.Title = "Изменение компании";
            _company = company;

            txtBoxTitle.Text = _company.Title;
        }

        public override bool EntityRemoved()
        {
            MessageBoxResult result = MessageBox.Show($"Данное действие удалит маршрутные точки и менеджеров, " +
                $"данные о которых содержат наименование данной компании." +
                $"\n\nВы действительно хотите удалить компанию: {_company.Title}?" +
                $"\n\nДанное действие невозможно отменить!",
                "Удаление компании", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                DatabaseContext.Database.Companies.Remove(_company);
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

            if (_company == null)
            {
                _company = new Company();
                DatabaseContext.Database.Companies.Add(_company);
            }

            AppendCompanyData();

            DatabaseContext.SaveDatabase();
            return true;
        }

        private void AppendCompanyData()
        {
            _company.Title = txtBoxTitle.Text;
        }

        private bool EntityValidated()
        {
            if (txtBoxTitle.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Введите наименование компании!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (DatabaseContext.Database.Companies.ToList().Any(c =>
                c.Title == txtBoxTitle.Text))
            {
                MessageBox.Show("Компания с таким наименованием уже существует!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
