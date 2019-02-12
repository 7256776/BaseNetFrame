using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 用户管理仓储
    /// </summary>
    public class UserInfoRepository : EfCoreRepositoryBase<NetCoreFrameDbContext, UserInfo, long>, IUserInfoRepository
    {

        private readonly IRepository<UserInfo, long> _repository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public UserInfoRepository(IDbContextProvider<NetCoreFrameDbContext> dbcontext, IRepository<UserInfo, long> repository) : base(dbcontext)
        {
            _repository = repository;
        }

        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfo GetUserInfo(long userId)
        {
            // userInfo = _repository.FirstOrDefault(userId);
            ////这货必须获取到才可以返回??
            //userInfo.SysRoleToUserList.ToList();
            //该方法等同于上面方法
            var userInfo = _repository.GetAllIncluding(x => x.SysRoleToUserList).FirstOrDefault(x => x.Id == userId);
            return userInfo;
        }

        /// <summary>
        /// 根据用户账号获取用户对象
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public UserInfo GetUserInfoByUserCode(string userCode)
        {
            var userInfo = _repository.GetAllIncluding(x => x.SysRoleToUserList).FirstOrDefault(x => x.UserCode == userCode);
            return userInfo;
        }

        /// <summary>
        /// 更新最后一次登录系统的日期
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task SetLastLoginTime(long id)
        {
            var user = await _repository.GetAsync(id);
            user.LastLoginTime = DateTime.Now;
            await _repository.UpdateAsync(user);
        }


    }
}
