using Microsoft.AspNetCore.Mvc;
using NTTRUNG_Lazy_Languages_Application.Interface.Base;
using NTTRUNG_Lazy_Languages_Application.Service.Base;

namespace NTTRUNG_Lazy_Languages_Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController<TModel, TDto> : CRUDController<TModel, TDto>
    {
        #region Fields
        protected readonly ICodeService<TModel, TDto> _codeService;
        #endregion
        #region Constructor

        public CodeController(ICodeService<TModel, TDto> codeService) : base(codeService)
        {
            _codeService = codeService;
        }
        #endregion
        #region Methods
        /// <summary>
        /// GET new Code
        /// </summary>
        /// <returns>Mã code mới nhất</returns>
        /// CreatedBy: NTTrung (13/07/2023)
        [HttpGet("NewCode/{prefix}")]
        public async Task<IActionResult> GetNewCode([FromRoute] string prefix)
        {
            var result = await _codeService.GetNewCodeAsync(prefix);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        #endregion
    }
}
