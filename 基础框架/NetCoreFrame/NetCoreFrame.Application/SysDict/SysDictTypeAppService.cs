using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    [Audited]
    public class SysDictTypeAppService : NetCoreFrameApplicationBase, ISysDictTypeAppService
    {
        private readonly ISysDictTypeRepository _sysDicTypetRepository;
        private readonly ISysDictAppService _sysDictAppService;

        public SysDictTypeAppService(
            ISysDictTypeRepository sysDictTypeRepository,
             ISysDictAppService sysDictAppService
        )
        {
            _sysDicTypetRepository = sysDictTypeRepository;
            _sysDictAppService = sysDictAppService;
        }

        /// <summary>
        /// 获取字典类型数据
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize] 
        public async Task<List<SysDictType>> GetSysDictTypeListAsync()
        {
            return await _sysDicTypetRepository.GetAllListAsync();
        }

        /// <summary>
        ///  查询字典类型对象
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<SysDictType> GetSysDictTypeByIdAsync(Guid id)
        {
            return await _sysDicTypetRepository.GetAsync(id);
        }

        /// <summary>
        /// 保存字典编码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public async Task<AjaxResponse> SaveSysDictTypeModel(SysDictTypeInput model)
        {
            Guid? resId;
            //验证重复
            if (CheckDictCode(model))
            {
                throw new UserFriendlyException("字典类型值重复", "您设置的字典类型值" + model.DictType + "重复!");
            }
            //新增或修改
            if (model.Id == null)
            {
                SysDictType modelInput = ObjectMapper.Map<SysDictType>(model);
                resId = await _sysDicTypetRepository.InsertAndGetIdAsync(modelInput);
            }
            else
            {
                //获取需要更新的数据
                SysDictType data = _sysDicTypetRepository.Get((Guid)model.Id);
                //映射需要修改的数据对象
                SysDictType m = ObjectMapper.Map(model, data);
                //提交修改
                await _sysDicTypetRepository.UpdateAsync(m);
                resId = model.Id;
            }
            //保存字典编码 (注释该段业务,保存字典类型的时候不保存对应的字典值明细数据,主要考虑页面操作习惯以及与明细自带的保存冲突问题)
            //if (model.SysDictInputList != null && model.SysDictInputList.Any())
            //{
            //    AjaxResponse res = await _sysDictAppService.SaveSysDictModel(model.SysDictInputList);
            //    if (!res.Success)
            //    {
            //        return res;
            //    }
            //}
            return new AjaxResponse { Success = true, Result = new { id = resId } };
        }

        /// <summary>
        /// 判断字典类型编码是否重复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public bool CheckDictCode(SysDictTypeInput model)
        {
            var data = _sysDicTypetRepository.FirstOrDefault(p => p.DictType == model.DictType && p.Id != model.Id);
            return data == null ? false : true;
        }

        /// <summary>
        /// 删除一个字典类型
        /// </summary>
        /// <param name="id"></param>
        [AbpAuthorize]
        public void DelSysDictType(SysDictTypeInput model)
        {
            if (model.Id == null) return;
            var dictType = _sysDicTypetRepository.GetAllList().Where(p => p.Id == model.Id);
            if (!dictType.Any())
            {
                throw new UserFriendlyException("删除字典类型失败", "字典类型不存在!");
            }
            //删除字典类型
            _sysDicTypetRepository.Delete(model.Id.Value);
            //删除字典编码
            _sysDictAppService.DeleteSysDictByDictType(model.DictType);
        }

    }
}
