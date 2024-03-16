using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business
{
    public interface IRoomHandler
    {
        Task<ApiResponse> Gets(string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetRoomById(Guid id);
        Task<ApiResponse> Create(RoomCreateUpdateModel model);
        Task<ApiResponse> Update(Guid id, RoomCreateUpdateModel model);
        Task<ApiResponse> Delete(Guid id);
    }
}
