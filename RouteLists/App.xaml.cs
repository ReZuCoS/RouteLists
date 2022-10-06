using System.Windows;
using System.Diagnostics;
using RouteLists.Services;

namespace RouteLists
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (!IsApplicationUnique())
            {
                App.Current.Shutdown();
                return;
            }
            
            SQLServiceController.Start();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            SQLServiceController.Stop();

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
