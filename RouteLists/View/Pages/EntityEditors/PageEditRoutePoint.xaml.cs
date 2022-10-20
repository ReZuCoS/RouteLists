using RouteLists.Model;
using RouteLists.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RouteLists.View.Pages.EntityEditors
{
    public partial class PageEditRoutePoint : EntityPage
    {
        public RoutePoint RoutePoint { get; set; } = null;
        public readonly List<RoutePoint> _routePoints;

        public PageEditRoutePoint(ref List<RoutePoint> routePoints)
        {
            InitializeComponent();
            this.Title = "Добавление маршрутной точки";
            cBoxCompany.ItemsSource = DatabaseContext.Database.Companies.Where(c =>
                c.Managers.Count > 0).ToList();

            _routePoints = routePoints;
        }

        public PageEditRoutePoint(ref List<RoutePoint> routePoints, RoutePoint routePoint)
        {
            InitializeComponent();
            this.Title = "Изменение маршрутной точки";
            cBoxCompany.ItemsSource = DatabaseContext.Database.Companies.Where(c =>
                c.Managers.Count > 0).ToList();

            RoutePoint = routePoint;
            _routePoints = routePoints;

            cBoxAction.SelectedItem = RoutePoint.Action;
            cBoxCompany.SelectedItem = RoutePoint.Manager.Company;
            cBoxManager.SelectedItem = RoutePoint.Manager;
            txtBoxInvoiceNumber.Text = RoutePoint.InvoiceNumbers;
            txtBoxAddress.Text = RoutePoint.Address;
            txtBoxComment.Text = RoutePoint.Comment;
            
            if (RoutePoint.Cost != null)
            {
                txtBoxCost.Text = RoutePoint.Cost.Value.ToString();
            }
        }

        public override bool EntityRemoved()
        {
            MessageBoxResult result = MessageBox.Show($"Вы действительно хотите точку: {RoutePoint.Address}" +
                $"\n\nДанное действие невозможно отменить!",
                "Удаление точки маршрута", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _routePoints.Remove(RoutePoint);
                DatabaseContext.Database.RoutePoints.Remove(RoutePoint);

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

            if (RoutePoint == null)
            {
                RoutePoint = new RoutePoint();
                _routePoints.Add(RoutePoint);
            }

            AppendRoutePointData();

            return true;
        }

        private bool EntityValidated()
        {
            if(cBoxAction.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите действие!");
                return false;
            }

            if (cBoxCompany.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите компанию!");
                return false;
            }

            if (cBoxManager.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите менеджера компании!");
                return false;
            }

            if (txtBoxInvoiceNumber.Text.Replace(" ", "") == string.Empty)
            {
                MessageBox.Show("Укажите номер счёта!");
                return false;
            }

            if (cBoxAction.SelectedIndex == 1 &&
                ((txtBoxCost.Text.Replace(" ", "") == string.Empty) ||
                !StringValidator.IsCorrectCost(txtBoxCost.Text)))
            {
                MessageBox.Show("Укажите цену товара в форматах\n\nЦена = 0\n\nЦена = 0.00!");
                return false;
            }

            if (txtBoxAddress.Text.Replace(" ", "") == string.Empty)
            {
                MessageBox.Show("Укажите адрес точки!");
                return false;
            }

            return true;
        }

        private void AppendRoutePointData()
        {
            RoutePoint.Address = txtBoxAddress.Text;
            RoutePoint.Action = cBoxAction.Text;
            RoutePoint.Manager = (Manager)cBoxManager.SelectedItem;
            RoutePoint.InvoiceNumbers = txtBoxInvoiceNumber.Text;
            RoutePoint.Comment = txtBoxComment.Text;
            RoutePoint.PointNumber = _routePoints.Count();

            if (cBoxAction.SelectedIndex == 1)
            {
                RoutePoint.Cost = Convert.ToDecimal(txtBoxCost.Text);
            }

            return;
        }

        private void LoadManagersList(object sender, SelectionChangedEventArgs e)
        {
            cBoxManager.ItemsSource = DatabaseContext.Database.Managers.ToList().Where(m =>
            m.Company == cBoxCompany.SelectedItem).ToList();
        }
    }
}
