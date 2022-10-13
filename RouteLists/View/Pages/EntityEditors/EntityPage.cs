using System.Windows.Controls;

namespace RouteLists.View.Pages.EntityEditors
{
    public abstract class EntityPage : Page
    {
        public abstract bool EntitySaved();
        public abstract bool EntityRemoved();
    }
}
