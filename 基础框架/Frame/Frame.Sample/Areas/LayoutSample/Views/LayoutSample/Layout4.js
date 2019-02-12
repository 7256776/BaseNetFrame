
var component = Vue.component('j-layoutsample4', {
    template: Vue.frameTemplate('LayoutSample/LayoutSample/Layout4'),
    created: function () {
        console.log('query参数');
        console.log(this.$route.query);

        //console.log('params参数');
        //console.log(this.$route.params);

        this.queryData = this.$route.query;
    },
    //props: {
    //    myParam: {
    //        type: String
    //    }
    //},
    //还可以通过属性接收参数,参数需要通过params传递
    props: ["myParam1", "myParam2"],
    data: function () {
        return {
            title: 'Layout4页面',
            queryData: {}
        }
    },
    watch: {
        //监听
    },
    methods: {

    }
});
