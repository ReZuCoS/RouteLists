using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteLists.Model
{
    public partial class Manager
    {
        public Manager()
        {
            RoutePoints = new HashSet<RoutePoint>();
        }

        public int ID { get; set; }

        public int CompanyID { get; set; }

        [StringLength(75)]
        public string Surname { get; set; }

        [StringLength(75)]
        public string Name { get; set; }

        [StringLength(75)]
        public string Patronymic { get; set; }

        public string FIO { get => $"{Surname} {Name} {Patronymic}"; }

        [StringLength(20)]
        public string Phone { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<RoutePoint> RoutePoints { get; set; }
    }
}
