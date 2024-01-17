using API_HotelManagement.common.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.Business.Services.Test
{
    public interface ITestHandler
    {
        Task<ApiResponse> GetAllTests(string search = "", int currentPage = 1, int pageSize = 10);
        Task<ApiResponse> GetTest(Guid id);
        Task<ApiResponse> CreateTest(TestCreateUpdateModel model);
        Task<ApiResponse> UpdateTest(Guid id, TestCreateUpdateModel model);
        Task<ApiResponse> DeleteTest(Guid id);

    }
}
