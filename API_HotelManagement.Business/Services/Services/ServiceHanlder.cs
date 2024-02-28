using API_HotelManagement.common.Utils;
using API_HotelManagement.Data.Data.Entitys;
using API_HotelManagement.Data.Data;
using Serilog;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using API_HotelManagement.common.Helps.Extensions;
using AutoMapper;

namespace API_HotelManagement.Business.Services.Services
{
    public class ServiceHanlder : IServiceHanlder
    {
        private readonly HtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceHanlder(HtDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
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
        public async Task<ApiResponse> CreateService(ServiceCreateUpdateModel model)
        {
            try
            {
                // Kiểm tra model và thực hiện các kiểm tra khác nếu cần thiết
                if (model == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "No information is transmitted.");
                }

                var queryService = _context.ht_Services.FirstOrDefault(o => o.ServiceName.Equals(model.ServiceName.Trim()) 
                && o.CreateBy.Equals(GetExtensions.GetUserId(_httpContextAccessor)));

                if (queryService != null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "Service cannot be empty.");
                }

                // Tạo một đối tượng ht_Test từ TestViewModel
                var entity = new ht_Service
                {
                    Id = Guid.NewGuid(),
                    ServiceName = model.ServiceName,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    Status = model.Status,
                    CategoryServiceId = model.CategoryServiceId,

                    //---------
                    CreateDate = DateTime.UtcNow,
                    CreateBy = GetExtensions.GetUserId(_httpContextAccessor),
                };

                // Thêm đối tượng vào DbContext
                _context.ht_Services.Add(entity);

                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();

                var result = _mapper.Map<ServiceViewModel>(entity);

                // Trả về một ApiResponseObject với dữ liệu của model sau khi tạo
                return new ApiResponseObject<ServiceViewModel>(result);
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
        public async Task<ApiResponse> DeleteService(Guid id)
        {
            try
            {
                var recordToDelete = await _context.ht_Services.FindAsync(id);

                if (recordToDelete != null)
                {
                    _context.ht_Services.Remove(recordToDelete);
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
        public async Task<ApiResponse> GetAllServices(string search = "", int currentPage = 1, int pageSize = 1)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var query = _context.ht_Services.Where(o => o.CreateBy.Equals(currentUserId)).AsQueryable();

                // Áp dụng bộ lọc nếu có
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(o => o.ServiceName.Contains(search.Trim()));
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

                var result = _mapper.Map<List<ServiceViewModel>>(data).ToList();

                return new ApiResponsePagination<ServiceViewModel>(result, totalItems, currentPage, pageSize);
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
        public async Task<ApiResponse> GetServiceById(Guid id)
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var result = await _context.ht_Services
                    .Where(test => test.Id == id)
                    .Select(test => new ServiceViewModel
                    {
                        Id = test.Id,
                        Price = test.Price,
                        ServiceName = test.ServiceName,
                        Status = test.Status,
                        Quantity = test.Quantity,

                        ModifiedDate = test.ModifiedDate,
                        ModifiedBy = test.ModifiedBy,
                        CreateBy = test.CreateBy,
                        CreateDate = test.CreateDate,
                    })
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "The Service does not exist by Id: " + id);
                }

                return new ApiResponse<ServiceViewModel>(result);
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
        public async Task<ApiResponse> UpdateService(Guid id, ServiceCreateUpdateModel model)
        {
            try
            {
                var queryService = _context.ht_Services.FirstOrDefault(o => o.ServiceName.Equals(model.ServiceName.Trim()) && o.Id != id);

                if (queryService != null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "Phone number cannot be empty.");
                }

                var serviceToUpdate = await _context.ht_Services.FindAsync(id);

                if (serviceToUpdate != null)
                {
                    serviceToUpdate.ServiceName = model.ServiceName;
                    serviceToUpdate.Price = model.Price;
                    serviceToUpdate.Status = model.Status;
                    serviceToUpdate.Quantity = model.Quantity;
                    serviceToUpdate.CategoryServiceId = model.CategoryServiceId;

                    //-------------
                    serviceToUpdate.ModifiedDate = DateTime.UtcNow;
                    serviceToUpdate.ModifiedBy = GetExtensions.GetUserId(_httpContextAccessor);

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

