using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Domain.Interface.UnitOfWork
{
    /// <summary>
    /// Giao diện đại diện cho đơn vị công việc (Unit of Work) trong quá trình thao tác với cơ sở dữ liệu
    /// </summary>
    /// CreatedBy: NTTrung (16/07/2023)
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {

        #region Fields
        /// <summary>
        /// Đối tượng kết nối đến cơ sở dữ liệu
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023)
        DbConnection Connection { get; }

        /// <summary>
        /// Giao dịch đang thực hiện
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023)
        DbTransaction? Transaction { get; }
        #endregion
        #region Methods
        /// <summary>
        /// Mở giao dịch
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023)
        void BeginTransaction();
        Task BeginTransactionAsync();
        /// <summary>
        /// Xác nhận và thực thi giao dịch
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023)
        void Commit();
        Task CommitAsync();
        /// <summary>
        /// Hủy bỏ và quay lại trạng thái ban đầu trước khi mở giao dịch
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023)
        void RollBack();
        Task RollBackAsync();
        #endregion
    }
}
