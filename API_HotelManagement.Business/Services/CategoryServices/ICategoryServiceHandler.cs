using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business.Services.CategoryServices
{
    public interface ICategoryServiceHandler
    {
        Task<ApiResponse> GetAllCategoryServices(string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetCategoryServiceById(Guid id);
        Task<ApiResponse> CreateCategoryService(CategoryServiceCreateUpdateModel model);
        Task<ApiResponse> UpdateCategoryService(Guid id, CategoryServiceCreateUpdateModel model);
        Task<ApiResponse> DeleteCategoryService(Guid id);
    }
}
