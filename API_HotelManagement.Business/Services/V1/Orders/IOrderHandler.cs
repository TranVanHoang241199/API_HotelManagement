using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business
{
    public interface IOrderHandler
    {
        Task<ApiResponse> Gets(string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetAllRoomDetails(Guid orderId, string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetserviceDetails(Guid orderId, string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetOrderById(Guid id);
        Task<ApiResponse> GetRoomDetailById(Guid id);
        Task<ApiResponse> GetServiceDetailById(Guid id);
        Task<ApiResponse> Create(OrderCreateUpdateModel model);
        Task<ApiResponse> CreateRoomDetail(Guid orderId, OrderRoomCreateUpdateModel model);
        Task<ApiResponse> CreateServiceDetail(Guid orderId, OrderServiceCreateUpdateModel model);
        Task<ApiResponse> Update(Guid id, OrderCreateUpdateModel model);
        Task<ApiResponse> UpdateRoomDetail(Guid id, OrderRoomCreateUpdateModel model);
        Task<ApiResponse> UpdateServiceDetail(Guid id, OrderServiceCreateUpdateModel model);
        Task<ApiResponse> Delete(Guid id);
        Task<ApiResponse> DeleteRoomDetail(Guid id);
        Task<ApiResponse> DeleteServiceDetail(Guid id);
    }
}
