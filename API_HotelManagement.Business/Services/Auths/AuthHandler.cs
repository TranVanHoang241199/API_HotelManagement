using API_HotelManagement.Business.Services.Rooms;
using API_HotelManagement.common.Helps;
using API_HotelManagement.common.Helps.Extensions;
using API_HotelManagement.common.Utils;
using API_HotelManagement.Data.Data;
using API_HotelManagement.Data.Data.Entitys;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace API_HotelManagement.Business.Services.Auths
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthHandler : IAuthHandler
    {

        private readonly HtDbContext _context;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        
        public AuthHandler(HtDbContext context, IConfiguration config, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _mapper  = mapper;
        }

        /// <summary>
        /// Authenticate Handles Login
        /// Authenticate Xử lý Login
        /// </summary>
        /// <param name="email">Email tài khoản người dùng</param>
        /// <param name="password">Mật khẩu người dùng</param>
        /// <returns>Token</returns>
        public async Task<ApiResponse> Authenticate(string userName, string password)
        {
            try
            {
                // kiểm tra có dữ liệu email hay pass không
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "Email and Pass cannot be blank.");
                }

                // Kiểm tra xem có người dùng nào khớp với tên người dùng không
                var userEntity = await _context.ht_Users
                    .Where(u => u.Email.Equals(userName) || u.UserName.Equals(userName) || u.Phone.Equals(userName))
                    .OrderByDescending(u => u.CreateDate)
                    .FirstOrDefaultAsync();

                // check mật khẩu mã hoá để so sánh
                if (userEntity == null || !BCrypt.Net.BCrypt.Verify(password, userEntity.Password))
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "User not found.");
                }

                if (string.IsNullOrWhiteSpace(userEntity.Role) ||
                    string.IsNullOrWhiteSpace(userEntity.Id.ToString()) ||
                    string.IsNullOrWhiteSpace(userEntity.FullName))
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "Users do not have enough information to process.");
                }

                // Tạo claims và token
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Role, userEntity.Role.ToString()),
                    new Claim("UserId", userEntity.Id.ToString()),
                    new Claim("DisplayName", userEntity.FullName),
                    new Claim("Email", userEntity.Email),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddHours(8),
                    signingCredentials: signIn
                );

                string tokenResult = new JwtSecurityTokenHandler().WriteToken(token);
                Log.Information($"User '{userEntity.Id}' successfully authenticated.");
                return new ApiResponseAuth(tokenResult);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// ChangePassword uses Change Password
        /// ChangePassword Đổi mật khẩu 
        /// </summary>
        /// <param name="user">Thông tin User đã đăng nhập</param>
        /// <param name="changePasswordModel">Mật khẩu cũ, mật khẩu mới và nhập lại mật khẩu</param>
        /// <returns></returns>
        public async Task<ApiResponse> ChangePassword(ClaimsPrincipal user, ChangePasswordModel changePasswordModel)
        {
            try
            {
                // Lấy thông tin người dùng từ context hoặc token
                var userId = user.FindFirst("UserId")?.Value;
                var userEntity = await _context.ht_Users.FindAsync(Guid.Parse(userId));

                if (userEntity == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "User not found.");
                }

                // Kiểm tra mật khẩu hiện tại
                if (!BCrypt.Net.BCrypt.Verify(changePasswordModel.CurrentPassword, userEntity.Password))
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "Current password is incorrect.");
                }

                // Kiểm tra mật khẩu mới và xác nhận mật khẩu mới
                if (string.IsNullOrEmpty(changePasswordModel.NewPassword) ||
                    !CheckExtensions.IsValidPass(changePasswordModel.NewPassword) ||
                    changePasswordModel.NewPassword != changePasswordModel.ConfirmNewPassword)
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "Invalid new password or confirm password does not match.");
                }

                // Cập nhật mật khẩu mới
                userEntity.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordModel.NewPassword);
                userEntity.PasswordUpdatedDate = DateTime.UtcNow;


                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
                // Tạo lại token sau khi thay đổi mật khẩu (nếu cần)
                var authenticationResult = await Authenticate(userEntity.Email, userEntity.Password);


                return new ApiResponse<bool>(true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get user information
        /// Get thông tin User đã đăng nhập
        /// </summary>
        /// <param name="user">User đã đăng nhập</param>
        /// <returns></returns>
        public async Task<ApiResponse> GetCurrentUser(ClaimsPrincipal user)
        {
            try
            {
                // Lấy thông tin người dùng từ context hoặc token
                var userId = user.FindFirst("UserId")?.Value;
                var userEntity = await _context.ht_Users.FindAsync(Guid.Parse(userId));

                if (userEntity == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "User not found.");
                }

                var userToAccount = _mapper.Map<UserViewModel>(userEntity);

                return new ApiResponse<UserViewModel>(userToAccount);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Create User Account
        /// tạo tài khoản User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResponse> Register(UserUpdateCreateModel request)
        {
            try
            {
                // Kiểm tra khác null, có ít nhất 6 ký tự, có ít nhất một chữ cái hoa, một chữ cái thường và một ký tự đặc biệt
                if (!CheckExtensions.IsValidPass(request.Password))
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "Password must have at least 6 characters, including at least one uppercase letter, one lowercase letter, one digit, and one special character.");
                }

                // Kiểm tra khác null, có ít nhất 6 ký tự, có ít nhất một chữ cái hoa, một chữ cái thường và một ký tự đặc biệt
                if (!CheckExtensions.IsValidUsername(request.UserName))
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "Username does not contain capital letters, numbers, or special characters and is between 4 and 50 characters long.");
                }

                // Check xem đây có phải là email không
                if (!CheckExtensions.IsValidEmail(request.Email))
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "Please enter email in _@_.com format.");
                }

                // Kiểm tra xem người dùng đã tồn tại chưa
                var existingUser = await _context.ht_Users.FirstOrDefaultAsync(o => o.UserName == request.UserName);

                if (existingUser != null)
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "UserName is already registered.");
                }

                // Tạo mới người dùng
                var newUser = new ht_User
                {
                    Id = Guid.NewGuid(),
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password), // Mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
                    Phone = request.Phone,
                    FullName = request.FullName,
                    BusinessAreas = request.BusinessAreas, 
                    IsDeleted = false,
                    Role = request.Role,
                    UserName = request.UserName,

                    //----------------
                    CreateDate = DateTime.UtcNow,
                };

                // Thêm người dùng mới vào cơ sở dữ liệu
                _context.ht_Users.Add(newUser);
                await _context.SaveChangesAsync();

                var userToAccount = _mapper.Map<UserViewModel>(newUser);

                return new ApiResponse<UserViewModel>(userToAccount);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, ghi log và trả về một phản hồi lỗi.
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ApiResponse> RemoveAccount(ClaimsPrincipal user)
        {
            try
            {
                // Lấy thông tin người dùng từ context hoặc token
                var userIdClaim = user.FindFirst("UserId");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "Invalid user token.");
                }

                var userEntity = await _context.ht_Users.FindAsync(userId);

                if (userEntity == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "User not found.");
                }

                userEntity.IsDeleted = true;
                userEntity.DeletedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new ApiResponse("Account removed successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ApiResponse> Logout(ClaimsPrincipal user)
        {
            try
            {
                // Lấy userId từ claims
                var userId = user.FindFirst("UserId")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "Invalid user.");
                }
                // Tạo các claims để thêm vào token black list
                var claims = new List<Claim>
                {
                    new Claim("Logout", "true"),
                    new Claim("UserId", userId),
                };

                // Lấy key từ cấu hình
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

                // Tạo token với các claims
                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

                return new ApiResponse("Logout successful.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Khôi phục tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResponse> RecoverAccount(Guid id)
        {
            try
            {
                // Lấy thông tin người dùng từ context hoặc token
                var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("UserId");

                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "Invalid user token.");
                }

                var userEntity = await _context.ht_Users.FindAsync(userId);

                if (userEntity == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "User not found.");
                }

                // Kiểm tra quyền trước khi khôi phục tài khoản
                if (!userEntity.Role.Equals(AppRole.Admin) && userId != userEntity.Id)
                {
                    return new ApiResponseError(HttpStatusCode.Forbidden, "You don't have permission to recover this account.");
                }

                // Lấy thông tin người dùng cần khôi phục
                var userToRecover = await _context.ht_Users.FindAsync(id);

                if (userToRecover == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "User to recover not found.");
                }

                // Khôi phục tài khoản khỏi cơ sở dữ liệu
                userToRecover.IsDeleted = false;

                await _context.SaveChangesAsync();

                return new ApiResponse("Account recovered successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật thông tin User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResponse> UpdateInformation(ClaimsPrincipal user, UserUpdateCreateModel request)
        {
            try
            {
                // Kiểm tra khác null, có ít nhất 6 ký tự, có ít nhất một chữ cái hoa, một chữ cái thường và một ký tự đặc biệt
                if (!CheckExtensions.IsValidPass(request.Password))
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "Password must have at least 6 characters, including at least one uppercase letter, one lowercase letter, one digit, and one special character.");
                }

                // Kiểm tra khác null, có ít nhất 6 ký tự, có ít nhất một chữ cái hoa, một chữ cái thường và một ký tự đặc biệt
                if (!CheckExtensions.IsValidUsername(request.UserName))
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "Username does not contain capital letters, numbers, or special characters and is between 4 and 50 characters long.");
                }

                // Check xem đây có phải là email không
                if (!CheckExtensions.IsValidEmail(request.Email))
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "Please enter email in _@_.com format.");
                }

                // Lấy thông tin người dùng từ context hoặc token
                var userId = user.FindFirst("UserId")?.Value;


                // Kiểm tra xem người dùng đã tồn tại chưa
                var existingUser = await _context.ht_Users.FirstOrDefaultAsync(o => o.UserName == request.UserName && o.Id == Guid.Parse(userId));

                if (existingUser != null)
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "UserName is already registered.");
                }

                // Cập nhật mật khẩu mới
                var userToUpdate = await _context.ht_Users.FindAsync(Guid.Parse(userId));

                if (userToUpdate == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "User not found.");
                }

                if (userToUpdate != null)
                {
                    userToUpdate.UserName = request.UserName;
                    userToUpdate.Password = request.Password;
                    userToUpdate.Phone = request.Phone;
                    userToUpdate.Email = request.Email;
                    userToUpdate.FullName = request.FullName;
                    userToUpdate.BusinessAreas = request.BusinessAreas;
                    userToUpdate.IsDeleted = request.IsDeleted;
                    userToUpdate.Role = request.Role;

                    //-------------
                    userToUpdate.ModifiedDate = DateTime.UtcNow;

                    await _context.SaveChangesAsync();

                    return new ApiResponse<bool>(true);
                }

                return new ApiResponse<bool>(HttpStatusCode.BadRequest, false, "update thất bại");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetAllUsers(string search = "", int currentPage = 1, int pageSize = 1)
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var query = _context.ht_Users.AsQueryable();

                // Áp dụng bộ lọc nếu có
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(o => o.FullName.Contains(search.Trim()) || o.UserName.ToString().Contains(search.Trim()));
                }

                // Lấy tổng số lượng phần tử
                var totalItems = await query.CountAsync();

                // Phân trang và lấy dữ liệu cho trang hiện tại
                var data = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(o => o.CreateDate)
                    .ToListAsync();

                var result = _mapper.Map<List<UserViewModel>>(data).ToList();

                return new ApiResponsePagination<UserViewModel>(result, totalItems, currentPage, pageSize);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Something went wrong: " + ex.Message);
            }
        }
    }
}
