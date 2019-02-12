
Vue.component('j-sidemenusub', {
     template: "<ul class='sub-menu'  >"+
                        "<li class='nav-item start  ' v-for='itemSub in menusdata' >" +
                            "<router-link v-bind:to='toUrl(itemSub.url)' class='nav-link'>"+
                            "<i v-bind:class='icoClass(itemSub.icon)'></i>"+
                            "<span class='title' > {{ itemSub.displayName }}</span > "+
                            "<span class='arrow ' v-if='isLeaf(itemSub.items)' ></span > "+
                            "</router-link >" +
                            "<sidemenusub v-bind:menusdata='itemSub.items' v-if='isLeaf(itemSub.items)'></sidemenusub>"+
                        "</li >" +
                    "</ul >",
    components: {
         sidemenusub: componentAssemble.J_SideMenuSub
     },
    props: {
        menusdata: {
            type: Array,
            default:[]
        }
    },
    created: function () {
        //
        //debugger;
    },
    data: function () {
        return {
        };
    },
    watch: {

    },
    methods: {
        icoClass: function (ico) {
            return "fa " + ico + " fa-fw";
        },
        isLeaf: function (item) {
            if (item.length > 0) {
                return true;
            }
            return false;
        },
        toUrl: function (url) {
            return url || '';
        }

    }
});
