﻿using Abp.Application.Services;
using Abp.Web.Models;
using System.Collections.Generic;

namespace NetCoreFrame.Application
{
    //[RemoteService(isEnabled: false)]//该属性可以设置服务是否创建webapi
    public interface ISysMenusAppService : IApplicationService
    {
        /// <summarySaveMenusModel
        /// 查询集合(按菜单字符节点嵌套包含)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        List<SysMenusData> GetMenusList();

        /// <summary>
        /// 查询集合(按菜单子父节点顺序排列
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<SysMenusData> GetMenusListOrderBy(long? roleId);

        /// <summary>
        /// 查询菜单对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysMenusInput GetMenusModel(long id);

        /// <summary>
        /// 保存菜单(新增,修改)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long SaveMenusModel(SysMenusInput model);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        void DelMenusModel(long id);

        /// <summary>
        /// 查询授权是否重复
        /// </summary>
        /// <param name="model">必须包含参数 授权名称PermissionName; 菜单Id</param>
        /// <returns></returns>
        bool IsPermissionRepeat(SysMenuParam model);

        }
}
