using Abp.Application.Services;
using Abp.Web.Models;
using NetCoreFrame.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    //
    public interface ISysDictAppService : IApplicationService
    {
        /// <summary>
        /// 获取字典主表数据
        /// </summary>
        /// <returns></returns>
        Task<List<SysDict>> GetSysDictList();

        /// <summary>
        ///  查询字典类型对象
        /// </summary>
        /// <returns></returns>
        List<SysDict> GetSysDictListByDictType(string dictType);

        /// <summary>
        /// 保存字典编码
        /// </summary>
        /// <param name="listSysDict"></param>
        /// <returns></returns>
        Task SaveSysDictModel(List<SysDictInput> listSysDict);

        /// <summary>
        /// 判断字典编码是否重复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool CheckDictCode(SysDictInput model);

        /// <summary>
        /// 删除一个字典编码
        /// </summary>
        /// <param name="listSysDict"></param>
        Task<bool> DelSysDict(List<SysDictInput> listSysDict);

        /// <summary>
        /// 根据DictType删除对应的字典编码值
        /// </summary>
        /// <param name="dictType"></param>
        /// <returns></returns>
        AjaxResponse DeleteSysDictByDictType(string dictType);
    }
}
