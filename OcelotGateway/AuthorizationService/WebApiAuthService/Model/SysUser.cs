using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAuthService
{
    public class SysUser
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string UserCode { get; set; }

        /// <summary>
        /// 
        /// </summary>	 
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>	 
        public string UserPass { get; set; }


    }
}
