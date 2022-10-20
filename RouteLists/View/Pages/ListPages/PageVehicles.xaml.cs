using RouteLists.Model;
using RouteLists.View.Pages.EntityEditors;
using RouteLists.View.Windows;
using RouteLists.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RouteLists.View.Pages.ListPages
{
    public partial class PageVehicles : Page
    {
        private List<Vehicle> _vehicles = new List<Vehicle>();

        public PageVehicles()
        {
            InitializeComponent();
            _ = DatabaseContext.Database.PassTypes.ToList();
            UpdateList();
        }

        public void UpdateList()
        {
            _vehicles = DatabaseContext.Database.Vehicles.ToList();

            _vehicles = _vehicles.Where(v => v.Number.ToLower()
            .Replace(" ", String.Empty)
            .Contains(textBoxSearh.Text.ToLower()
            .Replace(" ", String.Empty))).ToList();

            switch (comboBoxPassFilter.SelectedIndex)
            {
                case 1:
                    _vehicles = _vehicles.Where(v => v.VehiclePass != null && v.VehiclePass.ExpireType == VehiclePass.PassExpireType.Valid).ToList();
                    break;

                case 2:
                    _vehicles = _vehicles.Where(v => v.VehiclePass != null && v.VehiclePass.ExpireType == VehiclePass.PassExpireType.StartsExpire).ToList();
                    break;

                case 3:
                    _vehicles = _vehicles.Where(v => v.VehiclePass == null || v.VehiclePass.ExpireType == VehiclePass.PassExpireType.Expired).ToList();
                    break;

                default:
                    break;
            }

            listViewMain.ItemsSource = _vehicles;
        }

        private void EditSelectedVehicle(object sender, MouseButtonEventArgs e)
        {
            Vehicle selectedVehicle = (Vehicle)listViewMain.SelectedItem;

            WindowEntityEditor windowEditor = new WindowEntityEditor(new PageEditVehicle(selectedVehicle), true);

            bool isListUpdated = (bool)windowEditor.ShowDialog();

            if (isListUpdated)
            {
                UpdateList();
            }
        }

        private void AddVehicle(object sender, RoutedEventArgs e)
        {
            WindowEntityEditor windowEditor = new WindowEntityEditor(new PageEditVehicle(), false);

            bool isListUpdated = (bool)windowEditor.ShowDialog();

            if (isListUpdated)
            {
                UpdateList();
            }
        }

        private void ListUpdateOnSearh(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void UpdateListOnFilterChange(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void ClearFocus(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }
    }
}
