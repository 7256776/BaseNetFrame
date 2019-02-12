
Vue.component('j-topmenu', {
    template: Vue.frameTemplate('J_Menus/TopMenu'),
    components: {
        topmenusub: componentAssemble.J_TopMenuSub,
        topmenufullsub: componentAssemble.J_TopMenuFullSub
    },
    props: {
        //tile = 平铺     folding = 折叠
        menuType: {
            type: String,
            default: 'folding'          
        },
        menusdata: {
            type: Array
        },
        //设置菜单列数,仅限菜单menuType=tile 有效 
        menusCol: {
            type: Number,
            default: 4 
        },
        //设置菜单列布局是否占满页面所有宽度,仅限菜单menuType=tile 有效 
        isMenusColFull: {
            type: Boolean,
            default: false
        }
    },
    created: function () {
       
        this.$nextTick(function () {
            //Layout.initMainMenuDropdown();
        })
    },
    data: function () {
        return {
        };
    },
    watch: {

    },
    methods: {
        //设置菜单列数所对应的样式
        getMenusColClass: function () {
            switch (this.menusCol) {
                case 1:
                    return "col-md-12";
                case 2:
                    return "col-md-6";
                case 3:
                    return "col-md-4";
                case 4:
                    return "col-md-3";
                case 6:
                    return "col-md-2";
                default:
                    return "col-md-3";
            }
        },
        icoClass: function (ico) {
            return "fa " + ico + " fa-fw"
        },
        isLeaf: function (item) {
            if (item && item.length > 0) {
                return true
            }
            return false
        },
        toUrl: function (url) {
            return url || '';
        }

    }
});
