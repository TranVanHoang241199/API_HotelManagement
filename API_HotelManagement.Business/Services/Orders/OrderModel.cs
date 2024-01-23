using API_HotelManagement.Business.Services.Rooms;
using API_HotelManagement.Business.Services.Services;
using API_HotelManagement.common.Helps.HelpBusiness;
using API_HotelManagement.Data.Data.Entitys;
using AutoMapper;

namespace API_HotelManagement.Business.Services.Orders
{
    #region Order
    public class OrderViewModel : BaseViewModel
    {
        public string? CustomerPhone { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public string? Note { get; set; }

        public List<OrderRoomDetailViewModel>? OrderRoomDetails { get; set; }
        public List<OrderServiceDetailViewModel>? OrderServiceDetails { get; set; }
    }

    public class OrderCreateUpdateModel
    {
        public string? CustomerPhone { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public string? Note { get; set; }
        public List<Guid>? OrderRoomDetailId { get; set; } 
        public List<Guid>? OrderServiceDetailId { get; set; } 
    }
    #endregion Order

    #region OrderRoomDetail
    public class OrderRoomDetailViewModel
    {
        public Guid Id { get; set; }    
        public DateTime? OrderTime { get; set; }
        public int Quantity { get; set; }
        public string? TotalPrice { get; set; }
        public List<RoomViewModel>? Rooms { get; set; }
        
    }

    public class OrderRoomDetailCreateUpdateModel
    {
        public DateTime? OrderTime { get; set; }
        public int Quantity { get; set; }
        public string? TotalPrice { get; set; }
        public List<Guid>? RoomIds { get; set; }
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
        public DateTime? OrderTime { get; set; }
        public int Quantity { get; set; }
        public string? TotalPrice { get; set; }
        public List<Guid>? Service { get; set; }
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

            // OrderCreateUpdateModel to ht_Order
            //CreateMap<OrderCreateUpdateModel, ht_Order>().ForMember(dest => dest.OrderRoomDetails, opt => opt.Ignore()); // Ignore mapping for OrderDetails

        }
    }
}
