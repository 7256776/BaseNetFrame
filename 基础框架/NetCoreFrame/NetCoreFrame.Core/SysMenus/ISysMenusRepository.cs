using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Core
{
    public interface ISysMenusRepository : IRepository<SysMenus, long>
    {
        /// <summary>
        /// 获取所有启用的菜单
        /// </summary>
        /// <returns></returns>
        IQueryable<SysMenus> GetMenusAll();

        /// <summary>
        /// 转换菜单列表分贝按照根节点菜单按顺序加载子菜单,数据不包含子节点集合:
        /// <para>[{                                          </para>
        /// <para>    id:"1",                               </para>
        /// <para>    MenuName:"菜单名称",    </para>
        /// <para>    MenuNodeLevel:'1',          </para>
        /// <para>    MenuNode:'1'                  </para>
        /// <para>},                                          </para>
        /// <para>{                                           </para>
        ///  <para>   id:"2"                                </para>
        ///  <para>   MenuName:"菜单名称"     </para>
        ///  <para>   MenuNodeLevel:'2'          </para>
        ///  <para>   MenuNode:'1.2'                </para>
        ///  <para> },..]                                        </para>
        /// </summary>
        /// <param name="dataAll"></param>
        /// <param name="resList"></param>
        /// <returns></returns>
        List<SysMenus> ConvertMenusByOrderByList(List<SysMenus> dataAll);

        /// <summary>
        /// 转换菜单列表为:
        /// <para>{                                                             </para>
        /// <para>　id:"1"                                                  </para>
        /// <para>　　MenuName:"菜单名称"                    </para>
        /// <para>　　ChildrenMenus:[                              </para>
        /// <para>　　　{ id:"2", MenuName:"子菜单1"},    </para>
        /// <para>　　　{ id:"2", MenuName:"子菜单2"}     </para>
        /// <para>　　]                                                      </para>
        /// <para>}                                                             </para>
        /// </summary>                                                    
        /// <param name="dataAll"></param>
        /// <returns></returns>
        List<SysMenus> ConvertMenusByChildrenList(List<SysMenus> dataAll);

    }
}
