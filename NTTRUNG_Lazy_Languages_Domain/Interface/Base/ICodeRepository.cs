using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Domain.Interface.Base
{
    public interface ICodeRepository<TEntity, TModel> : ICRUDRepository<TEntity, TModel>
    {
        /// <summary>
        /// Get bản ghi theo mã
        /// </summary>
        /// <param name="code">Mã code bản ghi</param>
        /// <returns>bản ghi được tìm thấy</returns>
        Task<TEntity?> GetByCodeAsync(string entityCode);
        /// <summary>
        /// Lấy mã bản ghi mới
        /// </summary>
        /// <returns>Mã bản ghi</returns>
        /// CreatedBy: NTTrung (17/07/2023)
        Task<string> GetNewCodeAsync(string prefix);
        /// <summary>
        /// Hàm update mã mới
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task UpdateCodeAsync(string prefix);
    }
}
