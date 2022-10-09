using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteLists.Model
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Drivers = new HashSet<Driver>();
            RouteLists = new HashSet<RouteList>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Brand { get; set; }

        [Required]
        [StringLength(15)]
        public string Number { get; set; }

        public string[] NumberParts { get => Number.Split(' '); }

        public int Tonnage { get; set; }

        public virtual ICollection<Driver> Drivers { get; set; }

        public virtual ICollection<RouteList> RouteLists { get; set; }

        public virtual VehiclePass VehiclePass { get; set; }

        public bool HasVehiclePass { get => VehiclePass != null; }
    }
}
