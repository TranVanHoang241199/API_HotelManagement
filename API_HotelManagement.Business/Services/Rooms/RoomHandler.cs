using API_HotelManagement.common.Utils;
using API_HotelManagement.Data.Data.Entitys;
using API_HotelManagement.Data.Data;
using Serilog;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using API_HotelManagement.common.Helps.Extensions;
using AutoMapper;

namespace API_HotelManagement.Business.Services.Rooms
{
    public class RoomHandler : IRoomHandler
    {
        private readonly HtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoomHandler(HtDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
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
        public async Task<ApiResponse> CreateRoom(RoomCreateUpdateModel model)
        {
            try
            {
                // Kiểm tra model và thực hiện các kiểm tra khác nếu cần thiết
                if (model == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "No information is transmitted.");
                }

                var queryRoom = _context.ht_Rooms.FirstOrDefault(o => o.RoomName.Equals(model.RoomName.Trim()));

                if (queryRoom != null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "Room number cannot be empty.");
                }

                // Tạo một đối tượng ht_Test từ TestViewModel
                var entity = new ht_Room
                {
                    Id = Guid.NewGuid(),
                    RoomName = model.RoomName,
                    FloorNumber = model.FloorNumber,
                    Status = model.Status,
                    Price = model.Price,
                    CategoryRoomId = model.CategoryRoomId,

                    //---------
                    CreateDate = DateTime.UtcNow,
                    CreateBy = GetExtensions.GetUserId(_httpContextAccessor),
                };

                // Thêm đối tượng vào DbContext
                _context.ht_Rooms.Add(entity);

                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();

                var result = _mapper.Map<RoomViewModel>(entity);

                // Trả về một ApiResponseObject với dữ liệu của model sau khi tạo
                return new ApiResponseObject<RoomViewModel>(result);
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
        public async Task<ApiResponse> DeleteRoom(Guid id)
        {
            try
            {
                var recordToDelete = await _context.ht_Rooms.FindAsync(id);

                if (recordToDelete != null)
                {
                    _context.ht_Rooms.Remove(recordToDelete);
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
        public async Task<ApiResponse> GetAllRooms(string search = "", int currentPage = 1, int pageSize = 1)
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var query = _context.ht_Rooms.AsQueryable();

                // Áp dụng bộ lọc nếu có
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(o => o.RoomName.Contains(search.Trim()) || o.FloorNumber.ToString().Contains(search.Trim()));
                }

                // Lấy tổng số lượng phần tử
                var totalItems = await query.CountAsync();

                // Phân trang và lấy dữ liệu cho trang hiện tại
                var data = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(o => o.CreateDate)
                    .ToListAsync();

                var result = _mapper.Map<List<RoomViewModel>>(data).ToList();

                return new ApiResponsePagination<RoomViewModel>(result, totalItems, currentPage, pageSize);
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
        public async Task<ApiResponse> GetRoomById(Guid id)
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var result = await _context.ht_Rooms
                    .Where(test => test.Id == id)
                    .Select(test => new RoomViewModel
                    {
                        Id = test.Id,
                        Status = test.Status,
                        RoomName = test.RoomName,
                        FloorNumber = test.FloorNumber,
                        Price = test.Price,

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

                return new ApiResponse<RoomViewModel>(result);
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
        public async Task<ApiResponse> UpdateRoom(Guid id, RoomCreateUpdateModel model)
        {
            try
            {
                var queryRoom = _context.ht_Rooms.FirstOrDefault(o => o.RoomName.Equals(model.RoomName.Trim()) && o.Id != id);

                if (queryRoom != null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "Room number cannot be empty.");
                }

                var roomToUpdate = await _context.ht_Rooms.FindAsync(id);

                if (roomToUpdate != null)
                {
                    roomToUpdate.RoomName = model.RoomName;
                    roomToUpdate.FloorNumber = model.FloorNumber;
                    roomToUpdate.Status = model.Status;
                    roomToUpdate.Price = model.Price;
                    roomToUpdate.CategoryRoomId = model.CategoryRoomId;

                    //-------------
                    roomToUpdate.ModifiedDate = DateTime.UtcNow;
                    roomToUpdate.ModifiedBy = GetExtensions.GetUserId(_httpContextAccessor);

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
