using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.Data.Data.Entity
{
    public class ht_User : EntityBase
    {

        /// <summary>
        /// tên user
        /// </summary>
        [Required]
        public string? Username { get; set; }

        /// <summary>
        /// mật khẩu
        /// </summary>
        [Required]
        public string? Password { get; set; }

        /// <summary>
        /// thời gian đăng nhập mới nhất
        /// </summary>
        public DateTime LastLogin { get; set; }

        /// <summary>
        /// quyền (use - admin)
        /// </summary>
        public string? Role { get; set; }

    }
}
