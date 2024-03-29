﻿using API_HotelManagement.common.Helps;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data
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
        public decimal PriceAmount { get; set; }
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

        public Guid? CategoryServiceId { get; set; }
        public ht_CategoryService? CategoryService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<ht_OrderServiceDetail>? OrderServiceDetails { get; set; }
    }
}
