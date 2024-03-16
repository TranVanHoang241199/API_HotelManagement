using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business
{
    public interface ICategoryServiceHandler
    {
        Task<ApiResponse> Gets(string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetCategoryServiceById(Guid id);
        Task<ApiResponse> Create(CategoryServiceCreateUpdateModel model);
        Task<ApiResponse> Update(Guid id, CategoryServiceCreateUpdateModel model);
        Task<ApiResponse> Delete(Guid id);
    }
}
