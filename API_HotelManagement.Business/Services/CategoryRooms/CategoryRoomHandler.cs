using API_HotelManagement.common.Helps.Extensions;
using API_HotelManagement.common.Utils;
using API_HotelManagement.Data.Data.Entitys;
using API_HotelManagement.Data.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using API_HotelManagement.Business.Rooms.CategoryRooms;
using Microsoft.EntityFrameworkCore;

namespace API_HotelManagement.Business.Services.CategoryRooms
{
    public class CategoryRoomHandler : ICategoryRoomHandler
    {
        private readonly HtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryRoomHandler(HtDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
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
        public async Task<ApiResponse> CreateCategoryRoom(CategoryRoomCreateUpdateModel model)
        {
            try
            {
                // Kiểm tra model và thực hiện các kiểm tra khác nếu cần thiết
                if (model == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "No information is transmitted.");
                }

                var queryCategoryRoom = _context.ht_CategoryRooms.FirstOrDefault(o => o.CategoryName.Equals(model.Categoryname.Trim()) 
                && o.CreateBy.Equals(GetExtensions.GetUserId(_httpContextAccessor)));

                if (queryCategoryRoom != null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "CategoryRoom number cannot be empty.");
                }

                // Tạo một đối tượng ht_Test từ TestViewModel
                var entity = new ht_CategoryRoom
                {
                    Id = Guid.NewGuid(),
                    CategoryName = model.Categoryname,

                    //---------
                    CreateDate = DateTime.UtcNow,
                    CreateBy = GetExtensions.GetUserId(_httpContextAccessor),
                };

                // Thêm đối tượng vào DbContext
                _context.ht_CategoryRooms.Add(entity);

                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();

                var result = _mapper.Map<CategoryRoomViewModel>(entity);

                // Trả về một ApiResponseObject với dữ liệu của model sau khi tạo
                return new ApiResponseObject<CategoryRoomViewModel>(result);
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
        public async Task<ApiResponse> DeleteCategoryRoom(Guid id)
        {
            try
            {
                var recordToDelete = await _context.ht_CategoryRooms.FindAsync(id);

                if (recordToDelete != null)
                {
                    _context.ht_CategoryRooms.Remove(recordToDelete);
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
        public async Task<ApiResponse> GetAllCategoryRooms(string search = "", int currentPage = 1, int pageSize = 1)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var query = _context.ht_CategoryRooms.Where(o => o.CreateBy.Equals(currentUserId)).AsQueryable();

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

                var result = _mapper.Map<List<CategoryRoomViewModel>>(data).ToList();

                return new ApiResponsePagination<CategoryRoomViewModel>(result, totalItems, currentPage, pageSize);
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
        public async Task<ApiResponse> GetCategoryRoomById(Guid id)
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var result = await _context.ht_CategoryRooms
                    .Where(test => test.Id == id)
                    .Select(test => new CategoryRoomViewModel
                    {
                        Id = test.Id,
                        Categoryname = test.CategoryName,

                        //-------------------
                        ModifiedDate = test.ModifiedDate,
                        ModifiedBy = test.ModifiedBy,
                        CreateBy = test.CreateBy,
                        CreateDate = test.CreateDate,
                    })
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "The CategoryRoom does not exist by Id: " + id);
                }

                return new ApiResponse<CategoryRoomViewModel>(result);
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
        public async Task<ApiResponse> UpdateCategoryRoom(Guid id, CategoryRoomCreateUpdateModel model)
        {
            try
            {
                var queryCategoryRoom = _context.ht_CategoryRooms.FirstOrDefault(o => o.CategoryName.Equals(model.Categoryname.Trim()) && o.Id != id);

                if (queryCategoryRoom != null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "CategoryRoom number cannot be empty.");
                }

                var CategoryRoomToUpdate = await _context.ht_CategoryRooms.FindAsync(id);

                if (CategoryRoomToUpdate != null)
                {
                    CategoryRoomToUpdate.CategoryName = model.Categoryname;

                    //-------------
                    CategoryRoomToUpdate.ModifiedDate = DateTime.UtcNow;
                    CategoryRoomToUpdate.ModifiedBy = GetExtensions.GetUserId(_httpContextAccessor);

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
