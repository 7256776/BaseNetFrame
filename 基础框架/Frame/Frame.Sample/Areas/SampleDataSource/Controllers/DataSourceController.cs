using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Frame.Core;
using Frame.Web;

namespace Frame.Sample
{
    public class DataSourceController : FrameExtAbpController
    {
        // GET: SampleDataSource/GridData
        public ActionResult Index()
        {
            return View();
        }

        private static List<GridModel> DataList = GetGridDataList();
        private static List<GridModel> GetGridDataList()
        {
            List<GridModel> objList = new List<GridModel>();
            for (int i = 0; i < 50; i++)
            {
                objList.Add(new GridModel()
                {
                    id = i,
                    Title1 = "标题名称" + i,
                    Title2 = "内容名称" + i,
                    Title3 = "主题名称" + i,
                    Title4 = "分组名称" + i,
                    Title5 = "数据对象" + i
                });
            }
            return objList;
        }

        private static List<TreeModel> TreeDataList = GetTreeDataList();
        private static List<TreeModel> GetTreeDataList()
        {
            List<TreeModel> objList = new List<TreeModel>();
            for (int i = 0; i < 5; i++)
            {
                TreeModel treeModel = new TreeModel()
                {
                    id = Guid.NewGuid(),
                    NodeName = "节点_" + i,
                    NodeValue = "节点_" + i
                };
                for (int j = 0; j < 3; j++)
                {
                    TreeModel treeSubModel = new TreeModel()
                    {
                        id = Guid.NewGuid(),
                        parentId= treeModel.id,
                        NodeName = "节点_" + i+"_"+j,
                        NodeValue = "节点_" + i + "_" + j,
                    };
                    treeModel.NodeItems = new List<TreeModel>();
                    treeModel.NodeItems.Add(treeSubModel);
                }
                objList.Add(treeModel);
            }
            return objList;
        }
     
        /// <summary>
        /// 树节点数据
        /// </summary>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        public JsonResult GetTreeData()
        {
            return Json(TreeDataList);
        }

        /// <summary>
        /// 表格数据
        /// </summary>
        /// <param name="pagingDto"></param>
        /// <returns></returns>
        public JsonResult GetGridDataList(PagingDto pagingDto, string val)
        {
            if (val==null)
            {
                val = "";
            }
            pagingDto.Sorting = "id";
            var data = DataList.AsQueryable().Where(w => w.Title1.Contains(val)).GetPagingData<GridModel>(pagingDto);
            return Json(data);
        }

    }
}