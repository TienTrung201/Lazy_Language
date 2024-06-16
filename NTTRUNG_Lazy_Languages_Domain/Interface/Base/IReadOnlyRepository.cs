using NTTRUNG_Lazy_Languages_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Domain.Interface.Base
{
    public interface IReadOnlyRepository<TEntity, TModel>
    {
        /// <summary>
        /// Lấy bản ghi trong trang và lọc
        /// </summary>
        /// <paran name="entity">List Filter</paran>
        /// <returns>Danh sách bản ghi trong trang</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        /// <summary>
        Task<Pagination<TModel>> FilterAsync(FilterSort listFilter);
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        Task<IEnumerable<TModel>> GetAllAsync();

        /// <summary>
        /// Lấy bản ghi theo Id
        /// Không thấy bắn ra lỗi luôn
        /// </summary>
        /// <paran name="id">Id bản ghi</paran>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        Task<TModel> GetByIdAsync(Guid id);
        /// </summary>
        /// Tìm kiếm theo chuỗi ids
        /// </summary>
        /// <paran name="id">id</paran>
        /// <returns>Danh sách Đối tượng</returns>
        /// CreatedBy: NTTrung (03/08/2023)
        Task<IEnumerable<TModel>> GetByIdsAsync(string ids);
        /// <summary>
        /// Lấy bản ghi theo Id
        /// Không tìm thấy trả về null
        /// </summary>
        /// <paran name="id">Id bản ghi</paran>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        Task<TModel?> FindByIdAsync(Guid id);
        /// <summary>
        /// Phân trang tìm kiếm
        /// </summary>
        /// <returns>Số bản ghi + danh sách bản ghi</returns>
        /// CreatedBy: NTTrung (17/07/2023)
        Task<Pagination<TModel>> FilterAsync(int pageSize, int currentPage, string search);
    }
}
