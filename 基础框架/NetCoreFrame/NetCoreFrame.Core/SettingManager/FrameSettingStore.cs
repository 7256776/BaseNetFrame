using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// Implements <see cref="ISettingStore"/>.
    /// </summary>
    public class FrameSettingStore : ISettingStore, ITransientDependency
    {
        private readonly IRepository<SysSetting, Guid> _settingRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        public FrameSettingStore(
            IRepository<SysSetting, Guid> settingRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _settingRepository = settingRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 获取用户或租户所有的配置信息
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<List<SettingInfo>> GetAllListAsync(int? tenantId, long? userId)
        {
            //移除租户过滤  && s.TenantId == tenantId
            return
                (await _settingRepository.GetAllListAsync(s => s.UserId == userId))
                .Select(s => s.ToSettingInfo())
                .ToList();
        }

        /// <summary>
        /// 获取用户或租户所有的配置信息
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [UnitOfWork]
        public List<SettingInfo> GetAllList(int? tenantId, long? userId)
        {
            //移除租户过滤  && s.TenantId == tenantId
            return
                (_settingRepository.GetAllList(s => s.UserId == userId))
                .Select(s => s.ToSettingInfo())
                .ToList();
        }

        /// <summary>
        /// 获取用户或租户指定配置的对象
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<SettingInfo> GetSettingOrNullAsync(int? tenantId, long? userId, string name)
        {
            //移除租户过滤  && s.TenantId == tenantId
            return (await _settingRepository.FirstOrDefaultAsync(s => s.UserId == userId && s.Name == name))
            .ToSettingInfo();
        }

        /// <summary>
        /// 获取用户或租户指定配置的对象
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [UnitOfWork]
        public SettingInfo GetSettingOrNull(int? tenantId, long? userId, string name)
        {
            //移除租户过滤  && s.TenantId == tenantId
            return (_settingRepository.FirstOrDefault(s => s.UserId == userId && s.Name == name))
            .ToSettingInfo();
        }

        /// <summary>
        /// 删除用户的配置(单个配置对象)
        /// </summary>
        /// <param name="settingInfo"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task DeleteAsync(SettingInfo settingInfo)
        {
            //移除租户过滤  && s.TenantId == tenantId
            await _settingRepository.DeleteAsync(
                s => s.UserId == settingInfo.UserId && s.Name == settingInfo.Name);
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

        /// <summary>
        /// 删除用户的配置(单个配置对象)
        /// </summary>
        /// <param name="settingInfo"></param>
        /// <returns></returns>
        [UnitOfWork]
        public void Delete(SettingInfo settingInfo)
        {
            //移除租户过滤  && s.TenantId == tenantId
            _settingRepository.Delete(
               s => s.UserId == settingInfo.UserId && s.Name == settingInfo.Name);
            _unitOfWorkManager.Current.SaveChanges();
        }


        /// <summary>
        /// 新增用户的配置信息(单个配置对象)
        /// </summary>
        /// <param name="settingInfo"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task CreateAsync(SettingInfo settingInfo)
        {
            await _settingRepository.InsertAsync(settingInfo.ToSetting());
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

        /// <summary>
        /// 新增用户的配置信息(单个配置对象)
        /// </summary>
        /// <param name="settingInfo"></param>
        /// <returns></returns>
        [UnitOfWork]
        public void Create(SettingInfo settingInfo)
        {
            _settingRepository.Insert(settingInfo.ToSetting());
            _unitOfWorkManager.Current.SaveChanges();
        }
         
        /// <summary>
        /// 更新用户的配置信息(单个配置对象)
        /// </summary>
        /// <param name="settingInfo"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task UpdateAsync(SettingInfo settingInfo)
        {
            var setting = await _settingRepository.FirstOrDefaultAsync(
                s => s.TenantId == settingInfo.TenantId &&
                     s.UserId == settingInfo.UserId &&
                     s.Name == settingInfo.Name
                );
            if (setting != null)
            {
                setting.Value = settingInfo.Value;
            }
            await _unitOfWorkManager.Current.SaveChangesAsync();
        }

        /// <summary>
        /// 更新用户的配置信息(单个配置对象)
        /// </summary>
        /// <param name="settingInfo"></param>
        /// <returns></returns>
        [UnitOfWork]
        public void Update(SettingInfo settingInfo)
        {
            var setting = _settingRepository.FirstOrDefault(
              s => s.TenantId == settingInfo.TenantId &&
                   s.UserId == settingInfo.UserId &&
                   s.Name == settingInfo.Name
              );
            if (setting != null)
            {
                setting.Value = settingInfo.Value;
            }
            _unitOfWorkManager.Current.SaveChanges();
        }



    }
}
