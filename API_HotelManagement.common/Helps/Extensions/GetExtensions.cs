using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.common.Helps.Extensions
{
    public class GetExtensions
    {
        public static Guid GetUserId(IHttpContextAccessor httpContextAccessor)
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst("UserId");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                // Trả về Guid.Empty hoặc giá trị mặc định khác
                return Guid.Empty;
            }

            return userId;
        }
    }
}
