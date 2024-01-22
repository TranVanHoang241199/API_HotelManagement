using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.Business.Services.Rooms
{
    public class RoomViewModel
    {
        public Guid Id { get; set; }
        public string? RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public int Status { get; set; }

        //-----------
        public DateTime? CreateDate { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid ModifiedBy { get; set; }
    }

    public class RoomCreateUpdateModel
    {
        public string? RoomNumber { get; set; }
        public int FloorNumber { get; set; }
        public int Status { get; set; }
    }
}
