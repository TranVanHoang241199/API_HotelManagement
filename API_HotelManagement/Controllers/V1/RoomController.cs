﻿using API_HotelManagement.Business.Services.Rooms;
using API_HotelManagement.common.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_HotelManagement.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    //[ApiExplorerSettings(GroupName = "Users")]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/rooms")]
    [ApiExplorerSettings(GroupName = "Room")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomHandler _roomService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomService"></param>
        public RoomController(IRoomHandler roomService)
        {
            _roomService = roomService;
        }

        #region CRUD

        /// <summary>
        /// Lấy về theo bộ lọc
        /// </summary>
        /// <param name="search"></param>
        /// <param name="currentPage">Page hiển thị hiện tại </param>
        /// <param name="pageSize">Kích thước lỗi trang</param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("")]
        [ProducesResponseType(typeof(ApiResponsePagination<RoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRooms(string search = "", int currentPage = 1, int pageSize = 10)
        {

            var result = await _roomService.GetAllRooms(search, currentPage, pageSize);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Lấy về theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpGet, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<RoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoomById(Guid id)
        {

            var result = await _roomService.GetRoomById(id);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("")]
        [ProducesResponseType(typeof(ApiResponseObject<RoomViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateRoom([FromBody] RoomCreateUpdateModel model)
        {

            var result = await _roomService.CreateRoom(model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPut, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] RoomCreateUpdateModel model)
        {

            var result = await _roomService.UpdateRoom(id, model);

            return ApiHelper.TransformData(result);
        }

        /// <summary>
        /// Xoá
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize, HttpDelete, Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {

            var result = await _roomService.DeleteRoom(id);

            return ApiHelper.TransformData(result);
        }

        #endregion CRUD

    }
}