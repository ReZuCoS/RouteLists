using RouteLists.ViewModel;
using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows;

namespace RouteLists.Services
{
    public static class SQLServiceController
    {
        public static ServiceControllerStatus Status =>
            Service.Status;

        public static bool IsRunning =>
            Status == ServiceControllerStatus.Running;

        private static ServiceController Service =>
            new ServiceController(AppSettings.ServiceName);

        public static void Start()
        {
            try
            {
                if (Service.Status != ServiceControllerStatus.Running)
                    Service.Start();

                while (Service.Status != ServiceControllerStatus.Running)
                    Task.Delay(10);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.InnerException.Message, ex.GetType().ToString(),
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return;
            }
        }

        public static void Stop()
        {
            try
            {
                if (Service.Status == ServiceControllerStatus.Running)
                    Service.Stop();

                while (Service.Status != ServiceControllerStatus.Stopped)
                    Task.Delay(10);
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }
    }
}
