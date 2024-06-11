using Dapper;
using NTTRUNG_Laze_Languages_Domain;
using NTTRUNG_Laze_Languages_Domain.Common;
using NTTRUNG_Laze_Languages_Domain.Enum;
using NTTRUNG_Laze_Languages_Domain.Interface.Base;
using NTTRUNG_Laze_Languages_Domain.Interface.UnitOfWork;
using NTTRUNG_Laze_Languages_Domain.Resources.ErrorMessage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Laze_Languages_Infastructurce.Repository.Base
{
    /// <summary>
    /// Lớp cơ sở cho Repository chỉ đọc (ReadOnly) dùng cho việc truy vấn dữ liệu từ cơ sở dữ liệu.
    /// Kế thừa từ lớp IReadOnlyRepository để cung cấp các phương thức truy vấn dữ liệu cơ bản.
    /// </summary>
    /// <typeparam name="TEntity">Kiểu dữ liệu của đối tượng Entity</typeparam>
    /// <typeparam name="TModel">Kiểu dữ liệu của đối tượng Model</typeparam>
    /// CreatedBy: NTTrung (14/07/2023)
    public abstract class ReadOnlyRepository<TEntity, TModel> : IReadOnlyRepository<TEntity, TModel>
    {
        #region Fields
        protected readonly IUnitOfWork _uow;
        public virtual string TableName { get; protected set; } = typeof(TEntity).Name;
        public virtual string TableId { get; protected set; } = typeof(TEntity).Name + "Id";
        #endregion
        #region Constructor
        protected ReadOnlyRepository(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Lấy bản ghi trong trang và lọc
        /// </summary>
        /// <paran name="filter">List Filter</paran>
        /// <returns>Danh sách bản ghi trong trang</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        /// <summary>
        public async Task<Pagination<TModel>> FilterAsync(FilterSort filter)
        {
            var storedProcedureName = $"Proc_Filter";
            var parameters = new DynamicParameters();
            var queryWhere = new StringBuilder();
            var queryOrder = new StringBuilder();
            var queryLimit = new StringBuilder();
            var queryCode = new StringBuilder();
            int indexList = 0;
            if (filter.Filter?.Any() == true)
            {
                queryWhere.Append("Where ");
                int lengFilterProperties = filter.Filter.Count();
                filter.Filter.ForEach((filter) =>
                {
                    switch (filter.Operator)
                    {
                        case Operator.EQual:
                            queryWhere.Append($"view.{filter.Property} = '{filter.Value.Replace("'", "''")}' ");
                            break;
                        case Operator.NotEqual:
                            queryWhere.Append($"view.{filter.Property} != '{filter.Value.Replace("'", "''")}' ");
                            break;
                        case Operator.Contain:
                            queryWhere.Append($"view.{filter.Property} Like '%{filter.Value.Replace("'", "''")}%' ");
                            break;
                        case Operator.NotContain:
                            queryWhere.Append($"view.{filter.Property} Not Like '%{filter.Value.Replace("'", "''")}%' ");
                            break;
                        case Operator.Smaller:
                            queryWhere.Append($"view.{filter.Property} < '{filter.Value.Replace("'", "''")}' ");
                            break;
                        case Operator.Greater:
                            queryWhere.Append($"view.{filter.Property} > '{filter.Value.Replace("'", "''")}' ");
                            break;
                        case Operator.AllData:
                            queryWhere.Append($"(view.{filter.Property} = true or view.{filter.Property} = false) ");
                            break;
                        default:
                            break;
                    }
                    if (indexList != lengFilterProperties - 1)
                    {
                        queryWhere.Append("And ");
                    }
                    indexList++;
                });
            }

            if (!string.IsNullOrEmpty(filter.PropertySort))
            {
                switch (filter.TypeColumn)
                {
                    case TypeColumn.Code:
                        string filterBy = "Desc";
                        if (filter.SortBy == SortBy.Asc)
                        {
                            filterBy = "Asc";
                        }
                        queryOrder.Append($"Order by `Prefix` {filterBy}, `ValueCode` ");
                        queryCode.Append($", REGEXP_REPLACE( view.{filter.PropertySort},'[^a-zA-Z]' , '') AS `Prefix`, ");
                        queryCode.Append($"CAST( REGEXP_REPLACE( view.{filter.PropertySort},'[^0-9]' , '') AS UNSIGNED ) AS `ValueCode` ");
                        break;
                    default:
                        queryOrder.Append($"Order by view.{filter.PropertySort} ");
                        break;
                }

                switch (filter.SortBy)
                {
                    case SortBy.Desc:
                        queryOrder.Append($"Desc ");
                        break;
                    case SortBy.Asc:
                        queryOrder.Append($"Asc ");
                        break;
                    default:
                        break;
                }
            }
            if (filter.CurrentPage < 1)
            {
                filter.CurrentPage = 1;
            }
            queryLimit.Append($"Limit {filter.PageSize} Offset {(filter.CurrentPage - 1) * filter.PageSize}");
            var queryLimitString = queryLimit.ToString();
            var queryWhereString = queryWhere.ToString();
            var queryOrderString = queryOrder.ToString();
            var queryCodeString = queryCode.ToString();
            parameters.Add("@QueryLimit", queryLimitString);
            parameters.Add("@QueryWhere", queryWhereString);
            parameters.Add("@QueryOrder", queryOrderString);
            parameters.Add("@QueryCode", queryCodeString);
            parameters.Add("@TableName", TableName.ToLower());
            parameters.Add("@TotalRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var listData = await _uow.Connection.QueryAsync<TModel>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            var totalRecords = parameters.Get<int>("@TotalRecords");

            var result = new Pagination<TModel>(listData.ToList(), totalRecords);
            return result;
        }
        //public void BuildQueryStringWhere(StringBuilder query, string propertyName, object value, Operator operatorType)
        //{
        //    switch (operatorType)
        //    {
        //        case Operator.EQual:
        //            query.Append($"view.{propertyName} = {value} ");
        //            break;
        //        case Operator.NotEqual:
        //            query.Append($"view.{propertyName} != {value} ");
        //            break;
        //        case Operator.Contain:
        //            query.Append($"view.{propertyName} Like '%{value}%' ");
        //            break;
        //        case Operator.NotContain:
        //            query.Append($"view.{propertyName} NotLike '%{value}%' ");
        //            break;
        //        case Operator.Smaller:
        //            query.Append($"view.{propertyName} < {value} ");
        //            break;
        //        case Operator.Greater:
        //            query.Append($"view.{propertyName} > {value} ");
        //            break;
        //        default:
        //            break;
        //    }
        //}
        /// Lấy tất cả
        /// </summary>
        /// <returns>Danh sách Đối tượng</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var storedProcedureName = $"Proc_{TableName}_GetAll";
            var result = await _uow.Connection.QueryAsync<TModel>(storedProcedureName, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return result;
        }
        /// </summary>
        /// Tìm kiếm theo Id
        /// </summary>
        /// <paran name="id">id</paran>
        /// <returns>Danh sách Đối tượng</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        public async Task<TModel?> FindByIdAsync(Guid id)
        {
            var storedProcedureName = $"Proc_{TableName}_GetById";
            var param = new DynamicParameters();
            param.Add("@Id", id);

            var result = await _uow.Connection.QueryFirstOrDefaultAsync<TModel>(storedProcedureName, param, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            return result;
        }
        /// </summary>
        /// Tìm kiếm theo chuỗi ids
        /// </summary>
        /// <paran name="id">id</paran>
        /// <returns>Danh sách Đối tượng</returns>
        /// CreatedBy: NTTrung (03/08/2023)
        public async Task<IEnumerable<TModel>> GetByIdsAsync(string ids)
        {
            var storedProcedureName = $"Proc_{TableName}_GetByIds";
            var param = new DynamicParameters();
            param.Add("@Ids", ids);

            var result = await _uow.Connection.QueryAsync<TModel>(storedProcedureName, param, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return result;
        }
        /// </summary>
        /// Tìm kiếm theo Id
        /// </summary>
        /// <paran name="id">id</paran>
        /// <returns>Danh sách Đối tượng</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        public async Task<TModel> GetByIdAsync(Guid id)
        {
            var result = await FindByIdAsync(id);
            if (result == null)
            {
                throw new NotFoundException(string.Format(ErrorMessage.NotFound, id), (int)ErrorCode.NotFoundCode);
            }
            await CustomResult(result);
            return result;
        }
        public virtual async Task<TModel> CustomResult(TModel result)
        {
            return result;
        }
        /// <summary>
        /// Phân trang tìm kiếm
        /// </summary>
        /// <returns>Số bản ghi + danh sách bản ghi</returns>
        /// CreatedBy: NTTrung (17/07/2023)
        public async Task<Pagination<TModel>> FilterAsync(int pageSize, int currentPage, string search)
        {
            var storedProcedureName = $"Proc_{TableName}_Pagination";
            var parameters = new DynamicParameters();
            parameters.Add("@Search", search);
            parameters.Add("@CurrentPage", currentPage);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@TotalRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var listData = await _uow.Connection.QueryAsync<TModel>(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            var totalRecords = parameters.Get<int>("@TotalRecords");

            var result = new Pagination<TModel>(listData.ToList(), totalRecords);
            return result;
        }
        #endregion
    }
}
