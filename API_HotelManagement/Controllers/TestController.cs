using API_HotelManagement.Business.Services.Test;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace API_HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestHandler _testService;

        public TestController(ITestHandler testService)
        {
            _testService = testService;
        }

        /// <summary>
        /// Get ALL test
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage">Page hiển thị hiện tại </param>
        /// <param name="pageSize">Kích thước lỗi trang</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponsePagination<TestViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTests(string search = "", int currentPage = 1, int pageSize = 10)
        {

            var result = await _testService.GetAllTests(search, currentPage, pageSize);

            return ApiHelper.TransformData(result); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TestViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTest(Guid id)
        {

            var result = await _testService.GetTest(id);

            return ApiHelper.TransformData(result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseObject<TestViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTest([FromBody] TestCreateUpdateModel model)
        {

            var result = await _testService.CreateTest(model);

            return ApiHelper.TransformData(result);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTest(Guid id, [FromBody] TestCreateUpdateModel model)
        {

            var result = await _testService.UpdateTest(id, model);

            return ApiHelper.TransformData(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTest(Guid id)
        {

            var result = await _testService.DeleteTest(id);

            return ApiHelper.TransformData(result);
        }
    }
}