using Abp.Application.Services;
using Frame.Core;
using System.Collections.Generic;

namespace Frame.Application
{
    public interface ISysDictExtension : IApplicationService
    {
        /// <summary>
        /// 查询有效的字典类型列表
        /// </summary>
        /// <returns></returns>
        List<SysDictType> GetDictType();

        /// <summary>
        ///  通过字典类型查询有效的字典值列表
        /// </summary>
        /// <returns></returns>
        List<SysDict> GetDictByType(string dictType);
    }
}
