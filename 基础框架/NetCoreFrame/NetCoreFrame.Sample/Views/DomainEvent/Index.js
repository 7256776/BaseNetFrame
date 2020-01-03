
var component = Vue.component('domainevent', {
    template: Vue.frameTemplate('DomainEvent/Index'),
    created: function () {
    },
    data: function () {
        return {
            mesg: null,
            mesgList: [],
            formData: {
                id: null,
                message: '',
                keyName: ''
            },
            resultModelData: null,
            resultData: null
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
                _this.resultModelData = [];
                abp.message.success('新增成功了.');
                _this.getEntityEventProcessData()
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
                _this.resultModelData = [];
                abp.message.success('更新成功了.');
                _this.getEntityEventProcessData()
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
                _this.resultModelData = [];
                abp.message.success('更新成功了.');
                _this.getEntityEventProcessData()
            });
        },
        getEntityEventProcessData: function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/DomainEvent/GetEntityEventProcessData',
                type: 'POST'
            }).done(function (data, res, e) {
                _this.resultModelData = data;
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
                _this.formData.keyName = data.message;
                abp.message.success('完成了.');
            });
        },
        DoEventAsync: function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/DomainEvent/DoEventAsync',
                data: JSON.stringify(this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.formData.keyName = data;
                abp.message.success('完成了.');
            });
        }, 
        getCustomResultData: function () {
            var _this = this;
            $.ajax({
                url: '/DomainEvent/GetCustomResultData',
                type: "POST",
                //contentType: 'application/json',
                complete: function (e, state) {
                    _this.resultData = e.responseText;
                    abp.message.success('完成了.');
                }
            });//
        }, 
        getResultData: function () {
            var _this = this;
            //
            abp.ajax({
                url: '/DomainEvent/GetResultData',
                data: JSON.stringify(this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.resultData = e.responseText;
                abp.message.success('完成了.');
            });
        },
    }
});
