using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Frame.Core
{
    public static class FramePagingExtension
    {
        /// <summary>
        /// 设置分页
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        private static IQueryable<TEntity> ApplySorting<TEntity>(this IQueryable<TEntity> queryable, PagingDto pageModel)
        {
            if (!pageModel.Sorting.IsNullOrWhiteSpace())
            {
                //设置提供的排序字段
                queryable = queryable.OrderBy(pageModel.Sorting);
            }
            else
            {
                //设置默认排序字段
                var p = typeof(TEntity).GetProperty("Id");
                if (p != null)
                {
                    queryable = queryable.OrderBy("Id");
                }
            }
            return queryable;
        }

        /// <summary>
        /// 累加分页信息
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        private static IOrderedQueryable<TEntity> ApplyThenSorting<TEntity>(this IOrderedQueryable<TEntity> queryable, PagingDto pageModel)
        {
            //累加排序字段
            if (!pageModel.Sorting.IsNullOrWhiteSpace())
            {
                queryable = queryable.ThenBy(pageModel.Sorting);
            }
            return queryable;
        }

        /// <summary>
        /// 创建分页表达式,并且获取封装数据对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        private static PagedResultDto<TEntity> BuildPaging<TEntity>(this IQueryable<TEntity> queryable, PagingDto pageModel)
        {
            //构建分页查询
            var quer = queryable.Skip(pageModel.SkipCount).Take(pageModel.MaxResultCount);
            //返回数据集合
            var dataList = quer.ToList();
            //返回数据总条数
            var dataCount = queryable.Count();
            return new PagedResultDto<TEntity>(dataCount, dataList);
        }

        /// <summary>
        /// 分页查询并转换到仓储DTO对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="DtoTEntity"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public static PagedResultDto<DtoTEntity> GetPagingData<TEntity, DtoTEntity>(this IQueryable<TEntity> queryable, PagingDto pageModel)
        {
            queryable = queryable.ApplySorting(pageModel);
            var pagedResultDto = queryable.BuildPaging(pageModel);
            //完成映射
            var data = pagedResultDto.Items.MapTo<List<DtoTEntity>>();
            return new PagedResultDto<DtoTEntity>(pagedResultDto.TotalCount, data);
        }

        /// <summary>
        /// 分页查询并转换到仓储DTO对象
        /// </summary>
        /// <typeparam name="TEntity">数据实体对象</typeparam>
        /// <typeparam name="DtoTEntity">仓储实体对象</typeparam>
        /// <param name="queryable">主查询表达式(必须包含排序)</param>
        /// <param name="pageModel">分页属性对象</param>
        /// <returns></returns>
        public static PagedResultDto<DtoTEntity> GetPagingData<TEntity, DtoTEntity>(this IOrderedQueryable<TEntity> queryable, PagingDto pageModel)
        {
            queryable = queryable.ApplyThenSorting(pageModel);
            var pagedResultDto = queryable.BuildPaging(pageModel);
            //完成映射
            var data = pagedResultDto.Items.MapTo<List<DtoTEntity>>();
            return new PagedResultDto<DtoTEntity>(pagedResultDto.TotalCount, data);
        }

        /// <summary>
        /// 分页查询到实体对象
        /// </summary>
        /// <typeparam name="TEntity">数据实体对象</typeparam>
        /// <param name="queryable">主查询表达式(必须包含排序)</param>
        /// <param name="pageModel">分页属性对象</param>
        /// <returns></returns>
        public static PagedResultDto<TEntity> GetPagingData<TEntity>(this IQueryable<TEntity> queryable, PagingDto pageModel)
        {
            queryable = queryable.ApplySorting(pageModel);
            return queryable.BuildPaging(pageModel);
        }

        /// <summary>
        /// 分页查询到实体对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public static PagedResultDto<TEntity> GetPagingData<TEntity>(this IOrderedQueryable<TEntity> queryable, PagingDto pageModel)
        {
            queryable = queryable.ApplyThenSorting(pageModel);
            return queryable.BuildPaging(pageModel);
        }




    }
}
