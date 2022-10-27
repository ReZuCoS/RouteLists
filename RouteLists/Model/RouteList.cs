using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RouteLists.Model
{
    public partial class RouteList
    {
        public RouteList()
        {
            RoutePoints = new HashSet<RoutePoint>();
        }

        public int ID { get; set; }

        public string ListNumber
        {
            get
            {
                string vehicleType = Vehicle.Tonnage >= 1000 ? "ЛА" : "ГА";
                return $"{vehicleType} {Vehicle.NumberParts[1]} №{ListNumberPerMonth}";
            }
        }

        public string CompanyNames
        {
            get
            {
                if (RoutePoints.Count == 0)
                {
                    return "-";
                }
                else
                {
                    string[] companyNames = RoutePoints.Select(rp => rp.Manager.Company.Title).Distinct().ToArray();
                    return String.Join(", ", companyNames);
                }
            }
        }

        public int DriverID { get; set; }

        public int VehicleID { get; set; }

        public string FormattedDate =>
            Date.ToString("dd/MM/yyyy");

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int ListNumberPerMonth { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual ICollection<RoutePoint> RoutePoints { get; set; }
    }
}
