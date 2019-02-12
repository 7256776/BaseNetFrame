using Abp.Modules;
using System.Configuration;
using System.Reflection;
using System.Web.Mvc;

namespace Frame.MongoDB
{
    public class StartModuleMongoDB : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<IFrameMongoDBConfiguration, FrameMongoDBConfiguration>();

            //
            Configuration.Modules.FrameMongoDB().ConnectionString = ConfigurationManager.AppSettings["MongoDBConnection"];//"mongodb://192.168.1.232:27017";
            Configuration.Modules.FrameMongoDB().DatatabaseName = ConfigurationManager.AppSettings["MongoDBName"];// "FrameDB";

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            //
            ModelBinderProviders.BinderProviders.Add(new ObjectIdProvider());
        }
    }
}
