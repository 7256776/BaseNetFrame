
var component = Vue.component('j-searchform', {
    template: Vue.frameTemplate('J_Components/SearchForm'),
    created: function () {
        //获取配置信息
        this.frameSearchComponent = abp.setting.get("FrameSearchComponent");
    },
    data: function () {
        return {
            frameSearchComponent: 'never',
            searchText: "请输入关键字.."
        };
    },
    watch: {
        //监听
    },
    computed: {
    
    },
    methods: {
        doSearch: function (item) {
            var param = { value: this.searchText };
            var url = '/Views/J_Components/SearchPage';

            if (item) {
                if (item.url) {
                    url = item.url;
                }
                param = item.value;
                this.searchText = item.text;
            }
            //页面跳转
            //this.$router.push({ path: url, query: param,});
            //页面替换
            this.$router.replace({
                path: url,
                query: param,
            });
        }
    }


});
