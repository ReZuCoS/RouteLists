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
            PageVehicles pageVehicles = new PageVehicles();

            mainFrame.Navigate(pageVehicles);
        }

        private void OnNavigatedClearFrameHistory(object sender, NavigationEventArgs e)
        {
            mainFrame.NavigationService.RemoveBackEntry();
        }
    }
}
