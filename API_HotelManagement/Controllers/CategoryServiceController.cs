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
    [Route("api/v1/service")]
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
        /// show all CategoryServices
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage">Page hiển thị hiện tại </param>
        /// <param name="pageSize">Kích thước lỗi trang</param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("GetAllCategoryServices")]
        [ProducesResponseType(typeof(ApiResponsePagination<CategoryServiceViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategoryServices(string search = "", int currentPage = 1, int pageSize = 10)
        {

            var result = await _categoryServiceService.GetAllCategoryServices(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// show CategoryService based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("GetCategoryServiceById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryServiceViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryServiceById(Guid id)
        {

            var result = await _categoryServiceService.GetCategoryServiceById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Create a new CategoryService
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("CreateCategoryService")]
        [ProducesResponseType(typeof(ApiResponseObject<CategoryServiceViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCategoryService([FromBody] CategoryServiceCreateUpdateModel model)
        {

            var result = await _categoryServiceService.CreateCategoryService(model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Edit CategoryService information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPut, Route("UpdateCategoryService/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCategoryService(Guid id, [FromBody] CategoryServiceCreateUpdateModel model)
        {

            var result = await _categoryServiceService.UpdateCategoryService(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// delete CategoryService
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpDelete, Route("DeleteCategoryService/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCategoryService(Guid id)
        {

            var result = await _categoryServiceService.DeleteCategoryService(id);

            return ApiHelper.TransformData(result);
        }
    }
}
