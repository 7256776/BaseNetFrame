using System;
using System.Collections.Concurrent;
using Abp.Extensions;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;

namespace NetCoreFrame.Core
{
    public static class AppConfigurations
    {
        public static readonly string ConfigurationKey;
        public static readonly ConcurrentDictionary<string, IConfigurationRoot> ConfigurationCache;

        /// <summary>
        /// 初始配置文件全局变量key值并添加到线程集合
        /// </summary>
        static AppConfigurations()
        {
            ConfigurationKey = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")+"#APPSETTING";
            ConfigurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot GetConfigurationCache()
        {
            if (ConfigurationCache.TryGetValue(ConfigurationKey, out IConfigurationRoot appsetting))
            {
                return appsetting;
            }
            //
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return Get(Environment.CurrentDirectory, environment, false);
        }

        /// <summary>
        /// 获取指定环境变量的配置信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="environmentName"></param>
        /// <param name="addUserSecrets"></param>
        /// <returns></returns>
        public static IConfigurationRoot Get(string path, string environmentName = "", bool addUserSecrets = false)
        {
            //var cacheKey = path + "#" + environmentName;
            var cacheKey = ConfigurationKey;

            return ConfigurationCache.GetOrAdd(
                cacheKey,
                _ => BuildConfiguration(path, environmentName, addUserSecrets)
            );
        }

        /// <summary>
        /// 创建配置信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="environmentName"></param>
        /// <param name="addUserSecrets"></param>
        /// <returns></returns>
        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
        {
            //设置配置文件路径
            var builder = new ConfigurationBuilder().SetBasePath(path);
            //读取默认配置文件
            builder = builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //读取自定义 js、css引用文件
            builder = builder.AddJsonFile("customResource.json", optional: true, reloadOnChange: true);

            if (!environmentName.IsNullOrWhiteSpace())
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder = builder.AddEnvironmentVariables();

            if (addUserSecrets)
            {
                //用户机密文件
                //builder.AddUserSecrets(typeof(AppConfigurations).GetAssembly());
            }

            return builder.Build();
        }
    }
}
