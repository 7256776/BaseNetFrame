using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.UI;
using Abp.Web.Models;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreFrame.Application
{
    [AbpAuthorize]
    [Audited]
    public class SysMenusAppService : NetCoreFrameApplicationBase, ISysMenusAppService
    {
        private readonly ISysMenusRepository _sysMenusRepository;
        private readonly ISysMenuActionRepository _sysMenuActionRepository;
        private readonly ICacheManagerExtens _cacheManagerExtens;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly NavigationMenusExt _navigationMenusExt;


        public SysMenusAppService(
            ISysMenusRepository sysMenusRepository,
            ISysMenuActionRepository sysMenuActionRepository,
            ICacheManagerExtens cacheManagerExtens,
            IUnitOfWorkManager unitOfWorkManager,
            NavigationMenusExt navigationMenusExt
        )
        {
            _sysMenusRepository = sysMenusRepository;
            _sysMenuActionRepository = sysMenuActionRepository;
            _cacheManagerExtens = cacheManagerExtens;
            _navigationMenusExt = navigationMenusExt;
            _unitOfWorkManager = unitOfWorkManager;
        }


        /// <summary>
        /// 查询集合(按菜单字父节点嵌套包含)
        /// 查询所有
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public List<MenusOut> GetMenusList()
        {
            List<SysMenus> dataAll= _sysMenusRepository.GetAllList();
            //转换数据集合的关系格式
            dataAll = _sysMenusRepository.ConvertMenusByChildrenList(dataAll);
            return ObjectMapper.Map<List<MenusOut>>(dataAll); 
        }

        /// <summary>
        /// 查询集合(按菜单子父节点顺序排列)
        /// 查询启用的菜单/并设置当前角色关联菜单选中状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public List<MenusOut> GetMenusListOrderBy(long? roleId = null)
        {
            //查询相关子表数据
            List<SysMenus> dataAll= _sysMenusRepository.GetAllIncluding(x => x.SysMenuActions).ToList();
            if (roleId != null)
            {
                //获取当前角色的包含的菜单
                List<SysRoleToMenuAction> roleToMenuAction = _sysMenuActionRepository.GetMenuActionByRole(Convert.ToInt64(roleId)).ToList();
                //设置菜单选中状态
                foreach (var item in dataAll)
                {
                    var data = roleToMenuAction.Where(p => p.MenuID == item.Id);
                    if (!data.Any())
                        continue;
                    item.IsCheck = true;
                    foreach (var subAction in item.SysMenuActions)
                    {
                        var action = roleToMenuAction.Where(p => p.MenuID == item.Id && p.MenuActionID == subAction.Id);
                        if (action.Any())
                            subAction.IsCheck = true;
                    }
                }
            }
            List<SysMenus> resData = _sysMenusRepository.ConvertMenusByChildrenList(dataAll);
            return ObjectMapper.Map<List<MenusOut>>(resData);
        }

        /// <summary>
        /// 查询菜单对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public MenusInput GetMenusModel(long id)
        {
            //查询模块以及所包含的授权动作
            var data = _sysMenusRepository.GetAllIncluding(x => x.SysMenuActions).FirstOrDefault(x => x.Id == id);
            return ObjectMapper.Map<MenusInput>(data); 
        }

        /// <summary>
        /// 保存菜单(新增,修改)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public  AjaxResponse SaveMenusModel(MenusInput model)
        {
            long resId;
            #region 验证
            MenuData menuData = ObjectMapper.Map<MenuData>(model);
            var isRepeat = IsPermissionRepeat(menuData);
            if (isRepeat)
            {
                throw new UserFriendlyException("菜单授权名称重复", "您设置的授权名称"+ model.PermissionName + "重复!");
            }
            #endregion
            if (model.Id == null)
            {
                #region 新增
                SysMenus modelInput = ObjectMapper.Map<SysMenus>(model);
                resId =  _sysMenusRepository.InsertAndGetId(modelInput);
                #endregion
            }
            else
            {
                #region 编辑
                long id = Convert.ToInt64(model.Id);
                //获取需要更新的数据
                SysMenus data = _sysMenusRepository.Get(id);
                //映射需要修改的数据对象
                SysMenus sysMenus = ObjectMapper.Map(model, data);
                //清空映射对象中的子表集合(子表集合单独处理,不采用EF的循环调整对象的方式)
                sysMenus.SysMenuActions.Clear();
                //修改动作明细数据
                List<SysMenuAction> menuActionList = ObjectMapper.Map<List<SysMenuAction>>(model.SysMenuActions);
                _sysMenuActionRepository.UpdataMenusAction(menuActionList, id); 
                //修改菜单主数据
                var resModel =  _sysMenusRepository.Update(sysMenus);
                resId = resModel.Id;
                #endregion
            }
            //清除模块缓存
            _cacheManagerExtens.RemoveMenuActionPermissionCache();
            //重置初始菜单以及授权
            _navigationMenusExt.UpNavigationMenusProvider(ObjectMapper.Map<SysMenus>(model));
            return new AjaxResponse { Success = true, Result = new { id = resId } };
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public void DelMenusModel(long id)
        {
            var isParent = _sysMenusRepository.GetAll().Where(w => w.ParentID == id);
            if (isParent.Any())
            {
                throw new UserFriendlyException("删除菜单失败","请先删除当前菜单的子菜单!");
            }
            //删除模块明细
            _sysMenuActionRepository.DelMenusAction(id);
            //删除模块
            _sysMenusRepository.Delete(id);
            //提交操作后在进行缓存刷新
            _unitOfWorkManager.Current.SaveChanges();
            //清除模块缓存
            _cacheManagerExtens.RemoveMenuActionPermissionCache();
            //重置初始菜单以及授权
            _navigationMenusExt.UpNavigationMenusProvider();

        }

        /// <summary>
        /// 查询授权是否重复
        /// </summary>
        /// <param name="model">
        /// 必须包含参数 授权名称PermissionName; 菜单Id
        /// </param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool IsPermissionRepeat(MenuData model)
        {
            var data = _sysMenusRepository.FirstOrDefault(w => w.PermissionName == model.PermissionName && w.Id != model.Id);
            return data != null ? true : false;
        }



    }
}
