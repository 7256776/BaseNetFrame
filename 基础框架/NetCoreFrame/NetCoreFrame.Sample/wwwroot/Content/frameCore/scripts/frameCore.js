/* 框架核心Js库.  
 * 依赖 vue.js
*  依赖 jquery.js
 * */

//设置jquery支持IE9的跨域
jQuery.support.cors = true

//加载组件
var componentAssemble = {
    //初始首页内嵌的空组件容器用于重写注入项目中需要加载的自定义内容
    J_InitPage: function (resolve, reject) {
        Vue.prototype.pageLoad('j-initpage', 'Views/J_Components/InitPage').then(resolve, reject);
    },
    //网站标题头
    J_WebTitle: function (resolve, reject) {
        Vue.prototype.pageLoad('j-webtitle', 'Views/J_Components/WebTitle').then(resolve, reject);
    },

    //用户表单扩展
    J_UserInfoExtens: function (resolve, reject) {
        Vue.prototype.pageLoad('j-userinfoextens', 'Views/J_Components/UserInfoExtens').then(resolve, reject);
    },

    //侧边菜单
    J_SideMenu: function (resolve, reject) {
        Vue.prototype.pageLoad('j-sidemenu', 'Views/J_Menus/SideMenu').then(resolve, reject);
    },

    //侧边菜单(子菜单)
    J_SideMenuSub: function (resolve, reject) {
        Vue.prototype.pageLoad('j-sidemenusub', 'Views/J_Menus/SideMenuSub').then(resolve, reject);
    },

    //页面顶部横向布局菜单
    J_TopMenu: function (resolve, reject) {
        Vue.prototype.pageLoad('j-topmenu', 'Views/J_Menus/TopMenu').then(resolve, reject);
    },

    //页面顶部横向布局菜单(子菜单折叠)
    J_TopMenuSub: function (resolve, reject) {
        Vue.prototype.pageLoad('j-topmenusub', 'Views/J_Menus/TopMenusSub').then(resolve, reject);
    },

    //页面顶部横向布局菜单(子菜单平铺)
    J_TopMenuFullSub: function (resolve, reject) {
        Vue.prototype.pageLoad('j-topmenufullsub', 'Views/J_Menus/TopMenusFullSub').then(resolve, reject);
    },

    //JSON格式化组件
    J_JsonFormat: function (resolve, reject) {
        Vue.prototype.pageLoad('j-jsonformat', 'Views/J_Components/jsonFormat').then(resolve, reject);
    },

    //侧扩展窗体(聊天窗体)
    J_QuickSideBar: function (resolve, reject) {
        Vue.prototype.pageLoad('j-quicksidebar', 'Views/J_Components/QuickSidebar').then(resolve, reject);
    },

    //页面顶部工具栏菜单
    J_TopToolsMenu: function (resolve, reject) {
        Vue.prototype.pageLoad('j-toptoolsmenu', 'Views/J_Components/TopToolsMenu').then(resolve, reject);
    },

    //搜索工具栏
    J_SearchForm: function (resolve, reject) {
        Vue.prototype.pageLoad('j-searchform', 'Views/J_Components/SearchForm').then(resolve, reject);
    },

    //搜索工具栏内嵌扩展菜单
    J_SearchDropdown: function (resolve, reject) {
        Vue.prototype.pageLoad('j-searchdropdown', 'Views/J_Components/SearchDropdown').then(resolve, reject);
    },

    //头部工具栏
    J_HeadToolButton: function (resolve, reject) {
        Vue.prototype.pageLoad('j-headtoolbutton', 'Views/J_Components/HeadToolButton').then(resolve, reject);
    },
};

//
var initFrame = function (Vue, options) {
    //用户授权对象(通过注册组件this.GlobalAuthorizedEntity方式访问)
    Vue.prototype.GlobalAuthorizedEntity = {}

    // 设置全局方法或属性 (通过Vue.initGlobalAuthorized 调用 )
    Vue.initGlobalAuthorized = function (e) {
        abp.ajax({
            url: '/J_Account/GetUserPermission',
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
            //console.log('unbind=只调用一次，指令与元素解绑时调用。');
        }
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
     * }
     */
    Vue.prototype.pageRouter = function (pageOption) {
        if (!pageOption.name || !pageOption.path) {
            return;
        }
        var _this = this;
        pageOption.name = pageOption.name.trim();
        var rou = [{
            path: pageOption.path,
            name: pageOption.name,
            component: function (resolve, reject) {
                _this.pageLoad(pageOption.name, pageOption.path).then(resolve, reject);
            }
            //meta: { menuData: menusSub },
            //beforeEnter: _this.doBeforeEnter
        }]
        //注册路由
        this.$router.addRoutes(rou);
        
        //页面跳转
        this.$router.push({
            //通过URL传递参数,刷新后如果该路由已经注册的情况参数不会丢失 例: www.xxx.com?id=参数 
            path: pageOption.path,
            query: pageOption.params,
            //
            //name: pageOption.name,
            //params: pageOption.params
        });

        //
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
                //reject('您还冒配置路由地址呀');
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

    //注册组件this共有函数或属性
    Vue.prototype.getServiceUrl = function (url) {

        return url;
    }

}

//应用初始化
Vue.use(initFrame)

 




