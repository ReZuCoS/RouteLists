using System;
using System.Linq;
using RouteLists.ViewModel;

namespace RouteLists.Services
{
    internal static class DriverExpUpdateService
    {
        public static void Sync()
        {
            if (!IsYearChanged())
                return;
            
            UpdateDriverExp();
            UpdateSyncDate(DateTime.Now);

            DatabaseContext.SaveDatabase();
        }

        private static bool IsYearChanged()
        {
            int lastYearSync = int.Parse(AppSettings.SyncYear);
            return lastYearSync < DateTime.Now.Year;
        }

        private static void UpdateSyncDate(DateTime date)
        {
            AppSettings.SyncYear = date.ToString("yyyy");
        }

        private static void UpdateDriverExp()
        {
            DatabaseContext.Database.Drivers.ToList().ForEach(d => d.DrivingExperience += 1);
        }
    }
}
