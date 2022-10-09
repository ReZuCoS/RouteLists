using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteLists.Model
{
    public partial class VehiclePass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VehicleID { get; set; }

        public int PassTypeID { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExpireDate { get; set; }

        public enum PassExpireType
        {
            Valid = 0,
            StartsExpire = 1,
            Expired = 2
        }

        public PassExpireType ExpireType { get => IsExpiredOrStartsExpire(ExpireDate); }
        
        private static PassExpireType IsExpiredOrStartsExpire(DateTime date)
        {
            DateTime currentDate = DateTime.Now;

            if (date <= currentDate)
            {
                return PassExpireType.Expired;
            }

            if((date - currentDate).TotalDays < 30)
            {
                return PassExpireType.StartsExpire;
            }

            return PassExpireType.Valid;
        }

        public string FormattedExpiredDate { get => ExpireDate.ToString("dd/MM/yyyy"); }

        public virtual PassType PassType { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}