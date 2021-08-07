using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AppFrame.Core;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppFrame.Application
{

    public interface IAppBuildProjectCategoryAppService : IApplicationService
    {

        #region 方案

        /// <summary>
        /// 方案集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<AppSolutionGroupData>> GetSolutionList(AppSolutionData model);

        /// <summary>
        /// 方案对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AppSolutionData GetSolution(string id);

        /// <summary>
        /// 新增方案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> AddSolutionAndBuildProjectCategory(AppSolutionInput model);

        /// <summary>
        /// 编辑方案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> EditSolution(AppSolutionInput model);

        /// <summary>
        /// 删除授权账号
        /// </summary>
        /// <param name="ids"></param>
        Task DelSolution(List<string> ids);

        #endregion

        #region 指标

        /// <summary>
        /// 指标项目明细集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<AppBuildProjectCategoryData> GetBuildProjectCategoryDetail(AppBuildProjectCategoryData model);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppBuildProjectCategoryData> GetBuildProjectCategory(string id);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> SaveBuildProjectCategory(AppBuildProjectCategoryInput model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        Task DelBuildProjectCategory(List<string> ids);

        /// <summary>
        /// 设置启用状态
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<bool> SetBuildProjectCategoryActive(List<AppSolutionData> list);
        #endregion

        #region 指标配置

        /// <summary>
        /// 指标项目明细集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<AppBuildProjectCategoryConfigData>> GetBuildProjectCategoryConfigList(AppBuildProjectCategoryConfigData model);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppBuildProjectCategoryConfigData> GetBuildProjectCategoryConfig(string id);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> SaveBuildProjectCategoryConfig(AppBuildProjectCategoryConfigInput model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        Task DelBuildProjectCategoryConfig(List<string> ids);

        #endregion

        #region 文件管理
        /// <summary>
        /// 新增文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> AddAppFile(AppFileInput model);

        /// <summary>
        /// 查询文件集合
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        List<AppFile> GetFiles(string businessId);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DelAppFile(List<string> ids);

        #endregion

        #region 商品
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> SaveCommodity(AppCommodityInput model);

        /// <summary>
        /// 商品集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<AppCommodityData>> GetCommodityList(AppCommodityData model);

        /// <summary>
        /// 商品对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AppCommodityData> GetCommodity(Guid id);

        /// <summary>
        /// 商品删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> DelCommodity(string id);
        #endregion

    }
}
