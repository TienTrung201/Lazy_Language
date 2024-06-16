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
using static Dapper.SqlMapper;

namespace NTTRUNG_Lazy_Languages_Infastructurce.Repository.Base
{
    /// <summary>
    /// Lớp cơ sở cho Repository dùng cho các thao tác chỉ đọc (ReadOnly) và thao tác cơ bản (CRUD) với cơ sở dữ liệu.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu dữ liệu của đối tượng Entity</typeparam>
    /// <typeparam name="TModel">Kiểu dữ liệu của đối tượng Model</typeparam>
    /// CreatedBy: NTTrung (14/07/2023)
    public abstract class CRUDRepository<TEntity, TModel> :
        ReadOnlyRepository<TEntity, TModel>, ICRUDRepository<TEntity, TModel>
        where TEntity : BaseAudiEntity //chắc chắn TEntity phải kế thừa BaseAudiEntity thì mới .GetKey được
    {
        protected CRUDRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #region Methods
        /// <summary>
        /// Xóa một bản ghi khỏi cơ sở dữ liệu.
        /// </summary>
        /// <param name="entity">Đối tượng cần xóa</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        public async Task<int> DeleteAsync(TEntity entity)
        {
            var storedProcedureName = $"Proc_{TableName}_DeleteById";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", entity.GetKey());

            var result = await _uow.Connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return result;
        }
        /// <summary>
        /// Xóa nhiều bản ghi từ danh sách các id được cung cấp.
        /// </summary>
        /// <param name="ids">Chuỗi id của các bản ghi cần xóa</param>
        /// <returns>Số bản ghi thay đổi</returns>
        public virtual async Task<int> DeleteManyAsync(string ids)
        {
            var storedProcedureName = $"Proc_{TableName}_DeleteMany";
            var parameters = new DynamicParameters();
            parameters.Add("@Ids", ids);

            var result = await _uow.Connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            return result;
        }
        /// <summary>
        /// Xóa nhiều bản ghi từ danh sách các id được cung cấp.
        /// </summary>
        /// <param name="ids">Chuỗi id của các bản ghi cần xóa</param>
        /// <returns>Số bản ghi thay đổi</returns>
        public virtual async Task<int> DeleteManyAsync(List<string> ids)
        {
            var parameters = new DynamicParameters();
            var query = new StringBuilder();
            int indexData = 0;
            foreach (var id in ids)
            {
                var param = $"@id{indexData}";
                parameters.Add(param, id);
                query.Append($"Delete From `{TableName}` where {TableName}ID = {param}; ");
                indexData++;
            }
            var queryString = query.ToString();

            var result = await _uow.Connection.ExecuteAsync(queryString, parameters, commandType: CommandType.Text, transaction: _uow.Transaction);
            return result;
        }
        /// <summary>
        /// Tạo CreateParameters từ Entity  nhiều
        /// </summary>
        /// <paran name="entity">Đối tượng</paran>
        /// <returns>parameters</returns>
        private DynamicParameters CreateParametersFromEntity(TEntity entity)
        {
            var parameters = new DynamicParameters();//Tạo parameter
            var properties = typeof(TEntity).GetProperties();//Lấy danh sách các thuộc tính của đối tượng
            foreach (var property in properties)
            {
                var value = property.GetValue(entity);
                parameters.Add($"@{property.Name}", value);
            }
            return parameters;//Trả về đối tượng parameters từ đối tượng entity
        }

        /// <summary>
        /// Thêm nhiều
        /// </summary>
        /// <paran name="entity">Danh sách bản ghi thêm</paran>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: NTTrung (27/08/2023)
        public async Task<int> InsertMultipleAsync(List<TEntity> listEntity)
        {
            var parameters = new DynamicParameters();
            var query = new StringBuilder();
            var properties = typeof(TEntity).GetProperties();//Lấy danh sách các thuộc tính của đối tượng

            query.Append($"INSERT INTO {TableName} ");
            var listPropertiesToString = new List<string>();

            foreach (var property in properties)
            {
                listPropertiesToString.Add($"`{property.Name}`");
            }
            query.Append($"( {string.Join(", ", listPropertiesToString)} ) Values ");
            int indexData = 0;
            listEntity.ForEach((entity) =>
            {
                query.Append("( ");
                int indexColumn = 0;
                foreach (var property in properties)
                {
                    var value = property.GetValue(entity);
                    string nameProperty = property.Name;
                    string paramName = $"@{nameProperty}{indexData}";
                    query.Append(paramName);
                    parameters.Add(paramName, value);
                    if (indexColumn != properties.Count() - 1)
                    {
                        query.Append(", ");
                    }
                    indexColumn++;
                };

                if (indexData != listEntity.Count() - 1)
                {
                    query.Append("), ");
                }
                else
                {
                    query.Append(") ");
                }
                indexData++;
            });
            var queryString = query.ToString();

            var result = await _uow.Connection.ExecuteAsync(queryString, parameters, commandType: CommandType.Text, transaction: _uow.Transaction);
            return result;
        }
        /// <summary>
        /// Sửa nhiều
        /// </summary>
        /// <paran name="entity">Danh sách bản ghi sửa</paran>
        /// <returns>Bản ghi</returns>
        /// CreatedBy: NTTrung (27/08/2023)
        public async Task<int> UpdateMultipleAsync(List<TEntity> listEntity)
        {
            var parameters = new DynamicParameters();
            var query = new StringBuilder();
            var properties = typeof(TEntity).GetProperties();//Lấy danh sách các thuộc tính của đối tượng

            int indexData = 0;
            listEntity.ForEach((entity) =>
            {
                query.Append($"Update {TableName} Set ");
                int indexColumn = 0;
                foreach (var property in properties)
                {
                    var value = property.GetValue(entity);
                    string nameProperty = property.Name;
                    if (nameProperty != "CreatedDate")
                    {
                        string paramName = $"@{nameProperty}{indexData}";
                        query.Append($"`{nameProperty}` = {paramName} ");
                        parameters.Add(paramName, value);
                        if (indexColumn != properties.Count() - 2)//Bỏ qua createdDate nên - 2 và index từ 0
                        {
                            query.Append(", ");
                        }
                        indexColumn++; ;
                    }
                }
                indexData++;
                query.Append($"Where {TableName}Id = '{entity.GetKey()}'; ");
            });
            var queryString = query.ToString();

            var result = await _uow.Connection.ExecuteAsync(queryString, parameters, commandType: CommandType.Text, transaction: _uow.Transaction);
            return result;
        }
        #endregion
    }
}
