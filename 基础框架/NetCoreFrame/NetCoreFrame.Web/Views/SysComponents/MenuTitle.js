
Vue.component('sys-menutitle', {
    template: "<span>{{menuTitle}}</span>",
    created: function () {
        if (this.$route.meta && this.$route.meta.menuData.length > 0) {
            var i = this.$route.meta.menuData.length == 0 ? 0 : this.$route.meta.menuData.length - 1;
            this.menuTitle = this.$route.meta.menuData[i].displayName;
        }
    },
    data: function () {
        return {
          menuTitle:''
        };
    },
    watch: {
     
    },
    computed: {
       
    },
    methods: {

    }
});
