using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.Data.Data.Entitys
{
    [Table("ht_Role")]
    public class ht_Role : EntityBase
    {
        public string? RoleName { get; set; }
    }
}
