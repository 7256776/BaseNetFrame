using Abp.Dependency;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

/*
 * 返回值筛选器用于数据请求后返回时需要进行的业务操作,可以用来对返回数据进行格式统一处理,示例是对Json的处理.
 * 使用方式有两种:
 * 1. 设置全局所有请求都采用该筛选,在Startup 的ConfigureServices中注册     services.AddMvc(options => { options.Filters.Add(new FrameResultHandler());  });
 * 2. 方法二在需要的Action请求服务商添加标签[FrameResultHandler]
 */
namespace NetCoreFrame.Sample
{
    /// <summary>
    /// 
    /// </summary>
    public class FrameResultHandler : Attribute, IResultFilter, ITransientDependency
    {
        /// <summary>
        /// 数据返回结果后
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            //添加业务
        }

        /// <summary>
        /// 数据返回结果前
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is JsonResult)
            {
                //修改返回数据的封装格式
                context.Result = new JsonResult(new CustomAjaxResponse()
                {
                    CustomData = ((AjaxResponse)
                                                 (
                                                     (JsonResult)context.Result
                                                     ).Value
                                                 ).Result,
                    CustomSuccess = true
                });
            }
        }

    }

    /// <summary>
    /// 自定义返回值对象
    /// </summary>
    public class CustomAjaxResponse
    { 
        public object CustomData { get; set; } 
        public bool CustomSuccess { get; set; } 
    }

}

