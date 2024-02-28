using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data.Data.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Table("ht_Hotel")]
    public class ht_Hotel : EntityBase
    {
        public string? HotelName { get; set; }
        public string? Description { get; set; }

        public ICollection<ht_Role>? ht_Roles { get; set; }
        public ICollection<ht_CategoryRoom>? ht_CategoryRooms { get; set; }
        public ICollection<ht_CategoryService>? ht_CategoryServices { get; set; }
    }
}
