using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.UI;
using Abp.Web.Models;
using Frame.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frame.Application
{
    [AbpAuthorize]
    [Audited]
    public class SysMenusAppService : FrameExtApplicationService, ISysMenusAppService
    {
        private readonly ISysMenusRepository _sysMenusRepository;
        private readonly ISysMenuActionRepository _sysMenuActionRepository;
        private readonly ICacheManagerExtens _cacheManagerExtens;

        private readonly NavigationMenusExt _navigationMenusExt;


        public SysMenusAppService(
            ISysMenusRepository sysMenusRepository,
            ISysMenuActionRepository sysMenuActionRepository,
            ICacheManagerExtens cacheManagerExtens,
             NavigationMenusExt navigationMenusExt
        )
        {
            _sysMenusRepository = sysMenusRepository;
            _sysMenuActionRepository = sysMenuActionRepository;
            _cacheManagerExtens = cacheManagerExtens;
            _navigationMenusExt = navigationMenusExt;
        }


        /// <summary>
        /// 查询集合(按菜单字父节点嵌套包含)
        /// 查询所有
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize("MenusManager")]
        public List<MenusOut> GetMenusList()
        {
            List<SysMenus> dataAll= _sysMenusRepository.GetAllList();
            //转换数据集合的关系格式
            dataAll = _sysMenusRepository.ConvertMenusByChildrenList(dataAll);
            return dataAll.MapTo<List<MenusOut>>();
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
            //获取所有菜单模块
            List<SysMenus> dataAll = _sysMenusRepository.GetMenusAll().ToList();
            if (roleId != null)
            {
                //获取当前角色的包含的菜单
                List<SysRoleToMenuAction> roleToMenuAction = _sysMenuActionRepository.GetMenuActionByRole(Convert.ToInt64(roleId)).ToList();
                //设置菜单选中状态
                foreach (var item in dataAll)
                {
                    var data = roleToMenuAction.Where(p => p.MenuID == item.Id);
                    if (data.Any())
                    {
                        item.IsCheck = true;
                        foreach (var subAction in item.SysMenuActions)
                        {
                            var action = roleToMenuAction.Where(p => p.MenuID == item.Id && p.MenuActionID == subAction.Id);
                            if (action.Any())
                            {
                                subAction.IsCheck = true;
                            }
                        }
                    }
                }
            }
            List<SysMenus> resData = _sysMenusRepository.ConvertMenusByOrderByList(dataAll);
            return resData.MapTo<List<MenusOut>>();
        }

        /// <summary>
        /// 查询菜单对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize("MenusManager")]
        public MenusInput GetMenusModel(long id)
        {
            //查询模块以及所包含的授权动作
            var data = _sysMenusRepository.Get(id);
            //或使用如下方式效果同上
            //var data = _sysMenusRepository.GetAllIncluding(x => x.SysMenuActions).FirstOrDefault(x => x.Id == id);
            return data.MapTo<MenusInput>(); 
        }

        /// <summary>
        /// 保存菜单(新增,修改)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize("MenusManager.SaveMenus")]
        public  AjaxResponse SaveMenusModel(MenusInput model)
        {
            long resId;

            var isRepeat = IsPermissionRepeat(model.PermissionName, model.ID);
            if (isRepeat)
            {
                throw new UserFriendlyException("菜单授权名称重复", "您设置的授权名称"+ model.PermissionName + "重复!");
            }

            if (model.ID == null)
            {
                SysMenus modelInput = model.MapTo<SysMenus>();
                resId =  _sysMenusRepository.InsertAndGetId(modelInput);
            }
            else
            {

                MenusUpdata modelInput = model.MapTo<MenusUpdata>();
                long id = Convert.ToInt64(model.ID);
                //获取需要更新的数据
                SysMenus data = _sysMenusRepository.Get(id);
                //映射需要修改的数据对象
                SysMenus m = ObjectMapper.Map(modelInput, data);
                //修改动作明细数据
                List<SysMenuAction> menuActionList = model.SysMenuActions.MapTo<List<SysMenuAction>>();
                if (menuActionList!=null && menuActionList.Any())
                {
                    _sysMenuActionRepository.UpdataMenusAction(menuActionList, id);
                }
                else
                {
                    //如果保存时候未发现有设置的动作列表就清除掉原有的动作
                    _sysMenuActionRepository.DelMenusAction(id);
                }
                //修改菜单主数据
                var resModel =  _sysMenusRepository.Update(m);
                resId = resModel.Id;
            }

            //清除模块缓存
            _cacheManagerExtens.RemoveMenuActionPermissionCache();

            //重置初始菜单以及授权
            _navigationMenusExt.UpNavigationMenusProvider(model.MapTo<SysMenus>());

            return new AjaxResponse { Success = true, Result = new { id = resId } };
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize("MenusManager.DelMenus")]
        public void DelMenusModel(long id)
        {
           var isParent= _sysMenusRepository.GetAll().Where(w => w.ParentID == id);
            if (isParent.Any())
            {
                throw new UserFriendlyException("删除菜单失败","请先删除当前菜单的子菜单!");
            }
            //
            _sysMenuActionRepository.DelMenusAction(id);
            //
            _sysMenusRepository.Delete(id);
            //清除模块缓存
            _cacheManagerExtens.RemoveMenuActionPermissionCache();

        }

        /// <summary>
        /// 查询授权是否重复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool IsPermissionRepeat(string permission, long? id)
        {
            var data = _sysMenusRepository.GetAll().Where(w => w.PermissionName == permission && w.Id != id);
            if (data.Any())
            {
                return true;
            }
            return false;
        }



    }
}
