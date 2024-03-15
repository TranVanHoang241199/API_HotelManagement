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

        public List<OrderRoomDetailViewModel>? OrderRooms { get; set; }
        public List<OrderServiceDetailViewModel>? OrderServices { get; set; }
    }

    public class OrderCreateUpdateModel
    {
        public string? CustomerPhone { get; set; }
        public string? CustomerName { get; set; }
        public string? Identifier { get; set; }
        public string? Note { get; set; }
        public List<OrderRoomCreateUpdateModel>? OrderRooms { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public List<OrderServiceCreateUpdateModel>? OrderServices { get; set; } 
    }
    #endregion Order

    #region OrderRoomDetail
    /// <summary>
    /// order room view
    /// </summary>
    public class OrderRoomDetailViewModel : RoomViewModel
    {
        /// <summary>
        /// phòng cần thuê
        /// </summary>
        public Guid RoomId { get; set; }
        public Guid OrderId { get; set; }
        /// <summary>
        /// thời gian bắt đầu thuê
        /// </summary>
        public DateTime? TimeStart { get; set; }
        /// <summary>
        /// thời gian kết thúc thuê
        /// </summary>
        public DateTime? TimeEnd { get; set; }
    }

    /// <summary>
    /// order room create/update
    /// </summary>
    public class OrderRoomCreateUpdateModel
    {
        /// <summary>
        /// phòng cần thuê
        /// </summary>
        public Guid RoomId { get; set; }
        /// <summary>
        /// thời gian bắt đầu thuê
        /// </summary>
        public DateTime? TimeStart { get; set; }
        /// <summary>
        /// thời gian kết thúc thuê
        /// </summary>
        public DateTime? TimeEnd { get; set; }
    }

    #endregion OrderRoomDetail

    #region OrderServiceDetail
    /// <summary>
    /// order service view
    /// </summary>
    public class OrderServiceDetailViewModel : ServiceViewModel
    {
        /// <summary>
        /// thời gian giao lên phòng
        /// </summary>
        public DateTime? OrderTime { get; set; }
        /// <summary>
        /// số lượng 
        /// </summary>
        public new int? Quantity { get; set; }
        /// <summary>
        /// tổng tiền dịch vụ
        /// </summary>
        public decimal? TotalPrice { get; set; }
        /// <summary>
        /// id phòng thuê
        /// </summary>
        public Guid ServiceId { get; set; }
    }

    /// <summary>
    /// order service create/update
    /// </summary>
    public class OrderServiceCreateUpdateModel
    {
        /// <summary>
        /// thời gian order lên phòng
        /// </summary>
        public DateTime? OrderTime { get; set; }
        /// <summary>
        /// id dịch vụ
        /// </summary>
        public Guid ServiceId { get; set; }
        /// <summary>
        /// số lượng sản phẩm
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Phòng giao đồ ăn
        /// </summary>
        public Guid RoomOrDerviceId { get; set; }
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
                .ForMember(dest => dest.OrderRooms, opt => opt.MapFrom(src => src.OrderRoomDetails))
                .ForMember(dest => dest.OrderServices, opt => opt.MapFrom(src => src.OrderServiceDetails));
        }
    }
}
