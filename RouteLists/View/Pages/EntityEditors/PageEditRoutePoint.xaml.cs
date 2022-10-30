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

        public PageEditRoutePoint(ref List<RoutePoint> routePoints, RoutePoint routePoint) : this(ref routePoints)
        {
            RoutePoint = routePoint;

            cBoxAction.Text = RoutePoint.Action;
            cBoxCompany.SelectedItem = RoutePoint.Manager.Company;
            cBoxManager.SelectedItem = RoutePoint.Manager;
            txtBoxInvoiceNumber.Text = RoutePoint.InvoiceNumbers;
            txtBoxAddress.Text = RoutePoint.Address;
            txtBoxComment.Text = RoutePoint.Comment;
            
            if (RoutePoint.Cost != null)
                txtBoxCost.Text = RoutePoint.Cost.Value.ToString();
        }

        public override bool EntityRemoved()
        {
            MessageBoxResult result = MessageBox.Show($"Вы действительно хотите точку: {RoutePoint.Address}" +
                $"\n\nДанное действие невозможно отменить!",
                "Удаление точки маршрута", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                int pointID = RoutePoint.PointNumber;

                _routePoints.Remove(RoutePoint);

                if (DatabaseContext.Database.RoutePoints.ToList().Contains(RoutePoint))
                {
                    DatabaseContext.Database.RoutePoints.Remove(RoutePoint);
                }

                for (int i = pointID - 1; i < _routePoints.Count(); i++)
                {
                    _routePoints[i].PointNumber = i + 1;
                }

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

                RoutePoint.PointNumber = _routePoints.Count();
            }

            AppendRoutePointData();

            return true;
        }

        private bool EntityValidated()
        {
            if(cBoxAction.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите действие!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (cBoxCompany.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите компанию!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (cBoxManager.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите менеджера компании!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (txtBoxInvoiceNumber.Text.Replace(" ", "").Replace(Environment.NewLine, "") == string.Empty)
            {
                MessageBox.Show("Укажите номер счёта!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (cBoxAction.SelectedIndex == 1 &&
                ((txtBoxCost.Text.Replace(" ", "") == string.Empty) ||
                !StringValidator.IsCorrectCost(txtBoxCost.Text)))
            {
                MessageBox.Show("Укажите цену товара в форматах\n\nЦена = 0\n\nЦена = 0.00!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (txtBoxAddress.Text.Replace(" ", "").Replace(Environment.NewLine, "") == string.Empty)
            {
                MessageBox.Show("Укажите адрес точки!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (cBoxAction.SelectedIndex == 1)
                RoutePoint.Cost = Convert.ToDecimal(txtBoxCost.Text);
        }

        private void LoadManagersList(object sender, SelectionChangedEventArgs e)
        {
            cBoxManager.ItemsSource = DatabaseContext.Database.Managers.ToList().Where(m =>
            m.Company == cBoxCompany.SelectedItem).ToList();
        }
    }
}
