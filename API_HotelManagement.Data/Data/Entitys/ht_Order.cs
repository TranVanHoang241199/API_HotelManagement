using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data.Data.Entitys
{
    [Table("ht_Order")]
    public class ht_Order : EntityBase
    {
        public string? CustomerPhone { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        [Column(TypeName = "Text")]
        public string? Note { get; set; }

        // khoá ngoại Rooms
        public Guid? RoomId { get; set; }
        public ht_Room? Rooms { get; set; }
        public ICollection<ht_OrderDetail>? OrderDetails { get; set; }
    }
}
