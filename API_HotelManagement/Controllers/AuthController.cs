using API_HotelManagement.Business.Services.Auths;
using API_HotelManagement.common.Helps;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    //[ApiExplorerSettings(GroupName = "Auths")]
    [Route("api/v1/auth")]
    [AllowAnonymous]
    [ApiController]
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
        /// Login user
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
        /// register for user
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
        /// change Password for user
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
        /// Update information for User
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
        /// Recover disabled account
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
        /// delete account for user
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
        /// Get user login information
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
        /// Get user login information
        /// </summary>
        /// <returns></returns>
        [Authorize, HttpGet, Route("GetAllUser")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers(string search = "", int currentPage = 1, int pageSize = 10)
        {
            var result = await _authHandler.GetAllUsers(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
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
