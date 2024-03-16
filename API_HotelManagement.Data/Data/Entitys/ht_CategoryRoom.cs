using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data
{
    [Table("ht_CategoryRoom")]
    public class ht_CategoryRoom : EntityBase
    {
        public string? CategoryName { get; set; }

        public ICollection<ht_Room>? Rooms { get; set; }
    }
}
