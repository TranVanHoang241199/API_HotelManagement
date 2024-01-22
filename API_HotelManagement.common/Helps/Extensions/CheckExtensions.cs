using API_HotelManagement.common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API_HotelManagement.common.Helps.Extensions
{
    public class CheckExtensions
    {
        public static bool IsValidEmail(string email)
        {
            // Biểu thức chính quy kiểm tra định dạng email
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            return Regex.IsMatch(email, emailPattern);
        }

        /// <summary>
        /// Check mật khẩu có phù hợp với {[không được bỏ trống], [lớp hơn 6 ký tự], 
        /// [có it nhất một ký tự hoa], [có it nhất ký tự thường], [có it nhất một số], 
        /// [có ít nhất một ký tự đặc biệt] }
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static bool IsValidPass(string pass)
        {
            if (string.IsNullOrWhiteSpace(pass) ||
                    pass.Length < 6 ||
                    !Regex.IsMatch(pass, "[A-Z]") ||
                    !Regex.IsMatch(pass, "[a-z]") ||
                    !Regex.IsMatch(pass, "[0-9]") ||
                    !Regex.IsMatch(pass, "[^a-zA-Z0-9]"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check UserName
        /// Không được tồn rổng, không được bế hơn 4 và lớn hơn 16 
        /// không tồn tại chữ in hoa
        /// không tồn tại ký tự đặt biệt
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsValidUsername(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName) ||
                    userName.Length < 4 || userName.Length > 50 ||
                    Regex.IsMatch(userName, "[A-Z]") ||
                    Regex.IsMatch(userName, "[^a-zA-Z0-9]"))
            {
                return false;
            }
            return true;
        }
    }
}
