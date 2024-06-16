using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NTTRUNG_Lazy_Languages_Application.Interface.Base;
using NTTRUNG_Lazy_Languages_Domain.Common;

namespace NTTRUNG_Lazy_Languages_Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadOnlyController<TModel, TDto> : ControllerBase
    {
        #region Field
        private readonly IReadOnlyService<TModel, TDto> _readOnlyService;
        #endregion
        #region Constructor
        public ReadOnlyController(IReadOnlyService<TModel, TDto> readOnlyService)
        {
            _readOnlyService = readOnlyService;
        }
        #endregion

        // <summary>
        // Phân trang lọc sắp xếp
        // </summary>
        // <returns>danh sách bản ghi tìm thấy</returns>
        // createdby: nttrung (17/07/2023)
        [HttpPost("filter")]
        public async Task<IActionResult> FilterSortAsync([FromBody] FilterSort filter)
        {
            var result = await _readOnlyService.FilterAsync(filter);
            return Ok(result);
        }
        // <summary>
        // Phân trang tìm kiếm bản ghi
        // </summary>
        // <returns>danh sách bản ghi tìm thấy</returns>
        // createdby: nttrung (17/07/2023)
        [HttpGet("filter")]
        public async Task<IActionResult> FilterAsync([FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string? search)
        {
            var result = await _readOnlyService.FilterAsync(pageSize, pageNumber, search);
            return Ok(result);
        }
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: NTTrung (13/07/2023)
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _readOnlyService.GetAllAsync();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi cần lấy</param>
        /// <returns>Bản ghi được tìm thấy qua Id</returns>
        /// CreatedBy: NTTrung (13/07/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail([FromRoute] Guid id)
        {
            var result = await _readOnlyService.GetByIdAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
