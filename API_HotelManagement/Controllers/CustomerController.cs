using API_HotelManagement.Business.Services.Customers;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/customers")]
    [ApiController]
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
        /// <returns></returns>
        [Authorize, HttpGet, Route("")]
        [ProducesResponseType(typeof(ApiResponsePagination<CustomerViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCustomers(string search = "", int currentPage = 1, int pageSize = 10)
        {

            var result = await _customerService.GetAllCustomers(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy về theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<CustomerViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {

            var result = await _customerService.GetCustomerById(id);

            return ApiHelper.TransformData(result);
        }


    }
}
