using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using NetCoreFrame.Application;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebIndividual
{
    public class WebFileAppService : NetCoreFrameApplicationBase, IWebFileAppService
    {
        private readonly IRepository<WebFile, Guid> _webFilesRepository;
        private readonly IRepository<WebFileToBusiness, Guid> _webFileToBusinessRepository;
        private readonly IRepository<WebDoc, Guid> _webDocRepository;

        public WebFileAppService(
           IRepository<WebFile, Guid> webFilesRepository,
           IRepository<WebFileToBusiness, Guid> webFileToBusinessRepository,
           IRepository<WebDoc, Guid> webDocRepository
            )
        {
            _webFilesRepository = webFilesRepository;
            _webFileToBusinessRepository = webFileToBusinessRepository;
            _webDocRepository = webDocRepository;
        }

        #region 前端
        /// <summary>
        /// 查询集合
        /// </summary>
        /// <returns></returns>
        public List<WebDocPageDto> GetWebDocList()
        {
            var fileList = from web in _webDocRepository.GetAll()
                            join bu in _webFileToBusinessRepository.GetAll() on web.Id equals bu.BusinessId
                            join file in _webFilesRepository.GetAll() on bu.FileId equals file.Id
                            where web.IsActive == true
                            select new WebDocPageDto
                            {
                                Id = web.Id,
                                DocTitle = web.DocTitle,
                                DocSubhead = web.DocSubhead,
                                DocContent = web.DocContent,
                                FileId = file.Id,
                                FilePathOriginal = file.FilePathOriginal,
                                FilePathPreview = file.FilePathPreview,
                                FilePathThumbnail = file.FilePathThumbnail,
                            };

            return fileList.ToList();
        }
      
        #endregion

        #region 后端配置

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public List<WebDocDto> GetDocList()
        {
            var data = _webDocRepository.GetAll().Where(w => w.IsDeleted == false);
            return ObjectMapper.Map<List<WebDocDto>>(data);
        }

        /// <summary> 
        /// 查询对象       
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public WebDocDto GetDocModel(Guid id)
        {
            var data = _webDocRepository.Get(id);
            var model = ObjectMapper.Map<WebDocDto>(data);

            var fileToBusiness = (from web in _webDocRepository.GetAll()
                                  join bu in _webFileToBusinessRepository.GetAll() on web.Id equals bu.BusinessId
                                  join file in _webFilesRepository.GetAll() on bu.FileId equals file.Id
                                  where web.Id == id
                                  select file).FirstOrDefault();

            model.FileName = fileToBusiness == null ? "" : fileToBusiness.FileName;
            model.FileItem = fileToBusiness == null ? new string[] { } : new string[] { fileToBusiness.Id.ToString() };
            return model;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task SaveDocModel(WebDocDto model)
        {
            Guid id;
            if (model.Id == null)
            {
                WebDoc modelInput = ObjectMapper.Map<WebDoc>(model);
                id = await _webDocRepository.InsertAndGetIdAsync(modelInput);
            }
            else
            {
                id = model.Id.Value;
                //获取需要更新的数据
                WebDoc data = _webDocRepository.Get(model.Id.Value);
                //映射需要修改的数据对象
                WebDoc m = ObjectMapper.Map(model, data);
                //提交修改(实际上属于同一个工作单元执行修改可以忽略)
                await _webDocRepository.UpdateAsync(m);
            }
            if (id != null)
            {
                //处理文件信息
                _webFileToBusinessRepository.Delete(w => w.BusinessId == model.Id);
                foreach (var fileId in model.FileItem)
                {
                    _webFileToBusinessRepository.Insert(new WebFileToBusiness() { FileId = new Guid(fileId), BusinessId = id });
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public void DelDocModel(List<WebDocDto> model)
        {
            foreach (var item in model)
            {
                _webDocRepository.Delete(item.Id.Value);
            }
        }

        #endregion
         
        #region 文件上传
        /// <summary>
        /// 新增文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> AddFile(WebFileInput model)
        {
            WebFile modelInput = ObjectMapper.Map<WebFile>(model);
            var id = await _webFilesRepository.InsertAndGetIdAsync(modelInput);
            return id;
        }

        /// <summary>
        /// 查询文件集合
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public List<WebFile> GetFiles(string businessId)
        {
            var queryable = from f in _webFilesRepository.GetAll()
                            join fb in _webFileToBusinessRepository.GetAll() on f.Id equals fb.FileId
                            where fb.BusinessId.Equals(new Guid(businessId))
                            select f;
            return queryable.ToList();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task DelFile(List<string> ids)
        {
            foreach (var id in ids)
            {
                if (string.IsNullOrEmpty(id))
                {
                    continue;
                }
                _webFilesRepository.DeleteAsync(Guid.Parse(id));
            }
            return Task.CompletedTask;
        }

        #endregion
    }


}
