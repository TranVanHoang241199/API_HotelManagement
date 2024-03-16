using API_HotelManagement.Business;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/rooms")]
    [ApiExplorerSettings(GroupName = "Room")]
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

        #region CRUD

        /// <summary>
        /// Lấy về theo bộ lọc
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage">Page hiển thị hiện tại </param>
        /// <param name="pageSize">Kích thước lỗi trang</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpGet, Route("")]
        [ProducesResponseType(typeof(ApiResponsePagination<RoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Gets([FromQuery] string search = "", [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {

            var result = await _roomService.Gets(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy về theo Id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<RoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomById(Guid id)
        {

            var result = await _roomService.GetRoomById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="model">Dữ liệu</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpPost, Route("")]
        [ProducesResponseType(typeof(ApiResponseObject<RoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] RoomCreateUpdateModel model)
        {

            var result = await _roomService.Create(model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <param name="model">Dữ liệu</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpPut, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id, [FromBody] RoomCreateUpdateModel model)
        {

            var result = await _roomService.Update(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Xoá
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpDelete, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await _roomService.Delete(id);

            return ApiHelper.TransformData(result);
        }

        #endregion CRUD

    }
}
