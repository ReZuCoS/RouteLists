using RouteLists.Model;
using RouteLists.View.Windows;
using RouteLists.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RouteLists.View.Pages.EntityEditors
{
    public partial class PageEditRouteList : EntityPage
    {
        private RouteList _routeList = null;
        private List<RoutePoint> _routePoints = new List<RoutePoint>();

        public PageEditRouteList()
        {
            InitializeComponent();
            cBoxDriver.ItemsSource = DatabaseContext.Database.Drivers.ToList();
            cBoxVehicle.ItemsSource = DatabaseContext.Database.Vehicles.ToList();
        }

        public PageEditRouteList(RouteList routeList) : this()
        {
            this.Title = $"Изменение маршрутного листа";
            _routeList = routeList;

            cBoxDriver.SelectedItem = _routeList.Driver;
            cBoxVehicle.SelectedItem = _routeList.Vehicle;
            datePickerListDate.SelectedDate = _routeList.Date;
            _routePoints = _routeList.RoutePoints.ToList();

            UpdateList();
        }

        public override bool EntityRemoved()
        {
            MessageBoxResult result = MessageBox.Show($"Данное действие удалит маршрутный лист и все точки в нём.\n" +
                $"Вы действительно хотите удалить лист: {_routeList.ListNumber}\n\nДанное действие невозможно отменить!",
                "Удаление маршрутного листа", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                DatabaseContext.Database.RouteLists.Remove(_routeList);
                DatabaseContext.SaveDatabase();
                return true;
            }

            return false;
        }

        private void SelectDefaultVehicleForDriver(object sender, SelectionChangedEventArgs e)
        {
            Driver selectedDriver = (Driver)cBoxDriver.SelectedItem;
            
            if(selectedDriver == null)
                return;

            if (selectedDriver.Vehicle != null)
                cBoxVehicle.SelectedItem = selectedDriver.Vehicle;
        }

        public override bool EntitySaved()
        {
            if (!EntityValidated())
            {
                return false;
            }

            if (_routeList == null)
            {
                _routeList = new RouteList();
                DatabaseContext.Database.RouteLists.Add(_routeList);
            }

            AppendRouteListData();

            DatabaseContext.SaveDatabase();
            return true;
        }

        private bool EntityValidated()
        {
            if (cBoxDriver.SelectedItem == null ||
                cBoxVehicle.SelectedItem == null ||
                datePickerListDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите водителя, автомобиль и дату маршрутного листа!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (dataGridRoutePoints.Items.Count == 0)
            {
                MessageBox.Show("Маршрутный лист должен содержать одну или более маршрутных точек!",
                    "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void AppendRouteListData()
        {
            _routeList.Driver = (Driver)cBoxDriver.SelectedItem;
            _routeList.Vehicle = (Vehicle)cBoxVehicle.SelectedItem;
            _routeList.Date = datePickerListDate.SelectedDate.Value;
            _routeList.RoutePoints = _routePoints;
        }

        private void EditSelectedRoutePoint(object sender, MouseButtonEventArgs e)
        {
            RoutePoint routePoint = (RoutePoint)dataGridRoutePoints.SelectedItem;

            WindowEntityEditor windowEditor = new WindowEntityEditor(new PageEditRoutePoint(ref _routePoints, routePoint), true);

            bool isListUpdated = (bool)windowEditor.ShowDialog();

            if (isListUpdated)
                UpdateList();
        }

        private void AddRoutePoint(object sender, RoutedEventArgs e)
        {
            var page = new PageEditRoutePoint(ref _routePoints);

            WindowEntityEditor windowEditor = new WindowEntityEditor(page, false);

            bool isListUpdated = (bool)windowEditor.ShowDialog();

            if (isListUpdated)
                UpdateList();
        }

        private void UpdateList()
        {
            dataGridRoutePoints.ItemsSource = _routePoints.OrderBy(rp => rp.PointNumber).ToList();
        }
    }
}
