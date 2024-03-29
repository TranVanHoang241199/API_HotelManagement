﻿using API_HotelManagement.common.Helps;
using API_HotelManagement.common.Helps.HelpBusiness;
using API_HotelManagement.Data;
using AutoMapper;

namespace API_HotelManagement.Business
{
    public class ServiceViewModel : BaseViewModel
    {
        public string? ServiceName { get; set; }
        public decimal PriceAmount { get; set; }
        public int Quantity { get; set; }
        public EStatusService Status { get; set; }
    }

    public class ServiceCreateUpdateModel
    {
        public string? ServiceName { get; set; }
        public decimal PriceAmount { get; set; }
        public int Quantity { get; set; }
        public EStatusService Status { get; set; }
        public Guid CategoryServiceId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ServiceAutoMapper : Profile
    {
        public ServiceAutoMapper()
        {
            CreateMap<ht_Service, ServiceViewModel>(); // Auto map ht_Service to ServiceViewModel
            //CreateMap<ServiceCreateUpdateModel, ht_User>(); // Auto map ServiceCreateUpdateModel to ht_User
        }
    }
}
