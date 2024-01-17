using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.Business.Services.Auth
{
    /// <summary>
    /// model đăng nhập
    /// </summary>
    public class AuthUser
    {
        [Required(ErrorMessage = "Cần nhập Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Cần nhập Password")]
        public string? Password { get; set; }
    }

    /// <summary>
    /// model thông tin account
    /// </summary>
    public class UserAccountViewModel
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
        public DateTime LastLogin { get; set; }
    }

}
