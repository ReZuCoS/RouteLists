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
    public partial class PageRouteLists : Page
    {
        private List<RouteList> _routeLists = new List<RouteList>();

        public PageRouteLists()
        {
            InitializeComponent();
            _ = DatabaseContext.Database.Companies.ToList();
            datePickerStart.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            datePickerEnd.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);
            UpdateList();
        }

        public void UpdateList()
        {
            _routeLists = DatabaseContext.Database.RouteLists.ToList();

            _routeLists = _routeLists.Where(rl =>
            rl.Driver.FIO.ToLower().Contains(textBoxSearh.Text.ToLower()) ||
            rl.Vehicle.Number.Replace(" ", "").ToLower().Contains(textBoxSearh.Text.ToLower().Replace(" ", "")) ||
            rl.CompanyNames.ToLower().Contains(textBoxSearh.Text.ToLower()) ||
            rl.ListNumber.Replace(" ", "").ToLower().Contains(textBoxSearh.Text.ToLower())).ToList();

            switch (comboBoxDateFilter.SelectedIndex)
            {
                case 0:
                    var currentMonthRouteLists = _routeLists.Where(rl =>
                    rl.Date.Month == DateTime.Now.Month &&
                    rl.Date.Year == DateTime.Now.Year);

                    _routeLists = UpdateListNumberID(currentMonthRouteLists.ToList());
                    break;

                case 2:
                    var rangeRouteLists = _routeLists.Where(rl =>
                    (rl.Date >= datePickerStart.SelectedDate.Value) &&
                    (rl.Date <= datePickerEnd.SelectedDate.Value));

                    _routeLists = UpdateListNumberID(rangeRouteLists.ToList());
                    break;

                default:
                    _routeLists = UpdateListNumberID(_routeLists);
                    break;
            }

            listViewMain.ItemsSource = _routeLists.OrderBy(rl => rl.Date).ToList();
        }

        private List<RouteList> UpdateListNumberID(List<RouteList> lists)
        {
            List<Vehicle> vehicles = lists.Select(l => l.Vehicle)
                                          .Distinct()
                                          .ToList();
            
            foreach (var vehicle in vehicles)
            {
                int indexer = 1;
                
                foreach (RouteList list in lists.Where(l => l.Vehicle == vehicle)
                                                .OrderBy(l => l.Date)
                                                .ToList())
                {
                    list.ListNumberPerMonth = indexer;
                    indexer++;
                }
            }

            return lists;
        }

        private void AddRouteList(object sender, RoutedEventArgs e)
        {
            WindowEntityEditor windowEditor = new WindowEntityEditor(new PageEditRouteList(), false);

            bool isListUpdated = (bool)windowEditor.ShowDialog();

            if (isListUpdated)
            {
                UpdateList();
            }
        }

        private void EditSelectedRouteList(object sender, MouseButtonEventArgs e)
        {
            RouteList selectedRouteList = (RouteList)listViewMain.SelectedItem;

            WindowEntityEditor windowEditor = new WindowEntityEditor(new PageEditRouteList(selectedRouteList), true);

            bool isListUpdated = (bool)windowEditor.ShowDialog();

            if (isListUpdated)
            {
                UpdateList();
            }
        }

        private void UpdateListOnSearch(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void UpdateListOnFilterChange(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }

        private void ExportSelectedRouteList(object sender, RoutedEventArgs e)
        {
            RouteList routeList = (RouteList)listViewMain.SelectedItem;


        }
    }
}
