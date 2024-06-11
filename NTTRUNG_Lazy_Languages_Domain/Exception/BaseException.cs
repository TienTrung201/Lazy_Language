using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Domain
{
    /// <summary>
    /// Đối tượng trả về lỗi cho Api
    /// </summary>
    /// CreatedBy: NTTrung (16/07/2023) 
    public class BaseException
    {
        #region Properties
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// Thông báo lỗi cho Dev
        /// </summary>
        public string? DevMessage { get; set; }
        /// <summary>
        /// Thông báo lỗi cho User
        /// </summary>
        public string? UserMessage { get; set; }
        /// <summary>
        /// Id lỗi
        /// </summary>
        public string? TraceId { get; set; }
        /// <summary>
        /// Thông tin khác
        /// </summary>
        public string? MoreInfo { get; set; }
        /// <summary>
        /// Lỗi này dành cho validate đầu vào
        /// </summary>
        public object? Errors { get; set; }
        #endregion
        #region Method
        /// <summary>
        /// Hàm chuyển đối tượng sang dạng chuỗi Json
        /// </summary>
        /// <returns>JsonObject</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion
    }
}
