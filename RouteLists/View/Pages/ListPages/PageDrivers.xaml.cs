using RouteLists.Model;
using RouteLists.View.Pages.EntityEditors;
using RouteLists.View.Windows;
using RouteLists.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RouteLists.View.Pages.ListPages
{
    public partial class PageDrivers : Page
    {
        private List<Driver> _drivers = new List<Driver>();

        public PageDrivers()
        {
            InitializeComponent();
            UpdateList();
        }

        public void UpdateList()
        {
            _drivers = DatabaseContext.Database.Drivers.ToList();

            if (textBoxSearh.Text.Any(char.IsDigit) || textBoxSearh.Text.Any(char.IsSeparator))
            {
                _drivers = _drivers.Where(d => d.HasVehicle == true).ToList()
                    .Where(d => d.Vehicle.Number.ToLower()
                    .Replace(" ", String.Empty)
                    .Contains(textBoxSearh.Text.ToLower()
                    .Replace(" ", String.Empty))).ToList();
            }
            else
            {
                _drivers = _drivers.Where(d =>
                    d.FIO.ToLower()
                    .Contains(textBoxSearh.Text.ToLower())
                    ).ToList();
            }

            listViewMain.ItemsSource = _drivers;
        }

        private void EditSelectedDriver(object sender, MouseButtonEventArgs e)
        {
            Driver selectedDriver = (Driver)listViewMain.SelectedItem;

            WindowEntityEditor windowEditor = new WindowEntityEditor(new PageEditDriver(selectedDriver), true);

            bool isListUpdated = (bool)windowEditor.ShowDialog();

            if (isListUpdated)
            {
                UpdateList();
            }
        }

        private void AddDriver(object sender, RoutedEventArgs e)
        {
            WindowEntityEditor windowEditor = new WindowEntityEditor(new PageEditDriver(), false);

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

        private void CopyCarNumber(object sender, RoutedEventArgs e)
        {
            Driver driver = (Driver)listViewMain.SelectedItem;

            Clipboard.SetText(driver.Vehicle.Number);

            OnClipboardButtonClick((Button)sender);
        }

        private async void OnClipboardButtonClick(Button button)
        {
            button.IsEnabled = false;

            await Task.Run(() => Thread.Sleep(1000));

            button.IsEnabled = true;
        }

        private void ClearFocus(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        //TODO Сделать отчёт по водителям
        private void ShowDriverReport(object sender, RoutedEventArgs e)
        {
            Driver selectedDriver = (Driver)listViewMain.SelectedItem;

            WindowEntityEditor windowEditor = new WindowEntityEditor(new PageEditDriver(selectedDriver), true, true);

            bool isListUpdated = (bool)windowEditor.ShowDialog();

            if (isListUpdated)
            {
                UpdateList();
            }
        }
    }
}
