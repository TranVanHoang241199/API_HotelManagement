﻿using API_HotelManagement.common.Helps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.Data.Data.Entitys
{
    public class ht_User : EntityBase
    {
        /// <summary>
        /// Email tài khoản dùng đăng nhập
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string? UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string? Password { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        [MaxLength(100)]
        public string? Phone { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [MaxLength(100)]
        public string? Email { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        [MaxLength(100)]
        public string? FullName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        public EBusinessAreas BusinessAreas { get; set; }
        /// <summary>
        /// Thời gian cập nhật mật khẩu
        /// </summary>
        public DateTime? PasswordUpdatedDate { get; set; }
        /// <summary>
        /// kiểm tra vô hiệu hoá
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Thời gian vô hiệu hoá
        /// </summary>
        public DateTime? DeletedDate { get; set; }
        /// <summary>
        /// quyền (use - admin)
        /// </summary>
        [MaxLength(50)]
        public string? Role { get; set; }

    }
}
