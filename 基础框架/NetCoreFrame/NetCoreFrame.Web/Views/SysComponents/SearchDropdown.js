Vue.component('sys-searchdropdown', {
    template: Vue.frameTemplate('SysComponents/SearchDropdown'),
    props: ['searchCallback'],
    updated: function () {
        //加载完成后注册事件
    },
    created: function () {

        //获取搜索扩展按钮的数据
        this.menuList = abp.frameCore.frameSearch.menus;
    },
    data: function () {
        return {
            menuList: []
        };
    },
    watch: {

    },
    computed: {
        
    },
    methods: {
        doLink: function (item) {
            this.searchCallback(item);
        }
    }
});
