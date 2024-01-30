using API_HotelManagement.Business.Services.Rooms;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        private readonly IRoomHandler _roomService;

        public TestController(IRoomHandler roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return Ok("Thành Công");
        }

        /// <summary>
        /// show all rooms
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage">Page hiển thị hiện tại </param>
        /// <param name="pageSize">Kích thước lỗi trang</param>
        /// <returns></returns>
        [HttpGet, Route("GetAllRooms")]
        [ProducesResponseType(typeof(ApiResponsePagination<RoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRooms(string search = "", int currentPage = 1, int pageSize = 10)
        {

            var result = await _roomService.GetAllRooms(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }
    }
}
