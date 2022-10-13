using RouteLists.Model;
using System;
using System.Windows;

namespace RouteLists.ViewModel
{
    internal class DatabaseContext
    {
        internal static ModelRouteListsDB Database = new ModelRouteListsDB();

        public static void SaveDatabase()
        {
            try
            {
                Database.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show("При сохранении базы данных возникла ошибка:\n" + ex.Message +
                    "\n\nВнутренняя ошибка: " + ex.InnerException.InnerException.Message,
                    "Ошибка при сохранении базы данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
