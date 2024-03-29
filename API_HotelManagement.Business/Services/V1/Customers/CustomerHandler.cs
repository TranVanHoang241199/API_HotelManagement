﻿using API_HotelManagement.common.Utils;
using API_HotelManagement.Data;
using Serilog;
using System.Net;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using API_HotelManagement.common.Helps.Extensions;
using Microsoft.AspNetCore.Http;

namespace API_HotelManagement.Business
{
    public class CustomerHandler : ICustomerHandler
    {
        private readonly HtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerHandler(HtDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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
                var query = _context.ht_Orders.Where(o => o.CreatedBy.Equals(currentUserId)).AsQueryable();

                // Áp dụng bộ lọc nếu có
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(o => o.CustomerName.Contains(search.Trim()) || o.CustomerPhone.ToString().Contains(search.Trim()));
                }

                // Lấy tổng số lượng phần tử
                var totalItems = await query.CountAsync();

                // Phân trang và lấy dữ liệu cho trang hiện tại
                var data = await query
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(o => o.ModifiedDate)
                    .OrderBy(o => o.CreatedDate)
                    .Select(p => new CustomerViewModel
                    {
                        CustomerName = p.CustomerName,
                        CustomerPhone = p.CustomerPhone,
                    }).ToArrayAsync();



                var result = _mapper.Map<List<CustomerViewModel>>(data).ToList();

                return new ApiResponsePagination<CustomerViewModel>(result, totalItems, currentPage, pageSize);
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
        public async Task<ApiResponse> GetCustomerById(Guid id)
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu sử dụng LINQ
                var result = await _context.ht_Orders
                    .Where(test => test.Id == id)
                    .Select(test => new CustomerViewModel
                    {
                        CustomerName = test.CustomerName,
                        CustomerPhone = test.CustomerPhone,
                    })
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return new ApiResponseError(HttpStatusCode.BadRequest, "The Customer does not exist by Id: " + id);
                }

                return new ApiResponse<CustomerViewModel>(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                return new ApiResponseError(HttpStatusCode.InternalServerError, "Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
