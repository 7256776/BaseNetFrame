
var component = Vue.component('sys-searchpage', {
    template: Vue.frameTemplate('SysComponents/SearchPage'),
    created: function () {
        this.queryData = this.$route.query;
    },
    data: function () {
        return {
            queryData: null
        };
    },
    watch: {
        '$route': function (to, from) {
            this.queryData = this.$route.query;
        }
    },
    computed: {
        //计算
    },
    methods: {
        //函数
    }


});
