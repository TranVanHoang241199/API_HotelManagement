using API_HotelManagement.Data.Data.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.Business.Services.Orders
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public string? TimeStart { get; set; }
        public string? TimeEnd { get; set; }
        public string? Note { get; set; }

        //-----------
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class OrderCreateUpdateModel
    {
        public string? TimeStart { get; set; }
        public string? TimeEnd { get; set; }
        public string? Note { get; set; }
        public Guid CustomerId { get; set; }
        public Guid RoomId { get; set; }
    }
}
