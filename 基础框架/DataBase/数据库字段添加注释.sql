/*******************************************
 * ���ڸ������ݿ��������ֶ�˵��
 *******************************************/
USE DefaultDB

--�����־�� Sys_AuditLogs
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�⻧ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û�ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ServiceName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'MethodName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'Parameters'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ִ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ExecutionTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ִ��ʱ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ExecutionDuration'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ͻ��˵�ַ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ClientIpAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ͻ�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ClientName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������Ϣ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'BrowserInfo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�쳣��Ϣ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'Exception'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û�ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ImpersonatorUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�⻧ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'ImpersonatorTenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Զ�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_AuditLogs', @level2type=N'COLUMN',@level2name=N'CustomData'
GO


--������Ϣ�� Sys_ChatRecord
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'SenderUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'ReceiveUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'ChatDetailed'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����û�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ϣ״̬ 0δ�� 1�Ѷ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'ChatState'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=���� 2=����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_ChatRecord', @level2type=N'COLUMN',@level2name=N'SendOrReceive'
GO


--�����ֵ����ϸ�� Sys_Dict
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dict', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ֵ�����(ͨ������ĸ����)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dict', @level2type=N'COLUMN',@level2name=N'DictType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ֵ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dict', @level2type=N'COLUMN',@level2name=N'DictContent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ֵ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dict', @level2type=N'COLUMN',@level2name=N'DictCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ֵ�ֵ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dict', @level2type=N'COLUMN',@level2name=N'DictValue'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Dict', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

--�����ֵ����ͱ� Sys_DictType
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictType', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ֵ�����(ͨ������ĸ����)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictType', @level2type=N'COLUMN',@level2name=N'DictType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ֵ���������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictType', @level2type=N'COLUMN',@level2name=N'DictTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DictType', @level2type=N'COLUMN',@level2name=N'IsActive'
GO


--ģ�鶯���� Sys_MenuAction
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ģ��ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'MenuID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'ActionDisplayName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'ActionName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������Ȩ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'PermissionName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ȩģʽ:1=����ģʽ 2=��¼ģʽ 3=��¼ģʽ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'RequiresAuthModel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ񼤻�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_MenuAction', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

--ģ��� Sys_Menus
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ڵ�ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'ParentID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�˵�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'MenuDisplayName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�˵�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'MenuName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Զ�������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'CustomData'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ȩ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'PermissionName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ȩģʽ:1=����ģʽ 2=��¼ģʽ 3=��¼ģʽ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'RequiresAuthModel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ģ���ַ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'Url'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ͼ��css' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'Icon'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'OrderBy'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ񼤻�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menus', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

--��Ϣ�������� Sys_NotificationInfo
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'֪ͨ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'NotificationName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'֪ͨ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'NotificationDisplayName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'֪ͨ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'NotificationDescribe'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'֪ͨ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'NotificationType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationInfo', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO

--֪ͨ�����ѷ�����Ϣ�� Sys_NotificationsSend
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'֪ͨ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'NotificationName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'֪ͨ����(ͨ����JSON)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'Data'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'DataTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ��֪ͨ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'EntityTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ��֪ͨ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'EntityTypeAssemblyName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ��֪ͨ����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'EntityId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Info = 0, Success = 1, Warn = 2, Error = 3, Fatal = 4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'Severity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����֪ͨ���û�id����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'UserIds'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ý���֪ͨ���û�id����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'ExcludedUserIds'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����֪ͨ�⻧id����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'TenantIds'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsSend', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO


--֪ͨ������Ϣ���͵��⻧�� Sys_NotificationsToTenant
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�⻧id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'֪ͨ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'NotificationName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'֪ͨ����(ͨ����JSON)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'Data'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'DataTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ��֪ͨ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'EntityTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ��֪ͨ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'EntityTypeAssemblyName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ��֪ͨ����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'EntityId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Info = 0, Success = 1, Warn = 2, Error = 3, Fatal = 4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'Severity'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToTenant', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO



--֪ͨ������Ϣ���͵��û��� Sys_NotificationsToUser
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToUser', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�⻧ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToUser', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û�ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToUser', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����Sys_NotificationsToTenant������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToUser', @level2type=N'COLUMN',@level2name=N'TenantNotificationId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��Ϣ״̬ 0δ�� 1�Ѷ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToUser', @level2type=N'COLUMN',@level2name=N'State'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationsToUser', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO 

--֪ͨ���ı� Sys_NotificationSubscriptions
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�⻧id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û�id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'֪ͨ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'NotificationName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ��֪ͨ��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'EntityTypeName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ��֪ͨ������������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'EntityTypeAssemblyName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ʵ��֪ͨ����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'EntityId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_NotificationSubscriptions', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO


--��֯������ Sys_Org
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ϼ��ڵ�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'ParentOrgID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��֯��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'OrgCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��֯��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'OrgName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��֯�����ڵ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'OrgNode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��֯�������� 0=��˾ 1=����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'OrgType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��֯��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ�������֯����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Org', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

--��ɫ��Ϣ�� Sys_Roles
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ɫ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'RoleName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�⻧ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ񼤻�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'LastModifierUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Roles', @level2type=N'COLUMN',@level2name=N'LastModificationTime'
GO

--��ɫ��ģ���Լ�������ϵ�� Sys_RoleToMenuAction 
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToMenuAction', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ģ��ID����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToMenuAction', @level2type=N'COLUMN',@level2name=N'MenuID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToMenuAction', @level2type=N'COLUMN',@level2name=N'MenuActionID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ɫID����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToMenuAction', @level2type=N'COLUMN',@level2name=N'RoleID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ɫ�����Ķ����Ƿ�����ģ�� true=ģ����Ȩ false=������Ȩ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToMenuAction', @level2type=N'COLUMN',@level2name=N'IsMenu'
GO

--��ɫ���û���ϵ�� Sys_RoleToUser
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToUser', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û�ID����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToUser', @level2type=N'COLUMN',@level2name=N'UserID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��ɫID����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_RoleToUser', @level2type=N'COLUMN',@level2name=N'RoleID'
GO

--�������ñ� Sys_Settings

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�⻧ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û�ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ֵ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'Value'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'LastModificationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'LastModifierUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����û�ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Settings', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO

--�û�������Ϣ�� Sys_UserAccounts
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û��˺�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'UserCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'UserNameCn'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'Password'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�⻧ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'TenantId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ա�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'Sex'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�������ű���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'OrgCode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�����ַ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'EmailAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�绰' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'PhoneNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'Description'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����¼����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'LastLoginTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ񼤻�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'IsAdmin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ񳬼�����Ա' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'IsActive'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�Ƿ�ɾ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'IsDeleted'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'CreatorUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'CreationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'LastModifierUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'LastModificationTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ɾ�������û�ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'DeleterUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ɾ����������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'DeletionTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�û�ͼ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserAccounts', @level2type=N'COLUMN',@level2name=N'ImageUrl'
GO










