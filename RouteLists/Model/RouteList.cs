using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteLists.Model
{
    public partial class RouteList
    {
        [StringLength(25)]
        public string ID { get; set; }

        public int DriverID { get; set; }

        public int VehicleID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual RoutePoint RoutePoint { get; set; }
    }
}
