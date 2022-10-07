using System.Reflection;
using System.Windows;

namespace RouteLists.View.Windows
{
    public partial class WindowMain : Window
    {
        public WindowMain()
        {
            InitializeComponent();

            labelAppVersion.Content = $"v.{Assembly.GetExecutingAssembly().GetName().Version}";
        }
    }
}
