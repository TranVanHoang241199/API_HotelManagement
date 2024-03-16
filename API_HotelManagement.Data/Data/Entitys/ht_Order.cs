using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data
{
    [Table("ht_Order")]
    public class ht_Order : EntityBase
    {
        /// <summary>
        /// Số điện thoại khách hàng
        /// </summary>
        public string? CustomerPhone { get; set; }
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// Mã định danh điện tử
        /// </summary>
        public string? Identifier { get; set; }
        /// <summary>
        /// Ghi chú khách hàng
        /// </summary>
        [Column(TypeName = "Text")]
        public string? Note { get; set; }

        /// <summary>
        /// Danh sách dịch vụ đã đặt
        /// </summary>
        public ICollection<ht_OrderServiceDetail>? OrderServiceDetails { get; set; }
        /// <summary>
        /// Danh sách phòng đã đặt
        /// </summary>
        public ICollection<ht_OrderRoomDetail>? OrderRoomDetails { get; set; }
    }
}
