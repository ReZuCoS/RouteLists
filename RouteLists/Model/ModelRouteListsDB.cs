using System.Data.Entity;

namespace RouteLists.Model
{
    public partial class ModelRouteListsDB : DbContext
    {
        public ModelRouteListsDB()
            : base("name=ModelRouteListsDB")
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<PassType> PassTypes { get; set; }
        public virtual DbSet<RouteList> RouteLists { get; set; }
        public virtual DbSet<RoutePoint> RoutePoints { get; set; }
        public virtual DbSet<VehiclePass> VehiclePasses { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassType>()
                .HasMany(e => e.VehiclePasses)
                .WithRequired(e => e.PassType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RouteList>()
                .HasMany(e => e.RoutePoints)
                .WithRequired(e => e.RouteList)
                .HasForeignKey(e => e.RouteID);

            modelBuilder.Entity<RoutePoint>()
                .Property(e => e.Cost)
                .HasPrecision(15, 2);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.Drivers)
                .WithOptional(e => e.Vehicle)
                .HasForeignKey(e => e.MainVehicle);

            modelBuilder.Entity<Vehicle>()
                .HasOptional(e => e.VehiclePass)
                .WithRequired(e => e.Vehicle)
                .WillCascadeOnDelete();
        }
    }
}
