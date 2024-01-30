using API_HotelManagement.Business.Services.Rooms;
using API_HotelManagement.Business.Services.Services;
using API_HotelManagement.common.Helps.HelpBusiness;
using API_HotelManagement.Data.Data.Entitys;
using AutoMapper;

namespace API_HotelManagement.Business.Services.Orders
{
    #region Order
    public class OrderQuery
    {
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public string Search { get; set; } = "";
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 1;
    }

    public class OrderViewModel : BaseViewModel
    {
        public string? CustomerPhone { get; set; }
        public string? CustomerName { get; set; }
        public string? Identifier { get; set; }
        public string? Note { get; set; }

        public List<OrderRoomDetailViewModel>? OrderRoomDetails { get; set; }
        public List<OrderServiceDetailViewModel>? OrderServiceDetails { get; set; }
    }

    public class OrderCreateUpdateModel
    {
        public string? CustomerPhone { get; set; }
        public string? CustomerName { get; set; }
        public string? Identifier { get; set; }
        public string? Note { get; set; }
        public List<OrderRoomDetailCreateUpdateModel>? OrderRoomDetails { get; set; } 
        public List<OrderServiceDetailCreateUpdateModel>? OrderServiceDetails { get; set; } 
    }
    #endregion Order

    #region OrderRoomDetail
    public class OrderRoomDetailViewModel
    {
        public Guid Id { get; set; }    
        public DateTime? OrderTime { get; set; }
        public int Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public List<RoomViewModel>? Rooms { get; set; }
    }

    public class OrderRoomDetailCreateUpdateModel
    {
        //public Guid? OrderId { get; set; }
        public Guid? RoomId { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
    }

    #endregion OrderRoomDetail

    #region OrderServiceDetail
    public class OrderServiceDetailViewModel
    {
        public Guid Id { get; set; }
        public DateTime? OrderTime { get; set; }
        public int Quantity { get; set; }
        public string? TotalPrice { get; set; }
        public List<ServiceViewModel>? Services { get; set; }
    }

    public class OrderServiceDetailCreateUpdateModel
    {
        //public Guid? OrderId { get; set; }
        public Guid? ServiceId { get; set; }
        public int Quantity { get; set; }
        public Guid? RoomOrDerviceId { get; set; }
    }
    #endregion OrderServiceDetail

    /// <summary>
    /// 
    /// </summary>
    public class OrderAutoMapper : Profile
    {
        public OrderAutoMapper()
        {
            // ht_Order to OrderViewModel
            CreateMap<ht_Order, OrderViewModel>()
                .ForMember(dest => dest.OrderRoomDetails, opt => opt.MapFrom(src => src.OrderRoomDetails))
                .ForMember(dest => dest.OrderServiceDetails, opt => opt.MapFrom(src => src.OrderServiceDetails));

            // ht_OrderRoomDetail to OrderRoomDetailViewModel
            CreateMap<ht_OrderRoomDetail, OrderRoomDetailViewModel>()
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Room));

            // ht_OrderServiceDetail to OrderServiceDetailViewModel
            CreateMap<ht_OrderServiceDetail, OrderServiceDetailViewModel>()
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Service));

            // ... Add other mappings if necessary
        }
    }
}
