using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows;

namespace RouteLists.Services
{
    public static class SQLServiceController
    {
        private static ServiceController _service =>
            new ServiceController(ConfigurationManager.AppSettings["ServiceName"]);

        public static ServiceControllerStatus Status() =>
            _service.Status;
        public static void Start()
        {
            try
            {
                if (_service.Status != ServiceControllerStatus.Running)
                {
                    _service.Start();
                }

                while (_service.Status != ServiceControllerStatus.Running)
                {
                    Task.Delay(10);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.InnerException.Message, ex.GetType().ToString(), MessageBoxButton.OK);
                return;
            }
        }
        public static void Stop()
        {
            try
            {
                if (_service.Status == ServiceControllerStatus.Running)
                {
                    _service.Stop();
                }

                while (_service.Status != ServiceControllerStatus.Stopped)
                {
                    Task.Delay(10);
                }
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }
    }
}
