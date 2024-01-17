using API_HotelManagement.common.Utils;
using API_HotelManagement.Data.Data;
using API_HotelManagement.Data.Data.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_HotelManagement.Business.Services.Test
{
    public class TestHandler : ITestHandler
    {

        private readonly HtDbContext _context;
        //private readonly IMapper _mapper;

        public TestHandler(HtDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse> CreateTest(TestCreateUpdateModel model)
        {
            try
            {
                // Kiểm tra model và thực hiện các kiểm tra khác nếu cần thiết

                // Tạo một đối tượng ht_Test từ TestViewModel
                var entity = new ht_Test
                {
                    Id = Guid.NewGuid(), // Hoặc sử dụng Guid.NewGuid() để tạo ID mới
                    Name = model.Name,
                    // Các thuộc tính khác tương ứng
                    CreateBy = Guid.Parse("223f3220-2549-4da7-a691-6a954529c21e"),
                    CreateDate = DateTime.Now,
                    ModifiedBy = Guid.Empty,
                    ModifiedDate = DateTime.MinValue
                };

                // Thêm đối tượng vào DbContext
                _context.ht_Test.Add(entity);

                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();

                var result = new TestViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                };

                // Trả về một ApiResponseObject với dữ liệu của model sau khi tạo
                return new ApiResponseObject<TestViewModel>(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Something went wrong: " + ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteTest(Guid id)
        {
            try
            {
                var recordToDelete = await _context.ht_Test.FindAsync(id);

                if (recordToDelete != null)
                {
                    _context.ht_Test.Remove(recordToDelete);
                    await _context.SaveChangesAsync();

                    return new ApiResponse<bool>(true);
                }

                return new ApiResponse<bool>(HttpStatusCode.BadRequest, false, "xoá thất bại");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Something went wrong: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetAllTests(string search = "", int currentPage = 1, int pageSize = 1)
        {
            try
            {

                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var query = _context.ht_Test.AsQueryable();

                // Áp dụng bộ lọc nếu có
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(test => test.Name.Contains(search));
                }

                // Lấy tổng số lượng phần tử
                var totalItems = await query.CountAsync();

                // Phân trang và lấy dữ liệu cho trang hiện tại
                var data = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var result = data.Select(o => new TestViewModel { Id = o.Id, Name = o.Name }).ToList();

                return new ApiResponsePagination<TestViewModel>(result, totalItems, currentPage, pageSize);

            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Something went wrong: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetTest(Guid id)
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var result = await _context.ht_Test
                    .Where(test => test.Id == id)
                    .Select(test => new TestViewModel
                    {
                        Id = test.Id,
                        Name = test.Name,
                        // Map các trường khác của TestViewModel từ test entity
                        // ...
                    })
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "không tồn tại test có id này: " + id);
                }

                return new ApiResponse<TestViewModel>(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Something went wrong: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse> UpdateTest(Guid id, TestCreateUpdateModel model)
        {
            try
            {
                var testToUpdate = await _context.ht_Test.FindAsync(id);

                if (testToUpdate != null)
                {
                    // Cập nhật các thuộc tính của đối tượng với giá trị mới từ model
                    testToUpdate.Name = model.Name; // Thay đổi theo thuộc tính cần cập nhật

                    // Cập nhật thời gian sửa đổi lần cuối (nếu có)
                    testToUpdate.ModifiedBy = Guid.NewGuid(); // Thay đổi theo quy tắc của bạn
                    testToUpdate.ModifiedDate = DateTime.Now;

                    await _context.SaveChangesAsync();
                    return new ApiResponse<bool>(true);
                }

                return new ApiResponse<bool>(HttpStatusCode.BadRequest, false, "update thất bại");

            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Something went wrong: " + ex.Message);
            }
        }
    }
}
