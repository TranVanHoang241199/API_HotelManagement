using System.ComponentModel.DataAnnotations.Schema;

namespace API_HotelManagement.Data.Data.Entitys
{
    [Table("ht_CategoryRoom")]
    public class ht_CategoryRoom : EntityBase
    {
        public string? CategoryName { get; set; }

        public Guid HotelId { get; set; }
        public ht_Hotel? Hotel { get; set; }

        public ICollection<ht_Room>? Rooms { get; set; }
    }
}
