using API_HotelManagement.Business;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    //[Route("api/v{version:apiVersion}/room-categories")]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/room-categories")]
    [ApiExplorerSettings(GroupName = "Room Category")]
    public class CategoryRoomController : ControllerBase
    {
        private readonly ICategoryRoomHandler _CategoryRoomService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryRoomService"></param>
        public CategoryRoomController(ICategoryRoomHandler CategoryRoomService)
        {
            _CategoryRoomService = CategoryRoomService;
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
        [ProducesResponseType(typeof(ApiResponsePagination<CategoryRoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Gets([FromQuery] string search = "", [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {

            var result = await _CategoryRoomService.Gets(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy về theo Id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryRoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryRoomById(Guid id)
        {

            var result = await _CategoryRoomService.GetCategoryRoomById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="model">Dữ liệu</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpPost, Route("")]
        [ProducesResponseType(typeof(ApiResponseObject<CategoryRoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CategoryRoomCreateUpdateModel model)
        {

            var result = await _CategoryRoomService.Create(model);

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
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoryRoomCreateUpdateModel model)
        {

            var result = await _CategoryRoomService.Update(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// xoá
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpDelete, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await _CategoryRoomService.Delete(id);

            return ApiHelper.TransformData(result);
        }
        #endregion CRUD

    }
}
