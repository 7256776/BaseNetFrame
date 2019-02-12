using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.Timing;
using Microsoft.Extensions.Configuration;
using NetCoreFrame.Core;
using Microsoft.Extensions.Options;

namespace NetCoreFrame.Web
{
    public class CustomWebResource
    {
        /// <summary>
        /// �Զ���css��ʽ�ļ��б�
        /// </summary>
        public List<string> style { get; set; } = new List<string>();

        /// <summary>
        /// �Զ���js�ļ��б�
        /// </summary>
        public List<string> ScriptBottom { get; set; } = new List<string>();

        /// <summary>
        /// �Զ���js�ļ��б�
        /// </summary>
        public List<string> ScriptTop { get; set; } = new List<string>();
    }

  
}
