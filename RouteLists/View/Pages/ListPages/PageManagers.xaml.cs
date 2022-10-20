using RouteLists.Model;
using RouteLists.View.Pages.EntityEditors;
using RouteLists.View.Windows;
using RouteLists.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RouteLists.View.Pages.ListPages
{
    public partial class PageManagers : Page
    {
        private List<Manager> _managers = new List<Manager>();

        public PageManagers()
        {
            InitializeComponent();
            UpdateList();
        }

        public void UpdateList()
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

        private void EditSelectedManager(object sender, MouseButtonEventArgs e)
        {
            Manager selectedManager = (Manager)listViewMain.SelectedItem;

            WindowEntityEditor windowEditor = new WindowEntityEditor(new PageEditManager(selectedManager), true);

            bool isListUpdated = (bool)windowEditor.ShowDialog();

            if (isListUpdated)
            {
                UpdateList();
            }
        }

        private void AddManager(object sender, RoutedEventArgs e)
        {
            WindowEntityEditor windowEditor = new WindowEntityEditor(new PageEditManager(), false);

            bool isListUpdated = (bool)windowEditor.ShowDialog();

            if (isListUpdated)
            {
                UpdateList();
            }
        }

        private void UpdateListOnSearch(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }
    }
}
