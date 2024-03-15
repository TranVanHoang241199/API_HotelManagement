using API_HotelManagement.common.Utils;
using System.Security.Claims;

namespace API_HotelManagement.Business.Services.Auths
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthHandler
    {
        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ApiResponse> Authenticate(string UserName, string password);
        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="user"></param>
        /// <param name="changePasswordModel"></param>
        /// <returns></returns>
        Task<ApiResponse> ChangePassword(ClaimsPrincipal user, ChangePasswordModel changePasswordModel);
        /// <summary>
        /// Tạo tài khoản mới
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse> Register(UserUpdateCreateModel request);
        /// <summary>
        /// Cập nhật thông tin User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResponse> UpdateInformation(ClaimsPrincipal user, UserUpdateCreateModel request);
        /// <summary>
        /// Lấy Thông tin User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ApiResponse> GetCurrentUser(ClaimsPrincipal user);
        /// <summary>
        /// Vô hiệu hoá tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ApiResponse> RemoveAccount(ClaimsPrincipal user);
        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ApiResponse> Logout(ClaimsPrincipal user);
        /// <summary>
        /// Huỷ quyền vô hiệu hoá tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse> RecoverAccount(Guid id);
        Task<ApiResponse> CheckUsename(string usename);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ApiResponse> GetAllUsers(string search = "", int currentPage = 1, int pageSize = 1);
    }
}
