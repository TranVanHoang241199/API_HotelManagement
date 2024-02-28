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
            options.UseSqlServer(Configuration.GetConnectionString("WebHotelApiDbManagement_SqlServer"));
            //options.UseSqlServer(Configuration.GetConnectionString("WebHotelApiDbManagement_SqlServer_Sv_Somee"));
            //options.UseNpgsql(Configuration.GetConnectionString("WebHotelApiDbManagement_PostreSql"));
        }

        #region DBSet
        /// <summary>
        /// Test Table
        /// </summary>
        public DbSet<ht_User> ht_Users { get; set; } // User dang nhap
        public DbSet<ht_Hotel> ht_Hotels { get; set; } // khach san quan ly
        public DbSet<ht_Role> ht_Roles { get; set; } // quen trong khach san
        public DbSet<ht_CategoryRoom> ht_CategoryRooms { get; set; } // loai phong
        public DbSet<ht_CategoryService> ht_CategoryServices { get; set; } // loai dich vu
        public DbSet<ht_Room> ht_Rooms { get; set; } // phong 
        public DbSet<ht_Order> ht_Orders { get; set; } // 
        public DbSet<ht_OrderServiceDetail> ht_OrderServiceDetails { get; set; }
        public DbSet<ht_OrderRoomDetail> ht_OrderRoomDetails { get; set; }
        public DbSet<ht_Service> ht_Services { get; set; }
        #endregion DBSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region đặt giới hạn cho colum

            modelBuilder.Entity<ht_User>(entity => {
                // Đảm bảo UserName là duy nhất
                entity.HasIndex(e => e.UserName).IsUnique();

                // Cấu hình giới hạn cột
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Phone).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Role).HasMaxLength(50);
            });

            modelBuilder.Entity<ht_Service>(entity =>
            {
                // decimal(18, 2) : 18 chữ số tổng cùng với 2 chữ số sau dấu thập phân.
                entity.Property(s => s.Price).HasPrecision(18, 2);
            });

            modelBuilder.Entity<ht_OrderServiceDetail>(entity =>
            {
                // decimal(18, 2) : 18 chữ số tổng cùng với 2 chữ số sau dấu thập phân.
                entity.Property(s => s.TotalPrice).HasPrecision(18, 2);
            });

            modelBuilder.Entity<ht_Room>(entity =>
            {
                // decimal(18, 2) : 18 chữ số tổng cùng với 2 chữ số sau dấu thập phân.
                entity.Property(s => s.Price).HasPrecision(18, 2);
            });
            #endregion đặt giới hạn cho colum

            #region Nối bản
            // Foreign key {N ht_Role - 1 Hotel}
            modelBuilder.Entity<ht_Role>()
                .HasOne(od => od.User)
                .WithMany(o => o.ht_Roles)
                .HasForeignKey(od => od.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Foreign key {N ht_Role - 1 Hotel}
            modelBuilder.Entity<ht_Role>()
                .HasOne(od => od.Hotel)
                .WithMany(o => o.ht_Roles)
                .HasForeignKey(od => od.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Foreign key {N ht_CategoryService - 1 Hotel}
            modelBuilder.Entity<ht_CategoryService>()
                .HasOne(od => od.Hotel)
                .WithMany(o => o.ht_CategoryServices)
                .HasForeignKey(od => od.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Foreign key {N ht_CategoryRoom - 1 Hotel}
            modelBuilder.Entity<ht_CategoryRoom>()
                .HasOne(od => od.Hotel)
                .WithMany(o => o.ht_CategoryRooms)
                .HasForeignKey(od => od.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Foreign key {N ht_Service - 1 CategoryService}
            modelBuilder.Entity<ht_Service>()
                .HasOne(od => od.CategoryService)
                .WithMany(o => o.ht_Services)
                .HasForeignKey(od => od.CategoryServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Foreign key {N Room - 1 CategoryRoom}
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

            // Foreign key {N OrderRoomDetail - 1 Room}
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
    }
}
