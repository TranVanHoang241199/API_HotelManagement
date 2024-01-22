using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.Business.Services.Services
{
    public class ServiceViewModel
    {
        public Guid Id { get; set; }
        public string? NameService { get; set; }
        public double Money { get; set; }

        //-----------
        //-----------
        public DateTime? CreateDate { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid ModifiedBy { get; set; }
    }

    public class ServiceCreateUpdateModel
    {
        public string? NameService { get; set; }
        public double Money { get; set; }
    }
}
