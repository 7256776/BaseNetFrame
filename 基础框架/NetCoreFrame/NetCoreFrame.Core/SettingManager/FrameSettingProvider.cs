using Abp.Configuration;
using System.Collections.Generic;

namespace NetCoreFrame.Core
{
    public class FrameSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            //获取abp默认的配置
            //var abpSetting = context.Manager.GetAllSettingDefinitions();
            //设置自定义的配置
            var frameConfig = UtilityCommon.DeserializeJson<FrameConfig>("FrameConfig.json");
            List<SettingDefinition> settingList = new List<SettingDefinition>();
            if (frameConfig==null)
            {
                return settingList;
            }

            foreach (var item in frameConfig.FrameItemList)
            {
                settingList.Add(
                    new SettingDefinition(
                        name: item.Name,                                     //配置名称
                        defaultValue: item.Value,                           //配置默认值
                        customData: "",                                        //添加自定义数据
                        description: item.Name.L(),
                        displayName: item.Name.L(),
                        group: null,                                                //配置分组(用于ui页面布局)
                        isInherited: true,                                        //是否继承上级
                        isVisibleToClients: true,                              //是否显示到客户端json
                        scopes: SettingScopes.Application            //配置所属范围
                        )
                );
            }
             
            return settingList;
            //模板
            //return new[]
            //        {
            //        new SettingDefinition(
            //            name :"FrameUserSetting",            //配置名称
            //            defaultValue: "用户设置",                //配置默认值
            //            customData:"",                              //添加自定义数据
            //            description:"配置描述信息".L(),
            //            displayName:"配置显示名称".L(),
            //            group:null,                                     //配置分组(用于ui页面布局)
            //            isInherited:true,                             //是否继承上级
            //            isVisibleToClients: true,                  //是否显示到客户端json
            //            scopes:SettingScopes.User            //配置所属范围
            //            )
            //    };
        }


    }
}
