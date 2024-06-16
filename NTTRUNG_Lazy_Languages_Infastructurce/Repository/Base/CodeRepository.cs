using Dapper;
using NTTRUNG_Lazy_Languages_Domain.Entity;
using NTTRUNG_Lazy_Languages_Domain.Interface.Base;
using NTTRUNG_Lazy_Languages_Domain.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Infastructurce.Repository.Base
{
    /// <summary>
    /// Lớp cơ sở cho Repository dùng cho việc thao tác với dữ liệu mã code (Code).
    /// Kế thừa từ lớp BaseRepository để thực hiện các thao tác CRUD và ReadOnlyRepository để thực hiện thao tác chỉ đọc.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu dữ liệu của đối tượng Entity</typeparam>
    /// <typeparam name="TModel">Kiểu dữ liệu của đối tượng Model</typeparam>
    /// CreatedBy: NTTrung (14/07/2023)
    public abstract class CodeRepository<TEntity, TModel> : CRUDRepository<TEntity, TModel>, ICodeRepository<TEntity, TModel> where TEntity : BaseAudiEntity
    {
        #region Methods
        protected CodeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        /// <summary>
        /// Tìm data bằng Code 
        /// </summary>
        /// <paran name="entity">Code</paran>
        /// <returns>Đối tượng</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        public async Task<TEntity?> GetByCodeAsync(string code)
        {
            var storedProcedureName = $"Proc_{TableName}_GetByCode";
            var param = new DynamicParameters();
            param.Add($"@Code", code);
            var result = await _uow.Connection.QueryFirstOrDefaultAsync<TEntity>(storedProcedureName, param, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            return result;
        }
        /// <summary>
        /// lấy mã code mới
        /// </summary>
        /// <returns>mã mới</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        public async Task<string> GetNewCodeAsync(string prefix)
        {
            var storedProcedureName = $"Proc_Code_GetNewCode";
            var parameters = new DynamicParameters();
            parameters.Add("NewCode", dbType: DbType.String, direction: ParameterDirection.Output, size: 20);
            parameters.Add("@TableName", TableName);
            parameters.Add("@Prefix", prefix);
            await _uow.Connection.QueryFirstOrDefaultAsync<string>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            var result = parameters.Get<string>("NewCode");
            return result;
        }

        public async Task UpdateCodeAsync(string prefix)
        {
            var storedProcedureName = $"Proc_Code_Update";
            var parameters = new DynamicParameters();
            parameters.Add("@TableName", TableName);
            parameters.Add("@Prefix", prefix);
            await _uow.Connection.QueryFirstOrDefaultAsync<string>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
        }
        #endregion

    }
}
