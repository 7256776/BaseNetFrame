
var component = Vue.component('domainevent', {
    template: Vue.frameTemplate('DomainEvent/Index'),
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
        doInsertLog: function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/DomainEvent/InsertLog',
                data: JSON.stringify(this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.formData.id = data;
                abp.message.success('新增成功了.');
            });
        },
        doUpdateLog: function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/DomainEvent/UpdateLog',
                data: JSON.stringify(this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.formData.message = data.message;
                abp.message.success('更新成功了.');
            });
        },
        doDeleteLog: function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/DomainEvent/DeleteLog',
                data: JSON.stringify(this.formData.id),
                type: 'POST'
            }).done(function (data, res, e) {
                abp.message.success('更新成功了.');
            });
        },
        doDoEvent: function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/DomainEvent/DoEvent',
                data: JSON.stringify(this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.formData.message = data.message;
                abp.message.success('完成了.');
            });
        },


    }
});
