
var component = Vue.component('web-publishingcontent', {
    //template: $.ajax({ url: abp.appPath + 'SysRole/Index', async: false }).responseText,  //等同于 Vue.frameTemplate('SysRole/Index')
    template: Vue.frameTemplate('PublishingContent/Index'),
    created: function () {


        this.getDataList();
    },
    data: function () {
        return {
            content: '<h2>I am Example</h2>',
            editorOption: {
                // Some Quill options...
            },
       
            fileData: [],
            gridMenuData: [],
            formRules: {
                docTitle: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    { min: 1, max: 30, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                ],
                docSubhead: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    { min: 1, max: 500, message: '长度在 1 到 500 个字符', trigger: 'blur' }
                ]
            },
            formData: {
                id: null,
                docTitle: null,
                docSubhead: null,
                fileName: null,
                docContent: null,
                fileItem: [],
                isActive: true
            },
            pageOptions: {
                formDialog: false,
            },
            tableOptions: {
                tableData: [],
                selectRows: [],
                selectRow: {},
            },
        };
    },
    watch: {
        //监听 
    },
    methods: {
        doDelFileName: function (e) {
            this.$refs["uploadEl"].clearFiles();
            this.formData.fileItem = [];
            this.formData.fileName = '';
        },
        dohttpRequest: function (e) {
            var _this = this;
            //上传文件表单对象
            var formData = new FormData();
            formData.append("files", e.file);
            //
            abp.ajax({
                url: e.action,
                type: "POST",
                data: formData,
                contentType: false,//实体头部用于指示资源的MIME类型 media type 。这里要为false
                processData: false,//processData 默认为true，当设置为true的时候,jquery ajax 提交的时候不会序列化 data，而是直接使用data
            }).done(function (data, res, e) {
                //{ 
                //    description: null
                //    fileAlias: "210719083714285443.png"
                //    fileName: "6.png"
                //    filePathOriginal: "\\Uploads\\2021\\07\\19\\210719083714285443.png"
                //    filePathPreview: "\\Uploads\\2021\\07\\19\\210719083714285443_PreviewImg.png"
                //    filePathThumbnail: "\\Uploads\\2021\\07\\19\\210719083714285443_ThumbImg.png"
                //    fileSize: 285062
                //    fileSuffix: ".png"
                //    id: "f8670613-e006-4e6a-87fc-08d94ab1eff3"
                //    isActive: true
                //}
                _this.formData.fileItem.push(data.id);
                _this.formData.fileName = data.fileName;
                _this.tipSuccess('上传');
            }).fail(function (data, res, e) {
                _this.tipFail('上传');
            });

        },
        doRowSelectChange: function (selection) {
            this.tableOptions.selectRows = selection;
        },
        doRowclick: function (row, event, column) {
            //this.$refs["gridEl"].clearSelection();
            //设置选中行
            this.$refs["gridEl"].toggleRowSelection(row);
            //
            this.tableOptions.selectRow = row;
        },
        getDataList: function () {
            var _this = this;
            abp.ajax({
                url: '/PublishingContent/GetDocList',
                //data: JSON.stringify(_this.formData)
                //type: 'POST'
            }).done(function (data) {
                _this.tableOptions.tableData = data;
            });
        },
        doAdd: function () {
            this.pageOptions.formDialog = true;
            var _this = this;
            this.$nextTick(function () {
                _this.$refs["formEl"].resetFields();
                _this.$refs["uploadEl"].clearFiles();
            });
        },
        doEdit: function () {
            if (!this.tableOptions.selectRow.id) {
                this.tipShow('warn', 'IsSelect');
                return;
            }
            var _this = this;
            this.pageOptions.formDialog = true;
            abp.ajax({
                url: '/PublishingContent/GetDocModel',
                data: JSON.stringify(_this.tableOptions.selectRow.id)
            }).done(function (data) {
                _this.formData = data;
                //页面加载完成后执行验证避免出现错误的验证提示
                _this.$nextTick(function () {
                    _this.$refs["formEl"].validate();
                });
            });
        },
        doSaveForm: function () {
            var _this = this;
            this.$refs["formEl"].validate(
                function (valid) {
                    if (valid) {
                        //
                        abp.ajax({
                            url: '/PublishingContent/SaveDocModel',
                            data: JSON.stringify(_this.formData),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            _this.getDataList();
                            _this.pageOptions.formDialog = false
                            _this.tipSuccess('save');
                            _this.$refs["gridEl"].clearSelection();
                        }).fail(function (res, e) {
                            //console.log(res)
                        });
                    } else {
                        //console.log('error submit!!');
                        return false;
                    }
                });
        },
        doDelForm: function () {
            var _this = this;
            if (this.tableOptions.selectRows.length == 0) {
                this.tipShow('warn', 'IsSelect');
                return;
            }
            this.$confirm('确定删除?', '提示', {
                //confirmButtonText: '确定',
                //cancelButtonText: '取消',
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/PublishingContent/DelDocModel',
                    data: JSON.stringify(_this.tableOptions.selectRows),
                    type: 'POST'
                }).done(function (data, res, e) {
                    _this.tipSuccess('del');
                    _this.getDataList();
                });
            }).catch(function (action) {
                //取消操作必须有避免js链式调用报异常
            });
        },

        //v-on:blur="onEditorBlur($event)"
        //v-on:focus = "onEditorFocus($event)"
        //v-on:ready = "onEditorReady($event)"
        //onEditorBlur: function (e) {
        //    debugger
        //},
        //onEditorFocus: function (e) {
        //    debugger
        //},
        //onEditorReady: function (e) {
        //    debugger
        //},
    } 
});
