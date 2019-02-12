using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Frame.Core
{
    /// <summary>
    /// 重构Microsoft.AspNet.Identity方案
    /// 系统用户信息仓储
    /// </summary>
    /// <typeparam name="TUser"></typeparam> 
    public abstract class SysUserInfoStore<TUser> : 
        IUserStore<TUser, long>,
        IUserPasswordStore<TUser, long>,
        ITransientDependency
         where TUser : SysUserInfo<TUser>
    {
        /// <summary>
        /// 用户对象仓储
        /// </summary>
        private readonly IRepository<TUser, long> _userInfoRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userInfoRepository"></param>
        protected SysUserInfoStore(IRepository<TUser, long> userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

        #region IUserStore

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task CreateAsync(TUser user)
        {
            //设置初始密码
            await SetPasswordHashAsync(user, ConstantConfig.DefaultPassword);
            user.Id = await _userInfoRepository.InsertAndGetIdAsync(user);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns> 
        public async Task DeleteAsync(TUser user)
        {
             await _userInfoRepository.DeleteAsync(user.Id);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns> 
        public async Task UpdateAsync(TUser user)
        {
            await _userInfoRepository.UpdateAsync(user);
        }

        /// <summary>
        /// 根据用户ID获取用户对象
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns> 
        public async Task<TUser> FindByIdAsync(long userId)
        {
            return await _userInfoRepository.FirstOrDefaultAsync(userId);
        }

        /// <summary>
        /// 根据UserCode(用户账号)获取用户对象
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns> 
        public Task<TUser> FindByNameAsync(string userCode)
        {
            var userInfo = _userInfoRepository.GetAllIncluding(x => x.SysRoleToUserList).FirstOrDefault(x => x.UserCode == userCode);
            return Task.FromResult(userInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            //因为使用了IOC,所以就不需要啦
        }

        #endregion


        #region IUserPasswordStore

        /// <summary>
        /// 设置密码加密
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        [UnitOfWork]
        public  Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.Password = new PasswordHasher().HashPassword(passwordHash);
            return Task.FromResult(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [UnitOfWork]
        public  Task<string> GetPasswordHashAsync(TUser user)
        {
            return Task.FromResult(user.Password);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [UnitOfWork]
        public  Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }

        #endregion


    }
}
