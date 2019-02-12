using Abp.AutoMapper;
using Abp.Modules;
using Abp.Resources.Embedded;
using Abp.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace Jurassic.Library.Resources
{
    [DependsOn(typeof(AbpWebMvcModule))]
    public class ResourcesStartModule : AbpModule
    {
        /// <summary>
        /// 
        /// </summary>
        public override void PreInitialize()
        {
            Configuration.EmbeddedResources.Sources.Add(new EmbeddedResourceSet("/Content/", Assembly.GetExecutingAssembly(), "Jurassic.Library.Resources.Content"));
        }


        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}