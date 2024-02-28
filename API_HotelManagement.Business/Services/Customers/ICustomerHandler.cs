using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business.Services.Customers
{
    public interface ICustomerHandler
    {
        Task<ApiResponse> GetAllCustomers(string search = "", int currentPage = 1, int pageSize = 10);
        Task<ApiResponse> GetCustomerById(Guid id);
    }
}
