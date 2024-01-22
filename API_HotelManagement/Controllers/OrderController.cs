using API_HotelManagement.Business.Services.Orders;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    //[ApiExplorerSettings(GroupName = "Users")]
    [Route("api/v1/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderHandler _orderService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderService"></param>
        public OrderController(IOrderHandler OrderService)
        {
            _orderService = OrderService;
        }

        /// <summary>
        /// show all orders
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage">Page hiển thị hiện tại </param>
        /// <param name="pageSize">Kích thước lỗi trang</param>
        /// <returns></returns>
        [Authorize, HttpGet(), Route("GetAllOrders")]
        [ProducesResponseType(typeof(ApiResponsePagination<OrderViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrders(string search = "", int currentPage = 1, int pageSize = 10)
        {

            var result = await _orderService.GetAllOrders(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// show order based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpGet(), Route("GetOrderById{id}")]
        [ProducesResponseType(typeof(ApiResponse<OrderViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderById(Guid id)
        {

            var result = await _orderService.GetOrderById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost(), Route("CreateOrder")]
        [ProducesResponseType(typeof(ApiResponseObject<OrderViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateUpdateModel model)
        {

            var result = await _orderService.CreateOrder(model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Edit order information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPut(), Route("UpdateOrder{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderCreateUpdateModel model)
        {

            var result = await _orderService.UpdateOrder(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// delete order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpDelete(), Route("DeleteOrder{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {

            var result = await _orderService.DeleteOrder(id);

            return ApiHelper.TransformData(result);
        }
    }
}
