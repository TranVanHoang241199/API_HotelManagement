using API_HotelManagement.common.Helps.Extensions;
using API_HotelManagement.common.Utils;
using API_HotelManagement.Data.Data;
using API_HotelManagement.Data.Data.Entitys;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;
using System.Net;

namespace API_HotelManagement.Business.Services.Orders
{
    public class OrderHandler : IOrderHandler
    {
       
        private readonly HtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderHandler(HtDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
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
        public async Task<ApiResponse> CreateOrder(OrderCreateUpdateModel model)
        {
            try
            {
                // Kiểm tra model và thực hiện các kiểm tra khác nếu cần thiết
                if (model == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "No information is transmitted.");
                }

                // Tạo một đối tượng ht_Order từ OrderCreateUpdateModel
                var entityOrder = new ht_Order
                {
                    Id = Guid.NewGuid(),
                    CustomerPhone = model.CustomerPhone,
                    CustomerName = model.CustomerName,
                    Note = model.Note,

                    //---------
                    CreateDate = DateTime.UtcNow,
                    CreateBy = GetExtensions.GetUserId(_httpContextAccessor),
                };

                // Thêm đối tượng vào DbContext
                _context.ht_Orders.Add(entityOrder);

                // Thêm đối tượng OrderRoomDetail vào DbContext
                if (model.OrderServiceDetails.Any())
                {
                    var orderRoomDetails = CreateOrderRoomDetails(entityOrder.Id, model.OrderRoomDetails);
                    _context.ht_OrderRoomDetails.AddRange(orderRoomDetails);
                }

                // Thêm đối tượng OrderServiceDetail vào DbContext
                if (model.OrderServiceDetails.Any())
                {
                    var orderServiceDetails = CreateOrderServiceDetails(entityOrder.Id, model.OrderServiceDetails);
                    _context.ht_OrderServiceDetails.AddRange(orderServiceDetails);
                }

                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();

                var result = _mapper.Map<OrderViewModel>(entityOrder);

                // Trả về một ApiResponseObject với dữ liệu của model sau khi tạo
                return new ApiResponseObject<OrderViewModel>(result);
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
        public async Task<ApiResponse> DeleteOrder(Guid id)
        {
            try
            {
                var orderToDelete = await _context.ht_Orders.FindAsync(id);

                if (orderToDelete != null)
                {
                    // Xóa các chi tiết đặt phòng
                    var orderRoomDetails = await _context.ht_OrderRoomDetails
                        .Where(detail => detail.OrderId == id)
                        .ToListAsync();

                    _context.ht_OrderRoomDetails.RemoveRange(orderRoomDetails);

                    // Xóa các chi tiết dịch vụ
                    var orderServiceDetails = await _context.ht_OrderServiceDetails
                        .Where(detail => detail.OrderId == id)
                        .ToListAsync();

                    _context.ht_OrderServiceDetails.RemoveRange(orderServiceDetails);

                    // Xóa đơn đặt
                    _context.ht_Orders.Remove(orderToDelete);

                    await _context.SaveChangesAsync();

                    return new ApiResponse<bool>(true);
                }

                return new ApiResponse<bool>(HttpStatusCode.BadRequest, false, "Delete failed: Order not found");
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
        public async Task<ApiResponse> GetAllOrders(string search = "", int currentPage = 1, int pageSize = 1)
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var query = _context.ht_Orders.AsQueryable();

                // Áp dụng bộ lọc nếu có
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(o => o.CustomerName.Contains(search.Trim())
                        || o.CustomerPhone.ToString().Contains(search.Trim()));
                }

                // Lấy tổng số lượng phần tử
                var totalItems = await query.CountAsync();

                // Phân trang và lấy dữ liệu cho trang hiện tại
                var data = await query
                    .Include(order => order.OrderRoomDetails)
                        .ThenInclude(detail => detail.Room) // Chi tiết đặt phòng và phòng tương ứng
                    .Include(order => order.OrderServiceDetails)
                        .ThenInclude(detail => detail.Service) // Chi tiết dịch vụ và dịch vụ tương ứng
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(o => o.CreateDate)
                    .ToListAsync();

                var result = _mapper.Map<List<OrderViewModel>>(data).ToList();

                return new ApiResponsePagination<OrderViewModel>(result, totalItems, currentPage, pageSize);
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
        public async Task<ApiResponse> GetOrderById(Guid id)
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var order = await _context.ht_Orders
                    .Where(o => o.Id == id)
                    .Include(o => o.OrderRoomDetails)
                        .ThenInclude(detail => detail.Room)
                    .Include(o => o.OrderServiceDetails)
                        .ThenInclude(detail => detail.Service)
                    .FirstOrDefaultAsync();

                if (order == null)
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, $"The Order does not exist by Id: {id}");
                }

                var result = _mapper.Map<OrderViewModel>(order);

                return new ApiResponse<OrderViewModel>(result);
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
        public async Task<ApiResponse> UpdateOrder(Guid id, OrderCreateUpdateModel model)
        {
            try
            {
                // Check if the order exists
                var existingOrder = await _context.ht_Orders.FindAsync(id);

                if (existingOrder == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, $"Order with Id {id} not found.");
                }

                // Update order properties
                existingOrder.Identifier = model.Identifier;
                existingOrder.CustomerName = model.CustomerName;
                existingOrder.CustomerPhone = model.CustomerPhone;
                existingOrder.Note = model.Note;

                // Thêm đối tượng OrderRoomDetail vào DbContext
                if (model.OrderServiceDetails.Any())
                {
                    var orderRoomDetails = CreateOrderRoomDetails(id, model.OrderRoomDetails);
                    _context.ht_OrderRoomDetails.AddRange(orderRoomDetails);
                }

                // Thêm đối tượng OrderServiceDetail vào DbContext
                if (model.OrderServiceDetails.Any())
                {
                    var orderServiceDetails = CreateOrderServiceDetails(id, model.OrderServiceDetails);
                    _context.ht_OrderServiceDetails.AddRange(orderServiceDetails);
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                return new ApiResponse<bool>(true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Something went wrong: " + ex.Message);
            }
        }

        private IEnumerable<ht_OrderRoomDetail> CreateOrderRoomDetails(Guid OrderId, List<OrderRoomDetailCreateUpdateModel> model)
        {
            var orderRoomDetails = model.Select(item => new ht_OrderRoomDetail
            {
                Id = Guid.NewGuid(),
                TimeStart = item.TimeStart,
                TimeEnd = item.TimeEnd,

                RoomId = item.RoomId,
                OrderId = OrderId,
                //-----------
                CreateDate = DateTime.UtcNow,
                CreateBy = GetExtensions.GetUserId(_httpContextAccessor),
            });

            return orderRoomDetails;
        }

        private IEnumerable<ht_OrderServiceDetail> CreateOrderServiceDetails(Guid OrderId, List<OrderServiceDetailCreateUpdateModel> model)
        {
            var orderServiceDetails = model.Select(item => new ht_OrderServiceDetail
            {
                Id = Guid.NewGuid(),
                OrderTime = DateTime.UtcNow,
                Quantity = item.Quantity,
                TotalPrice = _context.ht_Services.FirstOrDefault(service => service.Id == item.ServiceId)?.Price * item.Quantity,

                ServiceId = item.ServiceId,
                OrderId = OrderId,
                //-----------
                CreateDate = DateTime.UtcNow,
                CreateBy = GetExtensions.GetUserId(_httpContextAccessor),
            });

            return orderServiceDetails;
        }

    }
}
