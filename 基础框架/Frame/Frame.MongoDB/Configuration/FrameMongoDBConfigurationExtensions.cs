using Abp.Configuration.Startup;

namespace Frame.MongoDB
{
    public static class FrameMongoDBConfigurationExtensions 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurations"></param>
        /// <returns></returns>
        public static IFrameMongoDBConfiguration FrameMongoDB(this IModuleConfigurations configurations)
        {
            return configurations.AbpConfiguration.Get<IFrameMongoDBConfiguration>();
        }

    }
}
