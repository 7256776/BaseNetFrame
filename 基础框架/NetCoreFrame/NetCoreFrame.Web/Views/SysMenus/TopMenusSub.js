 
Vue.component('sys-topmenusub', {
    template: "<ul class='dropdown-menu pull-left'  >"+
                        "<li v-bind:class='leafClass(itemSub.items)' v-for='itemSub in menusdata' >" +
                            "<router-link v-bind:to='toUrl(itemSub.url)'   class='nav-link nav-toggle'>"+
                                "<i v-bind:class='icoClass(itemSub.icon)'></i> "+
                                "<span class='title' > {{ itemSub.displayName }}</span > "+
                                "<span class='arrow ' v-if='isLeaf(itemSub.items)' ></span > "+
                            "</router-link>" +
                            "<topmenusub v-bind:menusdata='itemSub.items' v-if='isLeaf(itemSub.items)'></topmenusub>"+
                        "</li >" +
                    "</ul >",
     components: {
         topmenusub: componentAssemble.SysTopMenuSub
     },
    props: {
        menusdata: {
            type: Array
        }
    },
    created: function () {
        //再次注册子菜单展开事件
        this.$nextTick(function () {
            Layout.initMainMenuDropdown();
        })
    },
    data: function () {
        return {
        };
    },
    watch: {

    },
    methods: {
        leafClass: function (item) {
            if (item.length > 0) {
                return 'dropdown-submenu';
            }
            return ''
        },
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
