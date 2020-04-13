
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
            return url || '';
        }

    }
});
