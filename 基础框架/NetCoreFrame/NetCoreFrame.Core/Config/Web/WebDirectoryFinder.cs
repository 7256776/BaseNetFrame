using System;
using System.IO;
using System.Linq;
using Abp.Reflection.Extensions;

namespace NetCoreFrame.Core
{
    /// <summary>
    /// 通过web程序集的解决方案名称获取目录,如果名称变更需要调整
    /// 这个类用于查找Web项目的根路径
    /// 单元测试（查找视图）和实体框架核心命令行命令（查找conn字符串）
    /// </summary>
    [Obsolete("该方法过时,可以采用方式获取Url Environment.CurrentDirectory", true)]
    public static class WebDirectoryFinder
    {
        public static string GetContentRootFolder()
        { 
            var coreAssemblyDirectoryPath = Path.GetDirectoryName(typeof(NetCoreFrameCoreModule).GetAssembly().Location);
            if (coreAssemblyDirectoryPath == null)
            {
                throw new Exception("找不到 NetCoreFrameCoreModule 程序集的位置!");
            }

            var directoryInfo = new DirectoryInfo(coreAssemblyDirectoryPath);
            while (!DirectoryContains(directoryInfo.FullName, "NetCoreFrame.Web.sln"))
            {
                if (directoryInfo.Parent == null)
                {
                    throw new Exception("找不到内容根文件夹!");
                }

                directoryInfo = directoryInfo.Parent;
            }

            var webMvcFolder = Path.Combine(directoryInfo.FullName, "NetCoreFrame.Web");
            if (Directory.Exists(webMvcFolder))
            {
                return webMvcFolder;
            }

            throw new Exception("找不到web项目的根文件夹!");
        }

        private static bool DirectoryContains(string directory, string fileName)
        {
            return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
        }
    }
}
