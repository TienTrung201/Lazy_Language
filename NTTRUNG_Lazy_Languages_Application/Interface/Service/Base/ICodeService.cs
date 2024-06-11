using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Application.Interface.Base
{
    public interface ICodeService<Tmodel, TDto> : ICRUDService<Tmodel, TDto>
    {
        /// <summary>
        /// Lấy mã mới
        /// </summary>
        /// <returns>Mã mới</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        Task<string> GetNewCodeAsync(string prefix);
    }
}
