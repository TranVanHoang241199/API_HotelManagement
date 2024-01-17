using API_HotelManagement.common.Utils;
using API_HotelManagement.Data.Data;
using Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.Business.Services.Auth
{
    public class AuthHandler : IAuthHandler
    {

        private readonly HtDbContext _context;
        private readonly IConfiguration _config;

        public AuthHandler(HtDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        //public async Task<Response> Authenticate(AuthUser credentials)
        //{
        //    try
        //    {
        //        var userToAccount = new UserAccountViewModel();

        //        var user = await _context.
        //            .FirstOrDefaultAsync(u => u.Username.Equals(credentials.Username) && u.Password.Equals(credentials.Password));

        //        if (user == null)
        //            return new ApiResponse(HttpStatusCode.BadRequest, "Incorrect information");

        //        var claims = new List<Claim>
        //        {
        //            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        //            new Claim(ClaimTypes.Role, user.Role.ToString()),
        //            new Claim("Id", user.Id.ToString())
        //        };

        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //        var token = new JwtSecurityToken(
        //            _config["Jwt:Issuer"],
        //            _config["Jwt:Audience"],
        //            claims,
        //            expires: DateTime.Now.AddMinutes(20),
        //            signingCredentials: signIn
        //        );

        //        userToAccount.Token = new JwtSecurityTokenHandler().WriteToken(token).ToString();
        //        user.LastLogin = DateTime.Now;
        //        userToAccount.Username = user.Username;
        //        userToAccount.Id = user.Id;
        //        userToAccount.LastLogin = user.LastLogin;
        //        userToAccount.Role = user.Role;

        //        return new ResponseObject<UserAccount>(userToAccount, "Authentication successful");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, string.Empty);
        //        Log.Error("Credentials: {@credentials}", credentials);
        //        return new Response<UserCredentials>
        //        {
        //            Data = null,
        //            Message = ex.Message,
        //            Code = HttpStatusCode.InternalServerError
        //        };
        //    }
        //}
    }

}
