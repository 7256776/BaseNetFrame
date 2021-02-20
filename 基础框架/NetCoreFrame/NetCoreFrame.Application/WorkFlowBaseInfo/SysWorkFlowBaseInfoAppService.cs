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

        public readonly IViewSysFlowRoleToUserRepository _viewSysFlowRoleToUserRepository;
        public readonly ISysWorkFlowRoleRepository _sysWorkFlowRoleRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbcontext"></param>
        public SysWorkFlowBaseInfoAppService(
            ISysOrgRepository sysOrgRepository,
            IUserInfoRepository userInfoRepository,
            ISysMenusRepository sysMenusRepository,

            IViewSysFlowRoleToUserRepository viewSysFlowRoleToUserRepository,
            ISysWorkFlowRoleRepository sysWorkFlowRoleRepository
            )
        {
            _sysOrgRepository = sysOrgRepository;
            _userInfoRepository = userInfoRepository;
            _sysMenusRepository = sysMenusRepository;
            _viewSysFlowRoleToUserRepository = viewSysFlowRoleToUserRepository;
            _sysWorkFlowRoleRepository = sysWorkFlowRoleRepository;
        }

        /// <summary>
        /// 查询所有组织机构
        /// </summary>
        /// <returns></returns>
        public List<SysFlowOrgData> GetSysOrgAll()
        {
            var dataAll = _sysOrgRepository.GetAll().Where(w => w.IsActive == true).Select(s => new SysFlowOrgData()
            {
                OrgId = s.Id.ToString(),
                ParentOrgID = s.ParentOrgID.ToString(),
                OrgCode = s.OrgCode,
                OrgName = s.OrgName,
                OrgType = s.OrgType,
                OrgNode = s.OrgNode,
                OrderBy = s.OrderBy
            });
            var data = ConvertMenusList(dataAll.ToList());
            return data;
        }

        /// <summary>
        /// 查询所有业务流程
        /// </summary>
        /// <returns></returns>
        public List<SysFlowBusiness> GetSysBusinessModuleAll()
        {
            //仅查询业务模块
            var dataAll = _sysMenusRepository.GetAll().Where(w => w.IsActive == true && w.BusinessType == "2").Select(s => new SysFlowBusiness()
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
        public FlowPagingResult<SysFlowUser> GetSysUserPaging(FlowPagingParam<SysFlowUserSearch> flowPagingDto)
        {
            //参数对象
            var model = flowPagingDto.Params;
            //基础查询
            var queue = from user in _userInfoRepository.GetAll()
                        join org in _sysOrgRepository.GetAll()
                        on user.OrgCode equals org.OrgCode into joinedOrg
                        from OrgTmp in joinedOrg.DefaultIfEmpty()
                        select new SysFlowUser
                        {
                            UserId = user.Id.ToString(),
                            UserCode = user.UserCode,
                            UserNameCn = user.UserNameCn,
                            Sex = user.Sex,
                            EmailAddress = user.EmailAddress,
                            PhoneNumber = user.PhoneNumber,
                            OrgCode = user.OrgCode,
                            IsAdmin = user.IsAdmin,
                            OrgName = OrgTmp != null ? OrgTmp.OrgName : "",
                            OrgType = OrgTmp != null ? OrgTmp.OrgType : "",
                            OrgNode = OrgTmp != null ? OrgTmp.OrgNode : "",
                        };

            #region 筛选条件
            //用户账号或名称 条件筛选
            if (model != null && !string.IsNullOrEmpty(model.UserCodeOrName))
            {
                queue = queue.Where(w => (w.UserCode.Contains(model.UserCodeOrName) || w.UserNameCn.Contains(model.UserCodeOrName)));
            }
            //不包含子节点组织机构 条件筛选
            if (model != null && !string.IsNullOrEmpty(model.OrgCode) && !model.IsInclude)
            {
                queue = queue.Where(w => w.OrgCode == model.OrgCode);
            }
            //包含子节点组织机构
            if (model != null && !string.IsNullOrEmpty(model.OrgNode) && model.IsInclude)
            {
                queue = queue.Where(w => w.OrgNode.StartsWith(model.OrgNode));
            }
            //排序
            queue = queue.OrderBy(o => o.UserNameCn).OrderBy(o => o.UserId);
            #endregion
            //转义分页条件
            PagingDto pagingDto = new PagingDto() { PageIndex = flowPagingDto.PageIndex, MaxResultCount = flowPagingDto.PageSize };
            //输出查询数据
            var resultPageData = queue.GetPagingData<SysFlowUser>(pagingDto);

            return new FlowPagingResult<SysFlowUser>()
            {
                ResultData = resultPageData.Items,
                TotalCount = resultPageData.TotalCount
            };
        }

      /// <summary>
        /// 分页查询账号信息,并可根据参数进行筛选
        /// </summary>
        /// <param name="flowPagingDto"></param>
        /// <returns></returns>
        public FlowPagingResult<ViewSysFlowRoleToUser> GetFlowUserPaging(FlowPagingParam<SysFlowUserSearch> flowPagingDto)
        {
            //参数对象
            var model = flowPagingDto.Params;


            var queue = _viewSysFlowRoleToUserRepository.GetAll();


            //var queue = from flowRole in _sysWorkFlowRoleRepository.GetAll()
            //            join roleUser in _sysWorkFlowRoleToUserRepository.GetAll()
            //            on flowRole.Id equals roleUser.FlowRoleID
            //            join user in _userInfoRepository.GetAll()
            //           on roleUser.UserID equals user.Id.ToString()
            //            select new SysFlowRoleToUser
            //            {
            //                UserId = user.Id.ToString(),
            //                UserCode = user.UserCode,
            //                UserNameCn = user.UserNameCn,
            //                Sex = user.Sex,
            //                EmailAddress = user.EmailAddress,
            //                PhoneNumber = user.PhoneNumber,
            //                FlowRoleId = flowRole.Id,
            //                FlowRoleName = flowRole.FlowRoleName
                       
            //            };

            #region 筛选条件
            //用户账号或名称 条件筛选
            if (model != null && !string.IsNullOrEmpty(model.UserCodeOrName))
            {
                queue = queue.Where(w => (w.UserCode.Contains(model.UserCodeOrName) || w.UserNameCn.Contains(model.UserCodeOrName)));
            }
            //流程角色 条件筛选
            if (model != null && model.FlowRoleId!=null)
            {
                queue = queue.Where(w => w.FlowRoleId == model.FlowRoleId);
            }
            //排序
            queue = queue.OrderBy(o => o.UserNameCn).OrderBy(o => o.UserId);
            #endregion
            //转义分页条件
            PagingDto pagingDto = new PagingDto() { PageIndex = flowPagingDto.PageIndex, MaxResultCount = flowPagingDto.PageSize };
            //输出查询数据
            var resultPageData = queue.GetPagingData<ViewSysFlowRoleToUser>(pagingDto);

            return new FlowPagingResult<ViewSysFlowRoleToUser>()
            {
                ResultData = resultPageData.Items,
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