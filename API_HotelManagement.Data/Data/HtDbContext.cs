using API_HotelManagement.Data.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace API_HotelManagement.Data.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class HtDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public HtDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Connection DB SQL Server
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("WebHotelApiDbManagement"));
        }

        /// <summary>
        /// Test Table
        /// </summary>
        public DbSet<ht_Test> ht_Test { get; set; }
        public DbSet<ht_User> ht_User { get; set; }

        /// <summary>
        /// Khởi tạo dữ liệu test
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ht_Test>().HasData(
                new ht_Test
                {
                    Id = Guid.Parse("9d749d28-3405-45f7-8fd1-fdae05a0dea9"),
                    Name = "hello 1",



                    //----------------
                    CreateBy = Guid.Parse("7c67e72e-e773-447f-9f3e-9c3a480be605"), // người tạo
                    CreateDate = DateTime.Now, // thời gian tạo
                    ModifiedBy = Guid.Parse("7c67e72e-e773-447f-9f3e-9c3a480be605"), // người sửa đổi lần cuối
                    ModifiedDate = DateTime.Now // thời gian sửa đổi lần cuối
                },
                new ht_Test
                {
                    Id = Guid.Parse("801f03d6-5bb6-44b1-9460-9cdf4617aaf9"),
                    Name = "hello 2",


                    //----------------
                    CreateBy = Guid.Parse("7c67e72e-e773-447f-9f3e-9c3a480be605"), // người tạo
                    CreateDate = DateTime.Now, // thời gian tạo
                    ModifiedBy = Guid.Parse("7c67e72e-e773-447f-9f3e-9c3a480be605"), // người sửa đổi lần cuối
                    ModifiedDate = DateTime.Now // thời gian sửa đổi lần cuối
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
