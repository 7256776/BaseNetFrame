
Vue.component('sys-sidemenu', {
    template: Vue.frameTemplate('SysMenus/SideMenu'),
    components: {
        sidemenusub: componentAssemble.SysSideMenuSub
    },
    props: {
        menusdata: {
            type: Array
        }
    },
    created: function () {
        //
    },
    data: function () {
        return {
        };
    },
    watch: {

    },
    methods: {
        icoClass: function (ico) {
            return "fa " + ico + " fa-fw"
        },
        isLeaf: function (item) {
            if (item.length > 0) {
                return true
            }
            return false
        },
        toUrl: function (url) { 
            //如果路由设置了 / 默认不做任何操作
            if (url == '/' || url.length <= 1) {
                return '';
            }
            return url;
        }

    }
});
