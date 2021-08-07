using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebIndividual
{

    public interface IWebFileAppService : IApplicationService
    {
        #region 

        List<WebDocPageDto> GetWebDocList();

        #endregion

        #region 


        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        List<WebDocDto> GetDocList();

        /// <summary> 
        /// 查询对象       
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        WebDocDto GetDocModel(Guid id);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        Task SaveDocModel(WebDocDto model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        void DelDocModel(List<WebDocDto> model);

        #endregion

        #region 文件管理
        /// <summary>
        /// 新增文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> AddFile(WebFileInput model);

        /// <summary>
        /// 查询文件集合
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        List<WebFile> GetFiles(string businessId);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DelFile(List<string> ids);

        #endregion


    }
}
