using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_HotelManagement.common.Utils
{
    public class ApiHelper
    {

        /// <summary>
        /// Transform data to http response
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ActionResult TransformData(ApiResponse data)
        {
            var result = new ObjectResult(data) { StatusCode = (int)data.Status };
            return result;
        }

        ///// <summary>
        ///// Transform data to http response
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static IActionResult TransformData<T>(ApiResponse<T> data)
        //{
        //    return new ObjectResult(data)
        //    {
        //        StatusCode = (int)data.Status
        //    };
        //}

        //public static IActionResult TransformData<T>(ApiResponsePagination<T> data)
        //{
        //    return new ObjectResult(data)
        //    {
        //        StatusCode = (int)data.Status
        //    };
        //}


    }
}
