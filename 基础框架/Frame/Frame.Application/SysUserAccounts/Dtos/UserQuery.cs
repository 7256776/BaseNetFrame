﻿using Abp.AutoMapper;
using Frame.Core;
using System;

namespace Frame.Application
{
    [AutoMap(typeof(UserInfo))]
    public class UserQuery
    {
        
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>		
        public string UserNameCn { get; set; }

        /// <summary>
        /// 性别
        /// </summary>		
        public string Sex { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>		
        public string EmailAddress { get; set; }

        /// <summary>
        /// 描述
        /// </summary>		
        public string Description { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>		
        public string OrgCode { get; set; }

        /// <summary>
        /// 电话
        /// </summary>		
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 最后登录日期
        /// </summary>		
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>		
        public bool IsActive { get; set; }
        
    }
}
