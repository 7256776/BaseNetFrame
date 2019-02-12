using System.Web.Optimization;

namespace Frame.Main.Entrance
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //引用自定义的css文件
            bundles.Add(
               new StyleBundle("~/customStyle")
           //此处添加自定义的Css资源文件
           //.Include("~/Content/frameCore/css/customStyle.css", new CssRewriteUrlTransform())
           );

            // 引用自定义的js文件到 顶部
            bundles.Add(
                new ScriptBundle("~/customJavascriptTop")
            //此处添加自定义的Js资源文件
            //.Include("~/Content/frameCore/scripts/customJavascript.js")
            );

            // 引用自定义的js文件到 底部
            bundles.Add(
                new ScriptBundle("~/customJavascriptBottom")
            //此处添加自定义的Js资源文件
            //.Include("~/Content/frameCore/scripts/customJavascript.js")
            );
        }
    }
}
