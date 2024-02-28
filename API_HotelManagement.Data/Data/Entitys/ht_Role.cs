using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data.Data.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Table("ht_Role")]
    public class ht_Role : EntityBase
    {
        public string? RoleName { get; set; }

        public Guid UserId { get; set; }
        public ht_User? User { get; set; }

        public Guid HotelId { get; set; }
        public ht_Hotel? Hotel { get; set; }
    }
}
