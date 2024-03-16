using API_HotelManagement.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// (not auth)
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [AllowAnonymous, Route("api/v1/test")]
    [ApiExplorerSettings(GroupName = "_Test")]
    public class TestController : ControllerBase
    {

        private readonly IRoomHandler _roomService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomService"></param>
        public TestController(IRoomHandler roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// (not auth)
        /// </summary>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Thành Công");
        }

        ///// <summary>
        ///// show all rooms (not auth)
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="currentPage">Page hiển thị hiện tại </param>
        ///// <param name="pageSize">Kích thước lỗi trang</param>
        ///// <response code="200">Thành công</response>
        //[HttpGet, Route("Gets")]
        //[ProducesResponseType(typeof(ApiResponsePagination<RoomViewModel>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> Gets([FromQuery] string search = "", [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        //{

        //    var result = await _roomService.Gets(search, currentPage, pageSize);

        //    return ApiHelper.TransformData(result);
        //}
    }
}
