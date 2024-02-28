using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business.Services.Rooms
{
    public interface IRoomHandler
    {
        Task<ApiResponse> GetAllRooms(string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetRoomById(Guid id);
        Task<ApiResponse> CreateRoom(RoomCreateUpdateModel model);
        Task<ApiResponse> UpdateRoom(Guid id, RoomCreateUpdateModel model);
        Task<ApiResponse> DeleteRoom(Guid id);
    }
}
