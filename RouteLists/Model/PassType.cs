using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteLists.Model
{
    public partial class PassType
    {
        public PassType()
        {
            VehiclePasses = new HashSet<VehiclePass>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public virtual ICollection<VehiclePass> VehiclePasses { get; set; }
    }
}
