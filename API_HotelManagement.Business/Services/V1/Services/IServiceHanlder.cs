using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business
{
    public interface IServiceHanlder
    {
        Task<ApiResponse> Gets(string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetServiceById(Guid id);
        Task<ApiResponse> Create(ServiceCreateUpdateModel model);
        Task<ApiResponse> Update(Guid id, ServiceCreateUpdateModel model);
        Task<ApiResponse> Delete(Guid id);
    }
}
