using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data.Data.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Table("ht_Room")]
    public class ht_Room : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string? RoomNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int FloorNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<ht_Order>? Orders { get; set; }

    }
}
