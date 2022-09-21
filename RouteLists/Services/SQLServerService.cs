using System;
using System.Windows;
using System.Configuration;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace RouteLists.Services
{
    internal class SQLServerService
    {
        private ServiceController _service =>
            new ServiceController(ConfigurationManager.AppSettings["ServiceName"]);

        public ServiceControllerStatus Status()
        {
            return _service.Status;
        }

        public void Start()
        {
            try
            {
                _service.Start();

                while (_service.Status != ServiceControllerStatus.Running)
                {
                    Task.Delay(10);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButton.OK);
                return;
            }
        }

        public void Stop()
        {
            try
            {
                _service.Stop();

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
