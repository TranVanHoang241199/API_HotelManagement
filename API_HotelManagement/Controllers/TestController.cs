using API_HotelManagement.Business.Services.Auths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/test")]
    [AllowAnonymous]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Login()
        {
            return Ok("Thành Công");
        }
    }
}
