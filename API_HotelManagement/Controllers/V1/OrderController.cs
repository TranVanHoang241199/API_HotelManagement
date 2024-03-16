using API_HotelManagement.Business;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// Module Order phòng và dịch vụ
    /// </summary>
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
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpGet, Route("")]
        [ProducesResponseType(typeof(ApiResponsePagination<OrderViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Gets([FromQuery] string search = "", [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {

            var result = await _orderService.Gets(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy về theo Id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
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
        /// <param name="model">Dữ liệu</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpPost, Route("")]
        [ProducesResponseType(typeof(ApiResponseObject<OrderViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] OrderCreateUpdateModel model)
        {

            var result = await _orderService.Create(model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Cập nhật Order
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <param name="model">Dữ liệu</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpPut, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id, [FromBody] OrderCreateUpdateModel model)
        {

            var result = await _orderService.Update(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Xoá Order
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpDelete, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await _orderService.Delete(id);

            return ApiHelper.TransformData(result);
        }
        #endregion CRUD order

        #region CRUD room-detail

        /// <summary>
        /// Lấy về chi danh sách phòng theo bộ lọc
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="search"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpGet(), Route("room-details")]
        [ProducesResponseType(typeof(ApiResponsePagination<RoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRoomDetals(Guid orderId, [FromQuery] string search = "", [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {

            var result = await _orderService.GetAllRoomDetails(orderId, search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy về chi tiết phòng theo Id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpGet(), Route("room-detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<OrderRoomDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomDetailById(Guid id)
        {

            var result = await _orderService.GetRoomDetailById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Thêm phòng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="model">Dữ liệu</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpPost(), Route("room-detail")]
        [ProducesResponseType(typeof(ApiResponseObject<OrderRoomDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRoomDetail(Guid orderID, [FromBody] OrderRoomCreateUpdateModel model)
        {

            var result = await _orderService.CreateRoomDetail(orderID, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Sửa phòng
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <param name="model">Dữ liệu</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpPut(), Route("room-detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRoomDetail(Guid id, [FromBody] OrderRoomCreateUpdateModel model)
        {

            var result = await _orderService.UpdateRoomDetail(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Xoá phòng
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpDelete(), Route("room-detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRoomDetail(Guid id)
        {

            var result = await _orderService.DeleteRoomDetail(id);

            return ApiHelper.TransformData(result);
        }
        #endregion  room-detail

        #region CRUD service-detail

        /// <summary>
        /// Lấy về ds dịch vụ theo bộ lọc
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="search"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpGet(), Route("service-details")]
        [ProducesResponseType(typeof(ApiResponsePagination<OrderServiceDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetserviceDetals(Guid orderId, [FromQuery] string search = "", [FromQuery] int currentPage = 1, [FromQuery] int pageSize = 10)
        {

            var result = await _orderService.GetserviceDetails(orderId, search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy về dịch vụ theo Id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpGet(), Route("service-detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<OrderRoomDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetServiceDetailById(Guid id)
        {

            var result = await _orderService.GetServiceDetailById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Thêm dịch vụ
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="model">Dữ liệu</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpPost(), Route("service-detail")]
        [ProducesResponseType(typeof(ApiResponseObject<OrderServiceDetailViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateServiceDetail(Guid orderId, [FromBody] OrderServiceCreateUpdateModel model)
        {

            var result = await _orderService.CreateServiceDetail(orderId, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Cập nhật dịch vụ
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <param name="model">Dữ liệu</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpPut(), Route("service-detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateServiceDetail(Guid id, [FromBody] OrderServiceCreateUpdateModel model)
        {

            var result = await _orderService.UpdateServiceDetail(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Xoá dịch vụ
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Kết quả trả về</returns>
        /// <response code="200">Thành công</response>
        [Authorize, HttpDelete(), Route("service-detail/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteServiceDetail(Guid id)
        {

            var result = await _orderService.DeleteServiceDetail(id);

            return ApiHelper.TransformData(result);
        }
        #endregion CRUD service-detail

    }
}
