using API_HotelManagement.common.Helps;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data.Data.Entitys
{
    [Table("ht_Service")]
    public class ht_Service : EntityBase
    {
        /// <summary>
        /// Tên dịch vụ
        /// </summary>
        public string? ServiceName { get; set; }
        /// <summary>
        /// Giá dịch vụ
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Số lượng dịch vụ
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Trạng thái dịch vụ 
        /// 0 = Active : đang cung cấp
        /// 1 = End : hết cung cấp
        /// 2 = maintenance : đang bảo trì
        /// </summary>
        public EStatusService Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<ht_OrderServiceDetail>? OrderServiceDetails { get; set; }
    }
}
