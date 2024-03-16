using API_HotelManagement.common.Helps.Extensions;
using API_HotelManagement.common.Utils;
using API_HotelManagement.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;

namespace API_HotelManagement.Business
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

        public async Task<ApiResponse> Create(OrderCreateUpdateModel model)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                // Kiểm tra model và thực hiện các kiểm tra khác nếu cần thiết
                if (model == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "Không có thông tin nào được truyền đi.");
                }
                #region add table order
                // Tạo một đối tượng ht_Test từ TestViewModel
                var entity = new ht_Order
                {
                    Id = Guid.NewGuid(),
                    CustomerName = model.CustomerName,
                    CustomerPhone = model.CustomerPhone,
                    Identifier = model.Identifier,
                    Note = model.Note,

                    //---------
                    CreatedBy = currentUserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    ModifiedDate = new DateTime(),
                };

                // Thêm đối tượng vào DbContext
                _context.ht_Orders.Add(entity);

                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();

                // add room detail
                foreach (var room in model.OrderRooms)
                {
                    _context.ht_OrderRoomDetails.Add(new ht_OrderRoomDetail
                    {
                        Id = Guid.NewGuid(),
                        StartDate = room.StartDate,
                        EndDate = room.StartDate,

                        //--
                        RoomId = room.RoomId,
                        OrderId = entity.Id,

                        //---------
                        CreatedBy = currentUserId,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                        ModifiedDate = new DateTime(),
                    });
                }

                // add service detail
                foreach (var service in model.OrderServices)
                {
                    _context.ht_OrderServiceDetails.Add(new ht_OrderServiceDetail
                    {
                        Id = Guid.NewGuid(),
                        OrderDate = service.OrderDate,
                        Quantity = service.Quantity,
                        TotalPriceAmount = _context.ht_Services.Find(service.ServiceId).PriceAmount * service.Quantity,

                        //--
                        ServiceId = service.ServiceId,
                        OrderId = entity.Id,

                        //---------
                        CreatedBy = currentUserId,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                        ModifiedDate = new DateTime(),
                    });
                }

                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();
                #endregion add table order

                var result = new OrderViewModel
                {
                    Id = entity.Id,
                    Identifier = entity.Identifier,

                    CustomerName = entity.CustomerName,
                    CustomerPhone = entity.CustomerPhone,
                    Note = entity.Note,

                    // Order room
                    OrderRooms = _context.ht_OrderRoomDetails
                    .Where(orderRoom => orderRoom.OrderId == entity.Id) // Loại bỏ các bản ghi có RoomId là null
                    .Join(
                        _context.ht_Rooms.Where(room => room != null), // Loại bỏ các bản ghi null trong ht_Rooms
                        orderRoom => orderRoom.RoomId,
                        room => room.Id,
                        (orderRoom, room) => new OrderRoomDetailViewModel
                        {
                            Id = orderRoom.Id,
                            StartDate = orderRoom.StartDate,
                            EndDate = orderRoom.EndDate,

                            //-------------
                            RoomId = orderRoom.RoomId,
                            RoomName = room.RoomName,
                            FloorNumber = room.FloorNumber,
                            PriceAmount = room.PriceAmount,
                            Status = room.Status,

                            //-------------
                            CreatedBy = orderRoom.CreatedBy,
                            CreatedDate = orderRoom.CreatedDate,
                            ModifiedBy = orderRoom.ModifiedBy,
                            ModifiedDate = orderRoom.ModifiedDate,
                        }
                    ).ToList(),

                    // order service
                    OrderServices = _context.ht_OrderServiceDetails
                    .Where(orderService => orderService.OrderId == entity.Id)
                    .Join(
                        _context.ht_Services.Where(service => service != null), // Loại bỏ các bản ghi null trong ht_Services
                        orderService => orderService.ServiceId,
                        service => service.Id,
                        (orderService, service) => new OrderServiceDetailViewModel
                        {
                            Id = orderService.Id,
                            OrderDate = orderService.OrderDate,
                            TotalPriceAmount = orderService.TotalPriceAmount,
                            Quantity = orderService.Quantity,

                            //-------------
                            ServiceId = orderService.ServiceId,
                            PriceAmount = service.PriceAmount,
                            Status = service.Status,
                            ServiceName = service.ServiceName,

                            //-------------
                            CreatedBy = orderService.CreatedBy,
                            CreatedDate = orderService.CreatedDate,
                            ModifiedBy = orderService.ModifiedBy,
                            ModifiedDate = orderService.ModifiedDate,

                        }
                    ).ToList(),

                    //-------------
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    ModifiedBy = entity.ModifiedBy,
                    ModifiedDate = entity.ModifiedDate,
                };

                return new ApiResponseObject<OrderViewModel>(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse> CreateRoomDetail(Guid orderId, OrderRoomCreateUpdateModel model)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                // Kiểm tra model và thực hiện các kiểm tra khác nếu cần thiết
                if (model == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "Không có thông tin nào được truyền đi.");
                }

                // Tạo một đối tượng ht_Test từ TestViewModel
                var entity = new ht_OrderRoomDetail
                {
                    Id = Guid.NewGuid(),
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,

                    RoomId = model.RoomId,
                    OrderId = orderId,

                    //---------
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = currentUserId,
                    ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    ModifiedDate = new DateTime(),
                };

                // Thêm đối tượng vào DbContext
                _context.ht_OrderRoomDetails.Add(entity);

                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();

                var entityRoom = _context.ht_Rooms.FirstOrDefault(room => room.Id == entity.RoomId);

                var result = new OrderRoomDetailViewModel
                {
                    Id = entity.Id,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,

                    FloorNumber = entityRoom.FloorNumber,
                    PriceAmount = entityRoom.PriceAmount,
                    RoomName = entityRoom.RoomName,
                    Status = entityRoom.Status,

                    RoomId = model.RoomId,
                    OrderId = orderId,

                    //-------------
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    ModifiedBy = entity.ModifiedBy,
                    ModifiedDate = entity.ModifiedDate,
                };

                // Trả về một ApiResponseObject với dữ liệu của model sau khi tạo
                return new ApiResponseObject<OrderRoomDetailViewModel>(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse> CreateServiceDetail(Guid orderId, OrderServiceCreateUpdateModel model)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                // Kiểm tra model và thực hiện các kiểm tra khác nếu cần thiết
                if (model == null)
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "Không có thông tin nào được truyền đi.");
                }

                // Tạo một đối tượng ht_Test từ TestViewModel
                var entity = new ht_OrderServiceDetail
                {
                    Id = Guid.NewGuid(),
                    Quantity = model.Quantity,
                    OrderDate = model.OrderDate,
                    TotalPriceAmount = _context.ht_Services.Find(model.ServiceId).PriceAmount * model.Quantity,
                    OrderId = orderId,
                    ServiceId = Guid.NewGuid(),

                    //---------
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = currentUserId,
                    ModifiedBy = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    ModifiedDate = new DateTime(),
                };

                // Thêm đối tượng vào DbContext
                _context.ht_OrderServiceDetails.Add(entity);

                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();

                var entityRoom = _context.ht_Services.FirstOrDefault(service => service.Id == entity.ServiceId);

                var result = new OrderServiceDetailViewModel
                {
                    Id = entity.Id,
                    TotalPriceAmount = entity.TotalPriceAmount,
                    OrderDate = entity.OrderDate,
                    Quantity = entity.Quantity,
                    ServiceId = entity.ServiceId,

                    //--
                    PriceAmount = entityRoom.PriceAmount,
                    ServiceName = entityRoom.ServiceName,
                    Status = entityRoom.Status,

                    //-------------
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    ModifiedBy = entity.ModifiedBy,
                    ModifiedDate = entity.ModifiedDate,
                };

                // Trả về một ApiResponseObject với dữ liệu của model sau khi tạo
                return new ApiResponseObject<OrderServiceDetailViewModel>(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse> Delete(Guid id)
        {
            try
            {
                var recordToDelete = await _context.ht_Orders.FindAsync(id);

                if (recordToDelete != null)
                {
                    _context.ht_Orders.Remove(recordToDelete);
                    await _context.SaveChangesAsync();

                    return new ApiResponse<bool>(true);
                }

                return new ApiResponse<bool>(HttpStatusCode.NotFound, false, "Xoá thất bại.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteRoomDetail(Guid id)
        {
            try
            {
                var recordToDelete = await _context.ht_OrderRoomDetails.FindAsync(id);

                if (recordToDelete != null)
                {
                    _context.ht_OrderRoomDetails.Remove(recordToDelete);
                    await _context.SaveChangesAsync();

                    return new ApiResponse<bool>(true);
                }

                return new ApiResponse<bool>(HttpStatusCode.NotFound, false, "Xoá thất bại.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteServiceDetail(Guid id)
        {
            try
            {
                var recordToDelete = await _context.ht_OrderServiceDetails.FindAsync(id);

                if (recordToDelete != null)
                {
                    _context.ht_OrderServiceDetails.Remove(recordToDelete);
                    await _context.SaveChangesAsync();

                    return new ApiResponse<bool>(true);
                }

                return new ApiResponse<bool>(HttpStatusCode.NotFound, false, "Xoá thất bại.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllRoomDetails(Guid orderId, string search = "", int currentPage = 1, int pageSize = 1)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ và thực hiện kết nối với bảng ht_Room
                var query = _context.ht_OrderRoomDetails
                    .Where(o => o.CreatedBy.Equals(currentUserId) && o.OrderId.Equals(orderId))
                    .Join(_context.ht_Rooms,
                        roomDetail => roomDetail.RoomId,
                        room => room.Id,
                        (roomDetail, room) => new { roomDetail, room })
                    .AsQueryable();

                // Áp dụng bộ lọc nếu có
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(o => o.room.RoomName.Contains(search.Trim()) || o.room.FloorNumber.ToString().Contains(search.Trim()));
                }

                // Lấy tổng số lượng phần tử
                var totalItems = await query.CountAsync();

                // Phân trang và lấy dữ liệu cho trang hiện tại
                var data = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(roomDetail => roomDetail.roomDetail.CreatedDate)
                    .ToListAsync();

                var result = data.Select(orderRoom => new OrderRoomDetailViewModel
                {
                    Id = orderRoom.roomDetail.Id,
                    EndDate = orderRoom.roomDetail.EndDate,
                    StartDate = orderRoom.roomDetail.StartDate,

                    FloorNumber = orderRoom.room.FloorNumber,
                    RoomName = orderRoom.room.RoomName,
                    PriceAmount = orderRoom.room.PriceAmount,
                    Status = orderRoom.room.Status,

                    //--
                    RoomId = orderRoom.roomDetail.RoomId,
                    OrderId = orderRoom.roomDetail.Id,


                    //-------------
                    CreatedBy = orderRoom.roomDetail.CreatedBy,
                    CreatedDate = orderRoom.roomDetail.CreatedDate,
                    ModifiedBy = orderRoom.roomDetail.ModifiedBy,
                    ModifiedDate = orderRoom.roomDetail.ModifiedDate,
                }).ToList();

                return new ApiResponsePagination<OrderRoomDetailViewModel>(result, totalItems, currentPage, pageSize);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse> Gets(string search = "", int currentPage = 1, int pageSize = 1)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var query = _context.ht_Orders.Where(o => o.CreatedBy.Equals(currentUserId)).AsQueryable();

                // Áp dụng bộ lọc nếu có
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(o => o.CustomerName.Contains(search.Trim()));
                }

                // Lấy tổng số lượng phần tử
                var totalItems = await query.CountAsync();


                // Phân trang và lấy dữ liệu cho trang hiện tại
                var data = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(order => order.ModifiedDate)
                    .OrderBy(order => order.CreatedDate)
                    .ToListAsync();

                if (data == null || !data.Any()) // Kiểm tra nếu dữ liệu trả về là null hoặc không có phần tử
                {
                    return new ApiResponseError(HttpStatusCode.NotFound, "No data found.");
                }

                var result = data.Select(order => new OrderViewModel
                {
                    Id = order.Id,
                    Identifier = order.Identifier,

                    CustomerName = order.CustomerName,
                    CustomerPhone = order.CustomerPhone,
                    Note = order.Note,

                    // Order room
                    OrderRooms = _context.ht_OrderRoomDetails
                    .Where(orderRoom => orderRoom.OrderId == order.Id) // Loại bỏ các bản ghi có RoomId là null
                    .Join(
                        _context.ht_Rooms.Where(room => room != null), // Loại bỏ các bản ghi null trong ht_Rooms
                        orderRoom => orderRoom.RoomId,
                        room => room.Id,
                        (orderRoom, room) => new OrderRoomDetailViewModel
                        {
                            Id = orderRoom.Id,
                            StartDate = orderRoom.StartDate,
                            EndDate = orderRoom.EndDate,

                            //-------------
                            RoomId = orderRoom.RoomId,
                            RoomName = room.RoomName,
                            FloorNumber = room.FloorNumber,
                            PriceAmount = room.PriceAmount,
                            Status = room.Status,

                            //-------------
                            CreatedBy = orderRoom.CreatedBy,
                            CreatedDate = orderRoom.CreatedDate,
                            ModifiedBy = orderRoom.ModifiedBy,
                            ModifiedDate = orderRoom.ModifiedDate,
                        }
                    ).ToList(),

                    // order service
                    OrderServices = _context.ht_OrderServiceDetails
                    .Where(orderService => orderService.OrderId == order.Id)
                    .Join(
                        _context.ht_Services.Where(service => service != null), // Loại bỏ các bản ghi null trong ht_Services
                        orderService => orderService.ServiceId,
                        service => service.Id,
                        (orderService, service) => new OrderServiceDetailViewModel
                        {
                            Id = orderService.Id,
                            OrderDate = orderService.OrderDate,
                            TotalPriceAmount = orderService.TotalPriceAmount,
                            Quantity = orderService.Quantity,

                            //-------------
                            ServiceId = orderService.ServiceId,
                            PriceAmount = service.PriceAmount,
                            Status = service.Status,
                            ServiceName = service.ServiceName,

                            //-------------
                            CreatedBy = orderService.CreatedBy,
                            CreatedDate = orderService.CreatedDate,
                            ModifiedBy = orderService.ModifiedBy,
                            ModifiedDate = orderService.ModifiedDate,

                        }
                    ).ToList(),

                    //-------------
                    CreatedBy = order.CreatedBy,
                    CreatedDate = order.CreatedDate,
                    ModifiedBy = order.ModifiedBy,
                    ModifiedDate = order.ModifiedDate,
                }).ToList();

                return new ApiResponsePagination<OrderViewModel>(result, totalItems, currentPage, pageSize);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse> GetserviceDetails(Guid orderId, string search = "", int currentPage = 1, int pageSize = 1)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ và thực hiện kết nối với bảng ht_Room
                var query = _context.ht_OrderServiceDetails
                    .Where(o => o.CreatedBy.Equals(currentUserId) && o.OrderId.Equals(orderId))
                    .Join(_context.ht_Services,
                        serviceDetail => serviceDetail.ServiceId,
                        service => service.Id,
                        (serviceDetail, service) => new { serviceDetail, service })
                    .AsQueryable();

                // Áp dụng bộ lọc nếu có
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(o => o.service.ServiceName.Contains(search.Trim()));
                }

                // Lấy tổng số lượng phần tử
                var totalItems = await query.CountAsync();

                // Phân trang và lấy dữ liệu cho trang hiện tại
                var data = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(o => o.serviceDetail.CreatedDate)
                .ToListAsync();

                var result = data.Select(orderService => new OrderServiceDetailViewModel
                {
                    Id = orderService.serviceDetail.Id,
                    TotalPriceAmount = orderService.serviceDetail.TotalPriceAmount,
                    OrderDate = orderService.serviceDetail.OrderDate,
                    Quantity = orderService.serviceDetail.Quantity,

                    //--
                    ServiceId = orderService.serviceDetail.ServiceId,
                    ServiceName = orderService.service.ServiceName,
                    PriceAmount = orderService.service.PriceAmount,
                    Status = orderService.service.Status,

                    //-------------
                    CreatedBy = orderService.serviceDetail.CreatedBy,
                    CreatedDate = orderService.serviceDetail.CreatedDate,
                    ModifiedBy = orderService.serviceDetail.ModifiedBy,
                    ModifiedDate = orderService.serviceDetail.ModifiedDate,
                }).ToList();

                return new ApiResponsePagination<OrderServiceDetailViewModel>(result, totalItems, currentPage, pageSize);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse> GetOrderById(Guid id)
        {
            {
                try
                {
                    // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                    var result = await _context.ht_Orders
                        .Where(order => order.Id == id)
                        .Select(order => new OrderViewModel
                        {
                            Id = order.Id,
                            Identifier = order.Identifier,

                            CustomerName = order.CustomerName,
                            CustomerPhone = order.CustomerPhone,
                            Note = order.Note,

                            // Order room
                            OrderRooms = _context.ht_OrderRoomDetails
                    .Where(orderRoom => orderRoom.OrderId == order.Id) // Loại bỏ các bản ghi có RoomId là null
                    .Join(
                        _context.ht_Rooms.Where(room => room != null), // Loại bỏ các bản ghi null trong ht_Rooms
                        orderRoom => orderRoom.RoomId,
                        room => room.Id,
                        (orderRoom, room) => new OrderRoomDetailViewModel
                        {
                            Id = orderRoom.Id,
                            StartDate = orderRoom.StartDate,
                            EndDate = orderRoom.EndDate,

                            //-------------
                            RoomId = orderRoom.RoomId,
                            RoomName = room.RoomName,
                            FloorNumber = room.FloorNumber,
                            PriceAmount = room.PriceAmount,
                            Status = room.Status,

                            //-------------
                            CreatedBy = orderRoom.CreatedBy,
                            CreatedDate = orderRoom.CreatedDate,
                            ModifiedBy = orderRoom.ModifiedBy,
                            ModifiedDate = orderRoom.ModifiedDate,
                        }
                    ).ToList(),

                            // order service
                            OrderServices = _context.ht_OrderServiceDetails
                    .Where(orderService => orderService.OrderId == order.Id)
                    .Join(
                        _context.ht_Services.Where(service => service != null), // Loại bỏ các bản ghi null trong ht_Services
                        orderService => orderService.ServiceId,
                        service => service.Id,
                        (orderService, service) => new OrderServiceDetailViewModel
                        {
                            Id = orderService.Id,
                            OrderDate = orderService.OrderDate,
                            TotalPriceAmount = orderService.TotalPriceAmount,
                            Quantity = orderService.Quantity,

                            //-------------
                            ServiceId = orderService.ServiceId,
                            PriceAmount = service.PriceAmount,
                            Status = service.Status,
                            ServiceName = service.ServiceName,

                            //-------------
                            CreatedBy = orderService.CreatedBy,
                            CreatedDate = orderService.CreatedDate,
                            ModifiedBy = orderService.ModifiedBy,
                            ModifiedDate = orderService.ModifiedDate,

                        }
                    ).ToList(),

                            //-------------
                            CreatedBy = order.CreatedBy,
                            CreatedDate = order.CreatedDate,
                            ModifiedBy = order.ModifiedBy,
                            ModifiedDate = order.ModifiedDate,
                        })
                        .FirstOrDefaultAsync();

                    if (result == null)
                    {
                        return new ApiResponseError(HttpStatusCode.BadRequest, "The CategoryRoom does not exist by Id: " + id);
                    }

                    return new ApiResponse<OrderViewModel>(result);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, string.Empty);
                    return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

        public async Task<ApiResponse> GetRoomDetailById(Guid id)
        {
            try
            {

                var result = await _context.ht_OrderRoomDetails
                    .Where(orderRoom => orderRoom.Id == id) // Loại bỏ các bản ghi có RoomId là null
                    .Join(
                        _context.ht_Rooms.Where(room => room != null), // Loại bỏ các bản ghi null trong ht_Rooms
                        orderRoom => orderRoom.RoomId,
                        room => room.Id,
                        (orderRoom, room) => new OrderRoomDetailViewModel
                        {
                            Id = orderRoom.Id,
                            StartDate = orderRoom.StartDate,
                            EndDate = orderRoom.EndDate,

                            //-------------
                            RoomId = orderRoom.RoomId,
                            RoomName = room.RoomName,
                            FloorNumber = room.FloorNumber,
                            PriceAmount = room.PriceAmount,
                            Status = room.Status,

                            //-------------
                            CreatedBy = orderRoom.CreatedBy,
                            CreatedDate = orderRoom.CreatedDate,
                            ModifiedBy = orderRoom.ModifiedBy,
                            ModifiedDate = orderRoom.ModifiedDate,
                        }
                    ).FirstOrDefaultAsync();

                if (result == null)
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "The Room does not exist by Id: " + id);
                }

                return new ApiResponse<OrderRoomDetailViewModel>(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse> GetServiceDetailById(Guid id)
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var result = await _context.ht_OrderServiceDetails
                    .Where(orderService => orderService.Id == id)
                    .Join(
                        _context.ht_Services.Where(service => service != null), // Loại bỏ các bản ghi null trong ht_Services
                        orderService => orderService.ServiceId,
                        service => service.Id,
                        (orderService, service) => new OrderServiceDetailViewModel
                        {
                            Id = orderService.Id,
                            OrderDate = orderService.OrderDate,
                            TotalPriceAmount = orderService.TotalPriceAmount,
                            Quantity = orderService.Quantity,

                            //-------------
                            ServiceId = orderService.ServiceId,
                            PriceAmount = service.PriceAmount,
                            Status = service.Status,
                            ServiceName = service.ServiceName,

                            //-------------
                            CreatedBy = orderService.CreatedBy,
                            CreatedDate = orderService.CreatedDate,
                            ModifiedBy = orderService.ModifiedBy,
                            ModifiedDate = orderService.ModifiedDate,

                        }
                    ).FirstOrDefaultAsync();

                if (result == null)
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "The Service does not exist by Id: " + id);
                }

                return new ApiResponse<OrderServiceDetailViewModel>(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse> Update(Guid id, OrderCreateUpdateModel model)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                var orderToUpdate = await _context.ht_Orders.FindAsync(id);

                if (orderToUpdate == null)
                    return new ApiResponseError(HttpStatusCode.NotFound, "Không tìm thấy bản ghi");

                orderToUpdate.CustomerName = model.CustomerName;
                orderToUpdate.CustomerPhone = model.CustomerPhone;
                orderToUpdate.Identifier = model.Identifier;
                orderToUpdate.Note = model.Note;

                //-------------
                orderToUpdate.ModifiedDate = DateTime.UtcNow;
                orderToUpdate.ModifiedBy = currentUserId;

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    return new ApiResponse<bool>(HttpStatusCode.OK, true, "Thêm mới thành công");
                }

                return new ApiResponse<bool>(HttpStatusCode.NotFound, false, "Thêm mới thất bại");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateRoomDetail(Guid id, OrderRoomCreateUpdateModel model)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                var orderRoomToUpdate = await _context.ht_OrderRoomDetails.FindAsync(id);

                if (orderRoomToUpdate == null)
                    return new ApiResponseError(HttpStatusCode.NotFound, "Không tìm thấy bản ghi");

                orderRoomToUpdate.StartDate = model.StartDate;
                orderRoomToUpdate.EndDate = model.EndDate;

                //-------------
                orderRoomToUpdate.ModifiedDate = DateTime.UtcNow;
                orderRoomToUpdate.ModifiedBy = currentUserId;

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    return new ApiResponse<bool>(HttpStatusCode.OK, true, "Thêm mới thành công");
                }

                return new ApiResponse<bool>(HttpStatusCode.NotFound, false, "Thêm mới thất bại");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateServiceDetail(Guid id, OrderServiceCreateUpdateModel model)
        {
            try
            {
                // Lấy ID của người dùng hiện tại
                var currentUserId = GetExtensions.GetUserId(_httpContextAccessor);

                var orderServiceToUpdate = await _context.ht_OrderServiceDetails.FindAsync(id);

                if (orderServiceToUpdate == null)
                    return new ApiResponseError(HttpStatusCode.NotFound, "Không tìm thấy bản ghi");

                orderServiceToUpdate.OrderDate = model.OrderDate;
                orderServiceToUpdate.Quantity = model.Quantity;
                orderServiceToUpdate.TotalPriceAmount = _context.ht_Services.Find(orderServiceToUpdate.ServiceId).PriceAmount * model.Quantity;

                //-------------
                orderServiceToUpdate.ModifiedDate = DateTime.UtcNow;
                orderServiceToUpdate.ModifiedBy = currentUserId;

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    return new ApiResponse<bool>(HttpStatusCode.OK, true, "Thêm mới thành công");
                }

                return new ApiResponse<bool>(HttpStatusCode.NotFound, false, "Thêm mới thất bại");
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
