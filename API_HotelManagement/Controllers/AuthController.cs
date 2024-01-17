using API_HotelManagement.Business.Services.Auth;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthHandler _authHandler;

        public AuthController(IAuthHandler authHandler)
        {
            _authHandler = authHandler;
        }

        //[AllowAnonymous, HttpPost(), Route("LoginUser")]
        //[ProducesResponseType(typeof(ApiResponse<UserAccountViewModel>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> Login([FromBody] AuthUser request)
        //{
        //    var toKen = await _authHandler.Authenticate(param);

        //    if (toKen == null)
        //        return Unauthorized();

        //    return ApiHelper.TransformData(toKen);
        //}
    }
}
