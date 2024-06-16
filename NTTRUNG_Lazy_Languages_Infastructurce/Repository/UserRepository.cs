using Dapper;
using NTTRUNG_Lazy_Languages_Application.Dtos.Entity.Account;
using NTTRUNG_Lazy_Languages_Domain.Interface.Repository;
using NTTRUNG_Lazy_Languages_Domain;
using NTTRUNG_Lazy_Languages_Domain.Entity;
using NTTRUNG_Lazy_Languages_Domain.Enum;
using NTTRUNG_Lazy_Languages_Domain.Interface.UnitOfWork;
using NTTRUNG_Lazy_Languages_Domain.Model;
using NTTRUNG_Lazy_Languages_Domain.Resources.ErrorMessage;
using NTTRUNG_Lazy_Languages_Infastructurce.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Infastructurce.Repository
{
    public class UserRepository : CodeRepository<User, UserModel>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<UserModel> GetUserByCodeOrEmail(string email = "", string userCode = "")
        {
            var parameters = new DynamicParameters();
            var query = new StringBuilder();
            var pram = !string.IsNullOrWhiteSpace(email) ? "@email" : "@userCode";
            var value = !string.IsNullOrWhiteSpace(email) ? email : userCode;
            var columnWhere = !string.IsNullOrWhiteSpace(email) ? "email" : $"{TableName}Code";
            parameters.Add(pram, value);
            query.Append($"Select UserName, UserCode, PassWord, Email From `{TableName}` where {columnWhere} = {pram}; ");
            var queryString = query.ToString();

            var user = await _uow.Connection.QueryFirstOrDefaultAsync<UserModel>(queryString, parameters, commandType: CommandType.Text, transaction: _uow.Transaction);
            if (user == null)
            {
                throw new NotFoundException(string.Format(ErrorMessage.NotFound, userCode), (int)ErrorCode.NotFoundCode);
            }
            return user;
        }
    }
}
