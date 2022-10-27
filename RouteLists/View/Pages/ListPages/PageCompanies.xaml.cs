using RouteLists.Model;
using RouteLists.View.Pages.EntityEditors;
using RouteLists.View.Windows;
using RouteLists.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RouteLists.View.Pages.ListPages
{
    public partial class PageCompanies : Page
    {
        private List<Company> _companies = new List<Company>();

        public PageCompanies()
        {
            InitializeComponent();
            UpdateList();
        }

        public void UpdateList()
        {
            _companies = DatabaseContext.Database.Companies.ToList();

            _companies = _companies.Where(c => c.Title.ToLower()
            .Contains(textBoxSearh.Text.ToLower())
            ).ToList();

            listViewMain.ItemsSource = _companies;
        }

        private void EditSelectedDriver(object sender, MouseButtonEventArgs e)
        {
            Company selectedCompany = (Company)listViewMain.SelectedItem;

            WindowEntityEditor windowEditor = new WindowEntityEditor(new PageEditCompany(selectedCompany), true);

            bool isListUpdated = (bool)windowEditor.ShowDialog();

            if (isListUpdated)
                UpdateList();
        }

        private void AddCompany(object sender, RoutedEventArgs e)
        {
            WindowEntityEditor windowEditor = new WindowEntityEditor(new PageEditCompany(), false);

            bool isListUpdated = (bool)windowEditor.ShowDialog();

            if (isListUpdated)
                UpdateList();
        }

        private void UpdateListOnSearch(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void ClearFocus(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }
    }
}
