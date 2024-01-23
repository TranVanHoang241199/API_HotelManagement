using API_HotelManagement.common.Helps;
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
        /// Tên phòng
        /// </summary>
        [MaxLength(100)]
        public string? RoomName { get; set; }
        /// <summary>
        /// Số tầng
        /// </summary>
        public int FloorNumber { get; set; }
        /// <summary>
        /// Giá phòng
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Trạng thái hoạt động
        /// 0 = Active : đang hoạt động
        /// 1 = Stop : đang có khách 
        /// 2 = End : ngừng kinh doanh
        /// 3 = maintenance : đang bảo trì
        /// </summary>
        public EStatusRoom Status { get; set; }

        /// <summary>
        /// Dánh sách list OrderRoomDetail
        /// </summary>
        public ICollection<ht_OrderRoomDetail>? OrderRoomDetails { get; set; }
    }
}
