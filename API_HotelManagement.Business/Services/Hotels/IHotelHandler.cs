using API_HotelManagement.common.Utils;

namespace API_HotelManagement.Business.Services.Hotels
{
    public interface IHotelHandler
    {
        Task<ApiResponse> GetAllHotels(string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetHotelById(Guid id);
        Task<ApiResponse> CreateHotel(HotelCreateUpdateModel model);
        Task<ApiResponse> UpdateHotel(Guid id, HotelCreateUpdateModel model);
        Task<ApiResponse> DeleteHotel(Guid id);
    }
}
