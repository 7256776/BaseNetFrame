using System.Security.Claims;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// ��¼����ֵ
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class SysLoginResult<TUser>
        where TUser : SysUserAccounts
    {
        /// <summary>
        /// ��¼��֤״̬
        /// </summary>
        public LoginResultType Result { get; private set; }

        /// <summary>
        /// ��¼�û�����
        /// </summary>
        public TUser User { get; private set; }

        /// <summary>
        /// ��¼�û���Ȩƾ֤
        /// </summary>
        public ClaimsIdentity Identity { get; private set; }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string Message { get;  set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="user"></param>
        public SysLoginResult(LoginResultType result, TUser user = null)
        {
            Result = result;
            User = user;
        }

        /// <summary>
        /// Ĭ�ϵ�¼��Ȩͨ��(����)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="identity"></param>
        public SysLoginResult(TUser user, ClaimsIdentity identity)
            : this(LoginResultType.Success)
        {
            User = user;
            Identity = identity;
        }
    }


    /// <summary>
    /// ��¼��֤״̬ö��
    /// </summary>
    public enum LoginResultType : byte
    {
        /// <summary>
        /// �ɹ�
        /// </summary>
        Success = 1,
         
        /// <summary>
        /// �˺Ż������ַ��Ч
        /// </summary>
        InvalidUserNameOrEmailAddress = 2,

        /// <summary>
        /// ������Ч
        /// </summary>
        InvalidPassword = 3,

        /// <summary>
        /// �û�δ����
        /// </summary>
        UserIsNotActive = 4,

        /// <summary>
        /// �⻧��Ч
        /// </summary>
        InvalidTenancyName = 5,

        /// <summary>
        /// �⻧δ����
        /// </summary>
        TenantIsNotActive = 6,

        /// <summary>
        /// ������֤δͨ��
        /// </summary>
        UserEmailIsNotConfirmed = 7,

        /// <summary>
        /// δ��Ȩ�ⲿ��¼
        /// </summary>
        UnknownExternalLogin = 8,

        /// <summary>
        /// ������
        /// </summary>
        LockedOut = 9,

        /// <summary>
        /// �绰����δ��֤
        /// </summary>
        UserPhoneNumberIsNotConfirmed = 10,

        /// <summary>
        /// �����֤ע��ʧ��
        /// </summary>
        AuthenticationRegistrationFailure = 11,

        /// <summary>
        /// δ��ȡ�κ���Ϣ
        /// </summary>
        UnacquiredInformation = 12,

        /// <summary>
        /// �˺���Ч
        /// </summary>
        InvalidUserName = 13
    }


}