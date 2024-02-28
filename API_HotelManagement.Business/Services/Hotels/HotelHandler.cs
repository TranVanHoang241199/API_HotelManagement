using API_HotelManagement.common.Helps.Extensions;
using API_HotelManagement.common.Utils;
using API_HotelManagement.Data.Data;
using API_HotelManagement.Data.Data.Entitys;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;

namespace API_HotelManagement.Business.Services.Hotels
{
    /// <summary>
    /// 
    /// </summary>
    public class HotelHandler : IHotelHandler
    {
        private readonly HtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContextAccessor"></param>
        public HotelHandler(HtDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
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
        public async Task<ApiResponse> CreateHotel(HotelCreateUpdateModel model)
        {
            try
            {
                // Kiểm tra model và thực hiện các kiểm tra khác nếu cần thiết
                if (model == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "No information is transmitted.");
                }

                var queryCategoryHotel = _context.ht_Hotels.FirstOrDefault(o => o.HotelName.Equals(model.HotelName.Trim()) && o.CreateBy.Equals(GetExtensions.GetUserId(_httpContextAccessor)));

                if (queryCategoryHotel != null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "CategoryHotel number cannot be empty.");
                }

                // Tạo một đối tượng ht_Test từ TestViewModel
                var entity = new ht_Hotel
                {
                    Id = Guid.NewGuid(),
                    HotelName = model.HotelName,
                    Description = model.Description,

                    //---------
                    CreateDate = DateTime.UtcNow,
                    CreateBy = GetExtensions.GetUserId(_httpContextAccessor),
                };

                // Thêm đối tượng vào DbContext
                _context.ht_Hotels.Add(entity);

                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();

                var result = _mapper.Map<HotelViewModel>(entity);

                // Trả về một ApiResponseObject với dữ liệu của model sau khi tạo
                return new ApiResponseObject<HotelViewModel>(result);
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
        public async Task<ApiResponse> DeleteHotel(Guid id)
        {
            try
            {
                var recordToDelete = await _context.ht_Hotels.FindAsync(id);

                if (recordToDelete != null)
                {
                    _context.ht_Hotels.Remove(recordToDelete);
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
        public async Task<ApiResponse> GetAllHotels(string search = "", int currentPage = 1, int pageSize = 1)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var query = _context.ht_Hotels.Where(o => o.CreateBy.Equals(currentUserId))
                    .AsQueryable();

                foreach (var item in query)
                {
                    Console.WriteLine(item.CreateBy + " == " + currentUserId);
                }

                // Áp dụng bộ lọc nếu có
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(o => o.HotelName.Contains(search.Trim()));
                }

                // Lấy tổng số lượng phần tửư
                var totalItems = await query.CountAsync();

                // Phân trang và lấy dữ liệu cho trang hiện tại
                var data = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(o => o.CreateDate)
                    .ToListAsync();

                var result = _mapper.Map<List<HotelViewModel>>(data).ToList();

                return new ApiResponsePagination<HotelViewModel>(result, totalItems, currentPage, pageSize);
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
        public async Task<ApiResponse> GetHotelById(Guid id)
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var result = await _context.ht_Hotels
                    .Where(test => test.Id == id)
                    .Select(test => new HotelViewModel
                    {
                        Id = test.Id,
                        HotelName = test.HotelName,
                        Description = test.Description,

                        //---------------------------
                        ModifiedDate = test.ModifiedDate,
                        ModifiedBy = test.ModifiedBy,
                        CreateBy = test.CreateBy,
                        CreateDate = test.CreateDate,
                    })
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "The Room does not exist by Id: " + id);
                }

                return new ApiResponse<HotelViewModel>(result);
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
        public async Task<ApiResponse> UpdateHotel(Guid id, HotelCreateUpdateModel model)
        {
            try
            {
                var queryHotel = _context.ht_Hotels.FirstOrDefault(o => o.HotelName.Equals(model.HotelName.Trim()) && o.Id != id);

                if (queryHotel != null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "Room number cannot be empty.");
                }

                var hotelToUpdate = await _context.ht_Hotels.FindAsync(id);

                if (hotelToUpdate != null)
                {
                    hotelToUpdate.HotelName = queryHotel.HotelName;
                    hotelToUpdate.Description = queryHotel.Description;

                    //-------------
                    hotelToUpdate.ModifiedDate = DateTime.UtcNow;
                    hotelToUpdate.ModifiedBy = GetExtensions.GetUserId(_httpContextAccessor);

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
