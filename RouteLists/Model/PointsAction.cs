using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteLists.Model
{
    [Table("PointsAction")]
    public partial class PointsAction
    {
        public PointsAction()
        {
            RoutePoints = new HashSet<RoutePoint>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Action { get; set; }

        public virtual ICollection<RoutePoint> RoutePoints { get; set; }
    }
}
