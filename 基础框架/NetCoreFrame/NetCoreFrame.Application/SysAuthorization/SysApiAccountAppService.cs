using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using NetCoreFrame.Application;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.UI;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 授权账号
    /// </summary>
    public class SysApiAccountAppService : NetCoreFrameApplicationBase, ISysApiAccountAppService
    {
        private readonly ISysApiAccountRepository _sysApiAccountRepository;
        private readonly ISysApiClienToAccountRepository _sysApiClienToAccountRepository;
        private readonly ISysIdentityServerCacheAppService _sysIdentityServerCacheAppService;
        

        public SysApiAccountAppService(
            ISysApiAccountRepository sysApiAccountRepository,
            ISysApiClienToAccountRepository sysApiClienToAccountRepository,
            ISysIdentityServerCacheAppService sysIdentityServerCacheAppService
        )
        {
            _sysApiAccountRepository = sysApiAccountRepository;
            _sysApiClienToAccountRepository = sysApiClienToAccountRepository;
            _sysIdentityServerCacheAppService = sysIdentityServerCacheAppService;
        }

        /// <summary>
        /// 获取授权账号集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<SysApiAccountData>> GetSysApiAccountList(SysApiAccountData model)
        {
            var queryable = _sysApiAccountRepository.GetAll();
            if (!string.IsNullOrEmpty(model.UserName))
            {
                queryable.Where(w => w.UserName.Contains(model.UserName));
            }
            List< SysApiAccountData> list = ObjectMapper.Map<List<SysApiAccountData>>(queryable.ToList());
            return Task.FromResult(list);
        }

        /// <summary>
        /// 获取授权账号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<SysApiAccountData> GetSysApiAccount(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Task.FromResult<SysApiAccountData>(null);
            }
            var data = _sysApiAccountRepository.Get(Guid.Parse(id));
            SysApiAccountData model = ObjectMapper.Map<SysApiAccountData>(data);
            //获取关联的客户
            var linkData = _sysApiClienToAccountRepository.GetAllList(w => w.ApiAccountId == Guid.Parse(id));
            if (linkData.Any())
            {
                model.ApiClientId = linkData[0].ApiClientId;
            }
            return Task.FromResult(model);
        }

        /// <summary>
        /// 保存授权账号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> SaveSysApiAccount(SysApiAccountInput model)
        {
            var isRepeat = _sysApiAccountRepository.GetAllList(w => w.UserName == model.UserName && w.Id != model.Id).Any();
            if (isRepeat)
            {
                throw new UserFriendlyException("授权账号重复", "您设置的授权账号" + model.UserName + "重复!");
            }
            Guid id;
            if (model.Id == null)
            {
                SysApiAccount modelInput = ObjectMapper.Map<SysApiAccount>(model);
                id = await _sysApiAccountRepository.InsertAndGetIdAsync(modelInput);
            }
            else
            {
                SysApiAccount data = _sysApiAccountRepository.Get(model.Id.Value);
                SysApiAccount m = ObjectMapper.Map(model, data);
                await _sysApiAccountRepository.UpdateAsync(m);
                id = m.Id;
            }

            _sysApiClienToAccountRepository.Delete(w => w.ApiAccountId == id);
            if (!string.IsNullOrEmpty(model.ApiClientId))
            {
                _sysApiClienToAccountRepository.Insert(new SysApiClienToAccount() { ApiAccountId = id, ApiClientId = Guid.Parse(model.ApiClientId) });
            }
            //移除缓存
            _sysIdentityServerCacheAppService.RemoveClientAndAccountCache();
            return true;
        }

        /// <summary>
        /// 删除授权账号
        /// </summary>
        /// <param name="ids"></param>
        public Task<bool> DelSysApiAccount(List<string> ids)
        {
            foreach (var id in ids)
            {
                if (string.IsNullOrEmpty(id))
                {
                    continue;
                }
                _sysApiAccountRepository.Delete(Guid.Parse(id));
                //删除该客户相关的所有资源(目前对应关系是 一个账号仅对应一个客户 因此删除仅会删除一条关系数据)
                //注: 可扩展一对多
                _sysApiClienToAccountRepository.Delete(w => w.ApiAccountId == Guid.Parse(id));
            }
            //移除缓存
            _sysIdentityServerCacheAppService.RemoveClientAndAccountCache();
            return Task.FromResult(true);
        }





    }
}
