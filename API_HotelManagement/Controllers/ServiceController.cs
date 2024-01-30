using API_HotelManagement.Business.Services.Services;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    //[ApiExplorerSettings(GroupName = "Users")]
    [Route("api/v1/service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceHanlder _serviceService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServiceService"></param>
        public ServiceController(IServiceHanlder ServiceService)
        {
            _serviceService = ServiceService;
        }

        /// <summary>
        /// show all services
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage">Page hiển thị hiện tại </param>
        /// <param name="pageSize">Kích thước lỗi trang</param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("GetAllServices")]
        [ProducesResponseType(typeof(ApiResponsePagination<ServiceViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllServices(string search = "", int currentPage = 1, int pageSize = 10)
        {
            var result = await _serviceService.GetAllServices(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// show service based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("GetServiceById/{id}")]
        [ProducesResponseType(typeof(ApiResponse<ServiceViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetServiceById(Guid id)
        {
            var result = await _serviceService.GetServiceById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Create a new service
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("CreateService")]
        [ProducesResponseType(typeof(ApiResponseObject<ServiceViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateService([FromBody] ServiceCreateUpdateModel model)
        {
            var result = await _serviceService.CreateService(model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Edit service information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPut, Route("UpdateService/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateService(Guid id, [FromBody] ServiceCreateUpdateModel model)
        {
            var result = await _serviceService.UpdateService(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// delete service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpDelete, Route("DeleteService/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            var result = await _serviceService.DeleteService(id);

            return ApiHelper.TransformData(result);
        }
    }
}
