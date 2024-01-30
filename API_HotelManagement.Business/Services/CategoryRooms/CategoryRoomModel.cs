using API_HotelManagement.Business.Services.Rooms;
using API_HotelManagement.common.Helps.HelpBusiness;
using API_HotelManagement.Data.Data.Entitys;
using AutoMapper;

namespace API_HotelManagement.Business.Rooms.CategoryRooms
{
    public class CategoryRoomViewModel : BaseViewModel
    {
        public string? Categoryname { get; set; }
    }

    public class CategoryRoomCreateUpdateModel
    {
        public string? Categoryname { get; set; }
    }

    public class CategoryRoomAutoMapper : Profile
    {
        public CategoryRoomAutoMapper()
        {
            CreateMap<ht_CategoryRoom, CategoryRoomViewModel>(); // Auto map ht_Room to RoomViewModel
        }
    }
}

