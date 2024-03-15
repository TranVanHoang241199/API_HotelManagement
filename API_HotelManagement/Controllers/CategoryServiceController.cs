using API_HotelManagement.Business.Services.CategoryServices;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/services")]
    [ApiController]
    public class CategoryServiceController : ControllerBase
    {
        private readonly ICategoryServiceHandler _categoryServiceService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryServiceService"></param>
        public CategoryServiceController(ICategoryServiceHandler categoryServiceService)
        {
            _categoryServiceService = categoryServiceService;
        }

        /// <summary>
        /// Lấy về theo bộ lọc
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage">Page hiển thị hiện tại </param>
        /// <param name="pageSize">Kích thước lỗi trang</param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("")]
        [ProducesResponseType(typeof(ApiResponsePagination<CategoryServiceViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategoryServices(string search = "", int currentPage = 1, int pageSize = 10)
        {

            var result = await _categoryServiceService.GetAllCategoryServices(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy về theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryServiceViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryServiceById(Guid id)
        {

            var result = await _categoryServiceService.GetCategoryServiceById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("")]
        [ProducesResponseType(typeof(ApiResponseObject<CategoryServiceViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCategoryService([FromBody] CategoryServiceCreateUpdateModel model)
        {

            var result = await _categoryServiceService.CreateCategoryService(model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPut, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCategoryService(Guid id, [FromBody] CategoryServiceCreateUpdateModel model)
        {

            var result = await _categoryServiceService.UpdateCategoryService(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Xoá
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpDelete, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCategoryService(Guid id)
        {

            var result = await _categoryServiceService.DeleteCategoryService(id);

            return ApiHelper.TransformData(result);
        }
    }
}
