
var component = Vue.component('sqlservercache', {
    template: Vue.frameTemplate('SqlServerCache/Index'),
    created: function () {
    },
    data: function () {
        return {
            mesg: null,
            mesgList:[],
            formData: {
                id: null,
                message: '',
                keyName: ''
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
        doSet: function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/SqlServerCache/SetTime',
                data: JSON.stringify(this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                abp.message.success('发送成功了.');
            });
        },
        doGet: function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/SqlServerCache/GetTime',
                data: JSON.stringify(this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.formData.message = data;
                abp.message.success('获取成功了.');
            });

        },


    }
});
