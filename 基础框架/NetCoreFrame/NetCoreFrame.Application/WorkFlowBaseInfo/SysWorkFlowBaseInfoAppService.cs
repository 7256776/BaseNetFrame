using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Uow;
using NetCoreFrame.Core;
using NetCoreWorkFlow.Application;
using NetCoreWorkFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.Application
{
    public class SysWorkFlowBaseInfoAppService : NetCoreWorkFlowApplicationBase, ISysWorkFlowBaseInfoAppService
    {
        private readonly ISysOrgRepository _sysOrgRepository;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly ISysMenusRepository _sysMenusRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysWorkFlowBaseInfoAppService(
            ISysOrgRepository sysOrgRepository,
            IUserInfoRepository userInfoRepository,
            ISysMenusRepository sysMenusRepository
            )
        {
            _sysOrgRepository = sysOrgRepository;
            _userInfoRepository = userInfoRepository;
            _sysMenusRepository = sysMenusRepository;
        }

        /// <summary>
        /// 查询所有组织机构
        /// </summary>
        /// <returns></returns>
        public List<SysFlowOrgData> GetFlowOrgAll()
        {
            var dataAll = _sysOrgRepository.GetAll().Where(w => w.IsActive == true).Select(s => new SysFlowOrgData()
            {
                OrgId = s.Id.ToString(),
                ParentOrgID = s.ParentOrgID.ToString(),
                OrgCode = s.OrgCode,
                OrgName = s.OrgName,
                OrgType = s.OrgType,
                OrderBy = s.OrderBy
            });
            var data = ConvertMenusList(dataAll.ToList());
            return data;
        }

        /// <summary>
        /// 查询所有业务流程
        /// </summary>
        /// <returns></returns>
        public List<SysFlowBusinessModule> GetFlowBusinessModuleAll()
        {
            var dataAll = _sysMenusRepository.GetAll().Where(w => w.IsActive == true).Select(s => new SysFlowBusinessModule()
            {
                MenuId = s.Id.ToString(),
                ParentID = s.ParentID.ToString(),
                MenuName = s.MenuName,
                MenuDisplayName = s.MenuDisplayName,
                OrderBy = s.OrderBy,
                Icon = s.Icon
            });
            return dataAll.ToList();
        }

        /// <summary>
        /// 分页查询账号信息,并可根据参数进行筛选
        /// </summary>
        /// <param name="flowPagingDto"></param>
        /// <returns></returns>
        public FlowPagingResult<SysFlowAccounts> GetFlowAccountsPaging(FlowPagingParam<SysFlowAccountsSearch> flowPagingDto)
        {
            //参数对象
            var model = flowPagingDto.Params;
            //基础查询
            var queue = _userInfoRepository.GetAll();
            #region 筛选条件
            //用户账号或名称 条件筛选
            if (model != null && !string.IsNullOrEmpty(model.UserCodeOrName))
            {
                queue = queue.Where(w => (w.UserCode.Contains(model.UserCodeOrName) || w.UserNameCn.Contains(model.UserCodeOrName)));
            }
            //组织机构 条件筛选
            if (model != null && !string.IsNullOrEmpty(model.OrgCode))
            {
                queue = queue.Where(w => w.OrgCode == model.OrgCode);
            }
            //排序
            queue = queue.OrderBy(o => o.UserNameCn);
            #endregion
            //转义分页条件
            PagingDto pagingDto = new PagingDto() { PageIndex = flowPagingDto.PageIndex, MaxResultCount = flowPagingDto.PageSize };
            //输出查询数据
            var resultPageData = queue.GetPagingData<UserInfo>(pagingDto);
            var resultData = resultPageData.Items.Select(s => new SysFlowAccounts()
            {
                AccountId = s.Id.ToString(),
                UserCode = s.UserCode,
                UserNameCn = s.UserNameCn,
                Sex = s.Sex,
                EmailAddress = s.EmailAddress,
                PhoneNumber = s.PhoneNumber,
                OrgCode = s.OrgCode,
                IsAdmin = s.IsAdmin,
            });

            return new FlowPagingResult<SysFlowAccounts>()
            {
                ResultData = resultData.ToList(),
                TotalCount = resultPageData.TotalCount
            };
        }




        /// <summary>
        /// 转换机构列表为:
        /// [{
        ///     id:"1"
        ///     OrgName:"机构名称"
        ///     ChildrenSysOrg:[
        ///         { id:"2", OrgName:"子菜单1"},
        ///         { id:"2", OrgName:"子菜单2"}
        ///     ]
        /// },..]
        /// </summary>
        /// <param name="dataAll"></param>
        /// <returns></returns>
        private List<SysFlowOrgData> ConvertMenusList(List<SysFlowOrgData> dataAll)
        {
            int i = 1;
            //获取所有父节点
            var parentData = dataAll.Where(p => p.ParentOrgID == null);
            //获取所有子节点
            foreach (var m in parentData)
            {
                m.SysOrgLevel = i;
                m.ChildrenOrg = dataAll.Where(p => p.ParentOrgID == m.OrgId).OrderBy(des => des.OrderBy).OrderByDescending(des => des.OrgType).ToList();
                if (m.ChildrenOrg.Any())
                {
                    SetChildrenList(dataAll, m);
                }
                else
                {
                    m.IsLeaf = true;
                }
                i = 1;
            }
            //

            return parentData.ToList();
        }


        /// <summary>
        /// 递归获取所有子节点
        /// </summary>
        /// <param name="dataAll"></param>
        /// <param name="currentModel"></param>
        /// <param name="resList"></param>
        private void SetChildrenList(List<SysFlowOrgData> dataAll, SysFlowOrgData currentModel)
        {
            foreach (var item in currentModel.ChildrenOrg)
            {
                item.SysOrgLevel = currentModel.SysOrgLevel + 1;
                //
                item.ChildrenOrg = dataAll.Where(p => p.ParentOrgID == item.OrgId).OrderBy(asc => asc.OrderBy).OrderByDescending(des => des.OrgType).ToList();
                if (item.ChildrenOrg.Any())
                {
                    SetChildrenList(dataAll, item);
                }
                else
                {
                    item.IsLeaf = true;
                }
            }
        }


    }
}