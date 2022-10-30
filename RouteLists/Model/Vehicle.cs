using RouteLists.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RouteLists.Model
{
    public partial class Vehicle
    {
        public static string VehicleNumberFromString(string input) =>
            input.Replace(" ", "")
            .ToUpper()
            .Insert(1, " ")
            .Insert(5, " ")
            .Insert(8, " ");
        
        public static bool IsUniqueNumber(string number) =>
            !DatabaseContext.Database.Vehicles.ToList().Any(v => v.Number == number);

        public Vehicle()
        {
            Drivers = new HashSet<Driver>();
            RouteLists = new HashSet<RouteList>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Brand { get; set; }

        public string[] NumberParts =>
            Number.Split(' ');

        [Required]
        [StringLength(15)]
        public string Number { get; set; }

        public int Tonnage { get; set; }

        public bool HasVehiclePass =>
            VehiclePass != null;

        public virtual ICollection<Driver> Drivers { get; set; }

        public virtual ICollection<RouteList> RouteLists { get; set; }

        public virtual VehiclePass VehiclePass { get; set; }
    }
}
