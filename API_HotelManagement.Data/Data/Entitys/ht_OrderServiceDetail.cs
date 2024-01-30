using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data.Data.Entitys
{
    /// <summary>
    /// Bản nhiều nhiều Order N-N Service
    /// </summary>
    [Table("ht_OrderServiceDetail")]
    public class ht_OrderServiceDetail : EntityBase
    {
        /// <summary>
        /// Thời gian đặt
        /// </summary>
        public DateTime? OrderTime { get; set; }
        /// <summary>
        /// Số lượng sản phẩm
        /// </summary>
        public int? Quantity { get; set; }
        /// <summary>
        /// tổng giá tiền
        /// </summary>
        public decimal? TotalPrice { get; set; }

        /// Khoá ngoại Order
        public Guid? OrderId { get; set; }
        public ht_Order? Order { get; set; }

        // Khoá ngoại Service
        public Guid? ServiceId { get; set; }
        public ht_Service? Service { get; set; }
    }
}
