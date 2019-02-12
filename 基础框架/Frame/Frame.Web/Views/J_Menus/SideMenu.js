
Vue.component('j-sidemenu', {
    template: Vue.frameTemplate('J_Menus/SideMenu'),
    components: {
        sidemenusub: componentAssemble.J_SideMenuSub
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
