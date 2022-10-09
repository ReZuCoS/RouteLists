using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteLists.Model
{
    public partial class Driver
    {
        public Driver()
        {
            RouteLists = new HashSet<RouteList>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(75)]
        public string Surname { get; set; }

        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        [StringLength(75)]
        public string Patronymic { get; set; }

        [Column(TypeName = "date")]
        public DateTime Bithday { get; set; }

        public int DrivingExperience { get; set; }

        public int? MainVehicle { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual ICollection<RouteList> RouteLists { get; set; }
    }
}
