using AutoMapper;
using NTTRUNG_Lazy_Languages_Application.Interface.Base;
using NTTRUNG_Lazy_Languages_Domain.Common;
using NTTRUNG_Lazy_Languages_Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Application.Service.Base
{
    public abstract class ReadOnlyService<TEntity, TModel, TDto> : IReadOnlyService<TModel, TDto>
    {
        public virtual string TableName { get; protected set; } = typeof(TEntity).Name;
        #region Fields
        protected readonly IReadOnlyRepository<TEntity, TModel> _readOnlyRepository;
        protected readonly IMapper _mapper;
        #endregion
        #region Constructor
        protected ReadOnlyService(IReadOnlyRepository<TEntity, TModel> readOnlyRepository, IMapper mapper)
        {
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy bản ghi trong trang và lọc
        /// </summary>
        /// <paran name="entity">List Filter</paran>
        /// <returns>Danh sách bản ghi trong trang</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        /// <summary>
        public async Task<Pagination<TModel>> FilterAsync(FilterSort listFilter)
        {
            var result = await _readOnlyRepository.FilterAsync(listFilter);
            return result;
        }
        /// <summary>
        /// Phân trang tìm kiếm
        /// </summary>
        /// <returns>Danh sác bản ghi trong trang</returns>
        /// CreatedBy: NTTrung (17/07/2023)
        public async Task<Pagination<TModel>> FilterAsync(int? pageSize, int? currentPage, string? search)
        {
            if (string.IsNullOrEmpty(search))
            {
                search = "";
            }
            if (!pageSize.HasValue)
            {
                pageSize = 10;
            }
            if (!currentPage.HasValue || currentPage.Value < 1)
            {
                currentPage = 1;
            }
            var result = await _readOnlyRepository.FilterAsync(pageSize.Value, currentPage.Value, search);

            //pagedEmployees.Data = _mapper.Map<IEnumerable<EmployeeDto>>(pagedEmployees.Data);

            return result;
        }
        /// <summary>
        /// Lấy tất cả danh sách
        /// </summary>
        /// <returns>Mã code mới</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var model = await _readOnlyRepository.GetAllAsync();
            var lisTDto = _mapper.Map<List<TDto>>(model);
            return lisTDto;
        }
        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Bsản ghi tìm thấy</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        public async Task<TDto> GetByIdAsync(Guid id)
        {
            var entity = await _readOnlyRepository.GetByIdAsync(id);
            var entityDto = _mapper.Map<TDto>(entity);
            return entityDto;
        }
        #endregion
    }
}
