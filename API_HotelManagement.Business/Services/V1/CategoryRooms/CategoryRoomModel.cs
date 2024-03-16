using API_HotelManagement.common.Helps.HelpBusiness;
using API_HotelManagement.Data;
using AutoMapper;

namespace API_HotelManagement.Business
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

