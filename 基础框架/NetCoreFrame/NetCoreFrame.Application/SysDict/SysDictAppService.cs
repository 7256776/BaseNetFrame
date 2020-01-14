using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.UI;
using Abp.Web.Models;
using NetCoreFrame.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    [Audited]
    public class SysDictAppService : NetCoreFrameApplicationBase, ISysDictAppService
    {
        private readonly ISysDictRepository _sysDictRepository;
     

        public SysDictAppService(
            ISysDictRepository sysDictRepository
        )
        {
            _sysDictRepository = sysDictRepository;
           
        }

        /// <summary>
        /// 获取字典值数据
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize("DictManager")]
        public async Task<List<SysDict>> GetSysDictList()
        {
            return await _sysDictRepository.GetAllListAsync();
        }

        /// <summary>
        ///  查询字典编码集合
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize("DictManager")]
        public List<SysDict> GetSysDictListByDictType(string dictType)
        {
            var listSysDict = _sysDictRepository.GetAll().Where(p=>p.DictType == dictType).OrderBy(t=>t.DictCode).ToList();
            return listSysDict;
        }

        /// <summary>
        /// 保存字典编码
        /// </summary>
        /// <param name="listSysDict"></param>
        /// <returns></returns>
        [AbpAuthorize("DictManager.SaveDict")]
        public async Task<AjaxResponse> SaveSysDictModel(List<SysDictInput> listSysDict)
        {
            var validDt = from c in listSysDict group c by c.DictCode into lb where lb.Count() > 1 select new { DictCodeCount = lb.Count() };
            if (validDt.Any())
            {
                throw new UserFriendlyException("字典编码重复", "您设置的字典编码有重复!");
            }

            if (listSysDict != null && listSysDict.Any())
            {
                foreach (var item in listSysDict)
                {
                    if (item.EditState.ToUpper() != "MODIFY" && item.EditState.ToUpper() != "ADD")
                        continue;
                    if (CheckDictCode(item))
                    {
                        throw new UserFriendlyException("字典编码重复", "您设置的字典编码" + item.DictCode + "重复!");
                    }
                    if (item.EditState.ToUpper() == "ADD")
                    {
                        SysDict model = ObjectMapper.Map<SysDict>(item);
                        await _sysDictRepository.InsertAsync(model);
                    }
                    else
                    {
                        var submitData = _sysDictRepository.Get(item.Id.Value);
                        SysDict model = ObjectMapper.Map<SysDict>(submitData);
                        await _sysDictRepository.UpdateAsync(model);
                    }
                }
            }

            return new AjaxResponse { Success = true };
        }

        /// <summary>
        /// 判断字典编码是否重复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AbpAuthorize("DictManager")]
        public bool CheckDictCode(SysDictInput model)
        {
            var data = _sysDictRepository.FirstOrDefault(p =>
                p.DictType == model.DictType && p.DictCode == model.DictCode && p.Id != model.Id);
            return data != null;
        }

        /// <summary>
        /// 删除一个字典编码
        /// </summary>
        /// <param name="listSysDict"></param>
        [AbpAuthorize("DictManager.DeleteDict")]
        public async Task<bool> DelSysDict(List<SysDictInput> listSysDict)
        {
            if (!listSysDict.Any()) { return false; }
            foreach (var item in listSysDict)
            {
                if (item.Id == null)
                    continue;
                await _sysDictRepository.DeleteAsync(item.Id.Value);
            }
            return true;
        }

        /// <summary>
        /// 根据DictType删除对应的字典值
        /// </summary>
        /// <param name="dictType"></param>
        /// <returns></returns>
        [AbpAuthorize("DictManager.DelDict")]
        public AjaxResponse DeleteSysDictByDictType(string dictType)
        {
            var childrenList = _sysDictRepository.GetAllList().Where(p => p.DictType == dictType).ToList();
            if (!childrenList.Any())
            {
                return new AjaxResponse { Success = true, Result = 0 };
            }

            foreach (var item in childrenList)
            {
                _sysDictRepository.Delete(item.Id);
            }
            return new AjaxResponse() { Success = true, Result = childrenList.Count() };
        }
    }
}
