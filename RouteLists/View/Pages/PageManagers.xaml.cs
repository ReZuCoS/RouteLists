using RouteLists.Model;
using RouteLists.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace RouteLists.View.Pages
{
    public partial class PageManagers : Page
    {
        List<Manager> _managers = new List<Manager>();

        public PageManagers()
        {
            InitializeComponent();
            UpdateList();
        }

        private void UpdateListOnSearch(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void UpdateList()
        {
            _managers = DatabaseContext.Database.Managers.ToList();

            _managers = _managers.Where(d =>
            d.FIO.ToLower()
            .Contains(textBoxSearh.Text.ToLower()) ||
            d.Company.Title.ToLower()
            .Contains(textBoxSearh.Text.ToLower()) ||
            d.Phone.Replace(" ", String.Empty).Replace("+", String.Empty)
            .Contains(textBoxSearh.Text.Replace(" ", String.Empty).Replace("+", String.Empty))
            ).ToList();

            listViewMain.ItemsSource = _managers;
        }
    }
}
