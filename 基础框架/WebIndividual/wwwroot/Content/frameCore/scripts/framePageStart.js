﻿//
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
        //模板页添加自定义容器通过重写组件注入
        initpage: componentAssemble.SysInitPage,
        sidemenu: componentAssemble.SysSideMenu,
        quicksidebar: componentAssemble.SysQuickSideBar,
        toptoolsmenu: componentAssemble.SysTopToolsMenu,
        topmenu: componentAssemble.SysTopMenu,
        searchform: componentAssemble.SysSearchForm,
        searchdropdown: componentAssemble.SysSearchDropdown,
        webtitle: componentAssemble.SysWebTitle,
        headtoolbutton: componentAssemble.SysHeadToolButton
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
        //注册页面窗体变化事件
        window.onresize = this.winResize;
        //初次加载初始化页面尺寸
        this.winResize();
        //获取系统设置
        this.frameTopLayout.menusCol = abp.setting.getInt("FrameTopLayoutMenusCol");
        this.frameTopLayout.menusColFull = abp.setting.getBoolean("FrameTopLayoutMenusColFull");
        this.frameTopLayout.headToolButton = abp.setting.getBoolean("HeadToolButton");
        this.frameTopLayout.layoutMenusType = abp.setting.values.FrameTopLayoutMenusType;

        //注册初始页面加载完成后的事件,参数当前vue的this对象
        abp.event.on('frame.initPage.event', this.initializationPage);
        //注册设置默认路由事件
        abp.event.on('frame.initRouter.event', this.setDefaultRouter);
        //加载模块
        this.getMenuList();
        //加载默认路由(采用该方式调用时方便扩展时候其他js中可以重复使用该方式注册自定义的路由)
        abp.event.trigger('frame.initRouter.event', abp.frameCore.frameRoutes.getFrameRoutes());
        //this.setDefaultRouter();
        //初始化路由
        this.InitRouter();
    },
    methods: {
        winResize: function () {
            frameStore.screenWidth = document.documentElement.clientWidth;
            frameStore.screenHeight = document.documentElement.clientHeight ;
        },
        //此处监听页面加载完成后的事件
        initializationPage: function (fn) {
            /*
                添加下列js到你的项目中 doFn为定义的方法名称,可以自定义
                $(function () { 
                    var doFn = function (_this) {
                        _this为模板页面的vue组件对象
                        ....自定义要执行的内容
                    }
                    //执行事件
                    abp.event.trigger('frame.initPage.event', doFn);
                });
            */
            var _this = this;
            if (fn)
                fn(_this);
        },
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
                    if (item.url && item.url != '/') {
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
            abp.ajax({ url: '/SysHome/MenuList', }).done(function (data) {
                if (data) {
                    _this.menusData = data.items;
                }
            });
        }
    }
});

