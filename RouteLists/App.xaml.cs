using System.Windows;
using System.Diagnostics;
using RouteLists.Services;
using RouteLists.View.Windows;

namespace RouteLists
{
    public partial class App : Application
    {
        private readonly SQLServerService _sqlServerService = new SQLServerService();

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!IsApplicationUnique())
            {
                App.Current.Shutdown();
                return;
            }
            
            _sqlServerService.Start();

            WindowMain window = new WindowMain();
            window.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _sqlServerService.Stop();

            base.OnExit(e);
        }

        private bool IsApplicationUnique()
        {
            Process[] processes = Process.GetProcesses();

            foreach(Process p in processes)
            {
                if(p.ProcessName == "RouteLists" &&
                   p.Id != Process.GetCurrentProcess().Id)
                {
                    MessageBox.Show("Запущен другой процесс приложения!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return true;
        }
    }
}
