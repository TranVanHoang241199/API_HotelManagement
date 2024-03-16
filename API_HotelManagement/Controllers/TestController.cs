using API_HotelManagement.Business.Services.Rooms;
using API_HotelManagement.common.Utils;
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
        /// <returns></returns>
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Thành Công");
        }

        /// <summary>
        /// show all rooms (not auth)
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
