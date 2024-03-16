using API_HotelManagement.common.Helps.Extensions;
using API_HotelManagement.common.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using Microsoft.EntityFrameworkCore;
using API_HotelManagement.Data;

namespace API_HotelManagement.Business
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
        public async Task<ApiResponse> Create(CategoryRoomCreateUpdateModel model)
        {
            try
            {
                // Kiểm tra model và thực hiện các kiểm tra khác nếu cần thiết
                if (model == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "Không có thông tin nào được truyền đi.");
                }

                // Tạo một đối tượng ht_Test từ TestViewModel
                var entity = new ht_CategoryRoom
                {
                    Id = Guid.NewGuid(),
                    CategoryName = model.Categoryname,

                    //---------
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = GetExtensions.GetUserId(_httpContextAccessor),
                    ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    ModifiedDate = new DateTime(),
                };

                // Thêm đối tượng vào DbContext
                _context.ht_CategoryRooms.Add(entity);

                // Lưu thay đổi vào database
                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var result = _mapper.Map<CategoryRoomViewModel>(entity);

                    // Trả về một ApiResponseObject với dữ liệu của model sau khi tạo
                    return new ApiResponseObject<CategoryRoomViewModel>(result);
                }

                return new ApiResponseError(HttpStatusCode.NotFound, "Cập nhật thất bại");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse> Delete(Guid id)
        {
            try
            {
                var categoryRoomToDelete = await _context.ht_CategoryRooms.FindAsync(id);

                if (categoryRoomToDelete == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "Không tìm thấy bản ghi.");
                }

                _context.ht_CategoryRooms.Remove(categoryRoomToDelete);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    return new ApiResponse<bool>(HttpStatusCode.OK, true, "Xoá thành công.");
                }

                return new ApiResponse<bool>(HttpStatusCode.NotFound, false, "Xoá thất bại.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ApiResponse> Gets(string search = "", int currentPage = 1, int pageSize = 1)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var query = _context.ht_CategoryRooms.Where(o => o.CreatedBy.Equals(currentUserId)).AsQueryable();

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
                    .OrderBy(o => o.CreatedDate)
                    .ToListAsync();

                var result = _mapper.Map<List<CategoryRoomViewModel>>(data).ToList();

                return new ApiResponsePagination<CategoryRoomViewModel>(result, totalItems, currentPage, pageSize);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
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
                        CreatedBy = test.CreatedBy,
                        CreatedDate = test.CreatedDate,
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
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApiResponse> Update(Guid id, CategoryRoomCreateUpdateModel model)
        {
            try
            {

                var CategoryRoomToUpdate = await _context.ht_CategoryRooms.FindAsync(id);

                if (CategoryRoomToUpdate == null)
                    return new ApiResponseError(HttpStatusCode.NotFound, "Không tìm thấy bản ghi.");

                CategoryRoomToUpdate.CategoryName = model.Categoryname;

                //-------------
                CategoryRoomToUpdate.ModifiedDate = DateTime.UtcNow;
                CategoryRoomToUpdate.ModifiedBy = GetExtensions.GetUserId(_httpContextAccessor);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    return new ApiResponse(HttpStatusCode.OK, "Thêm mới thành công.");
                }

                return new ApiResponseError(HttpStatusCode.NotFound, "Thêm mới thất bại.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
