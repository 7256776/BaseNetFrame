using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Web.Models;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.UI;
using Abp.Application.Services;

namespace NetCoreFrame.Application
{
    [RemoteService(true)]
    [AbpAuthorize]
    [Audited] 
    public class SysRoleAppService : NetCoreFrameApplicationBase, ISysRoleAppService
    {
        private readonly ISysMenuActionRepository _sysMenuActionRepository;
        private readonly ISysRolesRepository _sysRolesRepository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ICacheManagerExtens _cacheManagerExtens;

        public SysRoleAppService(
            ISysRolesRepository sysRolesRepository,
            ISysMenuActionRepository sysMenuActionRepository,
            IUserInfoRepository userInfoRepository,
            IUnitOfWorkManager unitOfWorkManager,
             ICacheManagerExtens cacheManagerExtens)
        {
            _sysRolesRepository = sysRolesRepository;
            _sysMenuActionRepository = sysMenuActionRepository;
            _userInfoRepository = userInfoRepository;
            _cacheManagerExtens = cacheManagerExtens;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 查询角色集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public List<SysRoleData> GetRoleList()
        {
            var data = _sysRolesRepository.GetAll();
            return ObjectMapper.Map<List<SysRoleData>>(data); 
        }

        /// <summary> 
        /// 查询角色对象       
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public SysRoleData GetRoleModel(long id)
        {
            var data = _sysRolesRepository.Get(id);
            return ObjectMapper.Map<SysRoleData>(data);
        }

        /// <summary>
        /// 获取角色授权用户以及带授权用户集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize]
        [UnitOfWork]
        public SysRoleToUserData GetRoleToUser(long roleId)
        {
            SysRoleToUserData roleToUser = new SysRoleToUserData();
            //带授权用户集合(默认是查询所有用户)
            var userAll= _userInfoRepository.GetAllList();
            roleToUser.RoleNotUser= ObjectMapper.Map<List<UserInfoData>>(userAll.ToList()); 
            //查询授权用户集合
            var inData = _sysRolesRepository.GetRoleToUSer(roleId);
            roleToUser.RoleInUser = ObjectMapper.Map<List<SysRoleUserData>>(inData.ToList()); 
            return roleToUser;
        }

        /// <summary>
        /// 保存角色(新增,修改)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task SaveRoleModel(SysRoleInput model)
        {
            var isRepeat = _sysRolesRepository.GetAllList(w => w.RoleName == model.RoleName && w.Id != model.ID).Any();
            if (isRepeat)
            {
                throw new UserFriendlyException("角色名称重复", "您设置的角色名称" + model.RoleName + "重复!");
            }
            if (model.ID == null)
            {
                SysRoles modelInput = ObjectMapper.Map<SysRoles>(model);
                await _sysRolesRepository.InsertAndGetIdAsync(modelInput);
            }
            else
            {
                long id = Convert.ToInt64(model.ID);
                //获取需要更新的数据
                SysRoles data = _sysRolesRepository.Get(id);
                //映射需要修改的数据对象
                SysRoles m = ObjectMapper.Map(model, data);
                //提交修改(实际上属于同一个工作单元执行修改可以忽略)
                await _sysRolesRepository.UpdateAsync(m);
                //修改角色信息后移除缓存
                _cacheManagerExtens.RemoveRoleToPermissionCache(id);
            }

        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public  void DelRoleModel(List<SysRoleInput> model)
        {
            foreach (var item in model)
            {
                long id = Convert.ToInt64(item.ID);
                 _sysRolesRepository.Delete(id);
                //删除该角色授权
                _sysMenuActionRepository.DelRoleToMenuAction(id);
                //删除原有授权
                _sysRolesRepository.DelRoleToUser(id);
                //修改角色信息后移除缓存
                _cacheManagerExtens.RemoveRoleToPermissionCache(id);
            } 
        }

        /// <summary>
        /// 保存角色授权模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public void SaveRoleToMenu(SysRoleInput model)
        {
            //删除原有授权
            _sysMenuActionRepository.DelRoleToMenuAction(Convert.ToInt64(model.ID));
            //新增授权 
            _sysMenuActionRepository.AddRoleToMenuAction(ObjectMapper.Map<List<SysMenus>>(model.MenusActionList), Convert.ToInt64(model.ID));
            //修改角色授权信息后移除缓存
            _cacheManagerExtens.RemoveRoleToPermissionCache(Convert.ToInt64(model.ID));
            //删除缓存所有用户信息(确保所有用户的角色重新获取)
            _cacheManagerExtens.ClearUserInfoCache();

        }

        /// <summary>
        /// 保存角色用户授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool SaveRoleToUser(SysRoleToUserInput model)
        {
            //删除原有授权
            _sysRolesRepository.DelRoleToUser(model.RoleID);
            
            if (model.RoleToUserList==null || !model.RoleToUserList.Any())
            {
                //未设置授权用户直接返回
                return  true ;
            }
            //新增授权 
            _sysRolesRepository.AddRoleToUser(ObjectMapper.Map<List<SysRoleToUser>>(model.RoleToUserList));
            //清空所设置用户的缓存
            foreach (var item in model.RoleToUserList)
            {
                _cacheManagerExtens.RemoveUserInfoCache(item.UserID);
            }
            return true;
        }
 
    }
}
