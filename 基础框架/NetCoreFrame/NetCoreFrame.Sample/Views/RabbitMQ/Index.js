
var component = Vue.component('rabbitmq', {
    template: Vue.frameTemplate('RabbitMQ/Index'),
    created: function () {
    },
    data: function () {
        return {
            mesg: null,
            mesgList:[],
            formData: {
                id: null,
                message: '',
                queueName: '',
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
            abp.ajax({
                url: '/RabbitMQ/SendMessage',
                data: JSON.stringify(this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                console.log();
                abp.message.success('发送成功了.');
            });
        },
        doSubmitDirect: function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/RabbitMQ/SendDirectMessage',
                data: JSON.stringify(this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                console.log();
                abp.message.success('Direct模式发送成功了.');
            });

        },
        doSubmitFanout : function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/RabbitMQ/SendFanoutMessage',
                data: JSON.stringify(this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                console.log();
                abp.message.success('Fanout(订阅模式)发送成功了.');
            });

        },
        doSubmitTopic1: function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/RabbitMQ/SendTopicMessage',
                data: JSON.stringify(this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                console.log();
                abp.message.success('Topic模式发送成功了.');
            });
        },
        doSubmitTopic2: function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/RabbitMQ/SendTopicMessage',
                data: JSON.stringify(this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                console.log();
                abp.message.success('Topic模式发送成功了.');
            });
        },



        doGetMesg: function () {
            var _this = this;
            abp.ajax({
                url: '/RabbitMQ/GetMessage',
                type: 'POST'
            }).done(function (data, res, e) {
                _this.mesgList = data;
            });

        },
        doClearMessage: function () {
            var _this = this;
            abp.ajax({
                url: '/RabbitMQ/ClearMessage',
                type: 'POST'
            }).done(function (data, res, e) {
                _this.mesgList = data;
            });

        },
    }
});
