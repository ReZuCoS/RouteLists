using RouteLists.View.Pages;
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
            PageVehicles page = new PageVehicles();

            mainFrame.Navigate(page);
        }

        private void OpenDriversList(object sender, RoutedEventArgs e)
        {
            PageDrivers page = new PageDrivers();

            mainFrame.Navigate(page);
        }

        private void OpenCompaniesList(object sender, RoutedEventArgs e)
        {
            PageCompanies page = new PageCompanies();

            mainFrame.Navigate(page);
        }

        private void OpenManagersList(object sender, RoutedEventArgs e)
        {
            PageManagers page = new PageManagers();

            mainFrame.Navigate(page);
        }

        private void OnNavigatedClearFrameHistory(object sender, NavigationEventArgs e)
        {
            mainFrame.NavigationService.RemoveBackEntry();
        }
    }
}
