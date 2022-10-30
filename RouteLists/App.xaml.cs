using RouteLists.Services;
using RouteLists.ViewModel;
using System;
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
                Environment.Exit(0);
                return;
            }

            if (!SQLServiceController.IsRunning)
            {
                SQLServiceController.Start();
                return;
            }

            DriverExpUpdateService.Sync();
            
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (AppSettings.DisableSQLServerOnExit)
                SQLServiceController.Stop();

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
                    MessageBoxResult result = MessageBox.Show("Запущен другой процесс приложения!\n" +
                        "Завершить работу другого процесса?", "Ошибка",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        p.Kill();
                        return true;
                    }

                    return false;
                }
            }

            return true;
        }
    }
}
