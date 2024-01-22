using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business.Services.Orders
{
    public interface IOrderHandler
    {
        Task<ApiResponse> GetAllOrders(string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetOrderById(Guid id);
        Task<ApiResponse> CreateOrder(OrderCreateUpdateModel model);
        Task<ApiResponse> UpdateOrder(Guid id, OrderCreateUpdateModel model);
        Task<ApiResponse> DeleteOrder(Guid id);
    }
}
