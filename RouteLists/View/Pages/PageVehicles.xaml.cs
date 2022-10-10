using RouteLists.Model;
using RouteLists.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace RouteLists.View.Pages
{
    public partial class PageVehicles : Page
    {
        private List<Vehicle> _vehicles = new List<Vehicle>();

        public PageVehicles()
        {
            InitializeComponent();
            UpdateList();
        }
        
        private void ListUpdateOnSearh(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void UpdateListOnFilterChange(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }
         
        private void UpdateList()
        {
            _vehicles = DatabaseContext.Database.Vehicles.ToList();

            _vehicles = _vehicles.Where(v => v.Number.ToLower()
            .Replace(" ", String.Empty)
            .Contains(textBoxSearh.Text.ToLower()
            .Replace(" ", String.Empty))).ToList();

            switch (comboBoxPassFilter.SelectedIndex)
            {
                case 1:
                    _vehicles = _vehicles.Where(v => v.VehiclePass.ExpireType == VehiclePass.PassExpireType.Valid).ToList();
                    break;

                case 2:
                    _vehicles = _vehicles.Where(v => v.VehiclePass.ExpireType == VehiclePass.PassExpireType.StartsExpire).ToList();
                    break;

                case 3:
                    _vehicles = _vehicles.Where(v => v.VehiclePass.ExpireType == VehiclePass.PassExpireType.Expired).ToList();
                    break;

                default:
                    break;
            }

            listViewMain.ItemsSource = _vehicles;
        }

        private void ClearFocus(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }
    }
}
