using Abp.Application.Services;
using Abp.Web.Models;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    //
    public interface ISysDictTypeAppService : IApplicationService
    {
        /// <summary>
        /// 获取字典类型数据
        /// </summary>
        /// <returns></returns>
        Task<List<SysDictType>> GetSysDictTypeListAsync();

        /// <summary>
        ///  查询字典类型对象
        /// </summary>
        /// <returns></returns>
        Task<SysDictType> GetSysDictTypeByIdAsync(Guid id);

        /// <summary>
        /// 保存字典类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AjaxResponse> SaveSysDictTypeModel(SysDictTypeInput model);

        /// <summary>
        /// 判断字典类型是否重复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool CheckDictCode(SysDictTypeInput model);

        /// <summary>
        /// 删除一个字典类型
        /// </summary>
        /// <param name="id"></param>
        void DelSysDictType(SysDictTypeInput model);
    }
}
