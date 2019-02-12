using Abp;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 重构Microsoft.AspNet.Identity方案
    /// 系统用户信息仓储
    /// </summary>
    /// <typeparam name="TUser"></typeparam> 
    public abstract class SysUserInfoStore<TUser> :
        IUserPasswordStore<TUser>,
        IUserClaimStore<TUser>,
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
        /// 创建用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Check.NotNull(user, nameof(user));
            //
            user.Password = new PasswordHasher<TUser>().HashPassword(user, ConstantConfig.DefaultPassword);
            await _userInfoRepository.InsertAsync(user);
            //await SaveChanges(cancellationToken);
            return IdentityResult.Success;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Check.NotNull(user, nameof(user));
            await _userInfoRepository.DeleteAsync(user);

            //await SaveChanges(cancellationToken);

            return IdentityResult.Success;
        }

        /// <summary>
        /// 查找并返回指定<paramref name="userId"/>.的用户对象
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return _userInfoRepository.FirstOrDefaultAsync(userId.To<long>());
        }

        /// <summary>
        /// 查找并返回指定<paramref name="userCode"/>.的用户对象
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<TUser> FindByNameAsync(string userCode, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Check.NotNull(userCode, nameof(userCode));
            return _userInfoRepository.FirstOrDefaultAsync(u => u.UserCode == userCode);
        }

        /// <summary>
        /// 返回用户名称
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetNormalizedUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Check.NotNull(user, nameof(user));
            return Task.FromResult(user.UserCode);
        }

        /// <summary>
        /// 返回用户id
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetUserIdAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Check.NotNull(user, nameof(user));
            return Task.FromResult(user.Id.ToString());
        }

        /// <summary>
        /// 返回用户账号名称
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetUserNameAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Check.NotNull(user, nameof(user));
            return Task.FromResult(user.UserCode);
        }

        /// <summary>
        /// 设置用户名称
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userNameCn"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SetNormalizedUserNameAsync(TUser user, string userCode, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Check.NotNull(user, nameof(user));
            user.UserCode = userCode;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 设置用户账号
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SetUserNameAsync(TUser user, string userCode, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Check.NotNull(user, nameof(user));
            user.UserCode = userCode;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Check.NotNull(user, nameof(user));

            await _userInfoRepository.UpdateAsync(user);

            //await SaveChanges(cancellationToken);

            return IdentityResult.Success;
        }
        #endregion

        #region IUserClaimStore
        public Task AddClaimsAsync([NotNull] TUser user, [NotNull] IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            Check.NotNull(user, nameof(user));
            Check.NotNull(claims, nameof(claims));

            foreach (var claim in claims)
            {
                //从数据库获取凭证信息
                //user.Claims.Add(new UserClaim(user, claim));
            }
            return Task.FromResult(claims);
        }

        public Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken)
        {
            //此处从数据库获取授权声明
            IList<Claim> list = new List<Claim>();
            return Task.FromResult(list);
        }

        public Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            //未使用 所以没有具体实现
            IList<TUser> userList = new List<TUser>();
            return Task.FromResult(userList);
        }

        public Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            //未使用 所以没有具体实现
            return Task.FromResult(true);
        }

        public Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            //未使用 所以没有具体实现
            return Task.FromResult(true);
        }

        #endregion

        #region IUserPasswordStore

        /// <summary>
        /// 设置对象密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SetPasswordHashAsync(TUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Check.NotNull(user, nameof(user));
            user.Password = passwordHash;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取对象密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetPasswordHashAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Check.NotNull(user, nameof(user));
            return Task.FromResult(user.Password);
        }

        /// <summary>
        /// 验证密码是否为空
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> HasPasswordAsync(TUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Check.NotNull(user, nameof(user));
            return Task.FromResult(user.Password != null);
        }

        #endregion

        #region 获取登录信息
        public Task<TUser> GetLoginUserAsync(string userCode)
        {
            var userInfo = _userInfoRepository.GetAllIncluding(x => x.SysRoleToUserList).FirstOrDefault(x => x.UserCode == userCode);
            return Task.FromResult(userInfo);
            //return _userInfoRepository.FirstOrDefaultAsync(u => u.UserCode == userCode);
        } 

        #endregion

        public void Dispose()
        {
            //忽略
        }





    }
}
