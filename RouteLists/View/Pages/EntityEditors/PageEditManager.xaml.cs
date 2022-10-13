namespace RouteLists.View.Pages.EntityEditors
{
    public partial class PageEditManager : EntityPage
    {
        public PageEditManager()
        {
            InitializeComponent();
        }

        public override bool EntitySaved()
        {
            return true;
        }

        public override bool EntityRemoved()
        {
            return false;
        }
    }
}
