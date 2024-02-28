using API_HotelManagement.Business.Services.Hotels;
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
    public class HotelController : ControllerBase
    {
        private readonly IHotelHandler _hotelService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HotelService"></param>
        public HotelController(IHotelHandler HotelService)
        {
            _hotelService = HotelService;
        }

        /// <summary>
        /// show all Hotels
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage">Page hiển thị hiện tại </param>
        /// <param name="pageSize">Kích thước lỗi trang</param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("GetAllHotels")]
        [ProducesResponseType(typeof(ApiResponsePagination<HotelViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllHotels(string search = "", int currentPage = 1, int pageSize = 10)
        {

            var result = await _hotelService.GetAllHotels(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// show Hotel based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("GetHotelById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<HotelViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHotelById(Guid id)
        {

            var result = await _hotelService.GetHotelById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Create a new Hotel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("CreateHotel")]
        [ProducesResponseType(typeof(ApiResponseObject<HotelViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateUpdateModel model)
        {

            var result = await _hotelService.CreateHotel(model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Edit Hotel information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPut, Route("UpdateHotel/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] HotelCreateUpdateModel model)
        {

            var result = await _hotelService.UpdateHotel(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// delete Hotel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpDelete, Route("DeleteHotel/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {

            var result = await _hotelService.DeleteHotel(id);

            return ApiHelper.TransformData(result);
        }
    }
}
