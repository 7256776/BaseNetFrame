//
Vue.initGlobalAuthorized();

//设置切换页面后默认滚动到的位置
var vueRouter = new VueRouter({
    scrollBehavior: function (to, from, savedPosition) {
        return { x: 1, y: 1 };
    }
});

//
var vueApp = new Vue({
    el: '#app',
    components: {
        sidemenu: componentAssemble.J_SideMenu,
        quicksidebar: componentAssemble.J_QuickSideBar,
        toptoolsmenu: componentAssemble.J_TopToolsMenu,
        topmenu: componentAssemble.J_TopMenu,
        searchform: componentAssemble.J_SearchForm,
        searchdropdown: componentAssemble.J_SearchDropdown,
        webtitle: componentAssemble.J_WebTitle,
        headtoolbutton: componentAssemble.J_HeadToolButton
    },
    data: function () {
        return {
            routesList: [],
            breadcrumbMenu: [],
            menusData: [],
            frameTopLayout: {
                menusCol: 3,
                menusColFull: false,
                layoutMenusType: 'folding',
                headToolButton: false
            }
        }
    },
    router: vueRouter,
    created: function () {
        this.frameTopLayout.menusCol = abp.setting.getInt("FrameTopLayoutMenusCol");
        this.frameTopLayout.menusColFull = abp.setting.getBoolean("FrameTopLayoutMenusColFull");
        this.frameTopLayout.headToolButton = abp.setting.getBoolean("HeadToolButton");

        this.frameTopLayout.layoutMenusType = abp.setting.values.FrameTopLayoutMenusType;

        

        //注册设置默认路由事件
        abp.event.on('frame.initRouter.event', this.setDefaultRouter);
        //加载模块
        this.getMenuList();
        //加载默认路由
        abp.event.trigger('frame.initRouter.event', abp.frameCore.frameRoutes.getFrameRoutes());
        //this.setDefaultRouter();
        //初始化路由
        this.InitRouter();
    },
    methods: {
        //注册外部添加的路由
        setDefaultRouter: function (routes) {
            var _this = this;
            //获取初始路由并添加到路由集合中
            routes.forEach(function (item, index) {
                if (!item.name || !item.path) {
                    return;
                }
                item.name = item.name.trim()
                _this.routesList.push({
                    path: item.path,
                    name: item.name,
                    component: function (resolve, reject) {
                        _this.pageLoad(item.name, item.path ).then(resolve, reject);
                    },
                    meta: item.meta,
                    beforeEnter: _this.doBeforeEnter
                });
            });
        },
        InitRouter: function () {
            var _this = this;
            //
            var addRoutes = function (menuList, menus) {
                //
                menuList.items.forEach(function (item, index) {
                    //获取当前节点的所有上级节点
                    var menusSub = JSON.parse(JSON.stringify(menus));
                    menusSub.push(item);
                    //地址为空的不设置路由
                    if (item.url) {
                        //过滤空格
                        item.url = item.url.trim();
                        //设置路由集合
                        _this.routesList.push({
                            path: item.url,
                            name: item.name.trim(),
                            component: function (resolve, reject) {
                                _this.pageLoad(item.name.trim(), item.url).then(resolve, reject);
                            },
                            meta: { menuData: menusSub },
                            beforeEnter: _this.doBeforeEnter
                        });
                    }
                    //递归获取所有子节点菜单路由
                    addRoutes(item, menusSub)
                });
            }; 
            //设置路由列表
            addRoutes(abp.nav.menus.MainMenu, []);
            this.$router.addRoutes(this.routesList);
        },
        doBeforeEnter: function (to, from, next) {
            var menus = [];
            //设置菜单
            if (to.meta.menuData) {
                to.meta.menuData.forEach(function (item, index) {
                    menus.push({
                        url: item.url || '',
                        displayName: item.displayName
                    });
                })
            }
            this.breadcrumbMenu = menus;
            next();
        },
        getMenuList: function () {
            var _this = this;
            abp.ajax({ url: '/J_Home/MenuList', }).done(function (data) {
                if (data) {
                    _this.menusData = data.items;
                }
            });
        }
    }
});

