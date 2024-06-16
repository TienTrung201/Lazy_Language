using AutoMapper;
using NTTRUNG_Lazy_Languages_Application.Dtos;
using NTTRUNG_Lazy_Languages_Application.Interface.Base;
using NTTRUNG_Lazy_Languages_Domain.Entity;
using NTTRUNG_Lazy_Languages_Domain.Interface.Base;
using NTTRUNG_Lazy_Languages_Domain.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Application.Service.Base
{
    public abstract class CodeService<TEntity, TModel, TDto> : CRUDService<TEntity, TModel, TDto>,
        ICodeService<TModel, TDto> where TDto : BaseDto where TEntity : BaseAudiEntity

    {
        #region Fields
        protected readonly ICodeRepository<TEntity, TModel> _codeRepository;
        #endregion
        #region Constructor
        protected CodeService(ICodeRepository<TEntity, TModel> codeRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(codeRepository, mapper, unitOfWork)
        {
            _codeRepository = codeRepository;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Get code mới nhất
        /// </summary>
        /// <returns>Mã code mới</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        public async Task<string> GetNewCodeAsync(string prefix)
        {
            var newCode = await _codeRepository.GetNewCodeAsync(prefix);
            return newCode;
        }
        #endregion
    }
}
