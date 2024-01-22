using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business.Services.Services
{
    public interface IServiceHanlder
    {
        Task<ApiResponse> GetAllServices(string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetServiceById(Guid id);
        Task<ApiResponse> CreateService(ServiceCreateUpdateModel model);
        Task<ApiResponse> UpdateService(Guid id, ServiceCreateUpdateModel model);
        Task<ApiResponse> DeleteService(Guid id);
    }
}
