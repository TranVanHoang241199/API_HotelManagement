using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.Business.Services.Test
{
    public class TestViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    public class TestCreateUpdateModel
    {
        /// <summary>
        /// Name Test
        /// </summary>
        public string? Name { get; set; }
    }
}
