using Abp.EntityFrameworkCore;
using Abp.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreFrame.Core;
using System;
using System.Data.Common;
using System.Linq;

namespace NetCoreFrame.Infrastructure
{
    public class InfrastructureDbContext : NetCoreFrameDbContext
    {
        public InfrastructureDbContext(DbContextOptions<InfrastructureDbContext> options)
                : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
         
            #region 菜单

            #region 菜单id
            //#region 菜单
            int menuSysId = 1;
            int menuMenusId = 2;
            int menuUserId = 3;
            int menuRoleId = 4;
            int menuNotificationsId = 5;
            int menuAuditlogsId = 6;
            int menuOrgId = 7;
            int menuDictId = 8;
            #endregion

            #region 菜单
            modelBuilder.Entity<SysMenus>().HasData(
                //根节点菜单
                new SysMenus
                {
                    Id = menuSysId,
                    ParentID = null,
                    MenuDisplayName = "系统设置",
                    MenuName = "sys-config",
                    PermissionName = "",
                    RequiresAuthModel = "1",
                    Url = "",
                    Icon = "fa-list-ol",
                    OrderBy = 1,
                    IsActive = true
                },
                //菜单管理
                new SysMenus
                {
                    Id = menuMenusId,
                    ParentID = menuSysId,
                    MenuDisplayName = "菜单管理",
                    MenuName = "sys-menus",
                    PermissionName = "MenusManager",
                    RequiresAuthModel = "3",
                    Url = "/Views/SysMenus/Index",
                    Icon = "fa-list-ol",
                    OrderBy = 1,
                    IsActive = true
                },
                //用户管理
                new SysMenus
                {
                    Id = menuUserId,
                    ParentID = menuSysId,
                    MenuDisplayName = "用户管理",
                    MenuName = "sys-account",
                    PermissionName = "UserInfoManager",
                    RequiresAuthModel = "3",
                    Url = "/Views/SysAccount/index",
                    Icon = "fa-users",
                    OrderBy = 2,
                    IsActive = true
                },
                //角色管理
                new SysMenus
                {
                    Id = menuRoleId,
                    ParentID = menuSysId,
                    MenuDisplayName = "角色管理",
                    MenuName = "sys-roles",
                    PermissionName = "RoleManager",
                    RequiresAuthModel = "3",
                    Url = "/Views/SysRole/Index",
                    Icon = "fa-vcard",
                    OrderBy = 3,
                    IsActive = true
                },
                //消息通知
                new SysMenus
                {
                    Id = menuNotificationsId,
                    ParentID = menuSysId,
                    MenuDisplayName = "消息通知",
                    MenuName = "sys-notifications",
                    PermissionName = "NotificationsManager",
                    RequiresAuthModel = "3",
                    Url = "/Views/SysNotifications/Index",
                    Icon = "fa-bullhorn",
                    OrderBy = 5,
                    IsActive = true
                },
                //日志管理
                new SysMenus
                {
                    Id = menuAuditlogsId,
                    ParentID = menuSysId,
                    MenuDisplayName = "日志管理",
                    MenuName = "sys-auditlogs",
                    PermissionName = "LogManager",
                    RequiresAuthModel = "3",
                    Url = "/Views/SysAuditLogs/Index",
                    Icon = "fa-book",
                    OrderBy = 7,
                    IsActive = true
                },
                //组织机构
                new SysMenus
                {
                    Id = menuOrgId,
                    ParentID = menuSysId,
                    MenuDisplayName = "组织机构",
                    MenuName = "sys-org",
                    PermissionName = "OrgManager",
                    RequiresAuthModel = "2",
                    Url = "/Views/SysOrg/Index",
                    Icon = "fa-university",
                    OrderBy = 6,
                    IsActive = true
                },
                //字典管理
                new SysMenus
                {
                    Id = menuDictId,
                    ParentID = menuSysId,
                    MenuDisplayName = "字典管理",
                    MenuName = "sys-dict",
                    PermissionName = "DictManager",
                    RequiresAuthModel = "3",
                    Url = "/Views/SysDict/Index",
                    Icon = "fa-bookmark",
                    OrderBy = 7,
                    IsActive = true
                }
            );
            #endregion

            #region 动作
            modelBuilder.Entity<SysMenuAction>().HasData(
                //菜单管理动作
                new SysMenuAction
                {
                    Id = 1,
                    MenuID = menuMenusId,
                    ActionDisplayName = "新增",
                    ActionName = "btnAdd",
                    PermissionName = "",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 2,
                    MenuID = menuMenusId,
                    ActionDisplayName = "保存",
                    ActionName = "btnSave",
                    PermissionName = "MenusManager.SaveMenus",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 3,
                    MenuID = menuMenusId,
                    ActionDisplayName = "删除",
                    ActionName = "btnDel",
                    PermissionName = "MenusManager.DelMenus",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                //用户管理动作
                new SysMenuAction
                {
                    Id = 4,
                    MenuID = menuUserId,
                    ActionDisplayName = "新增",
                    ActionName = "btnAdd",
                    PermissionName = "UserInfoManager.SaveUser",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 5,
                    MenuID = menuUserId,
                    ActionDisplayName = "编辑",
                    ActionName = "btnEdit",
                    PermissionName = "UserInfoManager.SaveUser",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 6,
                    MenuID = menuUserId,
                    ActionDisplayName = "删除",
                    ActionName = "btnDel",
                    PermissionName = "UserInfoManager.DelUser",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 7,
                    MenuID = menuUserId,
                    ActionDisplayName = "重置密码",
                    ActionName = "btnResetPass",
                    PermissionName = "UserInfoManager.ResetPass",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                //角色管理动作
                new SysMenuAction
                {
                    Id = 8,
                    MenuID = menuRoleId,
                    ActionDisplayName = "新增",
                    ActionName = "btnAdd",
                    PermissionName = "RoleManager.SaveRole",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 9,
                    MenuID = menuRoleId,
                    ActionDisplayName = "编辑",
                    ActionName = "BtnEdit",
                    PermissionName = "RoleManager.SaveRole",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 10,
                    MenuID = menuRoleId,
                    ActionDisplayName = "删除",
                    ActionName = "btnDel",
                    PermissionName = "RoleManager.DelRole",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 11,
                    MenuID = menuRoleId,
                    ActionDisplayName = "模块授权",
                    ActionName = "btnMenu",
                    PermissionName = "RoleManager.SaveRoleToMenu",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 12,
                    MenuID = menuRoleId,
                    ActionDisplayName = "用户授权",
                    ActionName = "btnUser",
                    PermissionName = "RoleManager.SaveRoleToUser",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                //组织机构动作
                new SysMenuAction
                {
                    Id = 13,
                    MenuID = menuOrgId,
                    ActionDisplayName = "新增",
                    ActionName = "btnAdd",
                    PermissionName = "",
                    RequiresAuthModel = "2",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 14,
                    MenuID = menuOrgId,
                    ActionDisplayName = "保存",
                    ActionName = "btnSave",
                    PermissionName = "OrgManager.SaveSysOrg",
                    RequiresAuthModel = "2",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 15,
                    MenuID = menuOrgId,
                    ActionDisplayName = "删除",
                    ActionName = "btnDel",
                    PermissionName = "OrgManager.DelSysOrg",
                    RequiresAuthModel = "2",
                    IsActive = true
                },
                //字典管理
                new SysMenuAction
                {
                    Id = 16,
                    MenuID = menuDictId,
                    ActionDisplayName = "新增类型",
                    ActionName = "btnAddType",
                    PermissionName = "",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 17,
                    MenuID = menuDictId,
                    ActionDisplayName = "删除类型",
                    ActionName = "btnDelType",
                    PermissionName = "DictManager.DelDict",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 18,
                    MenuID = menuDictId,
                    ActionDisplayName = "保存",
                    ActionName = "btnSave",
                    PermissionName = "DictManager.SaveDict",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 19,
                    MenuID = menuDictId,
                    ActionDisplayName = "新增字典编码",
                    ActionName = "btnAddCode",
                    PermissionName = "",
                    RequiresAuthModel = "3",
                    IsActive = true
                },
                new SysMenuAction
                {
                    Id = 20,
                    MenuID = menuDictId,
                    ActionDisplayName = "删除字典编码",
                    ActionName = "btnDelCode",
                    PermissionName = "DictManager.DeleteDict",
                    RequiresAuthModel = "3",
                    IsActive = true
                }
             );
            #endregion

            #endregion

            #region 角色
            modelBuilder.Entity<SysRoles>().HasData(
                new SysRoles
                {
                    Id = 1,
                    RoleName = "admin角色",
                    Description = "动态生成的角色"
                    //IsActive = true
                }
            );

            #endregion

            #region 账号
            modelBuilder.Entity<UserInfo>().HasData(
                    UserInfo.CreateAdminUser(),
                   new UserInfo
                   {
                       Id = 2,
                       UserCode = "admin",
                       UserNameCn = "管理员",
                       ImageUrl = "2",
                       Sex = "0",
                       IsActive = true,
                       IsDeleted = false,
                       IsAdmin = true,
                       Password = new PasswordHasher<UserInfo>().HashPassword(null, ConstantConfig.DefaultPassword)
                   }
            );
            #endregion

            #region 用户,模块,角色关系
            //用户角色关系
            modelBuilder.Entity<SysRoleToUser>().HasData(new SysRoleToUser() { Id = 1, RoleID = 1, UserID = 2 });

            //模块角色关系
            modelBuilder.Entity<SysRoleToMenuAction>().HasData(
                new SysRoleToMenuAction() { Id = 1, RoleID = 1, IsMenu = true, MenuID = menuSysId, MenuActionID = null },
                new SysRoleToMenuAction() { Id = 2, RoleID = 1, IsMenu = true, MenuID = menuMenusId, MenuActionID = null },
                new SysRoleToMenuAction() { Id = 3, RoleID = 1, IsMenu = true, MenuID = menuUserId, MenuActionID = null },
                new SysRoleToMenuAction() { Id = 4, RoleID = 1, IsMenu = true, MenuID = menuRoleId, MenuActionID = null },
                new SysRoleToMenuAction() { Id = 5, RoleID = 1, IsMenu = true, MenuID = menuNotificationsId, MenuActionID = null },
                new SysRoleToMenuAction() { Id = 6, RoleID = 1, IsMenu = true, MenuID = menuAuditlogsId, MenuActionID = null },
                new SysRoleToMenuAction() { Id = 7, RoleID = 1, IsMenu = true, MenuID = menuOrgId, MenuActionID = null },
                new SysRoleToMenuAction() { Id = 8, RoleID = 1, IsMenu = true, MenuID = menuDictId, MenuActionID = null },
                //菜单管理动作                    
                new SysRoleToMenuAction() { Id = 9, RoleID = 1, IsMenu = false, MenuID = menuMenusId, MenuActionID = 1 },
                new SysRoleToMenuAction() { Id = 10, RoleID = 1, IsMenu = false, MenuID = menuMenusId, MenuActionID = 2 },
                new SysRoleToMenuAction() { Id = 11, RoleID = 1, IsMenu = false, MenuID = menuMenusId, MenuActionID = 3 },
                //用户管理动作                 
                new SysRoleToMenuAction() { Id = 12, RoleID = 1, IsMenu = false, MenuID = menuUserId, MenuActionID = 4 },
                new SysRoleToMenuAction() { Id = 13, RoleID = 1, IsMenu = false, MenuID = menuUserId, MenuActionID = 5 },
                new SysRoleToMenuAction() { Id = 14, RoleID = 1, IsMenu = false, MenuID = menuUserId, MenuActionID = 6 },
                new SysRoleToMenuAction() { Id = 15, RoleID = 1, IsMenu = false, MenuID = menuUserId, MenuActionID = 7 },
                //角色管理动作
                new SysRoleToMenuAction() { Id = 16, RoleID = 1, IsMenu = false, MenuID = menuRoleId, MenuActionID = 8 },
                new SysRoleToMenuAction() { Id = 17, RoleID = 1, IsMenu = false, MenuID = menuRoleId, MenuActionID = 9 },
                new SysRoleToMenuAction() { Id = 18, RoleID = 1, IsMenu = false, MenuID = menuRoleId, MenuActionID = 10 },
                new SysRoleToMenuAction() { Id = 19, RoleID = 1, IsMenu = false, MenuID = menuRoleId, MenuActionID = 11 },
                new SysRoleToMenuAction() { Id = 20, RoleID = 1, IsMenu = false, MenuID = menuRoleId, MenuActionID = 12 },
                //组织机构动作                                
                new SysRoleToMenuAction() { Id = 21, RoleID = 1, IsMenu = false, MenuID = menuOrgId, MenuActionID = 13 },
                new SysRoleToMenuAction() { Id = 22, RoleID = 1, IsMenu = false, MenuID = menuOrgId, MenuActionID = 14 },
                new SysRoleToMenuAction() { Id = 23, RoleID = 1, IsMenu = false, MenuID = menuOrgId, MenuActionID = 15 },
                //字典管理                                      
                new SysRoleToMenuAction() { Id = 24, RoleID = 1, IsMenu = false, MenuID = menuDictId, MenuActionID = 16 },
                new SysRoleToMenuAction() { Id = 25, RoleID = 1, IsMenu = false, MenuID = menuDictId, MenuActionID = 17 },
                new SysRoleToMenuAction() { Id = 26, RoleID = 1, IsMenu = false, MenuID = menuDictId, MenuActionID = 18 },
                new SysRoleToMenuAction() { Id = 27, RoleID = 1, IsMenu = false, MenuID = menuDictId, MenuActionID = 19 },
                new SysRoleToMenuAction() { Id = 28, RoleID = 1, IsMenu = false, MenuID = menuDictId, MenuActionID = 20 }
            );
            #endregion

            #region 设置默认通知信息
            modelBuilder.Entity<SysNotificationInfo>().HasData(
                new SysNotificationInfo
                {
                    Id = Guid.Parse("0BFB0DDD-BB12-4059-87A1-4E0294643EA4"),
                    NotificationName = "system",
                    NotificationDisplayName = "系统通知",
                    NotificationDescribe = "提供系统默认提示消息",
                    NotificationType = "sms"
                }
            );
            #endregion

            #region 设置订阅
            modelBuilder.Entity<NotificationSubscriptionInfo>().HasData(
                new NotificationSubscriptionInfo()
                {
                    Id = Guid.Parse("D9AED633-E9AE-40E6-8AD4-4EEC083B03B0"),
                    TenantId = 1,
                    UserId = 1,
                    NotificationName = "system"
                },
                new NotificationSubscriptionInfo()
                {
                    Id = Guid.Parse("15812862-B2D7-45E2-826E-25545057C334"),
                    TenantId = 1,
                    UserId = 2,
                    NotificationName = "system"
                }
            );
            #endregion

            #region 设置默认语言
            modelBuilder.Entity<SysSetting>().HasData(
                new SysSetting()
                {
                    Id = Guid.Parse("ECF6FAA6-27D6-4056-BB32-91357B639824"),
                    Name = "Abp.Localization.DefaultLanguageName",
                    Value = "zh-Hans",
                    CreationTime = DateTime.Now
                }
            );
            #endregion

        }

    }
}
