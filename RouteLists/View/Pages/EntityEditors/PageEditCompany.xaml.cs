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
            this.Title = "Добавление компании";
        }

        public PageEditCompany(Company company)
        {
            InitializeComponent();
            this.Title = "Изменение компании";
            _company = company;

            txtBoxTitle.Text = _company.Title;
        }

        public override bool EntityRemoved()
        {
            MessageBoxResult result = MessageBox.Show($"Данное действие удалит маршрутные точки и менеджеров, данные о которых " +
                $"содержат наименование данной компании.\n\n" +
                $"Вы действительно хотите удалить компанию: {_company.Title}?\n\nДанное действие невозможно отменить!",
                "Удаление компании", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                List<RoutePoint> routePoints = DatabaseContext.Database.RoutePoints.ToList().Where(rp => rp.Manager.Company == _company).ToList();
                DatabaseContext.Database.RoutePoints.RemoveRange(routePoints);

                List<Manager> managers = DatabaseContext.Database.Managers.ToList().Where(m => m.Company == _company).ToList();
                DatabaseContext.Database.Managers.RemoveRange(managers);

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

            AppendDriverData();

            DatabaseContext.SaveDatabase();
            return true;
        }

        private void AppendDriverData()
        {
            _company.Title = txtBoxTitle.Text;
        }

        private bool EntityValidated()
        {
            if (txtBoxTitle.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Введите наименование компании!");
                return false;
            }

            if (DatabaseContext.Database.Companies.ToList().Any(c =>
                c.Title == txtBoxTitle.Text))
            {
                MessageBox.Show("Компания с таким наименованием уже существует!");
                return false;
            }

            return true;
        }
    }
}
