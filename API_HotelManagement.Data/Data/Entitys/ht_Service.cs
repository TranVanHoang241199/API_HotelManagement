using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data.Data.Entitys
{
    [Table("ht_Service")]
    public class ht_Service : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string? NameService { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Money { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<ht_OrderDetail>? OrderDetails { get; set; }
    }
}
