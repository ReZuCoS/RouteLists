using RouteLists.Model;
using RouteLists.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace RouteLists.View.Pages
{
    public partial class PageCompanies : Page
    {
        private List<Company> _companies = new List<Company>();

        public PageCompanies()
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
            _companies = DatabaseContext.Database.Companies.ToList();

            _companies = _companies.Where(c => c.Title.ToLower()
            .Contains(textBoxSearh.Text.ToLower())
            ).ToList();

            listViewMain.ItemsSource = _companies;
        }
    }
}
