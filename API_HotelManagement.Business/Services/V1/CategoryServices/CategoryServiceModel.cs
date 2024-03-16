using API_HotelManagement.common.Helps.HelpBusiness;
using API_HotelManagement.Data;
using AutoMapper;

namespace API_HotelManagement.Business
{
    public class CategoryServiceViewModel : BaseViewModel
    {
        public string? Categoryname { get; set; }
    }

    public class CategoryServiceCreateUpdateModel
    {
        public string? Categoryname { get; set; }
    }

    public class CategoryServiceAutoMapper : Profile
    {
        public CategoryServiceAutoMapper()
        {
            CreateMap<ht_CategoryService, CategoryServiceViewModel>(); // Auto map ht_Service to ServiceViewModel
        }
    }
}
