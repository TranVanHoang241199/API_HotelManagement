using API_HotelManagement.Business.Services.Rooms;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    //[ApiExplorerSettings(GroupName = "Users")]
    [Route("api/v1/room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomHandler _roomService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomService"></param>
        public RoomController(IRoomHandler roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// show all rooms
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage">Page hiển thị hiện tại </param>
        /// <param name="pageSize">Kích thước lỗi trang</param>
        /// <returns></returns>
        [Authorize, HttpGet(), Route("GetAllRooms")]
        [ProducesResponseType(typeof(ApiResponsePagination<RoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRooms(string search = "", int currentPage = 1, int pageSize = 10)
        {

            var result = await _roomService.GetAllRooms(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// show room based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpGet(), Route("GetRoomById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<RoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomById(Guid id)
        {

            var result = await _roomService.GetRoomById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost(), Route("CreateRoom")]
        [ProducesResponseType(typeof(ApiResponseObject<RoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRoom([FromBody] RoomCreateUpdateModel model)
        {

            var result = await _roomService.CreateRoom(model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Edit room information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPut(), Route("UpdateRoom/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] RoomCreateUpdateModel model)
        {

            var result = await _roomService.UpdateRoom(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// delete room
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpDelete(), Route("DeleteRoom/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {

            var result = await _roomService.DeleteRoom(id);

            return ApiHelper.TransformData(result);
        }
    }
}
