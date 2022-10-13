using RouteLists.View.Pages.ListPages;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;

namespace RouteLists.View.Windows
{
    public partial class WindowMain : Window
    {
        public WindowMain()
        {
            InitializeComponent();

            this.MaxWidth += 16;
            labelAppVersion.Content = $"v.{Assembly.GetExecutingAssembly().GetName().Version}";
            mainFrame.Navigated += OnNavigatedClearFrameHistory;
        }

        private void OpenVehicleList(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new PageVehicles());
        }

        private void OpenDriversList(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new PageDrivers());
        }

        private void OpenCompaniesList(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new PageCompanies());
        }

        private void OpenManagersList(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new PageManagers());
        }

        private void OnNavigatedClearFrameHistory(object sender, NavigationEventArgs e)
        {
            mainFrame.NavigationService.RemoveBackEntry();
        }
    }
}
