/* 框架核心Js库.  
 * 依赖 vue.js
*  依赖 jquery.js
 * */
//
Vue.config.productionTip = false
 
//设置jquery支持IE9的跨域
jQuery.support.cors = true

//加载组件
var componentAssemble = {
    //初始首页内嵌的空组件容器用于重写注入项目中需要加载的自定义内容
    SysInitPage: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-initpage', 'Views/SysComponents/InitPage').then(resolve, reject);
    },
    //网站标题头
    SysWebTitle: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-webtitle', 'Views/SysComponents/WebTitle').then(resolve, reject);
    },

    //用户表单扩展
    SysUserInfoExtens: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-userinfoextens', 'Views/SysComponents/UserInfoExtens').then(resolve, reject);
    },

    //侧边菜单
    SysSideMenu: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-sidemenu', 'Views/SysMenus/SideMenu').then(resolve, reject);
    },

    //侧边菜单(子菜单)
    SysSideMenuSub: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-sidemenusub', 'Views/SysMenus/SideMenuSub').then(resolve, reject);
    },

    //页面顶部横向布局菜单
    SysTopMenu: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-topmenu', 'Views/SysMenus/TopMenu').then(resolve, reject);
    },

    //页面顶部横向布局菜单(子菜单折叠)
    SysTopMenuSub: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-topmenusub', 'Views/SysMenus/TopMenusSub').then(resolve, reject);
    },

    //页面顶部横向布局菜单(子菜单平铺)
    SysTopMenuFullSub: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-topmenufullsub', 'Views/SysMenus/TopMenusFullSub').then(resolve, reject);
    },

    //JSON格式化组件
    SysJsonFormat: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-jsonformat', 'Views/SysComponents/jsonFormat').then(resolve, reject);
    },

    //侧扩展窗体(聊天窗体)
    SysQuickSideBar: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-quicksidebar', 'Views/SysComponents/QuickSidebar').then(resolve, reject);
    },

    //页面顶部工具栏菜单
    SysTopToolsMenu: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-toptoolsmenu', 'Views/SysComponents/TopToolsMenu').then(resolve, reject);
    },

    //搜索工具栏
    SysSearchForm: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-searchform', 'Views/SysComponents/SearchForm').then(resolve, reject);
    },

    //搜索工具栏内嵌扩展菜单
    SysSearchDropdown: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-searchdropdown', 'Views/SysComponents/SearchDropdown').then(resolve, reject);
    },

    //头部工具栏
    SysHeadToolButton: function (resolve, reject) {
        Vue.prototype.pageLoad('sys-headtoolbutton', 'Views/SysComponents/HeadToolButton').then(resolve, reject);
    },
};

//
var initFrame = function (Vue, options) {
    //用户授权对象(通过注册组件this.GlobalAuthorizedEntity方式访问)
    Vue.prototype.GlobalAuthorizedEntity = {}

    // 设置全局方法或属性 (通过Vue.initGlobalAuthorized 调用 )
    Vue.initGlobalAuthorized = function (e) {
        abp.ajax({
            url: '/SysAccount/GetUserPermission',
            async: false,
            type: 'POST'
        }).done(function (data, res, e) {
            Vue.prototype.GlobalAuthorizedEntity = data;


            //设置头像默认路径
            Vue.prototype.GlobalAuthorizedEntity.DefaultImgUrl = '/Content/frameCore/img/avatars/';

            if (data.user) {
                //设置头像完整路径
                Vue.prototype.GlobalAuthorizedEntity.user.refresh = function (imgIndex) {
                    var imgUrl = Vue.prototype.GlobalAuthorizedEntity.DefaultImgUrl;
                    Vue.prototype.GlobalAuthorizedEntity.user.imageUrl_150 = imgUrl + imgIndex + '_150.png';
                    Vue.prototype.GlobalAuthorizedEntity.user.imageUrl_40 = imgUrl + imgIndex + '_40.png';
                }

                var imgUrl = Vue.prototype.GlobalAuthorizedEntity.DefaultImgUrl;
                var imgIndex = Vue.prototype.GlobalAuthorizedEntity.user.imageUrl;

                Vue.prototype.GlobalAuthorizedEntity.user.imageUrl_150 = imgUrl + imgIndex + '_150.png';
                Vue.prototype.GlobalAuthorizedEntity.user.imageUrl_40 = imgUrl + imgIndex + '_40.png';
            }
        }).fail(function (data, res, e) {
            //debugger;
        });
    }

    //设置自定义指令授权验证
    Vue.directive('IsAuthorized', {
        //自定义指令，可以通过组件添加属性即可使用(el.dataset在钩子间传递参数)
        bind: function (el, binding, vnode, oldVnode) {
            //console.log('bind=只调用一次，指令第一次绑定到元素时调用，用这个钩子函数可以定义一个在绑定时执行一次的初始化动作。')
            var actionName = vnode.data.ref;
            var menuName = vueApp.$route.name
            //如果未设置模块或授权ref的忽略权限判断
            if (!actionName || !menuName) {
                return
            }

            if (Vue.prototype.GlobalAuthorizedEntity.user) {
                if (Vue.prototype.GlobalAuthorizedEntity.user.isAdmin == true) {
                    return;
                }
            }

            if (!Vue.prototype.GlobalAuthorizedEntity.permission) {
                return;
            }

            var haspermission = Vue.prototype.GlobalAuthorizedEntity.permission.some(function (item, index) {
                if (item.menuName && item.handleName) {
                    if (item.menuName.toLowerCase() == menuName.toLowerCase() &&
                        item.handleName.toLowerCase() == actionName.toLowerCase()) {
                        return true;
                    }
                }
            });

            if (haspermission == false) {
                //移除按钮所有注册的事件以及属性绑定
                if (el.__vue__) {
                    el.__vue__.$destroy();
                }
                //隐藏组件
                el.style.display = "none";
            }
        },
        inserted: function (el, binding, vnode, oldVnode) {
            //console.log('inserted=被绑定元素插入父节点时调用 (父节点存在即可调用，不必存在于 document 中)。')
        },
        update: function (el, binding, vnode, oldVnode) {
            //console.log('update=所在组件的 VNode 更新时调用')
        },
        componentUpdated: function (el, binding, vnode, oldVnode) {
            //console.log('componentUpdated=所在组件的 VNode 及其孩子的 VNode 全部更新时调用')
        },
        unbind: function (el, binding, vnode, oldVnode) {
            //console.log('unbind=只调用一次，指令在元素解绑时调用。');
        }
    });

    // 设置全局方法或属性 (通过Vue.initGlobalAuthorized 调用 )
    Vue.initGlobalAuthorized = function (e) {
        abp.ajax({
            url: '/SysAccount/GetUserPermission',
            async: false,
            type: 'POST'
        }).done(function (data, res, e) {
            Vue.prototype.GlobalAuthorizedEntity = data;


            //设置头像默认路径
            Vue.prototype.GlobalAuthorizedEntity.DefaultImgUrl = '/Content/frameCore/img/avatars/';

            if (data.user) {
                //设置头像完整路径
                Vue.prototype.GlobalAuthorizedEntity.user.refresh = function (imgIndex) {
                    var imgUrl = Vue.prototype.GlobalAuthorizedEntity.DefaultImgUrl;
                    Vue.prototype.GlobalAuthorizedEntity.user.imageUrl_150 = imgUrl + imgIndex + '_150.png';
                    Vue.prototype.GlobalAuthorizedEntity.user.imageUrl_40 = imgUrl + imgIndex + '_40.png';
                }

                var imgUrl = Vue.prototype.GlobalAuthorizedEntity.DefaultImgUrl;
                var imgIndex = Vue.prototype.GlobalAuthorizedEntity.user.imageUrl;

                Vue.prototype.GlobalAuthorizedEntity.user.imageUrl_150 = imgUrl + imgIndex + '_150.png';
                Vue.prototype.GlobalAuthorizedEntity.user.imageUrl_40 = imgUrl + imgIndex + '_40.png';
            }
        }).fail(function (data, res, e) {
            //debugger;
        });
    }

    //设置表单标签设置Lable文本超长情况下的样式
    Vue.directive('FormLine', {
        //自定义指令，可以通过组件添加属性即可使用(el.dataset在钩子间传递参数)
        bind: function (el, binding, vnode, oldVnode) {
            if ( el.children &&  el.children.length>0) {
                el.children[0].style.lineHeight = "18px";
            }
        },
    });

    //加载模板页面(同步执行)
    Vue.frameTemplate = function (url) {
        return $.ajax({
            url: abp.appPath + url,
            async: false
        }).responseText;
    };

    /**
     * @param {any} pageOption : {
     *      name : '模板标签名称',
     *      path : '模板js地址(不包含.js后缀)',
     *      displayName : '模板显示名称'
     *      params:'参数对象'
     * }
     */
    Vue.prototype.pageRouter = function (pageOption) {
        var _this = this;

        if (!pageOption.name) {
            return;
        }
        //判断路由是否已经存在
        var isRoutes = this.$router.app.$data.routesList.filter(function (item, index) {
            if (pageOption.name.trim() == item.name) {
                return true;
            }
        });
        //路由不存在就add
        if (!isRoutes || isRoutes.length <= 0) {
            if (!pageOption.path) {
                return;
            }
            var route =
            {
                path: pageOption.path,
                name: pageOption.name.trim(),
                component: function (resolve, reject) {
                    _this.pageLoad(pageOption.name, pageOption.path).then(resolve, reject);
                }
                //meta: { menuData: menusSub },
                //beforeEnter: _this.doBeforeEnter
            }

            //注册到自定义的路由集合
            this.$router.app.$data.routesList.push(route);
            //注册到vue路由集合
            var routes = [];
            routes.push(route)
            //注册路由
            this.$router.addRoutes(routes);
        } else {
            pageOption.name = pageOption.name || isRoutes[0].name;
            pageOption.path = pageOption.path || isRoutes[0].path;
            pageOption.displayName = pageOption.displayName || isRoutes[0].displayName;
        }

        //页面跳转
        this.$router.push({
            //通过URL传递参数,刷新后如果该路由已经注册的情况参数不会丢失 例: www.xxx.com?id=参数 
            path: pageOption.path,
            query: pageOption.params,
            //
            //name: pageOption.name,
            //params: pageOption.params
        });

        //设置导航工具栏
        if (pageOption.displayName && this.$parent) {
            if (!this.$parent.$data)
                return;
            this.$parent.$data.breadcrumbMenu.push({
                url: pageOption.path,
                displayName: pageOption.displayName
            });
        }
    }

    //注册组件this共有函数或属性
    Vue.prototype.pageLoad = function (componentName, path) { 
        //判断地址第一个是否存在 / 如果存在就剔除掉
        var i = path.indexOf('/');
        if (i == 0) {
            path = path.substring(1).trim();
        }
        //
        if (path) {
            path = path + '.js';
        }
        //
        return new Promise(function (resolve, reject) {
            if (!path) {
                reject('请检查模块配置的路由地址.');
                return;
            }
            var script = document.createElement('script');
            script.src = abp.appPath + path;
            script.async = true;
            script.onload = function () {
                var component = Vue.component(componentName);
                if (component) {
                    resolve(component);
                } else {
                    reject();
                }
            };
            script.onerror = reject;
            document.body.appendChild(script);
        });
    }

    //注册组件this对象函数(获取用户图像)
    Vue.prototype.doUserImg = function (imgIndex, size) {
        var imgUrl = Vue.prototype.GlobalAuthorizedEntity.DefaultImgUrl;
        if (size == 150) {
            return imgUrl + imgIndex + '_150.png';
        }
        return imgUrl + imgIndex + '_40.png';
    };

    //注册组件this对象函数(设置语言的图标)
    Vue.prototype.doLanguageImg = function (img) {
        if (!img) {
            img = "cn.png";
        }
        return "background-image: url(/Content/assets/global/img/flags/" + img + ");"
    };

    //注册组件this对象函数(设置语言的连接)
    Vue.prototype.doLanguageUrl = function (name, applicationPath, requestPath) {
        return applicationPath + "AbpLocalization/ChangeCulture?cultureName=" + name + "&returnUrl=" + requestPath;
    };

    //注册组件this共有函数或属性 暂留预设
    Vue.prototype.getServiceUrl = function (url) {
        return url;
    }


}

//应用初始化
Vue.use(initFrame)

//
/*
*   简单的状态管理对象
*   screenHeight 当Dom高度     ┓   
*                                              ┣   初始值通过framePageStart.js 的 winResize              
*   screenWidth  当Dom宽度     ┛
*       
*/
var frameStore = Vue.observable({
    screenHeight: 0,
    screenWidth: 0
});

//混入全局对象(待改造)
Vue.mixin({
    created: function () {
       
    },
      //混入公用数据
     data: function () {
         return {
             /*
              *  统一表单样式
              *  formWinCol2    ┓
              *  formWinCol3    ┣ 设置表单的列数 分别 2-4列
              *  formWinCol4    ┛
              *  
              *  formFormItem1  ┓
              *  formFormItem2  ┣   设置表单元素的宽度,结合表单列数适配
              *  formFormItem3  ┣   数字 1-4表示所占用的列数
              *  formFormItem4  ┛
              */
             formWinCol2: '675px',                                  
             formWinCol3: '985px',
             formWinCol4: '1300px',
             formFormItem1: { width: '200px' },
             formFormItem2: { width: '513px' },
             formFormItem3: { width: '825px' },
             formFormItem4: { width: '1140px' },
         }
    },
    computed: {
        frameWidthStyle: function () {
            return function (size) {
                var n = this.frameWidth(size);
                return { width: n, overflow: 'auto' };
            }
        },
        frameHeightStyle: function () {
            return function (size) {
                var n = this.frameHeight(size) ;
                return { height: n, overflow: 'auto' };
            }
        },
        frameWidth: function () {
            return function (size) {
                return this._sysWinSize(frameStore.screenWidth, size) + 'px';
            }
        },
        frameHeight: function () {
            return function (size) {
                return this._sysWinSize(frameStore.screenHeight, size) - 200 + 'px';
            }
        }
    },
    methods: {
        _sysWinSize: function (num, size) {
            if (!size) {
                return num;
            }
            size = size.toUpperCase();
            if (size == 'L') {
                return num * 0.8;
            }
            else if (size == 'M') {
                return num * 0.6;
            }
            else if (size == 'S') {
                return num * 0.4;
            } else {
                return num;
            }
        },
        _operationType: function (type) {
            type = type.toUpperCase();
            switch (type) {
                case 'SAVE':
                    return abp.frameCore.localization.getLocalization('Save');
                case 'Edit':
                    return abp.frameCore.localization.getLocalization('Editor');
                case 'ADD':
                    return abp.frameCore.localization.getLocalization('Add');
                case 'DEL':
                    return abp.frameCore.localization.getLocalization('Del');
                default:
                    return "";
            }
        },
        _tipBase: function (type, details, message) {
            type = type.toUpperCase();
            switch (type) {
                case 'INFO':
                    return abp.message.info(details, message);
                case 'SUCCESS':
                    return abp.message.success(details, message);
                case 'WARN':
                    return abp.message.warn(details, message);
                case 'ERROR':
                    return abp.message.error(details, message);
                default:
                    return abp.message.info(details, message);
            }
        },
        tipSuccess: function (type, details) {
            /*
             *  type        操作类型 save, edit, add, del
             *  details     消息详细
             */
            var successful = abp.frameCore.localization.getLocalization('Success');
            var message = this._operationType(type) + successful;
            abp.message.success(details, message);
        },
        tipFail: function (type, details) {
            /*
            *  type        操作类型 save, edit, add, del
            *  details     消息详细
            */
            var failure = abp.frameCore.localization.getLocalization('Failure');
            var message = this._operationType(type) + failure;
            abp.message.success(details, message);
        },
        tipShowFormat: function () {
            /*  调用带参数本地化
            *  tipLocalizat('本地化的key值')                                                   一个参数:  本地化对应的key值, 默认是info类型tip
            *  tipLocalizat('info','本地化的key值')                                           二个参数:  消息类型(info,success,warn,error), 本地化对应的key值
            *  tipLocalizat('info','本地化的key值','支持格式化 { 0 } 的参数')       三个参数:  消息类型, 本地化对应的key值, 格式化 { 0 } 的参数列表可多个
            */
            //参数集合
            var arg = arguments.length;
            var type = 'info';
            var localization = '';
            //获取消息参数
            type = arg > 1 ? arguments[0] : '';
            //获取消息提示
            localization = arg == 1 ? arguments[0] : '';
            localization = arg >= 2 ? arguments[1] : localization;
            //获取本地化提示消息
            var message = abp.frameCore.localization.getLocalization(localization);
            for (var i = 0; i < arguments.length - 2; i++) {
                var reg = new RegExp("\\{" + i + "\\}", "gm");
                message = message.replace(reg, arguments[i + 2]);
            }
            this._tipBase(type, message, '');
        },
        tipShow: function () {
            /*  调用提示消息(默认本地化转换)
             *  tipShow('提示消息')                                     一个参数:  提示消息,默认是info类型tip
             *  tipShow('info','提示消息')                             二个参数:  消息类型(info,success,warn,error), 提示消息
             *  tipShow('info','提示消息','提示消息详情')       三个参数:  消息类型, 提示消息, 提示消息详情
             */
            //参数集合
            var arg = arguments.length;
            //
            var type = 'info';
            var message = '';
            var details = '';
            //获取消息参数
            type = arg > 1 ? arguments[0] : '';
            //获取消息提示
            message = arg == 1 ? arguments[0] : '';
            message = arg >= 2 ? arguments[1] : message;
            //获取消息提示详情
            details = arg == 3 ? arguments[2] : '';
            //默认进行本地化转换
            this._tipBase(type, message, details);
        },

        /*
            _this.tipSuccess('save');
            _this.tipSuccess('edit');
            _this.tipSuccess('add');
            _this.tipSuccess('del');

            this.tipFail('save');
            this.tipFail('edit');
            this.tipFail('add');
            this.tipFail('del');

            this.tipShow('warn','IsSelect');
            this.tipShow('success','IsSelect');
            this.tipShow('error','IsSelect');

        */

    }

})
 




