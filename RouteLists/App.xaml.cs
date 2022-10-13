using RouteLists.Services;
using System.Diagnostics;
using System.Windows;

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
            //TODO Enable SQL service Stopper
            //SQLServiceController.Stop();

            base.OnExit(e);
        }

        private bool IsApplicationUnique()
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process p in processes)
            {
                if (p.ProcessName == "RouteLists" &&
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
