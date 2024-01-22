using API_HotelManagement.Data.Data.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace API_HotelManagement.Data.Data
{
    /// <summary>
    /// DB context
    /// </summary>
    public class HtDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public HtDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        /// <summary>
        /// Connection DB SQL Server
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebHotelApiDbManagement_PostreSql"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Foreign key {N Order - 1 Rooms}
            modelBuilder.Entity<ht_Order>()
                .HasOne(o => o.Rooms)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Foreign key {N OrderDetail - 1 Order}
            modelBuilder.Entity<ht_OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Foreign key {N OrderDetail - 1 Service}
            modelBuilder.Entity<ht_OrderDetail>()
                .HasOne(od => od.Service)
                .WithMany(s => s.OrderDetails)
                .HasForeignKey(od => od.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        /// <summary>
        /// Test Table
        /// </summary>
        public DbSet<ht_User> ht_Users { get; set; }
        public DbSet<ht_Room> ht_Rooms { get; set; }
        public DbSet<ht_Order> ht_Orders { get; set; }
        public DbSet<ht_OrderDetail> ht_OrderDetails { get; set; }
        public DbSet<ht_Service> ht_Services { get; set; }
    }
}
