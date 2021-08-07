
var component = Vue.component('sys-commodityinput', {
    template: Vue.frameTemplate('AppBaseConfig/CommodityInput'),
    created: function () {
        //
        this.formCommodityData = this.$route.params;
        if (this.formCommodityData && this.formCommodityData.id) {
            this.getCommodity();
        }
        this.initUnitData();
        this.initPointerType();
    },
    data: function () {
        return {
            unitData:[],
            pointerTypeData: [],
            activeSidebar: 0,
            unitIndex : 0,
            pointerTypeIndex: 0,
            fileList:[],
            formItem: {
                showPointerType: false,
                showUnit: false,
            },
            formCommodityData: {
                id: null,
                categoryIndex: 0,
                categoryCode: null,
                categoryName: null,
                unitCode: null,
                unitName: null,
            },
            formData: {
                id: null,
                commodity: null,
                brand: null,
                categoryCode: null,
                categoryName: null,
                unitCode: null,
                unitName: null,
                price: null,
                fileIds: [],
                appFiles: [],
                isActive: true,
                description: null,
                fileId: null
            },
            formRules: {
                required: [
                    { required: true, trigger: 'onBlur' },
                ], 
            },
        };
    },
    watch: {
        //监听
    },
    computed: {
    },
    methods: {
        onClickLeft: function () {
            this.$router.replace({
                name: 'sys-commodity',
                params: this.formCommodityData
            });
            this.$router.go(-1)
        },
        onClickRight: function () {
            this.$refs["formEl"].submit();
        },
        doConfirmPointer: function (item, index) {
            this.formData.categoryCode = item.dictCode;
            this.formData.categoryName = item.dictContent;
            this.formItem.showPointerType = false
        },
        doConfirmUnit: function (item, index) {
            this.formData.unitCode = item.dictCode;
            this.formData.unitName = item.dictContent;
            this.formItem.showUnit = false
        },

        initUnitData: function () {
            var _this = this;
            abp.ajax({
                url: '/SysDict/GetSysDictListByDictType',
                data: JSON.stringify("DW"),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.unitData = data;
                //
                _this.unitData.forEach(function (item, index) {
                    if (item.dictCode == _this.formCommodityData.unitCode) {
                        _this.unitIndex = index;
                        return;
                    }
                });
            });
        },
        initPointerType: function () {
            var _this = this;
            abp.ajax({
                url: '/SysDict/GetSysDictListByDictType',
                data: JSON.stringify("PL"),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.pointerTypeData = data;
                //
                _this.pointerTypeData.forEach(function (item, index) {
                    if (item.dictCode == _this.formCommodityData.categoryCode) {
                        _this.pointerTypeIndex = index;
                        return;
                    }
                });
            });
        },
        getCommodity: function () {
            var _this = this;
            abp.ajax({
                url: '/AppBaseConfig/GetCommodity',
                data: JSON.stringify(_this.formCommodityData.id),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.formData = data;
                //
                _this.formData.appFiles = _this.formData.appFiles || [];
                _this.formData.fileIds = _this.formData.fileIds || [];
                _this.formData.appFiles.forEach(function (item, index) {
                    _this.fileList.push({
                        id: item.id,
                        url: item.filePathPreview
                    });
                    _this.formData.fileIds.push(item.id);
                });
            });
        },
        doSubmit: function () {
            var _this = this;
            //debugger
            //!this.formData.fileId && 
            this.formData.fileIds = this.formData.fileIds || [];
            this.formData.fileId = '';
            if (this.formData.fileIds.length > 0) {
                this.formData.fileId = this.formData.fileIds[0];
            }
            abp.ajax({
                url: '/AppBaseConfig/SaveCommodity',
                data: JSON.stringify(_this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.tipSuccess('save');
                _this.onClickLeft();
            });
        },
        doAfterRead: function (file) {
            var _this = this;
            //上传文件表单对象
            var formData = new FormData();
            formData.append("files", file.file);
            //
            file.status = 'uploading';
            file.message = '上传中...';
            //
            abp.ajax({
                url: "/AppBusiness/AppFileUpload",
                type: "POST",
                data: formData,
                contentType: false,//实体头部用于指示资源的MIME类型 media type 。这里要为false
                processData: false,//processData 默认为true，当设置为true的时候,jquery ajax 提交的时候不会序列化 data，而是直接使用data
            }).done(function (data, res, e) {
                if (res.success != true) {
                    file.status = 'failed';
                    file.message = '上传失败';
                    return;
                }
                _this.formData.fileIds = _this.formData.fileIds || [];
                _this.formData.fileIds.push(data.id);
                if (_this.formData.fileIds.length > 0) {
                    _this.formData.fileId = _this.formData[0];
                }
                file.id = data.id;
                file.status = 'done';
                file.message = '上传完成';
            }).fail(function (data, res, e) {
                file.status = 'failed';
                file.message = '上传失败';
            });

        },
        doFileDelete: function (file) {
            var _this = this;
            this.$dialog.confirm({
                title: '提示',
                message: '确定删除?',
                beforeClose: function (action, done) {
                    if (action == 'confirm') {
                        _this.formData.fileIds = _this.formData.fileIds || [];
                        _this.formData.fileIds = _this.formData.fileIds.filter(function (item, index) {
                            if (item != file.id) {
                                return true;
                            }
                        });

                        _this.fileList = _this.fileList || [];
                        _this.fileList = _this.fileList.filter(function (item, index) {
                            if (item.id != file.id) {
                                return true;
                            }
                        });

                        done();
                    }
                    if (action == 'cancel') {
                        done();
                    }
                }
            });
            return false;
        },


    }

});
