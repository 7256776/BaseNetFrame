using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Frame.Application;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Frame.Sample
{
    /// <summary>
    /// 用户基本信息扩展 实现接口 IUserInfoExtens 可以完成登录验证的后台业务重写
    /// </summary>
    public class UserInfoExtens : FrameExtApplicationService,IUserInfoExtens, ITransientDependency
    {
        private readonly IRepository<SysUserAccountsExtens, long> _sysUserAccountsExtens;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UserInfoExtens(
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<SysUserAccountsExtens, long> sysUserAccountsExtens)
        {
            _sysUserAccountsExtens = sysUserAccountsExtens;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 保存用户基本信息扩展
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserInfo(UserInput model)
        {
            SysUserAccountsInput modelEx = JsonConvert.DeserializeObject<SysUserAccountsInput>(model.UserInfoExtensJson);
            long id = Convert.ToInt64(model.ID);
            //获取需要更新的数据
            SysUserAccountsExtens data = _sysUserAccountsExtens.Get(id);
            //映射需要修改的数据对象
            SysUserAccountsExtens m = ObjectMapper.Map(modelEx, data);
            //提交修改
            await _sysUserAccountsExtens.UpdateAsync(m);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 获取扩展数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserOut GetUserModel(long id)
        {
            return _sysUserAccountsExtens.Get(id).MapTo<SysUserAccountsOut>();
        }


    }
}
