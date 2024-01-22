using API_HotelManagement.common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.Business.Services.Orders
{
    public class OrderHandler : IOrderHandler
    {
        public Task<ApiResponse> CreateOrder(OrderCreateUpdateModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> DeleteOrder(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetAllOrders(string search = "", int currentPage = 1, int pageSize = 1)
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
