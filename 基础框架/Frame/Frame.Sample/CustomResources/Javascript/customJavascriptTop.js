(function () {
    //匿名函数页面顺序加载,优先于VUE初始化

    // 项目静态路由
    var projectRoutes = [
        {
            name: 'j-layoutsample4',
            path: '/Areas/LayoutSample/Views/LayoutSample/Layout4',
        }
    ];

    //注册项目中的路由页面(此处注册的是没有在菜单管理中配置的页面,主要用作页面跳转)
    abp.frameCore.frameRoutes.setFrameRoutes(projectRoutes);

    abp.frameCore.frameSearch.menus = [
        {
            text: '论坛',
            value: {
                myParam1: "论坛",
                myParam2: "[{ 'name': '论坛参数' }]"
            },
            url: '',
        },
        {
            text: '网站',
            value: {
                myParam1: "网站",
                myParam2: "[{ 'name': '网站参数' }]"
            },
            url: ''
        }
    ];

})();

$(function () {
    //页面启动完成后加载,滞后于VUE初始化
});
