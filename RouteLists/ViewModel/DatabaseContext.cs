using RouteLists.Model;
using System;
using System.Data.Entity.Validation;
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
            catch(DbEntityValidationException ex)
            {
                string validationErrors = string.Empty;

                foreach (var ve in ex.EntityValidationErrors)
                {
                    foreach (var vs in ve.ValidationErrors)
                    {
                        validationErrors += $"Property \"{vs.PropertyName}\": {vs.ErrorMessage}\n";
                    }
                }

                MessageBox.Show("При сохранении базы данных произошла ошибка валидации данных:" +
                    "\n\n" + validationErrors,
                    "Ошибка валидации данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show("При сохранении базы данных возникла непредвиденная ошибка:\n" + ex.Message,
                    "Ошибка при сохранении базы данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Database = new ModelRouteListsDB();
            }
        }
    }
}
