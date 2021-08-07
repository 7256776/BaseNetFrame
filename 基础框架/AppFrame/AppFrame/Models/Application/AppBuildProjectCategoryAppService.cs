using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using AppFrame.Core;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppFrame.Application
{
    [AbpAuthorize]
    public class AppBuildProjectCategoryAppService : NetCoreFrameApplicationBase, IAppBuildProjectCategoryAppService
    {
        private readonly IRepository<AppBuildProjectCategory, Guid> _appBuildProjectCategoryRepository;
        private readonly IRepository<AppBuildProjectCategoryConfig, Guid> _appBuildProjectCategoryConfigRepository;
        private readonly IRepository<AppSolution, Guid> _appSolutionRepository;
        private readonly IRepository<AppFile, Guid> _appFilesRepository;
        private readonly IRepository<AppCommodity, Guid> _appCommodityRepository;
        private readonly IRepository<AppFileToBusiness, Guid> _appFileToBusinessRepository;
        private readonly IRepository<AppBuildProjectCategoryToCommodity, Guid> _appBuildProjectCategoryToCommodityRepository;

        private readonly IUserInfoRepository _userInfoRepository;
        private readonly ISysDictRepository _sysDictRepository;


        public AppBuildProjectCategoryAppService(
           IRepository<AppBuildProjectCategory, Guid> appBuildProjectCategoryRepository,
           IRepository<AppBuildProjectCategoryConfig, Guid> appBuildProjectCategoryRepositoryConfig,
           IRepository<AppSolution, Guid> appSolutionRepository,
           IRepository<AppFile, Guid> appFilesRepository,
           IRepository<AppCommodity, Guid> appCommodityRepository,
           IRepository<AppFileToBusiness, Guid> appFileToBusinessRepository,
           IRepository<AppBuildProjectCategoryToCommodity, Guid> appBuildProjectCategoryToCommodityRepository,
           ISysDictRepository sysDictRepository,
           IUserInfoRepository userInfoRepository
            )
        {
            _appBuildProjectCategoryRepository = appBuildProjectCategoryRepository;
            _appBuildProjectCategoryConfigRepository = appBuildProjectCategoryRepositoryConfig;
            _appSolutionRepository = appSolutionRepository;
            _appFilesRepository = appFilesRepository;
            _appCommodityRepository = appCommodityRepository;
            _appFileToBusinessRepository = appFileToBusinessRepository;
            _appBuildProjectCategoryToCommodityRepository = appBuildProjectCategoryToCommodityRepository;
            _sysDictRepository = sysDictRepository;
            _userInfoRepository = userInfoRepository;
        }

        #region 方案

        /// <summary>
        /// 方案集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<AppSolutionGroupData>> GetSolutionList(AppSolutionData model)
        {
            var queryable = from sol in _appSolutionRepository.GetAll()
                            join brc in _appBuildProjectCategoryRepository.GetAll() on sol.Id equals brc.SolutionId
                            join file in _appFilesRepository.GetAll() on sol.FileId equals file.Id into leftTmp
                            from leftTab in leftTmp.DefaultIfEmpty()
                            join user in _userInfoRepository.GetAll() on sol.CreatorUserId equals user.Id into userTmp
                            from userTab in userTmp.DefaultIfEmpty()
                            where brc.IsActive == true
                            select new { sol, leftTab.FilePathOriginal, leftTab.FilePathPreview, leftTab.FilePathThumbnail, userTab.UserNameCn, brc };

            #region 分组表达式方式
            //group new { brc } by new
            //{
            //    sol,
            //} into groupTab
            //select new AppSolutionGroupData()
            //{
            //    AppSolutionData= groupTab.Key.sol,
            //    Amount = groupTab.Sum(s => s.brc.Amount),
            //    ActualPrice = groupTab.Sum(s => s.brc.ActualPrice),
            //    BudgetPrice = groupTab.Sum(s => s.brc.BudgetPrice),
            //};
            //        var studentSumScore_2 = lst.GroupBy(m => m.Name)
            //.Select(k => new { Name = k.Key, Scores = k.Sum(l => l.Score) })
            //.OrderBy(m => m.Scores).ToList();
            #endregion
            //分组前先进行数据条件筛选
            if (!string.IsNullOrEmpty(model.SolutionName))
            {
                queryable = queryable.Where(w => w.sol.SolutionName.Contains(model.SolutionName));
            }
            //
            var result = queryable.GroupBy(g => new { g.sol, g.FilePathOriginal, g.FilePathPreview, g.FilePathThumbnail, g.UserNameCn }).Select(s => new AppSolutionGroupData()
            {
                AppSolutionData = s.Key.sol,
                FilePathOriginal = s.Key.FilePathOriginal,
                FilePathPreview = s.Key.FilePathPreview,
                FilePathThumbnail = s.Key.FilePathThumbnail,
                UserNameCn = s.Key.UserNameCn,
                Amount = s.Sum(o => o.brc.Amount),
                ActualPrice = s.Sum(o => o.brc.ActualPrice),
                BudgetPrice = s.Sum(o => o.brc.BudgetPrice)
            });

            return Task.FromResult(result.ToList());
        }

        /// <summary>
        /// 方案对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AppSolutionData GetSolution(string id)
        {
            var queryable = from c in _appSolutionRepository.GetAll()
                            join file in _appFilesRepository.GetAll() on c.FileId equals file.Id into fileTmp
                            from fileTab in fileTmp.DefaultIfEmpty()
                            where c.Id.Equals(new Guid(id))
                            select new AppSolutionData
                            {
                                Id = c.Id,
                                SolutionName = c.SolutionName,
                                CreatorUserId = c.CreatorUserId,
                                LastModifierUserId = c.LastModifierUserId,
                                CreationTime = c.CreationTime,
                                LastModificationTime = c.LastModificationTime,
                                Description = c.Description,
                                IsActive = c.IsActive,
                                FileId = c.FileId,
                                FilePathPreview = fileTab != null ? fileTab.FilePathPreview : null,
                            };

            if (queryable.Any())
            {
                return queryable.ToList()[0];
            }
            return new AppSolutionData();
        }

        /// <summary>
        /// 新增方案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> AddSolutionAndBuildProjectCategory(AppSolutionInput model)
        {
            var isRepeat = _appSolutionRepository.FirstOrDefault(w => w.SolutionName == model.SolutionName && w.CreatorUserId == AbpSession.UserId && w.Id != model.Id);
            if (isRepeat != null && isRepeat.Id != null)
            {
                throw new UserFriendlyException("方案名称重复", "您设置的方案" + model.SolutionName + "重复!");
            }
            AppSolution modelInput = ObjectMapper.Map<AppSolution>(model);
            Guid id = await _appSolutionRepository.InsertAndGetIdAsync(modelInput);
            //初始化方案指标
            var data = _appBuildProjectCategoryConfigRepository.GetAll().Where(w => w.IsActive);

            if (!data.Any())
            {
                return id;
            }
            foreach (var item in data.ToList())
            {
                AppBuildProjectCategory appBuildProjectCategory = new AppBuildProjectCategory()
                {
                    SolutionId = id,
                    CategoryName = item.CategoryName,
                    CategoryCode = item.CategoryCode,
                    ItemName = item.ItemName,
                    ClassifyName = item.ClassifyName,
                    UnitCode = item.UnitCode,
                    Amount = 0,
                    Price = 0,
                    BudgetPrice = 0,
                    ActualPrice = 0,
                    IsActive = true,
                    Description = ""
                };
                await _appBuildProjectCategoryRepository.InsertAsync(appBuildProjectCategory);
            }
            return id;
        }

        /// <summary>
        /// 编辑方案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> EditSolution(AppSolutionInput model)
        {
            var isRepeat = _appSolutionRepository.FirstOrDefault(w => w.SolutionName == model.SolutionName && w.CreatorUserId == AbpSession.UserId && w.Id != model.Id);
            if (isRepeat != null && isRepeat.Id != null)
            {
                throw new UserFriendlyException("方案名称重复", "您设置的方案" + model.SolutionName + "重复!");
            }
            //
            var modelInput = _appSolutionRepository.Get(model.Id.Value);
            modelInput = ObjectMapper.Map(model, modelInput);
            //
            await _appSolutionRepository.UpdateAsync(modelInput);
            return modelInput.Id;
        }

        /// <summary>
        /// 删除方案以及配置指标
        /// </summary>
        /// <param name="ids"></param>
        public Task DelSolution(List<string> ids)
        {
            foreach (var id in ids)
            {
                if (string.IsNullOrEmpty(id))
                {
                    continue;
                }
                _appSolutionRepository.DeleteAsync(Guid.Parse(id));
                _appBuildProjectCategoryRepository.Delete(d => d.SolutionId == Guid.Parse(id));
            }
            return Task.CompletedTask;
        }

        #endregion

        #region 指标

        /// <summary>
        /// 指标项目明细集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<AppBuildProjectCategoryData> GetBuildProjectCategoryDetail(AppBuildProjectCategoryData model)
        {
            var queryable = from pcc in _appBuildProjectCategoryRepository.GetAll()
                            join dic in _sysDictRepository.GetAll() on pcc.UnitCode equals dic.DictCode
                            where dic.DictType.Equals("DW")
                            && pcc.SolutionId == model.SolutionId
                            && pcc.CategoryCode == model.CategoryCode
                            select new AppBuildProjectCategoryData
                            {
                                Id = pcc.Id,
                                CategoryCode = pcc.CategoryCode,
                                CategoryName = pcc.CategoryName,
                                ClassifyName = pcc.ClassifyName,
                                ItemName = pcc.ItemName,
                                UnitCode = pcc.UnitCode,
                                UnitName = dic.DictContent,
                                ActualPrice = pcc.ActualPrice,
                                Amount = pcc.Amount,
                                BudgetPrice = pcc.BudgetPrice,
                                Description = pcc.Description,
                                Price = pcc.Price,
                                SolutionId = pcc.SolutionId,
                                IsActive = pcc.IsActive
                            };
            queryable = queryable.OrderBy(o => o.CategoryName).OrderBy(o => o.ClassifyName).OrderBy(o => o.ItemName);
            return queryable.ToList();
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<AppBuildProjectCategoryData> GetBuildProjectCategory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Task.FromResult<AppBuildProjectCategoryData>(null);
            }
            var data = _appBuildProjectCategoryRepository.Get(Guid.Parse(id));
            AppBuildProjectCategoryData model = ObjectMapper.Map<AppBuildProjectCategoryData>(data);

            var queryable = from bc in _appBuildProjectCategoryToCommodityRepository.GetAll()

                            join bpc in _appBuildProjectCategoryRepository.GetAll() on bc.BuildProjectCategoryId equals bpc.Id into bcTmp
                            from bcTab in bcTmp.DefaultIfEmpty()

                            join c in _appCommodityRepository.GetAll() on bc.CommodityId equals c.Id into cTmp
                            from cTab in cTmp.DefaultIfEmpty()
                            where bcTab.Id.Equals(new Guid(id))
                            select new AppBuildProjectCategoryToCommodityData()
                            {
                                BuildProjectCategoryId = bc.BuildProjectCategoryId,
                                CommodityId = bc.CommodityId,
                                CategoryCode = bcTab.CategoryCode,
                                CategoryName = bcTab.CategoryName,
                                Commodity = cTab.Commodity,
                                Brand = cTab.Brand,
                                Price = cTab.Price,
                                UnitCode = cTab.UnitCode,
                            };

            model.AppBuildProjectCategoryToCommodityList = queryable.ToList();

            return Task.FromResult(model);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> SaveBuildProjectCategory(AppBuildProjectCategoryInput model)
        {
            var isRepeat = _appBuildProjectCategoryRepository.FirstOrDefault(w =>
            w.SolutionId == model.SolutionId &&
            w.CategoryName == model.CategoryName &&
            w.ClassifyName == model.ClassifyName &&
            w.ItemName == model.ItemName &&
            w.Id != model.Id);
            if (isRepeat != null && isRepeat.Id != null)
            {
                throw new UserFriendlyException("设置重复", "您设置的" + model.CategoryName + "-" + model.ItemName + "-" + model.ClassifyName + "重复!");
            }

            AppBuildProjectCategory modelInput = ObjectMapper.Map<AppBuildProjectCategory>(model);
            Guid id = await _appBuildProjectCategoryRepository.InsertOrUpdateAndGetIdAsync(modelInput);

            //
            _appBuildProjectCategoryToCommodityRepository.Delete(d => d.BuildProjectCategoryId == model.Id);
            if (model.commodityList != null)
            {
                foreach (var commodity in model.commodityList)
                {
                    if (string.IsNullOrEmpty(commodity))
                    {
                        continue;
                    }
                    var modelSubInput = new AppBuildProjectCategoryToCommodity();
                    modelSubInput.BuildProjectCategoryId = id;
                    modelSubInput.CommodityId = new Guid(commodity);
                    _appBuildProjectCategoryToCommodityRepository.Insert(modelSubInput);
                }
            }
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        public Task DelBuildProjectCategory(List<string> ids)
        {
            foreach (var id in ids)
            {
                if (string.IsNullOrEmpty(id))
                {
                    continue;
                }
                _appBuildProjectCategoryRepository.DeleteAsync(Guid.Parse(id));
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 设置启用状态
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<bool> SetBuildProjectCategoryActive(List<AppSolutionData> list)
        {
            foreach (var item in list)
            {
                if (item.Id == null)
                {
                    continue;
                }
                AppBuildProjectCategory data = _appBuildProjectCategoryRepository.Get(item.Id.Value);
                data.IsActive = item.IsActive.Value;
                await _appBuildProjectCategoryRepository.UpdateAsync(data);
            }
            return true;
        }
        #endregion

        #region 指标配置

        /// <summary>
        /// 指标项目明细集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<AppBuildProjectCategoryConfigData>> GetBuildProjectCategoryConfigList(AppBuildProjectCategoryConfigData model)
        {
            var queryable = from pcc in _appBuildProjectCategoryConfigRepository.GetAll()
                            join dic in _sysDictRepository.GetAll() on pcc.UnitCode equals dic.DictCode
                            where dic.DictType.Equals("DW")
                            select new AppBuildProjectCategoryConfigData
                            {
                                Id = pcc.Id,
                                CategoryCode = pcc.CategoryCode,
                                CategoryName = pcc.CategoryName,
                                ClassifyName = pcc.ClassifyName,
                                ItemName = pcc.ItemName,
                                UnitCode = pcc.UnitCode,
                                UnitName = dic.DictContent,
                                IsActive = pcc.IsActive
                            };

            if (model.IsActive != null)
            {
                queryable = queryable.Where(w => w.IsActive == model.IsActive);
            }
            if (model.CategoryCode != null)
            {
                queryable = queryable.Where(w => w.CategoryCode == model.CategoryCode);
            }
            queryable.OrderBy(o => o.CategoryName).OrderBy(o => o.ClassifyName).OrderBy(o => o.ItemName);
            List<AppBuildProjectCategoryConfigData> data = ObjectMapper.Map<List<AppBuildProjectCategoryConfigData>>(queryable);
            return Task.FromResult(data);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<AppBuildProjectCategoryConfigData> GetBuildProjectCategoryConfig(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Task.FromResult<AppBuildProjectCategoryConfigData>(null);
            }
            var data = _appBuildProjectCategoryConfigRepository.Get(Guid.Parse(id));
            AppBuildProjectCategoryConfigData model = ObjectMapper.Map<AppBuildProjectCategoryConfigData>(data);
            return Task.FromResult(model);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> SaveBuildProjectCategoryConfig(AppBuildProjectCategoryConfigInput model)
        {
            var isRepeat = _appBuildProjectCategoryConfigRepository.FirstOrDefault(w =>
            w.CategoryName == model.CategoryName &&
            w.ClassifyName == model.ClassifyName &&
            w.ItemName == model.ItemName &&
            w.Id != model.Id);
            if (isRepeat != null && isRepeat.Id != null)
            {
                throw new UserFriendlyException("设置重复", "您设置的" + model.CategoryName + "-" + model.ItemName + "-" + model.ClassifyName + "重复!");
            }

            AppBuildProjectCategoryConfig modelInput = ObjectMapper.Map<AppBuildProjectCategoryConfig>(model);
            Guid id = await _appBuildProjectCategoryConfigRepository.InsertOrUpdateAndGetIdAsync(modelInput);
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        public Task DelBuildProjectCategoryConfig(List<string> ids)
        {
            foreach (var id in ids)
            {
                if (string.IsNullOrEmpty(id))
                {
                    continue;
                }
                _appBuildProjectCategoryConfigRepository.DeleteAsync(Guid.Parse(id));
            }
            return Task.CompletedTask;
        }

        #endregion

        #region 文件上传
        /// <summary>
        /// 新增文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> AddAppFile(AppFileInput model)
        {
            AppFile modelInput = ObjectMapper.Map<AppFile>(model);
            var id = await _appFilesRepository.InsertAndGetIdAsync(modelInput);
            return id;
        }

        /// <summary>
        /// 查询文件集合
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public List<AppFile> GetFiles(string businessId)
        {
            var queryable = from f in _appFilesRepository.GetAll()
                            join fb in _appFileToBusinessRepository.GetAll() on f.Id equals fb.FileId
                            where fb.BusinessId.Equals(new Guid(businessId))
                            select f;
            return queryable.ToList();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task DelAppFile(List<string> ids)
        {
            foreach (var id in ids)
            {
                if (string.IsNullOrEmpty(id))
                {
                    continue;
                }
                _appFilesRepository.DeleteAsync(Guid.Parse(id));
            }
            return Task.CompletedTask;
        }

        #endregion

        #region 商品

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> SaveCommodity(AppCommodityInput model)
        {
            var isRepeat = _appCommodityRepository.FirstOrDefault(w => w.Commodity == model.Commodity && w.Id != model.Id);
            if (isRepeat != null && isRepeat.Id != null)
            {
                throw new UserFriendlyException("设置重复", "您设置的商品" + model.Commodity + "重复!");
            }
            AppCommodity modelInput = ObjectMapper.Map<AppCommodity>(model);
            Guid id = await _appCommodityRepository.InsertOrUpdateAndGetIdAsync(modelInput);
            //
            _appFileToBusinessRepository.Delete(w => w.BusinessId == model.Id);
            if (model.FileIds != null && model.FileIds.Any())
            {
                foreach (var item in model.FileIds)
                {
                    _appFileToBusinessRepository.Insert(new AppFileToBusiness()
                    {
                        FileId = new Guid(item),
                        BusinessId = id,
                    });
                }
            }
            return id;
        }

        /// <summary>
        /// 商品集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<AppCommodityData>> GetCommodityList(AppCommodityData model)
        {
            var queryable = from c in _appCommodityRepository.GetAll()
                            join dicUnit in _sysDictRepository.GetAll() on c.UnitCode equals dicUnit.DictCode into dicUnitTmp
                            from dicUnitTab in dicUnitTmp.DefaultIfEmpty()
                            join dicCategory in _sysDictRepository.GetAll() on c.CategoryCode equals dicCategory.DictCode into dicCategoryTmp
                            from dicCategoryTab in dicCategoryTmp.DefaultIfEmpty()
                            join file in _appFilesRepository.GetAll() on c.FileId equals file.Id into fileTmp
                            from fileTab in fileTmp.DefaultIfEmpty()
                            where dicUnitTab.DictType.Equals("DW") && dicCategoryTab.DictType.Equals("PL")
                            select new AppCommodityData
                            {
                                Id = c.Id,
                                CategoryCode = c.CategoryCode,
                                CategoryName = dicCategoryTab.DictContent,
                                UnitName = dicUnitTab.DictContent,
                                UnitCode = c.UnitCode,
                                Brand = c.Brand,
                                Commodity = c.Commodity,
                                Description = c.Description,
                                Price = c.Price,
                                CreationTime = c.CreationTime,
                                LastModificationTime = c.LastModificationTime,
                                IsActive = c.IsActive,
                                FileId = c.Id,
                                FilePathThumbnail = fileTab != null ? fileTab.FilePathThumbnail : null,
                            };

            if (model.IsActive != null)
            {
                queryable = queryable.Where(w => w.IsActive == model.IsActive);
            }
            if (model.CategoryCode != null)
            {
                queryable = queryable.Where(w => w.CategoryCode == model.CategoryCode);
            }
            queryable.OrderBy(o => o.Commodity).OrderBy(o => o.Brand);
            List<AppCommodityData> data = ObjectMapper.Map<List<AppCommodityData>>(queryable);
            return Task.FromResult(data);
        }

        /// <summary>
        /// 商品对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<AppCommodityData> GetCommodity(Guid id)
        {
            var queryable = from c in _appCommodityRepository.GetAll()
                            join dicUnit in _sysDictRepository.GetAll() on c.UnitCode equals dicUnit.DictCode into dicUnitTmp
                            from dicUnitTab in dicUnitTmp.DefaultIfEmpty()
                            join dicCategory in _sysDictRepository.GetAll() on c.CategoryCode equals dicCategory.DictCode into dicCategoryTmp
                            from dicCategoryTab in dicCategoryTmp.DefaultIfEmpty()
                            where dicUnitTab.DictType.Equals("DW") && dicCategoryTab.DictType.Equals("PL") 
                            && c.Id== id
                            select new AppCommodityData
                            {
                                Id = c.Id,
                                CategoryCode = c.CategoryCode,
                                CategoryName = dicCategoryTab.DictContent,
                                UnitName = dicUnitTab.DictContent,
                                UnitCode = c.UnitCode,
                                Brand = c.Brand,
                                Commodity = c.Commodity,
                                Description = c.Description,
                                Price = c.Price,
                                CreationTime = c.CreationTime,
                                LastModificationTime = c.LastModificationTime,
                                IsActive = c.IsActive,
                                FileId = c.FileId,
                            };

            if (!queryable.Any())
            {
                return Task.FromResult(new AppCommodityData());
            }
            var model = queryable.ToList()[0];
            var files = from c in _appCommodityRepository.GetAll()
                        join ftb in _appFileToBusinessRepository.GetAll() on c.Id equals ftb.BusinessId
                        join f in _appFilesRepository.GetAll() on ftb.FileId equals f.Id
                        where c.Id == id
                        select f;

            model.AppFiles = files.ToList();

            return Task.FromResult(model);
        }

        /// <summary>
        /// 商品删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<bool> DelCommodity(string id)
        {
            _appCommodityRepository.Delete(new Guid(id));
            _appFileToBusinessRepository.Delete(d => d.BusinessId == new Guid(id));
            return Task.FromResult(true);
        }

        #endregion



    }
}
