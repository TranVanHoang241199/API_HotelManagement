using API_HotelManagement.Business.Rooms.CategoryRooms;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/room")]
    [ApiController]
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

        /// <summary>
        /// show all CategoryRooms
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage">Page hiển thị hiện tại </param>
        /// <param name="pageSize">Kích thước lỗi trang</param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("GetAllCategoryRooms")]
        [ProducesResponseType(typeof(ApiResponsePagination<CategoryRoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategoryRooms(string search = "", int currentPage = 1, int pageSize = 10)
        {

            var result = await _CategoryRoomService.GetAllCategoryRooms(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// show CategoryRoom based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("GetCategoryRoomById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryRoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryRoomById(Guid id)
        {

            var result = await _CategoryRoomService.GetCategoryRoomById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Create a new CategoryRoom
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("CreateCategoryRoom")]
        [ProducesResponseType(typeof(ApiResponseObject<CategoryRoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCategoryRoom([FromBody] CategoryRoomCreateUpdateModel model)
        {

            var result = await _CategoryRoomService.CreateCategoryRoom(model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Edit CategoryRoom information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPut, Route("UpdateCategoryRoom/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCategoryRoom(Guid id, [FromBody] CategoryRoomCreateUpdateModel model)
        {

            var result = await _CategoryRoomService.UpdateCategoryRoom(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// delete CategoryRoom
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpDelete, Route("DeleteCategoryRoom/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCategoryRoom(Guid id)
        {

            var result = await _CategoryRoomService.DeleteCategoryRoom(id);

            return ApiHelper.TransformData(result);
        }
    }
}
