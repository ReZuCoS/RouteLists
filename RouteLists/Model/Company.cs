using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RouteLists.Model
{
    public partial class Company
    {
        public Company()
        {
            Managers = new HashSet<Manager>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        public virtual ICollection<Manager> Managers { get; set; }
    }
}
