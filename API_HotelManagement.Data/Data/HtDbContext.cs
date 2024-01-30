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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public HtDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
            // 
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        /// <summary>   
        /// Connection DB SQL Server
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            //options.UseSqlServer(Configuration.GetConnectionString("WebHotelApiDbManagement_SqlServer"));
            options.UseSqlServer(Configuration.GetConnectionString("WebHotelApiDbManagement_SqlServer_Sv_Somee"));
            //options.UseNpgsql(Configuration.GetConnectionString("WebHotelApiDbManagement_PostreSql"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region đặt giới hạn cho colum
            // decimal(18, 2) : 18 chữ số tổng cùng với 2 chữ số sau dấu thập phân.
            modelBuilder.Entity<ht_Service>()
           .Property(s => s.Price)
           .HasPrecision(18, 2);

            // decimal(18, 2) : 18 chữ số tổng cùng với 2 chữ số sau dấu thập phân.
            modelBuilder.Entity<ht_Room>()
           .Property(s => s.Price)
           .HasPrecision(18, 2);
            #endregion đặt giới hạn cho colum

            #region Nối bản
            // Foreign key {N OrderRoomDetail - 1 Order}
            modelBuilder.Entity<ht_Service>()
                .HasOne(od => od.CategoryService)
                .WithMany(o => o.ht_Services)
                .HasForeignKey(od => od.CategoryServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Foreign key {N CategoryRoom - 1 Room}
            modelBuilder.Entity<ht_Room>()
                .HasOne(od => od.CategoryRoom)
                .WithMany(o => o.Rooms)
                .HasForeignKey(od => od.CategoryRoomId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Foreign key {N OrderRoomDetail - 1 Order}
            modelBuilder.Entity<ht_OrderRoomDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderRoomDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Foreign key {N OrderRoomDetail - 1 Service}
            modelBuilder.Entity<ht_OrderRoomDetail>()
                .HasOne(od => od.Room)
                .WithMany(s => s.OrderRoomDetails)
                .HasForeignKey(od => od.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Foreign key {N OrderServiceDetail - 1 Order}
            modelBuilder.Entity<ht_OrderServiceDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderServiceDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Foreign key {N OrderServiceDetail - 1 Service}
            modelBuilder.Entity<ht_OrderServiceDetail>()
                .HasOne(od => od.Service)
                .WithMany(s => s.OrderServiceDetails)
                .HasForeignKey(od => od.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            #endregion Nối bản
        }

        /// <summary>
        /// Test Table
        /// </summary>
        public DbSet<ht_User> ht_Users { get; set; }
        public DbSet<ht_CategoryRoom> ht_CategoryRooms { get; set; }
        public DbSet<ht_CategoryService> ht_CategoryServices { get; set; }
        public DbSet<ht_Room> ht_Rooms { get; set; }
        public DbSet<ht_Order> ht_Orders { get; set; }
        public DbSet<ht_OrderServiceDetail> ht_OrderServiceDetails { get; set; }
        public DbSet<ht_OrderRoomDetail> ht_OrderRoomDetails { get; set; }
        public DbSet<ht_Service> ht_Services { get; set; }
    }
}
