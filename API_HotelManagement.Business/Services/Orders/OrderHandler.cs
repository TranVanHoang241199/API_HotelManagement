using API_HotelManagement.Business.Services.Rooms;
using API_HotelManagement.Business.Services.Services;
using API_HotelManagement.common.Utils;
using API_HotelManagement.Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;

namespace API_HotelManagement.Business.Services.Orders
{
    public class OrderHandler : IOrderHandler
    {
        private readonly HtDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderHandler(HtDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<ApiResponse> CreateOrder(OrderCreateUpdateModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> DeleteOrder(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetAllOrders(string search = "", int currentPage = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetOrderById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> UpdateOrder(Guid id, OrderCreateUpdateModel model)
        {
            throw new NotImplementedException();
        }
    }
}
