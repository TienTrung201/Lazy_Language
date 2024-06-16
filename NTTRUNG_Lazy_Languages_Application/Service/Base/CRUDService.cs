using AutoMapper;
using NTTRUNG_Lazy_Languages_Application.Dtos;
using NTTRUNG_Lazy_Languages_Application.Interface.Base;
using NTTRUNG_Lazy_Languages_Domain.Entity;
using NTTRUNG_Lazy_Languages_Domain.Enum;
using NTTRUNG_Lazy_Languages_Domain.Interface.Base;
using NTTRUNG_Lazy_Languages_Domain.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTRUNG_Lazy_Languages_Application.Service.Base
{
    public abstract class CRUDService<TEntity, TModel, TDto>
        : ReadOnlyService<TEntity, TModel, TDto>,
        ICRUDService<TModel, TDto> where TDto : BaseDto where TEntity : BaseAudiEntity
    {
        #region Fields
        protected readonly ICRUDRepository<TEntity, TModel> _crudRepository;
        protected readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constructor
        protected CRUDService(ICRUDRepository<TEntity, TModel> crudRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(crudRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _crudRepository = crudRepository;
        }

        #endregion
        #region Methods
        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <paran name="id">id của bản ghi cần xóa</paran>
        /// <returns>số bản ghi thay đổi</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        public virtual async Task<int> DeleteAsync(Guid id)
        {

            var model = await _crudRepository.GetByIdAsync(id);

            var entity = _mapper.Map<TEntity>(model);

            var result = await _crudRepository.DeleteAsync(entity);
            return result;

        }
        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <paran name="ids">Chuỗi id của bản ghi cần xóa</paran>
        /// <returns>số bản ghi thay đổi</returns>
        /// CreatedBy: NTTrung (14/07/2023)
        public async Task<int> DeleteManyAsync(List<string> ids)
        {
            //try
            //{
            //    await _unitOfWork.BeginTransactionAsync();
            var result = await _crudRepository.DeleteManyAsync(ids);
            //await _unitOfWork.CommitAsync();
            return result;
            //}
            //catch (Exception)
            //{
            //    await _unitOfWork.RollBackAsync();
            //    throw;
            //}

        }
        /// <summary>
        /// Thêm sửa xóa data
        /// </summary>
        /// <paran name="DATA">Thông tin hàng hóa và list Item</paran>
        /// <returns>Bản ghi thay đổi</returns>
        /// CreatedBy: NTTrung (23/08/2023)
        public async Task<int> SaveData(List<TDto> listData)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                PreSave(listData);
                var result = 0;

                var listDelete = listData.Where(entity => entity.EditMode == EditMode.Delete).ToList();
                var listUpdate = listData.Where(entity => entity.EditMode == EditMode.Update).ToList();
                var listCreate = listData.Where(entity => entity.EditMode == EditMode.Create).ToList();

                //--------------------------------------Xóa những hàng hóa bị xóa--------------------------
                if (listDelete.Count() > 0)
                {
                    //Hàm validate xóa
                    await ValidateListDelete(listDelete);
                    var listIdsDelete = listDelete.Select(entity => entity.GetKey().ToString()).ToList();
                    //var listIdsToString = string.Join(",", listIdsDelete);
                    result += await _crudRepository.DeleteManyAsync(listIdsDelete);
                }
                //---------------------------------------Update list hàng hóa-------------------------------
                //Kiểm tra xem mã hàng hóa và mã vạch có trùng không
                if (listUpdate.Count() > 0)
                {
                    await ValidateListUpdate(listUpdate);
                    //Không có lỗi thì Update
                    var listEntityUpdate = _mapper.Map<List<TEntity>>(listUpdate);

                    result += await _crudRepository.UpdateMultipleAsync(listEntityUpdate);

                }
                //---------------------------------------Create list hàng hóa--------------------------------
                //Kiểm tra xem mã hàng hóa và mã vạch có trùng không
                if (listCreate.Count() > 0)
                {
                    await ValidateListCreate(listCreate);
                    //Không có lỗi thì thêm mới
                    var listEntityCreate = _mapper.Map<List<TEntity>>(listCreate);

                    result += await _crudRepository.InsertMultipleAsync(listEntityCreate);

                }

                result += await AfterSave(listData);
                await AfterSaveSuccess(listData);
                await _unitOfWork.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackAsync();
                throw;
            }
        }
        
        /// <summary>
        /// Trước khi lưu
        /// </summary>
        /// <param name="data">Bản ghi được gửi đến</param>
        /// CreatedBy: NTTrung (27/08/2023)
        public virtual void PreSave(List<TDto> listData) 
        {
            foreach (var item in listData)
            {
                
                if(item.EditMode == EditMode.Create)
                {
                    item.SetValue($"{TableName}ID", Guid.NewGuid());
                    item.SetValue($"CreatedDate", DateTimeOffset.Now); 
                    item.SetValue($"ModifiedDate", DateTimeOffset.Now);
                    item.SetValue($"CreatedBy", "Tientrung");
                    item.SetValue($"ModifiedBy", "Tientrung");
                }
                else if (item.EditMode == EditMode.Update)
                {
                    item.SetValue($"ModifiedBy", "Tientrung");
                    item.SetValue($"ModifiedDate", DateTimeOffset.Now);
                }
            }
        }
        /// <summary>
        /// Sau khi lưu
        /// </summary>
        /// <param name="data">Bản ghi được gửi đến</param>
        /// CreatedBy: NTTrung (27/08/2023)
        public virtual async Task<int> AfterSave(List<TDto> listData)
        {
            return 0;
        }
        /// <summary>
        /// Sau khi lưu thành công
        /// </summary>
        /// <param name="data">Bản ghi được gửi đến</param>
        /// CreatedBy: NTTrung (27/08/2023)
        public virtual async Task AfterSaveSuccess(List<TDto> listData) { }
        /// <summary>
        /// Validate trước khi xóa list
        /// </summary>
        /// <param name="data">Bản ghi được gửi đến</param>
        /// CreatedBy: NTTrung (27/08/2023)
        public virtual async Task ValidateListDelete(List<TDto> data) { }
        /// <summary>
        /// Validate trước khi sửa list
        /// </summary>
        /// <param name="data">Bản ghi được gửi đến</param>
        /// CreatedBy: NTTrung (27/08/2023)
        public virtual async Task ValidateListUpdate(List<TDto> data) { }
        /// <summary>
        /// Validate trước khi thêm list
        /// </summary>
        /// <param name="data">Bản ghi được gửi đến</param>
        /// CreatedBy: NTTrung (27/08/2023)
        public virtual async Task ValidateListCreate(List<TDto> data) { }

        #endregion
    }
}
