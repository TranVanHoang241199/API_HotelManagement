using API_HotelManagement.Business.Services.CategoryRooms;
using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business.Rooms.CategoryRooms
{
    public interface ICategoryRoomHandler
    {
        Task<ApiResponse> GetAllCategoryRooms(string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetCategoryRoomById(Guid id);
        Task<ApiResponse> CreateCategoryRoom(CategoryRoomCreateUpdateModel model);
        Task<ApiResponse> UpdateCategoryRoom(Guid id, CategoryRoomCreateUpdateModel model);
        Task<ApiResponse> DeleteCategoryRoom(Guid id);
    }
}
