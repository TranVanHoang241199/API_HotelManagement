using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business.Services.Orders
{
    public interface IOrderHandler
    {
        Task<ApiResponse> GetAllOrders(string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetAllOrderRoomDetails(Guid orderId, string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetAllOrderServiceDetails(Guid orderId, string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetOrderById(Guid id);
        Task<ApiResponse> GetOrderRoomDetailById(Guid id);
        Task<ApiResponse> GetOrderServiceDetailById(Guid id);
        Task<ApiResponse> CreateOrder(OrderCreateUpdateModel model);
        Task<ApiResponse> CreateOrderRoom(Guid orderId, OrderRoomCreateUpdateModel model);
        Task<ApiResponse> CreateOrderService(Guid orderId, OrderServiceCreateUpdateModel model);
        Task<ApiResponse> UpdateOrder(Guid id, OrderCreateUpdateModel model);
        Task<ApiResponse> UpdateOrderRoom(Guid id, OrderRoomCreateUpdateModel model);
        Task<ApiResponse> UpdateOrderService(Guid id, OrderServiceCreateUpdateModel model);
        Task<ApiResponse> DeleteOrder(Guid id);
        Task<ApiResponse> DeleteOrderRoom(Guid id);
        Task<ApiResponse> DeleteOrderService(Guid id);
    }
}
