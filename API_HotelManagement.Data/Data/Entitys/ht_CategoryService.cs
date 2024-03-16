using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data
{
    [Table("ht_CategoryService")]
    public class ht_CategoryService : EntityBase
    {
        public string? CategoryName { get; set; }

        public ICollection<ht_Service>? ht_Services { get; set; }
    }
}
