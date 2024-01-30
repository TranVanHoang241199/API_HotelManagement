using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.Data.Data.Entitys
{
    /// <summary>
    /// Bản nhiều nhiều Order N-N Room
    /// </summary>
    [Table("ht_OrderRoomDetail")]
    public class ht_OrderRoomDetail : EntityBase
    {
        /// <summary>
        /// Thời gian đặc phòng
        /// </summary>
        public DateTime? TimeStart { get; set; }
        /// <summary>
        /// Thời gian trả phòng
        /// </summary>
        public DateTime? TimeEnd { get; set; }

        /// Khoá ngoại Order
        public Guid? OrderId { get; set; }
        public ht_Order? Order { get; set; }

        // Khoá ngoại Service
        public Guid? RoomId { get; set; }
        public ht_Room? Room { get; set; }
    }
}
