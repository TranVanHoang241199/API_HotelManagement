using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.common.Helps.HelpBusiness
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// Người Tạo 
        /// </summary>
        public Guid CreateBy { get; set; }
        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public Guid ModifiedBy { get; set; }
    }
}
