/*******************************************
 * 用于更新数据库各个表的字段说明
 *******************************************/
USE DefaultDB

--审计日志表 Sys_AuditLogs
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'租户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ServiceName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'函数名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'MethodName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'Parameters'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'执行日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ExecutionTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'执行时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ExecutionDuration'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ClientIpAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ClientName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'浏览器信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'BrowserInfo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'异常信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'Exception'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ImpersonatorUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'租户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ImpersonatorTenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自定义数据' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'CustomData'
GO


--聊天消息表 Sys_ChatRecord
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'SenderUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'ReceiveUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'聊天内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'ChatDetailed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息状态 0未读 1已读' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'ChatState'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=发送 2=接收' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'SendOrReceive'
GO


--基础字典表明细表 Sys_Dict
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dict', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典类型(通常是字母编码)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dict', @level2type=N'COLUMN',@level2name=N'DictType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dict', @level2type=N'COLUMN',@level2name=N'DictContent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dict', @level2type=N'COLUMN',@level2name=N'DictCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dict', @level2type=N'COLUMN',@level2name=N'DictValue'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dict', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

--基础字典类型表 Sys_DictType
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictType', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典类型(通常是字母编码)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictType', @level2type=N'COLUMN',@level2name=N'DictType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictType', @level2type=N'COLUMN',@level2name=N'DictTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictType', @level2type=N'COLUMN',@level2name=N'IsActive'
GO


--模块动作表 Sys_MenuAction
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模块ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'MenuID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'ActionDisplayName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'ActionName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作授权名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'PermissionName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权模式:1=开放模式 2=登录模式 3=登录模式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'RequiresAuthModel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否激活' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

--模块表 Sys_Menus
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父节点ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'ParentID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'MenuDisplayName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'MenuName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'自定义数据' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'CustomData'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'PermissionName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权模式:1=开放模式 2=登录模式 3=登录模式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'RequiresAuthModel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模块地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'Url'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图标css' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'Icon'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'OrderBy'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否激活' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

--消息订阅主表 Sys_NotificationInfo
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'NotificationName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知别名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'NotificationDisplayName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'NotificationDescribe'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'NotificationType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO

--通知订阅已发送信息表 Sys_NotificationsSend
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知的名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'NotificationName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知数据(通常是JSON)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'Data'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据类型命名名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'DataTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实体通知对象名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'EntityTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实体通知对象命名名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'EntityTypeAssemblyName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实体通知对象ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'EntityId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Info = 0, Success = 1, Warn = 2, Error = 3, Fatal = 4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'Severity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接受通知的用户id数组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'UserIds'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'不用接受通知的用户id数组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'ExcludedUserIds'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接受通知租户id数组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'TenantIds'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO


--通知订阅信息发送到租户表 Sys_NotificationsToTenant
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'租户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知的名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'NotificationName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知数据(通常是JSON)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'Data'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据类型命名名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'DataTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实体通知对象名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'EntityTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实体通知对象命名名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'EntityTypeAssemblyName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实体通知对象ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'EntityId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Info = 0, Success = 1, Warn = 2, Error = 3, Fatal = 4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'Severity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO



--通知订阅信息发送到用户表 Sys_NotificationsToUser
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToUser', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'租户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToUser', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToUser', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关联Sys_NotificationsToTenant表主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToUser', @level2type=N'COLUMN',@level2name=N'TenantNotificationId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息状态 0未读 1已读' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToUser', @level2type=N'COLUMN',@level2name=N'State'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToUser', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO 

--通知订阅表 Sys_NotificationSubscriptions
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'租户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'通知的名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'NotificationName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实体通知对象名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'EntityTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实体通知对象命名名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'EntityTypeAssemblyName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实体通知对象ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'EntityId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO


--组织机构表 Sys_Org
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级节点主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'ParentOrgID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'OrgCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'OrgName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构节点编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'OrgNode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构类型 0=公司 1=部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'OrgType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用组织机构' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

--角色信息表 Sys_Roles
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'RoleName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'租户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否激活' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'LastModifierUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'LastModificationTime'
GO

--角色与模块以及动作关系表 Sys_RoleToMenuAction 
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToMenuAction', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模块ID主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToMenuAction', @level2type=N'COLUMN',@level2name=N'MenuID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动作ID主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToMenuAction', @level2type=N'COLUMN',@level2name=N'MenuActionID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToMenuAction', @level2type=N'COLUMN',@level2name=N'RoleID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色关联的对象是否属于模块 true=模块授权 false=动作授权' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToMenuAction', @level2type=N'COLUMN',@level2name=N'IsMenu'
GO

--角色与用户关系表 Sys_RoleToUser
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToUser', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToUser', @level2type=N'COLUMN',@level2name=N'UserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToUser', @level2type=N'COLUMN',@level2name=N'RoleID'
GO

--基础设置表 Sys_Settings

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'租户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设置名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设置值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'Value'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'LastModificationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'LastModifierUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO

--用户基础信息表 Sys_UserAccounts
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户账号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'UserCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'UserNameCn'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'Password'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'租户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'Sex'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属部门编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'OrgCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'EmailAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'PhoneNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'LastLoginTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否激活' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'IsAdmin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否超级管理员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'IsDeleted'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'LastModifierUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'LastModificationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除操作用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'DeleterUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除操作日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'DeletionTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户图像' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'ImageUrl'
GO










