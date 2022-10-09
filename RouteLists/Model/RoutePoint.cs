using System.ComponentModel.DataAnnotations;

namespace RouteLists.Model
{
    public partial class RoutePoint
    {
        [Key]
        [StringLength(25)]
        public string RouteID { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        public int ActionID { get; set; }

        public int ManagerID { get; set; }

        [Required]
        [StringLength(150)]
        public string InvoiceNumbers { get; set; }

        public decimal? Cost { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual PointsAction PointsAction { get; set; }

        public virtual RouteList RouteList { get; set; }
    }
}
