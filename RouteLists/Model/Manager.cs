using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteLists.Model
{
    public partial class Manager
    {
        public static string GetPhoneFromString(string input) =>
            input.Replace(" ", "")
            .Replace("+", "")
            .Replace("-", "")
            .Replace("(", "")
            .Replace(")", "")
            .Insert(0, "+")
            .Insert(2, " (")
            .Insert(7, ") ")
            .Insert(12, "-")
            .Insert(15, "-");

        public Manager()
        {
            RoutePoints = new HashSet<RoutePoint>();
        }

        public int ID { get; set; }

        public int CompanyID { get; set; }

        public string FIO =>
            string.Join(" ", Name, Patronymic, Surname);

        [StringLength(75)]
        public string Surname { get; set; }

        [StringLength(75)]
        public string Name { get; set; }

        [StringLength(75)]
        public string Patronymic { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<RoutePoint> RoutePoints { get; set; }
    }
}
