using System.Web;
using System.Web.Optimization;

namespace Frame.Sample
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //引用自定义的css文件
            bundles.Add(
               new StyleBundle("~/customStyle")
                   .Include("~/CustomResources/Style/customStyle.css", new CssRewriteUrlTransform())
           );

            // 引用自定义的js文件到 顶部
            bundles.Add(
                new ScriptBundle("~/customJavascriptTop")
                    .Include("~/CustomResources/Javascript/customJavascriptTop.js")
            );

            // 引用自定义的js文件到 底部
            bundles.Add(
                new ScriptBundle("~/customJavascriptBottom")
                    .Include("~/CustomResources/Javascript/customJavascriptBottom.js")
            );
        }
    }
}
