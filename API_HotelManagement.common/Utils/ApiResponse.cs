using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_HotelManagement.common.Utils
{
    public class ApiResponse
    {
        public ApiResponse(HttpStatusCode status, string message)
        {
            Status = status;
            Message = message;
        }

        public ApiResponse(string message)
        {
            Message = message;
        }

        public ApiResponse()
        {
        }

        //[JsonProperty(Order = 1)]
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;

        public string Message { get; set; } = "Success";
        //public long TotalTime { get; set; } = 0;
    }

    #region login
    public class ApiResponseAuth : ApiResponse
    {
        public ApiResponseAuth(string accessToken) : base(HttpStatusCode.OK, "Success")
        {
            AccessToken = accessToken;
        }

        public ApiResponseAuth(HttpStatusCode status, string accessToken) : base(status, "OK")
        {
            AccessToken = accessToken;
        }

        public ApiResponseAuth(HttpStatusCode status, string accessToken, string message) : base(status, message)
        {
            AccessToken = accessToken;
        }

        //[JsonProperty(Order = 2)] // cho nằm vt cột 3
        public string AccessToken { get; set; }
    }
    #endregion login


    #region Get

    public class ApiResponse<T> : ApiResponse
    {
        public ApiResponse(T data) : base(HttpStatusCode.OK, "Success")
        {
            Data = data;
        }

        public ApiResponse(HttpStatusCode status, T data) : base(status, "OK")
        {
            Data = data;
        }

        public ApiResponse(HttpStatusCode status, T data, string message) : base(status, message)
        {
            Data = data;
        }

        public T Data { get; set; }
    }

    public class ApiResponseList<T> : ApiResponse
    {
        public ApiResponseList(List<T> data) : base(HttpStatusCode.OK, "Success")
        {
            Data = data;
        }

        public List<T> Data { get; set; }
    }

    public class ApiResponsePagination<T> : ApiResponse
    {
        public ApiResponsePagination(List<T> data, long totalItems, int currentPage, int pageSize) : base(HttpStatusCode.OK, "Success")
        {
            Meta = new PaginationMeta
            {
                TotalItems = totalItems,
                CurrentPage = currentPage,
                PageSize = pageSize
            };
            Data = data;
        }

        public PaginationMeta Meta { get; set; }
        public List<T> Data { get; set; }
    }

    public class PaginationMeta
    {
        /// <summary>
        /// tổng số phần tử 
        /// </summary>
        public long TotalItems { get; set; }
        /// <summary>
        /// số lượng trang dữ liệu mà bạn có
        /// (được tính dựa vào totalItems và PageSize)
        /// </summary>
        public int TotalPage
        {
            get
            {
                if (PageSize == 0)
                    return 1;
                int p = (int)(TotalItems / PageSize);
                if (TotalItems % PageSize > 0)
                    p += 1;
                return p;
            }
        }
        /// <summary>
        /// trang dữ liệu hiện tại mà bạn đang yêu cầu hoặc đang xem (Nhập vào)
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// kích thước của mỗi trang dữ liệu (Nhập vào)
        /// </summary>
        public int PageSize { get; set; }
    }

    #endregion Get

    #region Post

    public class ApiResponseObject<T> : ApiResponse
    {
        public ApiResponseObject(T data) : base(HttpStatusCode.OK, "Success")
        {
            Data = data;
        }

        public ApiResponseObject(T data, string message) : base(HttpStatusCode.OK, message)
        {
            Data = data;
        }

        public ApiResponseObject(T data, string message, HttpStatusCode status) : base(status, message)
        {
            Data = data;
        }

        public T Data { get; set; }
    }

    #endregion Post

    #region Update

    public class ApiResponseUpdate : ApiResponse
    {
        public ApiResponseUpdate(Guid id) : base(HttpStatusCode.OK, "Success")
        {
            Data = new ApiResponseUpdateModel { Id = id };
        }

        public ApiResponseUpdate(Guid id, string message) : base(HttpStatusCode.OK, message)
        {
            Data = new ApiResponseUpdateModel { Id = id };
        }

        public ApiResponseUpdate(HttpStatusCode status, string message, Guid id) : base(status, message)
        {
            Data = new ApiResponseUpdateModel { Id = id };
        }

        public ApiResponseUpdateModel Data { get; set; }
    }

    public class ApiResponseUpdateMulti : ApiResponse
    {
        public ApiResponseUpdateMulti(List<ApiResponseUpdate> data) : base(HttpStatusCode.OK, "Success")
        {
            Data = data;
        }

        public List<ApiResponseUpdate> Data { get; set; }
    }

    public class ApiResponseUpdateModel
    {
        public Guid Id { get; set; }
    }

    #endregion Update

    #region Delete

    public class ApiResponseDelete : ApiResponse
    {
        public ApiResponseDelete(Guid id, string name) : base(HttpStatusCode.OK, "Success")
        {
            Data = new ApiResponseDeleteModel { Id = id, Name = name };
        }

        public ApiResponseDelete(HttpStatusCode status, string message, Guid id, string name) : base(status, message)
        {
            Data = new ApiResponseDeleteModel { Id = id, Name = name };
        }

        public ApiResponseDeleteModel Data { get; set; }
    }

    public class ApiResponseDeleteMulti : ApiResponse
    {
        public ApiResponseDeleteMulti(List<ApiResponseDelete> data) : base(HttpStatusCode.OK, "Success")
        {
            Data = data;
        }

        public List<ApiResponseDelete> Data { get; set; }
    }

    public class ApiResponseDeleteModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    #endregion Delete

    #region Error

    public class ApiResponseError : ApiResponse
    {
        public ApiResponseError(HttpStatusCode status, string message, List<Dictionary<string, string>> errorDetail = null) : base(status, message)
        {
            ErrorDetail = errorDetail;
        }

        public List<Dictionary<string, string>> ErrorDetail { get; set; }
    }

    #endregion Error
}
