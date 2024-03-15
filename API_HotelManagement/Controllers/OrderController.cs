using API_HotelManagement.Business.Services.Orders;
using API_HotelManagement.Business.Services.Rooms;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    //[ApiExplorerSettings(GroupName = "Users")]
    //[Route("api/v{version:apiVersion}/orders")]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v1/orders")]
    [ApiExplorerSettings(GroupName = "Order")]
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

        #region  CRUD order
        /// <summary>
        /// Lấy về theo bộ lọc
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage">Page hiển thị hiện tại </param>
        /// <param name="pageSize">Kích thước lỗi trang</param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("")]
        [ProducesResponseType(typeof(ApiResponsePagination<OrderViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrders(string search = "", int currentPage = 1, int pageSize = 10)
        {

            var result = await _orderService.GetAllOrders(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy về theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<OrderViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderById(Guid id)
        {

            var result = await _orderService.GetOrderById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Tạo order
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("")]
        [ProducesResponseType(typeof(ApiResponseObject<OrderViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateUpdateModel model)
        {

            var result = await _orderService.CreateOrder(model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Cập nhật Order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPut, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderCreateUpdateModel model)
        {

            var result = await _orderService.UpdateOrder(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Xoá Order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpDelete, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {

            var result = await _orderService.DeleteOrder(id);

            return ApiHelper.TransformData(result);
        }
        #endregion CRUD order

        #region CRUD room-detail

        /// <summary>
        /// Lấy về chi danh sách phòng theo bộ lọc
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Authorize, HttpGet(), Route("room-details")]
        [ProducesResponseType(typeof(ApiResponsePagination<RoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrderRoomDetals(Guid orderId, string search = "", int currentPage = 1, int pageSize = 10)
        {

            var result = await _orderService.GetAllOrderRoomDetails(orderId, search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy về chi tiết phòng theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpGet(), Route("room-detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<OrderRoomDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderRoomDetailById(Guid id)
        {

            var result = await _orderService.GetOrderRoomDetailById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Thêm phòng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost(), Route("room-detail")]
        [ProducesResponseType(typeof(ApiResponseObject<OrderRoomDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateOrderRoomDetail(Guid orderID, [FromBody] OrderRoomCreateUpdateModel model)
        {

            var result = await _orderService.CreateOrderRoom(orderID, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Sửa phòng
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPut(), Route("room-detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrderRoom(Guid id, [FromBody] OrderRoomCreateUpdateModel model)
        {

            var result = await _orderService.UpdateOrderRoom(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Xoá phòng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpDelete(), Route("room-detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteOrderRoom(Guid id)
        {

            var result = await _orderService.DeleteOrder(id);

            return ApiHelper.TransformData(result);
        }
        #endregion  room-detail

        #region  service-detail

        /// <summary>
        /// Lấy về ds dịch vụ theo bộ lọc
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Authorize, HttpGet(), Route("service-details")]
        [ProducesResponseType(typeof(ApiResponsePagination<OrderServiceDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrderServiceDetals(Guid orderId, string search = "", int currentPage = 1, int pageSize = 10)
        {

            var result = await _orderService.GetAllOrderServiceDetails(orderId, search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy về dịch vụ theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpGet(), Route("service-detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<OrderRoomDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderServiceDetailById(Guid id)
        {

            var result = await _orderService.GetOrderServiceDetailById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Thêm dịch vụ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost(), Route("service-detail")]
        [ProducesResponseType(typeof(ApiResponseObject<OrderServiceDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateOrderServiceDetail(Guid orderId, [FromBody] OrderServiceCreateUpdateModel model)
        {

            var result = await _orderService.CreateOrderService(orderId, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Cập nhật dịch vụ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPut(), Route("service-detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrderService(Guid id, [FromBody] OrderServiceCreateUpdateModel model)
        {

            var result = await _orderService.UpdateOrderService(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Xoá dịch vụ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpDelete(), Route("service-detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteOrderDetail(Guid id)
        {

            var result = await _orderService.DeleteOrder(id);

            return ApiHelper.TransformData(result);
        }
        #endregion  service-detail

    }
}
