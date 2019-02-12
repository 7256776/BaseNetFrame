
var component = Vue.component('geetest-test', {
    template: Vue.frameTemplate('Geetest/Index'),
    components: {
        "geetest-plug": sampleAssemble.GeetestPlug,
    },
    created: function () {
    },
    data: function () {
        return {
            geetest: {
                geetestChallenge: null,
                geetestValidate: null,
                geetestSeccode: null,
            },
            formData: {
                id: null,
            },
        };
    },
    watch: {
        //监听
    },
    computed: {
        //计算
    },
    methods: {
        doSubmit: function () {
            //初始加载数据
            var _this = this;
            //
           
        },
        doPostCallback: function (geetestdata) {
            //初始加载数据
            this.geetest = geetestdata;
            abp.message.success('居然验证成功了.');
        },
    }
});
