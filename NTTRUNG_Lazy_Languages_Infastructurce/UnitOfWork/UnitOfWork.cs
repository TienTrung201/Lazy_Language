using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTTRUNG_Lazy_Languages_Domain.Interface.UnitOfWork;
using MySqlConnector;

namespace NTTRUNG_Lazy_Languages_Infastructurce.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbConnection _connection;
        private DbTransaction? _transaction = null;
        public UnitOfWork(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }

        public DbConnection Connection => _connection;

        public DbTransaction? Transaction => _transaction;

        /// <summary>
        /// Mở giao dịch
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023)
        public void BeginTransaction()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _transaction = _connection.BeginTransaction();
            }
            else
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();

            }
        }
        /// <summary>
        /// Mở giao dịch
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023)
        public async Task BeginTransactionAsync()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _transaction = await _connection.BeginTransactionAsync();
            }
            else
            {
                await _connection.OpenAsync();
                _transaction = await _connection.BeginTransactionAsync();

            }
        }
        /// <summary>
        /// Xác nhận và thực thi giao dịch
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023)
        public void Commit()
        {
            _transaction?.Commit();
            Dispose();
        }
        /// <summary>
        /// Xác nhận và thực thi giao dịch
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023)
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
            await DisposeAsync();
        }
        /// <summary>
        /// Giải phóng tài nguyên, đóng kết nối kết nối và giao dịch.
        /// Được gọi khi bạn đã hoàn thành công việc với UnitOfWork.
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023)
        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;

            _connection.Close();
        }
        /// <summary>
        /// Giải phóng tài nguyên, đóng kết nối kết nối và giao dịch.
        /// Được gọi khi bạn đã hoàn thành công việc với UnitOfWork.
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023)
        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();

            }
            _transaction = null;
            await _connection.CloseAsync();
        }
        /// <summary>
        /// Hủy bỏ và quay lại trạng thái ban đầu trước khi mở giao dịch
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023)
        public void RollBack()
        {
            _transaction?.Rollback();
            Dispose();
        }
        /// <summary>
        /// Hủy bỏ và quay lại trạng thái ban đầu trước khi mở giao dịch
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023)
        public async Task RollBackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
            await DisposeAsync();
        }
    }
}
