using Abp.Auditing;
using Abp.Authorization;
using NetCoreFrame.Core;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreFrame.Application
{
    [Audited]
    public class SysDictExtension : NetCoreFrameApplicationBase, ISysDictExtension
    {
        private readonly ISysDictRepository _sysDictRepository;
        private readonly ISysDictTypeRepository _sysDictTypeRepository;

        public SysDictExtension(
            ISysDictRepository sysDictRepository,
            ISysDictTypeRepository sysDictTypeRepository
        )
        {
            _sysDictRepository = sysDictRepository;
            _sysDictTypeRepository = sysDictTypeRepository;
        }

        
        /// <summary>
        ///  通过字典类型查询有效的字典值列表
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public List<SysDict> GetDictByType(string dictType)
        {
            var listSysDict = _sysDictRepository.GetAllList().Where(p => p.DictType == dictType && p.IsActive == true).OrderBy(t => t.DictCode).ToList();
            return listSysDict;
        }

        /// <summary>
        /// 查询有效的字典类型列表
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize]
        public List<SysDictType> GetDictType()
        {
            var listSysDictType = _sysDictTypeRepository.GetAllList().Where(p => p.IsActive == true).OrderBy(t => t.DictType).ToList();
            return listSysDictType;
        }
    }
}
