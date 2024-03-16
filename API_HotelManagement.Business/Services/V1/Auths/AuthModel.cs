using API_HotelManagement.common.Helps;
using API_HotelManagement.Data;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace API_HotelManagement.Business
{
    /// <summary>
    /// model đăng nhập
    /// </summary>
    public class LoginView
    {
        [Required(ErrorMessage = "Email is enter required")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Password is enter required")]
        public string Password { get; set; } = "";
    }

    /// <summary>
    /// model thông tin account
    /// </summary>
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public EBusinessAreas BusinessAreas { get; set; }
        public bool IsDeleted { get; set; }
        public string? Role { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? PasswordUpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserUpdateCreateModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public EBusinessAreas BusinessAreas { get; set; }
        public bool IsDeleted { get; set; }
        public string? Role { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "required to enter current Password")]
        public string? CurrentPassword { get; set; }
        [Required(ErrorMessage = "required to enter New Password")]
        public string? NewPassword { get; set; }
        [Required(ErrorMessage = "required to enter Confirm Password")]
        public string? ConfirmNewPassword { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserAutoMapper : Profile
    {
        public UserAutoMapper()
        {
            CreateMap<ht_User, UserViewModel>(); // Auto map ht_User to UserViewModel
            CreateMap<UserUpdateCreateModel, ht_User>(); // Auto map ht_User to UserViewModel
        }
    }

}

