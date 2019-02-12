using Abp.Notifications;
using Frame.Core;
using System;
using System.Linq;

namespace Frame.Infrastructure.Migrations.SeedData
{
    public class DefaultFrameDB
    {
        private readonly DataDbContext _context;

        public DefaultFrameDB(DataDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultData();
            //测试数据
            //CreateTestData();
        }

        private void CreateDefaultData()
        {
            #region 菜单
            //创建根节点菜单
            var menuRoot = _context.SysMenuss.FirstOrDefault(r => r.MenuName == "j-sys");
            if (menuRoot == null)
            {
                menuRoot = _context.SysMenuss.Add(
                    new SysMenus
                    {
                        MenuDisplayName = "系统设置",
                        MenuName = "j-sys",
                        PermissionName = "",
                        RequiresAuthModel = "1",
                        Url = "",
                        Icon = "fa-list-ol",
                        OrderBy = 1,
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            #region 创建菜单管理

            //创建菜单管理
            var menu = _context.SysMenuss.FirstOrDefault(r => r.MenuName == "j-menus");
            if (menu == null)
            {
                menu = _context.SysMenuss.Add(
                    new SysMenus
                    {
                        ParentID = menuRoot.Id,
                        MenuDisplayName = "菜单管理",
                        MenuName = "j-menus",
                        PermissionName = "MenusManager",
                        RequiresAuthModel = "3",
                        Url = "/Views/J_Menus/Index",
                        Icon = "fa-list-ol",
                        OrderBy = 1,
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            //创建菜单管理动作权限
            var addMenuActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnAdd" && u.MenuID == menu.Id);
            if (addMenuActions == null)
            {
                addMenuActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = menu.Id,
                        ActionDisplayName = "新增",
                        ActionName = "btnAdd",
                        PermissionName = "",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var saveMenuActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnSave" && u.MenuID == menu.Id);
            if (saveMenuActions == null)
            {
                saveMenuActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = menu.Id,
                        ActionDisplayName = "保存",
                        ActionName = "btnSave",
                        PermissionName = "MenusManager.SaveMenus",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var delMenuActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnDel" && u.MenuID == menu.Id);
            if (delMenuActions == null)
            {
                delMenuActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = menu.Id,
                        ActionDisplayName = "删除",
                        ActionName = "btnDel",
                        PermissionName = "MenusManager.DelMenus",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }
            #endregion

            #region 创建用户管理
            //创建菜单管理
            var menuUser = _context.SysMenuss.FirstOrDefault(r => r.MenuName == "j-account");
            if (menuUser == null)
            {
                menuUser = _context.SysMenuss.Add(
                    new SysMenus
                    {
                        ParentID = menuRoot.Id,
                        MenuDisplayName = "用户管理",
                        MenuName = "j-account",
                        PermissionName = "UserInfoManager",
                        RequiresAuthModel = "3",
                        Url = "/Views/J_Account/index",
                        Icon = "fa-users",
                        OrderBy = 2,
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            //创建菜单管理动作权限
            var addUserMenuActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnAdd" && u.MenuID == menuUser.Id);
            if (addUserMenuActions == null)
            {
                addUserMenuActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = menuUser.Id,
                        ActionDisplayName = "新增",
                        ActionName = "btnAdd",
                        PermissionName = "UserInfoManager.SaveUser",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var editUserMenuActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnEdit" && u.MenuID == menuUser.Id);
            if (editUserMenuActions == null)
            {
                editUserMenuActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = menuUser.Id,
                        ActionDisplayName = "编辑",
                        ActionName = "btnEdit",
                        PermissionName = "UserInfoManager.SaveUser",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var delUserMenuActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnDel" && u.MenuID == menuUser.Id);
            if (delUserMenuActions == null)
            {
                delUserMenuActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = menuUser.Id,
                        ActionDisplayName = "删除",
                        ActionName = "btnDel",
                        PermissionName = "UserInfoManager.DelUser",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var resetPassMenuActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnResetPass" && u.MenuID == menuUser.Id);
            if (resetPassMenuActions == null)
            {
                resetPassMenuActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = menuUser.Id,
                        ActionDisplayName = "重置密码",
                        ActionName = "btnResetPass",
                        PermissionName = "UserInfoManager.ResetPass",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }
            #endregion

            #region 创建角色管理
            //创建菜单管理
            var menuRole = _context.SysMenuss.FirstOrDefault(r => r.MenuName == "j-roles");
            if (menuRole == null)
            {
                menuRole = _context.SysMenuss.Add(
                    new SysMenus
                    {
                        ParentID = menuRoot.Id,
                        MenuDisplayName = "角色管理",
                        MenuName = "j-roles",
                        PermissionName = "RoleManager",
                        RequiresAuthModel = "3",
                        Url = "/Views/J_Role/Index",
                        Icon = "fa-vcard",
                        OrderBy = 3,
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            //创建菜单管理动作权限
            var addRoleMenuActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnAdd" && u.MenuID == menuRole.Id);
            if (addRoleMenuActions == null)
            {
                addRoleMenuActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = menuRole.Id,
                        ActionDisplayName = "新增",
                        ActionName = "btnAdd",
                        PermissionName = "RoleManager.SaveRole",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var editRoleMenuActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnEdit" && u.MenuID == menuRole.Id);
            if (editRoleMenuActions == null)
            {
                editRoleMenuActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = menuRole.Id,
                        ActionDisplayName = "编辑",
                        ActionName = "BtnEdit",
                        PermissionName = "RoleManager.SaveRole",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var delRoleMenuActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnDel" && u.MenuID == menuRole.Id);
            if (delRoleMenuActions == null)
            {
                delRoleMenuActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = menuRole.Id,
                        ActionDisplayName = "删除",
                        ActionName = "btnDel",
                        PermissionName = "RoleManager.DelRole",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var appMenuActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnMenu" && u.MenuID == menuRole.Id);
            if (appMenuActions == null)
            {
                appMenuActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = menuRole.Id,
                        ActionDisplayName = "模块授权",
                        ActionName = "btnMenu",
                        PermissionName = "RoleManager.SaveRoleToMenu",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var appUserMenuActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnUser" && u.MenuID == menuRole.Id);
            if (appUserMenuActions == null)
            {
                appUserMenuActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = menuRole.Id,
                        ActionDisplayName = "用户授权",
                        ActionName = "btnUser",
                        PermissionName = "RoleManager.SaveRoleToUser",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }
            #endregion

            #region 创建消息通知
            //创建菜单管理
            var menuNotifications = _context.SysMenuss.FirstOrDefault(r => r.MenuName == "j-notifications");
            if (menuNotifications == null)
            {
                menuNotifications = _context.SysMenuss.Add(
                    new SysMenus
                    {
                        ParentID = menuRoot.Id,
                        MenuDisplayName = "消息通知",
                        MenuName = "j-notifications",
                        PermissionName = "NotificationsManager",
                        RequiresAuthModel = "3",
                        Url = "/Views/J_Notifications/Index",
                        Icon = "fa-bullhorn",
                        OrderBy = 5,
                        IsActive = true
                    });
                _context.SaveChanges();
            }
            #endregion

            #region 创建日志管理
            //创建菜单管理
            var menuAuditlogs = _context.SysMenuss.FirstOrDefault(r => r.MenuName == "j-auditlogs");
            if (menuAuditlogs == null)
            {
                menuNotifications = _context.SysMenuss.Add(
                    new SysMenus
                    {
                        ParentID = menuRoot.Id,
                        MenuDisplayName = "日志管理",
                        MenuName = "j-auditlogs",
                        PermissionName = "LogManager",
                        RequiresAuthModel = "3",
                        Url = "/Views/J_AuditLogs/Index",
                        Icon = "fa-book",
                        OrderBy = 7,
                        IsActive = true
                    });
                _context.SaveChanges();
            }
            #endregion

            #region 创建组织机构

            //创建菜单管理
            var org = _context.SysMenuss.FirstOrDefault(r => r.MenuName == "j-org");
            if (org == null)
            {
                org = _context.SysMenuss.Add(
                    new SysMenus
                    {
                        ParentID = menuRoot.Id,
                        MenuDisplayName = "组织机构",
                        MenuName = "j-org",
                        PermissionName = "OrgManager",
                        RequiresAuthModel = "2",
                        Url = "/Views/J_Org/Index",
                        Icon = "fa-university",
                        OrderBy = 6,
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            //创建菜单管理动作权限
            var addOrgActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnAdd" && u.MenuID == org.Id);
            if (addOrgActions == null)
            {
                addOrgActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = org.Id,
                        ActionDisplayName = "新增",
                        ActionName = "btnAdd",
                        PermissionName = "",
                        RequiresAuthModel = "2",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var saveOrgActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnSave" && u.MenuID == org.Id);
            if (saveOrgActions == null)
            {
                saveOrgActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = org.Id,
                        ActionDisplayName = "保存",
                        ActionName = "btnSave",
                        PermissionName = "OrgManager.SaveSysOrg",
                        RequiresAuthModel = "2",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var delOrgActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnDel" && u.MenuID == org.Id);
            if (delOrgActions == null)
            {
                delOrgActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = org.Id,
                        ActionDisplayName = "删除",
                        ActionName = "btnDel",
                        PermissionName = "OrgManager.DelSysOrg",
                        RequiresAuthModel = "2",
                        IsActive = true
                    });
                _context.SaveChanges();
            }
            #endregion

            #region 创建字典管理

            //创建字典管理
            var dict = _context.SysMenuss.FirstOrDefault(r => r.MenuName == "j-dict");
            if (dict == null)
            {
                dict = _context.SysMenuss.Add(
                    new SysMenus
                    {
                        ParentID = menuRoot.Id,
                        MenuDisplayName = "字典管理",
                        MenuName = "j-dict",
                        PermissionName = "DictManager",
                        RequiresAuthModel = "3",
                        Url = "/Views/J_Dict/Index",
                        Icon = "fa-bookmark",
                        OrderBy = 7,
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            //创建菜单管理动作权限
            var addTypeActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnAddType" && u.MenuID == dict.Id);
            if (addTypeActions == null)
            {
                addTypeActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = dict.Id,
                        ActionDisplayName = "新增类型",
                        ActionName = "btnAddType",
                        PermissionName = "",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var delTypeActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnDelType" && u.MenuID == dict.Id);
            if (delTypeActions == null)
            {
                delTypeActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = dict.Id,
                        ActionDisplayName = "删除类型",
                        ActionName = "btnDelType",
                        PermissionName = "DictManager.DelDict",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var saveDictActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnSave" && u.MenuID == dict.Id);
            if (saveDictActions == null)
            {
                saveDictActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = dict.Id,
                        ActionDisplayName = "保存",
                        ActionName = "btnSave",
                        PermissionName = "DictManager.SaveDict",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var addCodeActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnAddCode" && u.MenuID == dict.Id);
            if (addCodeActions == null)
            {
                addCodeActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = dict.Id,
                        ActionDisplayName = "新增字典编码",
                        ActionName = "btnAddCode",
                        PermissionName = "",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            var delCodeActions = _context.SysMenuActions.FirstOrDefault(u => u.ActionName == "btnDelCode" && u.MenuID == dict.Id);
            if (delCodeActions == null)
            {
                delCodeActions = _context.SysMenuActions.Add(
                    new SysMenuAction
                    {
                        MenuID = dict.Id,
                        ActionDisplayName = "删除字典编码",
                        ActionName = "btnDelCode",
                        PermissionName = "DictManager.DeleteDict",
                        RequiresAuthModel = "3",
                        IsActive = true
                    });
                _context.SaveChanges();
            }
            #endregion

            #endregion

            #region 角色
            //创建角色
            var adminRole = _context.SysRoless.FirstOrDefault(r => r.RoleName == "admin角色");
            if (adminRole == null)
            {
                adminRole = _context.SysRoless.Add(
                    new SysRoles
                    {
                        RoleName = "admin角色",
                        Description = "动态生成的admin角色,正式环境可以删除"
                        //IsActive = true
                    });
                _context.SaveChanges();
            }
            #endregion

            #region 账号
            //创建管理员账号
            var adminUser = _context.UserInfos.FirstOrDefault(u => u.UserCode == "admin");
            if (adminUser == null)
            {
                adminUser = UserInfo.CreateAdminUser();
                _context.UserInfos.Add(adminUser);
                _context.SaveChanges();
            }

            //创建超级管理员用户
            var sysUser = _context.UserInfos.FirstOrDefault(u => u.UserCode == "sys");
            if (sysUser == null)
            {
                sysUser = UserInfo.CreateAdminUser();
                sysUser.IsAdmin = true;
                sysUser.UserCode = "sys";
                sysUser.UserNameCn = "系统管理员";
                sysUser.ImageUrl = "w";
                _context.UserInfos.Add(sysUser);
                _context.SaveChanges();
            }
            #endregion

            #region 用户角色关系
            //创建用户角色关系
            var roleToUser = _context.SysRoleToUsers.FirstOrDefault(u => u.UserID == adminUser.Id && u.RoleID == adminRole.Id);
            if (roleToUser == null)
            {
                roleToUser = new SysRoleToUser()
                {
                    RoleID = adminRole.Id,
                    UserID = adminUser.Id
                };
                _context.SysRoleToUsers.Add(roleToUser);
                _context.SaveChanges();
            }
            #endregion

            #region 角色模块关系

            //角色模块关系
            foreach (var item in _context.SysMenuss.ToList())
            {
                var roleToMenu = _context.SysRoleToMenuActions.FirstOrDefault(u => u.IsMenu == true && u.MenuID == item.Id && u.RoleID == adminRole.Id);
                if (roleToMenu == null)
                {
                    roleToMenu = new SysRoleToMenuAction()
                    {
                        RoleID = adminRole.Id,
                        IsMenu = true,
                        MenuID = item.Id,
                        MenuActionID = null
                    };
                    _context.SysRoleToMenuActions.Add(roleToMenu);
                }
                _context.SaveChanges();
            }

            //角色与动作授权关系
            foreach (var item in _context.SysMenuActions.ToList())
            {
                var roleToMenuAction = _context.SysRoleToMenuActions.FirstOrDefault(u => u.IsMenu == false && u.MenuID == item.MenuID && u.MenuActionID == item.Id && u.RoleID == adminRole.Id);
                if (roleToMenuAction == null)
                {
                    roleToMenuAction = new SysRoleToMenuAction()
                    {
                        RoleID = adminRole.Id,
                        IsMenu = false,
                        MenuID = item.MenuID,
                        MenuActionID = item.Id
                    };
                    _context.SysRoleToMenuActions.Add(roleToMenuAction);
                    _context.SaveChanges();
                }
            }

            #endregion

            #region 设置默认通知信息
            //创建默认通知
            var notificationInfo = _context.SysNotificationInfos.FirstOrDefault(u => u.NotificationName == "system");
            if (notificationInfo == null)
            {
                _context.SysNotificationInfos.Add(
                     new SysNotificationInfo
                     {
                         Id = Guid.NewGuid(),
                         NotificationName = "system",
                         NotificationDisplayName = "系统通知",
                         NotificationDescribe = "提供系统默认提示消息",
                         NotificationType = "sms"
                     });
                _context.SaveChanges();
            }

            //设置订阅
            var adminSubscription = _context.NotificationSubscriptionInfos.FirstOrDefault(u => u.UserId == adminUser.Id);
            if (adminSubscription == null)
            {
                _context.NotificationSubscriptionInfos.Add(new NotificationSubscriptionInfo()
                {
                    Id = Guid.NewGuid(),
                    TenantId = 1,
                    UserId = adminUser.Id,
                    NotificationName = "system"
                });
                _context.SaveChanges();
            }

            //设置订阅
            var sysSubscription = _context.NotificationSubscriptionInfos.FirstOrDefault(u => u.UserId == sysUser.Id);
            if (sysSubscription == null)
            {
                _context.NotificationSubscriptionInfos.Add(new NotificationSubscriptionInfo()
                {
                    Id = Guid.NewGuid(),
                    TenantId = 1,
                    UserId = sysUser.Id,
                    NotificationName = "system"
                });
                _context.SaveChanges();
            }
            #endregion
        }


        private void CreateTestData()
        {
            string[] menuStr = new string[] { "模块页面", "菜单页面", "系统页面", "工具页面", "用户页面", "设置页面" };
            #region 菜单

            //创建菜单测试数据(主菜单)
            var menu = _context.SysMenuss.FirstOrDefault(r => r.MenuName == "菜单列表测试");
            if (menu == null)
            {
                menu = _context.SysMenuss.Add(
                    new SysMenus
                    {
                        MenuDisplayName = "菜单列表测试",
                        MenuName = "菜单列表测试",
                        PermissionName = "TestManager",
                        RequiresAuthModel = "1",
                        Url = "",
                        Icon = "fa-list-ol",
                        OrderBy = 1,
                        IsActive = true
                    });
                _context.SaveChanges();
            }

            for (int i = 0; i < 6; i++)
            {
                string menuName = menuStr[i] + "_" + i;
                //二级子菜单
                var menuSub = _context.SysMenuss.FirstOrDefault(r => r.MenuName == menuName);
                if (menuSub == null)
                {
                    menuSub = _context.SysMenuss.Add(
                        new SysMenus
                        {
                            ParentID = menu.Id,
                            MenuDisplayName = menuName,
                            MenuName = menuName,
                            PermissionName = "TestManager",
                            RequiresAuthModel = "1",
                            Url = "",
                            Icon = "fa-list-ol",
                            OrderBy = 1,
                            IsActive = true
                        });
                    _context.SaveChanges();
                }
                //三级子菜单
                for (int j = 0; j < 6; j++)
                {
                    //因为linq语法中拼接字符串偶尔会出现些CD的现象所以得先拼接好字符串在进行处理
                    string menuSubName = menuSub.MenuName + "_" + j;
                    var menuSubSub = _context.SysMenuss.FirstOrDefault(r => r.MenuName == menuSubName);
                    if (menuSubSub == null)
                    {
                        menuSubSub = _context.SysMenuss.Add(
                           new SysMenus
                           {
                               ParentID = menuSub.Id,
                               MenuDisplayName = menuSub.MenuDisplayName + "_" + j,
                               MenuName = menuSub.MenuName + "_" + j,
                               PermissionName = "TestManager",
                               RequiresAuthModel = "1",
                               Url = "",
                               Icon = "fa-list-ol",
                               OrderBy = 1,
                               IsActive = true
                           });
                        _context.SaveChanges();
                    }
                }

            }

            #endregion

        }


    }
}

