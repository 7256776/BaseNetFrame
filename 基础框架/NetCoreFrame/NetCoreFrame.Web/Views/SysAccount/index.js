
var component = Vue.component('sys-account', {
    template: Vue.frameTemplate('SysAccount/Index'),
    components: {
        userinfoextens: componentAssemble.SysUserInfoExtens,
        orgtreeselection: componentAssemble.SysOrgTreeSelection,
    },
    //updated: function () {
    //    var isShowUserInfo = this.$refs["userFormEx"];
    //},
    //mounted: function () {
    //    var isShowUserInfo = this.$refs["userFormEx"];
    //},
    created: function () {
        //var isShowUserInfo = this.$refs["userFormEx"].isShowUserInfo;
        this.getUserList();
        //
        //this.initMenusData();
        //超级管理员显示是否删除的表单下拉框
        this.isShowDeleteInput = Vue.prototype.GlobalAuthorizedEntity.user.isAdmin;
    },
    data: function () {
        return {
            isShowDeleteInput: false,
            isCloseUserInfoEx: false,
            activeName: 'userInfo',
            formRules: {
                userCode: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    { min: 1, max: 30, message: '长度在 1 到 30 个字符', trigger: 'blur' },
                    { validator: this.validateUserCode, message: '仅限数字,字母,下划线组成', trigger: 'blur' }
                ],
                userNameCn: [
                    { required: true, message: '必填项', trigger: 'change' },
                    { min: 1, max: 30, message: '长度在 1 到 30 个字符', trigger: 'blur' }
                ],
                emailAddress: [
                    { type: 'email', message: '请输入正确的邮箱地址', trigger: 'blur,change' }
                ]
            },
            formData: {
                id: null,
                userCode: null,
                userNameCn: null,
                sex: "1",
                emailAddress: null,
                phoneNumber: null,
                description: '',
                isActive: true,
                isDeleted: false,
                userInfoExtensJson: '',
                orgCode: ''
            },
            formDataEx: {},
            tableOptions: {
                tableData: [],
                selectRows: [],
                selectRow: {}
            },
            pageOptions: {
                searchTxt: '',
                formDialog: false,
                isUserCode: false,
                pageIndex: 1,
                maxResultCount: 20,
                total: 0
            }
        };
    },
    watch: {
       //监听
    },
    methods: { 
        initTab: function () {
            var _this = this;
            this.$nextTick(function () {
                //判断扩展表单是否存在表单(扩展表单的ref=formUserDataEx)来显示扩展页面
                var isFormEx = _this.$refs["userFormEx"].$refs["formUserDataEx"];
                _this.isCloseUserInfoEx = isFormEx ? true : false;
            });
        },
        doRowSelectChange: function (selection) {
            this.tableOptions.selectRows = selection
        },
        doRowclick: function (row, event, column) {
            //设置选中行
            this.$refs.dataGrid.toggleRowSelection(row);
            //
            this.tableOptions.selectRow = row;
        },
        refreshGrid: function () {
            this.pageOptions.pageIndex = 1;
            this.getUserList();
        },
        getUserList: function () {
            var _this = this;
            //
            var parameter = {
                params: { userCode: this.pageOptions.searchTxt },
                pagingDto: this.pageOptions
            }; 
            abp.ajax({
                url: '/SysAccount/GetUserList',
                data: JSON.stringify(parameter)
                //type: 'POST'
            }).done(function (data) {
                _this.pageOptions.total = data.totalCount;
                _this.tableOptions.tableData = data.items;
                _this.tableOptions.selectRow = {};
                _this.tableOptions.selectRows = [];
            });
        },
        doAdd: function () {
            var _this = this;
            //
            this.activeName = "userInfo";
            this.pageOptions.formDialog = true;
            //
            this.initTab();
            this.$nextTick(function () {
                _this.$refs.formUserData.resetFields();
                  //重置扩展用户信息的表单
                if (_this.$refs["userFormEx"]) {
                    _this.$refs["userFormEx"].doResetFields();
                }
            });
            this.pageOptions.isUserCode = false;
        },
        doEdit: function () {
            var _this = this;
            //
            this.activeName = "userInfo";
            if (!this.tableOptions.selectRow.id) {
                this.tipShow('warn', 'IsSelect');
                return;
            }
            this.pageOptions.formDialog = true;
            this.pageOptions.isUserCode = true;
            //
            this.initTab();
            abp.ajax({
                url: '/SysAccount/GetUserModel/',
                data: JSON.stringify(_this.tableOptions.selectRow.id),
                type: 'POST'
            }).done(function (data) {
                _this.formData = data;
                //获取用户扩展数据
                _this.formDataEx = data.userInfoEx;
                //页面加载完成后执行验证避免出现错误的验证提示
                _this.$nextTick(function () {
                    _this.$refs.formUserData.validate();
                });
            });
        },
        doSaveForm: function () {
            var _this = this;
           //
            var formEx = this.$refs["userFormEx"].doSubmitForm()
            if (!formEx.isSubmit) {
                return false;
            }
            //获取扩展数据
            this.formData.userInfoExtensJson = JSON.stringify(formEx.formData);

            this.$refs["formUserData"].validate(
                function (valid) {
                    if (valid) {
                        //
                        abp.ajax({
                            url: '/SysAccount/SaveUserModel',
                            data: JSON.stringify(_this.formData)
                            //type: 'POST'
                        }).done(function (data, res, e) {
                            /*
                             data = 返回值(自定义的)
                             res   = ABP封装的返回对象
                             e      = 原生ajax返回对象
                             */
                            _this.pageOptions.formDialog = false;
                            _this.getUserList();
                            _this.tipSuccess('save');
                        });
                        //基本上可以忽略监控fail,abp已经完成了这部分处理
                        //fail(function (res, e) { });

                    } else {
                        _this.tipShow('warn', 'verifyData');
                        return false;
                    }
                }
            );
        },
        doDelForm: function () {
            var _this = this;

            if (this.tableOptions.selectRows.length === 0) {
                this.tipShow('warn', 'IsSelect');
                return;
            }
            this.$confirm('确定删除所选择的用户?', '提示', {
                //confirmButtonText: '确定',
                //cancelButtonText: '取消',
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/SysAccount/DelUserModel',
                    data: JSON.stringify(_this.tableOptions.selectRows),
                    type: 'POST'
                }).done(function (data, res, e) {
                    _this.tipSuccess('del');
                    _this.refreshGrid();
                });
            }).catch(function (action) {
                //取消操作必须有避免js链式调用报异常
            });
        },
        doResetPass: function () {
            var _this = this;
            if (!this.tableOptions.selectRow.id) {
                this.tipShow('warn', 'IsSelect');
                return;
            }
            this.$confirm('确定重置所选择用户的密码?', '提示', { type: 'warning' })
                .then(function () {
                    abp.ajax({
                        url: '/SysAccount/ResetUserPass',
                        data: JSON.stringify(_this.tableOptions.selectRow.id)
                    }).done(function (data, res, e) {
                        _this.tipSuccess('密码重置成功,新密码' + data);
                    });
                    //基本上可以忽略监控fail,abp已经完成了这部分处理
                    //fail(function (res, e) { });
                }).catch(function (action) {
                    //取消操作必须有避免js链式调用报异常
                });
        },
        formatterSex: function (row, column) {
            if (row.sex === "1") {
                return '男';
            } else if (row.sex === "0") {
                return '女';
            } else {
                return '其他';
            }
        },
        validateUserCode: function (rule, value, callback) {
            var reg = /^[0-9a-zA-Z_]+$/;
            if (!value) {
                callback();
                return;
            }
            if (!reg.test(value)) {
                callback(new Error());
                return;
            }
            //验证通过必须执行
            callback();
        },
        handleSizeChange: function (val) {
            this.pageOptions.maxResultCount = val;
            this.getUserList();
        },
        handleCurrentChange: function (val, e) {
            this.pageOptions.pageIndex = val;
            this.getUserList();
        }
    }
});
