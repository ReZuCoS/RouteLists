using RouteLists.View.Pages.ListPages;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;

namespace RouteLists.View.Windows
{
    public partial class WindowMain : Window
    {
        private readonly PageVehicles _pageVehicles = new PageVehicles();
        private readonly PageDrivers _pageDrivers = new PageDrivers();
        private readonly PageCompanies _pageCompanies = new PageCompanies();
        private readonly PageManagers _pageManagers = new PageManagers();
        private readonly PageRouteLists _pageRouteLists = new PageRouteLists();

        public WindowMain()
        {
            InitializeComponent();

            this.MaxWidth += 16;
            labelAppVersion.Content = $"v.{Assembly.GetExecutingAssembly().GetName().Version}";
            mainFrame.Navigated += OnNavigatedClearFrameHistory;
        }

        private void OpenVehicleList(object sender, RoutedEventArgs e)
        {
            _pageVehicles.UpdateList();
            mainFrame.Navigate(_pageVehicles);
        }

        private void OpenDriversList(object sender, RoutedEventArgs e)
        {
            _pageDrivers.UpdateList();
            mainFrame.Navigate(_pageDrivers);
        }

        private void OpenCompaniesList(object sender, RoutedEventArgs e)
        {
            _pageCompanies.UpdateList();
            mainFrame.Navigate(_pageCompanies);
        }

        private void OpenManagersList(object sender, RoutedEventArgs e)
        {
            _pageManagers.UpdateList();
            mainFrame.Navigate(_pageManagers);
        }

        private void OpenRouteList(object sender, RoutedEventArgs e)
        {
            _pageRouteLists.UpdateList();
            mainFrame.Navigate(_pageRouteLists);
        }

        private void OnNavigatedClearFrameHistory(object sender, NavigationEventArgs e)
        {
            mainFrame.NavigationService.RemoveBackEntry();
        }
    }
}
