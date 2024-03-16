using API_HotelManagement.Business.Services.Auths;
using API_HotelManagement.common.Helps;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    //[Route("api/v{version:apiVersion}/auths")]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/auths")]
    [ApiExplorerSettings(GroupName = "Auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthHandler _authHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authHandler"></param>
        public AuthController(IAuthHandler authHandler)
        {
            _authHandler = authHandler;
        }

        /// <summary>
        /// Đăng nhập (not auth)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous, HttpPost, Route("login")]
        [ProducesResponseType(typeof(ApiResponseAuth), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginView request)
        {
            var model = await _authHandler.Authenticate(request.UserName, request.Password);

            if (model == null)
                return Unauthorized();

            return ApiHelper.TransformData(model);
        }

        /// <summary>
        /// Tạo tài khoản (not auth)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous, HttpPost, Route("register")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] UserUpdateCreateModel request)
        {
            var model = await _authHandler.Register(request);

            return ApiHelper.TransformData(model);
        }

        /// <summary>
        /// Đổi mật khẩu user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize, HttpPut, Route("change-pass")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePass([FromBody] ChangePasswordModel request)
        {
            var result = await _authHandler.ChangePassword(User, request);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Cập nhật thông tin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize, HttpPut, Route("update-information")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateInformation([FromBody] UserUpdateCreateModel request)
        {
            var result = await _authHandler.UpdateInformation(User, request);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// khôi phục tài khoản bị vô hiệu hoá
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = AppRole.Admin), HttpPut, Route("recover-account/{id}")]
        [ProducesResponseType(typeof(ApiResponse<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RecoverAccount(Guid id)
        {
            var result = await _authHandler.RecoverAccount(id);

            return ApiHelper.TransformData(result);
        }


        /// <summary>
        /// Xoá/vô hiệu hoá tài khoản
        /// </summary>
        /// <returns></returns>
        [Authorize, HttpDelete, Route("remove-account")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveAccount()
        {
            var result = await _authHandler.RemoveAccount(User);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// lấy thông tin tài khoản đã đăng nhập
        /// </summary>
        /// <returns></returns>
        [Authorize, HttpGet, Route("me")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Me()
        {
            var result = await _authHandler.GetCurrentUser(User);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy thông tin tài khoản(not auth)
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous, HttpGet, Route("check-usename")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckUsename(string usename)
        {
            var result = await _authHandler.CheckUsename(usename);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy tất cả tài khoản (not auth) (test)
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous, HttpGet, Route("")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers(string search = "", int currentPage = 1, int pageSize = 10)
        {
            var result = await _authHandler.GetAllUsers(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// lấy tất cả role (not auth) (test)
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous, HttpGet, Route("role")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoles()
        {
            var result = AppRole.GetAllRoles().ToList();

            var response = new ApiResponse<List<string>>(result);

            return ApiHelper.TransformData(response);
        }

        ///// <summary>
        ///// logout and revoke the token
        ///// </summary>
        ///// <returns></returns>
        //[Authorize, HttpPost(), Route("logout")]
        //[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> Logout()
        //{
        //    var result = await _authHandler.Logout(User);

        //    return ApiHelper.TransformData(result);
        //}


    }
}
