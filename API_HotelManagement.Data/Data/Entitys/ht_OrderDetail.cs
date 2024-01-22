using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data.Data.Entitys
{
    [Table("ht_OrderDetail")]
    public class ht_OrderDetail : EntityBase
    {
        /// Khoá ngoại Order
        public Guid? OrderId { get; set; }
        public ht_Order? Order { get; set; }

        // Khoá ngoại Service
        public Guid? ServiceId { get; set; }
        public ht_Service? Service { get; set; }
    }
}
