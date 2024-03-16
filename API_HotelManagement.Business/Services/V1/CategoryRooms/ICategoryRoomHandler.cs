using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business
{
    public interface ICategoryRoomHandler
    {
        Task<ApiResponse> Gets(string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetCategoryRoomById(Guid id);
        Task<ApiResponse> Create(CategoryRoomCreateUpdateModel model);
        Task<ApiResponse> Update(Guid id, CategoryRoomCreateUpdateModel model);
        Task<ApiResponse> Delete(Guid id);
    }
}
