using RouteLists.Model;
using RouteLists.View.Pages.EntityEditors;
using RouteLists.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace RouteLists.View.Pages.ReportPages
{
    public partial class DriverReportPage : EntityPage
    {
        private readonly Driver _driver;

        public DriverReportPage(Driver driver)
        {
            InitializeComponent();

            _driver = driver;
            labelDriverName.Content = driver.FIO;
            datePickerStart.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            datePickerEnd.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);

            UpdateData();
        }

        private void UpdateData()
        {
            if (datePickerStart.SelectedDate == null ||
                datePickerEnd.SelectedDate == null)
                return;

            List<RouteList> routeLists = DatabaseContext.Database.RouteLists.ToList().Where(rl =>
                rl.Driver == _driver &
                rl.Date >= datePickerStart.SelectedDate.Value &
                rl.Date <= datePickerEnd.SelectedDate.Value).ToList();

            labelShippingsCount.Content = routeLists.Count;
            labelCostSum.Content = routeLists.Sum(rl =>
                rl.RoutePoints.Sum(rp =>
                    rp.Cost)) + " ₽";
        }

        private void UpdateOnFilterChange(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        public override bool EntityRemoved()
        {
            return false;
        }

        public override bool EntitySaved()
        {
            return false;
        }
    }
}
