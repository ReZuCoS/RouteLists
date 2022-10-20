using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
                if(RoutePoints.Count == 0)
                {
                    return "-";
                }

                List<string> companyNames = new List<string>();

                foreach (var routePoint in RoutePoints)
                {
                    if (!companyNames.Contains(routePoint.Manager.Company.Title))
                    {
                        companyNames.Add(routePoint.Manager.Company.Title);
                    }
                }

                return String.Join(", ", companyNames.ToArray());
            }
        }

        public int DriverID { get; set; }

        public int VehicleID { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public string FormattedDate
        {
            get
            {
                return Date.ToString("dd/MM/yyyy");
            }
        }

        public int ListNumberPerMonth { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual ICollection<RoutePoint> RoutePoints { get; set; }
    }
}
