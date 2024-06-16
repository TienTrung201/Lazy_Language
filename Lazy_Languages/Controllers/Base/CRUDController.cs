using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NTTRUNG_Lazy_Languages_Application.Interface.Base;
using NTTRUNG_Lazy_Languages_Application.Service.Base;

namespace NTTRUNG_Lazy_Languages_Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDController<TModel, TDto> : ReadOnlyController<TModel, TDto>
    {
        #region Fields
        protected readonly ICRUDService<TModel, TDto> _crudService;
        #endregion
        #region Constructor
        public CRUDController(ICRUDService<TModel, TDto> crudService) : base(crudService)
        {
            _crudService = crudService;
        }
        #endregion
        #region Methods
        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Trả về số bản ghi thay đổi</returns>
        /// CreatedBy: NTTrung (13/07/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _crudService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        /// <summary>
        /// DELETE Nhiều bản ghi
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>Trả về số bản ghi thay đổi</returns>
        /// CreatedBy: NTTrung (13/07/2023)
        [HttpDelete("DeleteMany")]
        public async Task<IActionResult> Delete([FromBody] List<string> ids)
        {
            var result = await _crudService.DeleteManyAsync(ids);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        // <summary>
        // Lưu thông tin hàng hóa
        // </summary>
        // <returns>Số bản ghi thay đổi trong db</returns>
        // createdby: nttrung (22/08/2023)
        [Authorize]
        [HttpPost("SaveData")]
        public async Task<IActionResult> SaveData([FromBody] List<TDto> listData)
        {
            var result = await _crudService.SaveData(listData);
            return Ok(result);
        }
        #endregion
    }
}
