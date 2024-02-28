using API_HotelManagement.common.Helps.Extensions;
using API_HotelManagement.common.Utils;
using API_HotelManagement.Data.Data.Entitys;
using API_HotelManagement.Data.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace API_HotelManagement.Business.Services.CategoryServices
{
    public class CategoryServiceHandler : ICategoryServiceHandler
    {
        private readonly HtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryServiceHandler(HtDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse> CreateCategoryService(CategoryServiceCreateUpdateModel model)
        {
            try
            {
                // Kiểm tra model và thực hiện các kiểm tra khác nếu cần thiết
                if (model == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "No information is transmitted.");
                }

                var queryCategoryService = _context.ht_CategoryServices.FirstOrDefault(o => o.CategoryName.Equals(model.Categoryname.Trim())
                && o.CreateBy.Equals(GetExtensions.GetUserId(_httpContextAccessor)));

                if (queryCategoryService != null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "CategoryService number cannot be empty.");
                }

                // Tạo một đối tượng ht_Test từ TestViewModel
                var entity = new ht_CategoryService
                {
                    Id = Guid.NewGuid(),
                    CategoryName = model.Categoryname,

                    //---------
                    CreateDate = DateTime.UtcNow,
                    CreateBy = GetExtensions.GetUserId(_httpContextAccessor),
                };

                // Thêm đối tượng vào DbContext
                _context.ht_CategoryServices.Add(entity);

                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();

                var result = _mapper.Map<CategoryServiceViewModel>(entity);

                // Trả về một ApiResponseObject với dữ liệu của model sau khi tạo
                return new ApiResponseObject<CategoryServiceViewModel>(result);
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
        public async Task<ApiResponse> DeleteCategoryService(Guid id)
        {
            try
            {
                var recordToDelete = await _context.ht_CategoryServices.FindAsync(id);

                if (recordToDelete != null)
                {
                    _context.ht_CategoryServices.Remove(recordToDelete);
                    await _context.SaveChangesAsync();

                    return new ApiResponse<bool>(true);
                }

                return new ApiResponse<bool>(HttpStatusCode.BadRequest, false, "delete failed");
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
        public async Task<ApiResponse> GetAllCategoryServices(string search = "", int currentPage = 1, int pageSize = 1)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var query = _context.ht_CategoryServices.Where(o => o.CreateBy.Equals(currentUserId)).AsQueryable();

                // Áp dụng bộ lọc nếu có
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(o => o.CategoryName.Contains(search.Trim()));
                }

                // Lấy tổng số lượng phần tử
                var totalItems = await query.CountAsync();

                // Phân trang và lấy dữ liệu cho trang hiện tại
                var data = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(o => o.ModifiedDate)
                    .OrderBy(o => o.CreateDate)
                    .ToListAsync();

                var result = _mapper.Map<List<CategoryServiceViewModel>>(data).ToList();

                return new ApiResponsePagination<CategoryServiceViewModel>(result, totalItems, currentPage, pageSize);
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
        public async Task<ApiResponse> GetCategoryServiceById(Guid id)
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var result = await _context.ht_CategoryServices
                    .Where(o => o.Id == id)
                    .Select(o => new CategoryServiceViewModel
                    {
                        Id = o.Id,
                        Categoryname = o.CategoryName,

                        ModifiedDate = o.ModifiedDate,
                        ModifiedBy = o.ModifiedBy,
                        CreateBy = o.CreateBy,
                        CreateDate = o.CreateDate,
                    })
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "The CategoryService does not exist by Id: " + id);
                }

                return new ApiResponse<CategoryServiceViewModel>(result);
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
        public async Task<ApiResponse> UpdateCategoryService(Guid id, CategoryServiceCreateUpdateModel model)
        {
            try
            {
                var queryCategoryService = _context.ht_CategoryServices.FirstOrDefault(o => o.CategoryName.Equals(model.Categoryname.Trim()) && o.Id != id);

                if (queryCategoryService != null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "CategoryService number cannot be empty.");
                }

                var CategoryServiceToUpdate = await _context.ht_CategoryServices.FindAsync(id);

                if (CategoryServiceToUpdate != null)
                {
                    CategoryServiceToUpdate.CategoryName = model.Categoryname;


                    //-------------
                    CategoryServiceToUpdate.ModifiedDate = DateTime.UtcNow;
                    CategoryServiceToUpdate.ModifiedBy = GetExtensions.GetUserId(_httpContextAccessor);

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
