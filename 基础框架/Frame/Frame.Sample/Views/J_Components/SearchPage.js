
var component = Vue.component('j-searchpage', {
    template: Vue.frameTemplate('J_Components/SearchPage'),
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
