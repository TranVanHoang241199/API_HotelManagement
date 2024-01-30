using API_HotelManagement.common.Helps;
using API_HotelManagement.common.Helps.HelpBusiness;
using API_HotelManagement.Data.Data.Entitys;
using AutoMapper;

namespace API_HotelManagement.Business.Services.Rooms
{

    public class RoomViewModel : BaseViewModel
    {
        public string? RoomName { get; set; }
        public int FloorNumber { get; set; }
        public decimal Price { get; set; }
        public EStatusRoom Status { get; set; }
    }

    public class RoomCreateUpdateModel
    {
        public string? RoomName { get; set; }
        public int FloorNumber { get; set; }
        public decimal Price { get; set; }
        public EStatusRoom Status { get; set; }
        public Guid CategoryRoomId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RoomAutoMapper : Profile
    {
        public RoomAutoMapper()
        {
            CreateMap<ht_Room, RoomViewModel>(); // Auto map ht_Room to RoomViewModel
            //CreateMap<RoomCreateUpdateModel, ht_Room>(); // Auto map RoomCreateUpdateModel to ht_Room
        }
    }
}
