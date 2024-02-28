using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data.Data.Entitys
{
    [Table("ht_CategoryService")]
    public class ht_CategoryService : EntityBase
    {
        public string? CategoryName { get; set; }

        public Guid HotelId { get; set; }
        public ht_Hotel? Hotel { get; set; }

        public ICollection<ht_Service>? ht_Services { get; set; }
    }
}
