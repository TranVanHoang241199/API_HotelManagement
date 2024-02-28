using API_HotelManagement.Business.Rooms.CategoryRooms;
using API_HotelManagement.common.Helps.HelpBusiness;
using API_HotelManagement.Data.Data.Entitys;
using AutoMapper;

namespace API_HotelManagement.Business.Services.Hotels
{
    public class HotelViewModel : BaseViewModel
    {
        public string? HotelName { get; set; }
        public string? Description { get; set; }
    }

    public class HotelCreateUpdateModel
    {
        public string? HotelName { get; set; }
        public string? Description { get; set; }
    }

    public class HotelAutoMapper : Profile
    {
        public HotelAutoMapper()
        {
            CreateMap<ht_Hotel, HotelViewModel>(); // Auto map ht_Room to RoomViewModel
        }
    }
}
