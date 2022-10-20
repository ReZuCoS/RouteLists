using RouteLists.View.Pages.EntityEditors;
using System.Windows;

namespace RouteLists.View.Windows
{
    public partial class WindowEntityEditor : Window
    {
        private readonly EntityPage _currentPage;

        public WindowEntityEditor(EntityPage page, bool editMode)
        {
            InitializeComponent();

            buttonRemove.Visibility = editMode ? Visibility.Visible : Visibility.Hidden;

            _currentPage = page;
            mainFrame.Navigate(page);
            this.Title = page.Title;
        }

        public WindowEntityEditor(EntityPage page, bool editMode, bool isReadOnly) : this(page, false)
        {
            buttonSave.Visibility = isReadOnly ? Visibility.Hidden : Visibility.Visible;
        }

        private void SaveEntity(object sender, RoutedEventArgs e)
        {
            if (_currentPage.EntitySaved())
            {
                this.DialogResult = true;
            }
        }

        private void RemoveEntity(object sender, RoutedEventArgs e)
        {
            if (_currentPage.EntityRemoved())
            {
                this.DialogResult = true;
            }
        }

        private void CloseEditor(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
