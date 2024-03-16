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
    [Route("api/v1/customers")]
    [ApiExplorerSettings(GroupName = "Customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerHandler _customerService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerService"></param>
        public CustomerController(ICustomerHandler customerService)
        {
            _customerService = customerService;
        }


        /// <summary>
        /// Lấy về theo bộ lọc
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage">Page hiển thị hiện tại </param>
        /// <param name="pageSize">Kích thước lỗi trang</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpGet, Route("")]
        [ProducesResponseType(typeof(ApiResponsePagination<CustomerViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Gets([FromQuery] string search = "", [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {

            var result = await _customerService.Gets(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy về theo Id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CustomerViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {

            var result = await _customerService.GetCustomerById(id);

            return ApiHelper.TransformData(result);
        }


    }
}
