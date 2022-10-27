using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteLists.Model
{
    public partial class Driver
    {
        public static int GetAgeFromBithday(DateTime birthDate)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthDate.Year;
            if (birthDate > now.AddYears(-age)) age--;

            return age;
        }

        public Driver()
        {
            RouteLists = new HashSet<RouteList>();
        }

        public int ID { get; set; }

        public string FIO =>
            string.Join(" ", Name, Patronymic, Surname);

        [Required]
        [StringLength(75)]
        public string Surname { get; set; }

        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        [StringLength(75)]
        public string Patronymic { get; set; }

        public int Age =>
            GetAgeFromBithday(Bithday);

        [Column(TypeName = "date")]
        public DateTime Bithday { get; set; }

        public int DrivingExperience { get; set; }

        public int? MainVehicle { get; set; }

        public bool HasVehicle =>
            Vehicle != null;

        public virtual Vehicle Vehicle { get; set; }

        public virtual ICollection<RouteList> RouteLists { get; set; }
    }
}
