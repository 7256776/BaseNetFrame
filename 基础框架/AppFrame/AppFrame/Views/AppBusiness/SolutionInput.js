
var component = Vue.component('sys-solutioninput', {
    template: Vue.frameTemplate('AppBusiness/SolutionInput'),
    created: function () {
        this.formData = this.$route.params;
        if (this.formData.id) {
            this.getSolution();
            this.isShowNext = true;
        }
    },
    data: function () {
        return {
            formData: {
                id: null,
                fileId: null,
                solutionName: null,
                description: null,
                isActive: true,
            },
            formRules: {
                required: [
                    { required: true, trigger: 'onBlur' },
                ],
            },
            fileList: [],
            isShowNext: false
        };
    },
    watch: {
        //监听
    },
    computed: {

    },
    methods: {
        doFileDelete: function (file) {
            var _this = this;
            this.formData.fileId = null;
            return true;

            //此处删除不对物理文件进行处理
            //if (!file.id) {
            //    return false;
            //}
            //var ids = [file.id]
            //
            //abp.ajax({
            //    url: "/AppBusiness/DelFile",
            //    data: JSON.stringify(ids),
            //    type: 'POST'
            //}).done(function (data, res, e) {
            //    _this.fileList = _this.fileList.filter(function (item, index) {
            //        if (item.id != file.id) {
            //            return true;
            //        }
            //    });
            //});
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
                _this.formData.fileId = data.id;
                file.id = data.id;
                file.status = 'done';
                file.message = '上传完成';
            }).fail(function (data, res, e) {
                file.status = 'failed';
                file.message = '上传失败';
            });

        },
        onClickLeft: function () {
            this.$router.replace({
                name: 'sys-solution',
            });
            this.$router.go(-1)
        },
        onClickRight: function () {
            this.$refs["formEl"].submit();
        },
        doPointerType: function () {
            this.$router.replace({
                name: 'sys-pointertypeinput',
                params: this.formData
            });
        },
        getSolution: function () {
            var _this = this;
            abp.ajax({
                url: '/AppBusiness/GetSolution',
                data: JSON.stringify(_this.formData.id ),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.formData = data;
                _this.fileList = _this.fileList || [];
                _this.fileList.push({
                    id: _this.formData.id,
                    url: location.origin + _this.formData.filePathPreview
                });
            });
        },
        doSubmit: function () {
            var _this = this;
            var url = '/AppBusiness/AddSolutionAndBuildProjectCategory';
            if (this.isShowNext) {
                url = '/AppBusiness/EditSolution';
            }
            abp.ajax({
                url: url,
                data: JSON.stringify(_this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.tipSuccess('save');
                _this.formData.id = data;
                _this.$router.replace({
                    name: 'sys-pointertypeinput',
                    params: _this.formData
                });
            });
        },

    }

});
