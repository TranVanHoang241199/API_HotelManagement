using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business
{
    public interface ICustomerHandler
    {
        Task<ApiResponse> Gets(string search = "", int currentPage = 1, int pageSize = 10);
        Task<ApiResponse> GetCustomerById(Guid id);
    }
}
