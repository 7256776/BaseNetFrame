using Abp.Auditing;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.UI;
using Abp.Web.Models;
using NetCoreFrame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    [Audited]
    public class SysOrgAppService : NetCoreFrameApplicationBase, ISysOrgAppService
    {
        private readonly ISysOrgRepository _sysOrgRepository;
        public SysOrgAppService(ISysOrgRepository sysOrgRepository)
        {
            _sysOrgRepository = sysOrgRepository;
        }

        /// <summary>
        /// 获取组织结构
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize("OrgManager")]
        public List<SysOrg> GetSysOrgList()
        {
            var dataAll = _sysOrgRepository.GetAllList();
            //转换数据集合的关系格式
            dataAll = _sysOrgRepository.ConvertSysOrgByChildrenList(dataAll);
            return dataAll;
        }

        /// <summary>
        /// 添加一个组织机构节点
        /// </summary>
        /// <param name="model"></param>
        [AbpAuthorize("OrgManager.SaveSysOrg")]
        public async Task<AjaxResponse> SaveSysOrgModel(SysOrgInput model)
        {
            Guid? resId;
            string parentOrgNode = "";

            //验证重复
            if (CheckOrgCode(ObjectMapper.Map<SysOrgData>(model)))
            {
                throw new UserFriendlyException("机构编码重复", "您设置的机构编码" + model.OrgCode + "重复!");
            }

            if (model.ParentOrgID != null)
            {
                var pData = _sysOrgRepository.Get(model.ParentOrgID.Value);
                if (pData != null)
                {
                    parentOrgNode = pData.OrgNode;
                }
            }

            if (model.Id == null)
            {
                SysOrg modelInput = ObjectMapper.Map<SysOrg>(model);
                modelInput.OrgNode = parentOrgNode + model.OrgCode + ".";
                resId = await _sysOrgRepository.InsertAndGetIdAsync(modelInput);
            }
            else
            {
                //获取需要更新的数据
                SysOrg data = _sysOrgRepository.Get((Guid) model.Id);

                //父节点或者OrgCode有变化则当前节点及其子节点的OrgNode也需要改变
                if (data.ParentOrgID != model.ParentOrgID || data.OrgCode != model.OrgCode)
                {
                    string oldOrgNode = data.OrgNode;
                    data.OrgNode = parentOrgNode + model.OrgCode + ".";
                    _sysOrgRepository.UpdateChildrensOrgNode(data.OrgNode, oldOrgNode);
                }
                //映射需要修改的数据对象
                SysOrg m = ObjectMapper.Map(model, data);
                //提交修改
                await _sysOrgRepository.UpdateAsync(m);
                resId = model.Id;
            }

            return new AjaxResponse {Success = true, Result = new {id = resId}};
        }

        /// <summary>
        /// 删除一个组织机构节点
        /// </summary>
        /// <param name="id"></param>
        [AbpAuthorize("OrgManager.DelSysOrg")]
        public void DelSysOrg(Guid id)
        {
            var childrenList = _sysOrgRepository.GetAllList().Where(p => p.ParentOrgID == id);
            if (childrenList.Any())
            {
                throw new UserFriendlyException("删除机构失败", "请先删除当前机构的子节点!");
            }
            _sysOrgRepository.Delete(id);
        }

        /// <summary>
        /// 查找某个组织机构节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AbpAuthorize("OrgManager")]
        public SysOrg GetSysOrgModel(Guid id)
        {
            return _sysOrgRepository.Get(id);
        }

        /// <summary>
        /// 判断OrgCode是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orgCode"></param>
        /// <returns>true: 已存在  false: 不存在</returns>
        [AbpAuthorize]
        public bool CheckOrgCode(SysOrgData model)
        {
            var data = _sysOrgRepository.FirstOrDefault(p => p.OrgCode == model.OrgCode && p.Id != model.Id);
            return data != null;
        }

    }
}
