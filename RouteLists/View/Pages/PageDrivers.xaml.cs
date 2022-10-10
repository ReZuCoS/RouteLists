using RouteLists.Model;
using RouteLists.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RouteLists.View.Pages
{
    public partial class PageDrivers : Page
    {
        private List<Driver> _drivers = new List<Driver>();

        public PageDrivers()
        {
            InitializeComponent();
            UpdateList();
        }

        private void UpdateList()
        {
            _drivers = DatabaseContext.Database.Drivers.ToList();

            _drivers = _drivers.Where(d => d.FIO.ToLower()
            .Contains(textBoxSearh.Text.ToLower())
            ).ToList();

            listViewMain.ItemsSource = _drivers;
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

        private static async void OnClipboardButtonClick(Button button)
        {
            button.IsEnabled = false;

            await Task.Run(() =>
            {
                Thread.Sleep(1000);
            });

            button.IsEnabled = true;
        }

        private void ClearFocus(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }
    }
}
