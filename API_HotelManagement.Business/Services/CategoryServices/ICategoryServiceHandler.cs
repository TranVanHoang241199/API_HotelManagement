using API_HotelManagement.Business.Services.CategoryRooms;
using API_HotelManagement.common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.Business.Services.CategoryServices
{
    public interface ICategoryServiceHandler
    {
        Task<ApiResponse> GetAllCategoryServices(string search = "", int currentPage = 1, int pageSize = 1);
        Task<ApiResponse> GetCategoryServiceById(Guid id);
        Task<ApiResponse> CreateCategoryService(CategoryServiceCreateUpdateModel model);
        Task<ApiResponse> UpdateCategoryService(Guid id, CategoryServiceCreateUpdateModel model);
        Task<ApiResponse> DeleteCategoryService(Guid id);
    }
}
