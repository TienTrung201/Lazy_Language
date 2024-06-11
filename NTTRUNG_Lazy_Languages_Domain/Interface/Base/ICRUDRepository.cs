using NTTRUNG_Laze_Languages_Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Domain.Interface.Base
{
    public interface ICRUDRepository<TEntity, TModel> : IReadOnlyRepository<TEntity, TModel>
    {
        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <paran name="entity">Thông tin chi tiết bản ghi xóa</paran>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        Task<int> DeleteAsync(TEntity entity);
        Task<int> DeleteManyAsync(string ids);
        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <paran name="ids">Chuỗi các Id bản ghi</paran>
        /// <returns>số bản ghi thay đổi</returns>
        /// CreatedBy: NTTrung (17/07/2023)
        Task<int> DeleteManyAsync(List<string> ids);

        /// <summary>
        /// Thêm nhiều
        /// </summary>
        /// <paran name="entity">Danh sách bản ghi thêm</paran>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: NTTrung (27/08/2023)
        Task<int> InsertMultipleAsync(List<TEntity> listEntity);
        /// <summary>
        /// Sửa nhiều
        /// </summary>
        /// <paran name="entity">Danh sách bản ghi sửa</paran>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: NTTrung (27/08/2023)
        Task<int> UpdateMultipleAsync(List<TEntity> listEntity);
    }
}
