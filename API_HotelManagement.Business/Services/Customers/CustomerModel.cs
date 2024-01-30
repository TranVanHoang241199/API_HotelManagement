using API_HotelManagement.common.Helps;
using API_HotelManagement.common.Helps.HelpBusiness;
using API_HotelManagement.Data.Data.Entitys;
using AutoMapper;

namespace API_HotelManagement.Business.Services.Customers
{

    public class CustomerViewModel
    {
        /// <summary>
        /// Số điện thoại khách hàng
        /// </summary>
        public string? CustomerPhone { get; set; }
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string? CustomerName { get; set; }
    }
}
