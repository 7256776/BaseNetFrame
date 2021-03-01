var component = Vue.component('sys-flowconfig',
{
    template: Vue.frameTemplate('SysFlowDesigner/FlowConfig'),
    components: {
        flowuserselect: componentAssemble.FlowUserSelect,
    },
    created: function () {
        this.getWorkFlowRoleDataList();
    },
    mounted: function () {
        var data = this.$refs["userDataGrid"];
    },
    data: function () {
        return {
            roleOptions: {
                tableData: [],
                currentRoleId: '',
                formDialog: false,
            },
            roleFormData: {
                id: '',
                flowRoleName: '',
                isActive: true,
                description: '',
            },
            formRules: {
                flowRoleName: [
					{ required: true, message: '必填项', trigger: 'blur' },
					{ min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                ],
            },
            userTableOptions: {
                tableData: [],
                selectRows: []
            },
            userPageOptions: {
                params: {
                    userCodeOrName: '',
                    flowRoleId: '',
                },
                pageIndex: 1,
                pageSize: 20,
                total: 0
            },
            //用户选择组件
            flowUserSelectOptions: {
                visible: false,
                selectData: []
            },

            //流程类型设置
            flowTypeOptions: {
                params: {
                    flowTypeName: '',
                },
                formDialog: false,
                tableData: [],
                selectRows: [],
                selectRow: {}
            },
            flowTypeFormData: {
                id: '',
                flowTypeName: '',
                isActive: true,
                isReadOnly: true,
                description: '',
            },
            flowTypeRules: {
                flowTypeName: [
					{ required: true, message: '必填项', trigger: 'blur' },
					{ min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                ],
            },

            //流程数据源设置
            flowDataSourceOptions: {
                params: {
                    dataSourceName: '',
                },
                formDialog: false,
                tableData: [],
                selectRows: [],
                selectRow: {}
            },
            flowDataSourceFormData: {
                id: '',
                dataSourceType: '',
                dataSourceName: '',
                dataSourceWay: '',
                isActive: true,
                description: '',
            },
            flowDataSourceRules: {
                dataSourceName: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    { min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                ],
                dataSourceType: [
                   { required: true, message: '必填项', trigger: 'blur' },
                   { min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                ],
            },

        }
    },
    watch: {
        //监听
    },
    computed: {
        //计算
    },
    methods: {
        doNav: function (page) {
            if (page == 'tabRole') {
                this.getWorkFlowRoleDataList();
            } else if (page == 'tabFlowType') {
                this.getWorkFlowTypeDataList();
            } else if (page == 'tabFlowDataSource') {
                this.getWorkFlowDataSourceList();
            }
        },
        getWorkFlowRoleDataList: function () {
            var _this = this;
            abp.ajax({
                url: '/SysFlowDesigner/GetWorkFlowRoleList',
            }).done(function (data) {
                //
                _this.roleOptions.currentRoleId = null;
                if (data.length > 0) {
                    _this.roleOptions.currentRoleId = data[0].id;  
                    _this.refreshUserByRole();
                }
                //
                _this.roleOptions.tableData = data;
            });
        },
        doClickNode: function (id) {
            this.roleOptions.currentRoleId = id;
            this.userPageOptions.pageIndex = 1;
            this.refreshUserByRole();
        },
        doRoleSaveForm: function () {
            var _this = this;
            this.$refs["formRoleEl"].validate(
				function (valid) {
				    if (valid) {
				        //
				        abp.ajax({
				            url: '/SysFlowDesigner/SaveWorkFlowRole',
				            data: JSON.stringify(_this.roleFormData),
				            type: 'POST'
				        }).done(function (data, res, e) {
				            _this.roleOptions.formDialog = false
				            _this.tipSuccess('save');
				            _this.getWorkFlowRoleDataList();
				        });
				    } else {
				        return false;
				    }
				});
        },
        doRoleAdd: function () {
            this.roleOptions.formDialog = true;
            var _this = this;
            this.$nextTick(function () {
                _this.$refs["formRoleEl"].resetFields();
            });

        },
        doRoleEdit: function () {
            var _this = this;
            if (!_this.roleOptions.currentRoleId) {
                this.tipShow('warn', 'IsSelect');
                return;
            } 
            this.roleOptions.formDialog = true;
            abp.ajax({
                url: '/SysFlowDesigner/GetRoleModel',
                data: JSON.stringify(_this.roleOptions.currentRoleId)
            }).done(function (data, res, e) {
                if (!data) {
                    _this.tipShow('error', '未获取到数据');
                    return;
                }
                _this.roleFormData = data;
                _this.$refs["formRoleEl"].clearValidate();
            });
        },
        doRoleDel: function () {
            var _this = this;
            if (!this.roleOptions.currentRoleId) {
                this.tipShow('error', 'IsSelect');
                return;
            }
            this.$confirm('确定删除?', '提示', {
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/SysFlowDesigner/DelRoleModel',
                    data: JSON.stringify(_this.roleOptions.currentRoleId)
                }).done(function (data, res, e) {
                    _this.tipSuccess('del');
                    _this.getWorkFlowRoleDataList();
                });
            }).catch(function (action) {
                //取消操作必须有避免js链式调用报异常
            });

        },
        doRowSelectChange: function (selection) {
            this.userTableOptions.selectRows = selection
        },
        doRowclick: function (row, event, column) {
            //设置选中行
            this.$refs["gridRoleToUserEl"].toggleRowSelection(row);
        },
        //用户列表
        refreshUserByRole: function () {
            this.userPageOptions.pageIndex = 1;
            this.getUserByRole();
        },
        getUserByRole: function () {
            var _this = this;
            //
            this.userPageOptions.params.flowRoleId = this.roleOptions.currentRoleId;
            abp.ajax({
                url: '/SysFlowDesigner/GetFlowUserPaging',
                data: JSON.stringify(_this.userPageOptions)
            }).done(function (data, res, e) {
                _this.userPageOptions.total = data.totalCount;
                _this.userTableOptions.tableData = data.resultData;
                _this.userTableOptions.selectRows = [];
            });
        },
        handleSizeChange: function (val) {
            this.userPageOptions.pageSize = val;
            this.getUserByRole();
        },
        handleCurrentChange: function (val, e) {
            this.userPageOptions.pageIndex = val;
            this.getUserByRole();
        },
        //关联用户设置
        doLinkUser: function () {
            var _this = this;
            if (!this.roleOptions.currentRoleId) {
                this.tipShow('warn', '提示消息', '请选择一条流程角色');
                return;
            }
            this.flowUserSelectOptions.visible = true;
        },
        doDelLinkUser: function () {
            var _this = this;
            if (this.userTableOptions.selectRows.length==0) {
                this.tipShow('warn', '提示消息', '请选择用户');
                return;
            }
            var roleUser = {
                flowRoleID: this.roleOptions.currentRoleId,
                userList: []
            };
            //
            this.userTableOptions.selectRows.forEach(function (item, index) {
                roleUser.userList.push(item.userId);
            });
            this.$confirm('确定移除?', '提示', {
                type: 'warning'
            }).then(function () {
                //
                abp.ajax({
                    url: '/SysFlowDesigner/DelWorkFlowRoleToUser',
                    data: JSON.stringify(roleUser)
                }).done(function (data, res, e) {
                    _this.refreshUserByRole();
                    _this.tipSuccess('del');
                });
            }).catch(function (action) {
                //取消操作必须有避免js链式调用报异常
            });
        },
        doClosed: function () {
            var _this = this;
            var roleUser = {
                flowRoleID: this.roleOptions.currentRoleId,
                userList: []
            };
            //
            this.flowUserSelectOptions.selectData.forEach(function (item, index) {
                roleUser.userList.push(item.userId);
            });
            this.flowUserSelectOptions.selectData = [];
            //
            abp.ajax({
                url: '/SysFlowDesigner/SaveWorkFlowRoleToUser',
                data: JSON.stringify(roleUser)
            }).done(function (data, res, e) {
                //判断是否有数据建立关系(后台对重复添加关联用户进行了验证)
                if (data > 0) {
                    _this.refreshUserByRole();
                    _this.tipSuccess('关联用户');
                }
            });
        },

        //审核流程类型 设置
        doFlowRowSelectChange: function (selection) {
            this.flowTypeOptions.selectRows = selection
        },
        doFlowRowclick: function (row, event, column) {
            //设置选中行
            this.$refs["gridFlowEl"].toggleRowSelection(row);
            //
            this.flowTypeOptions.selectRow = row;
        },
        getWorkFlowTypeDataList: function () {
            var _this = this;
            abp.ajax({
                url: '/SysFlowDesigner/GetWorkFlowTypeList',
                data: JSON.stringify(_this.flowTypeOptions.params)
            }).done(function (data) {
                _this.flowTypeOptions.tableData = data;
                _this.flowTypeOptions.selectRows = [];
                _this.flowTypeOptions.selectRow = {};
            });
        },
        //
        doAddFlow: function () {
            var _this = this;
            this.flowTypeOptions.formDialog = true
            this.$nextTick(function () {
                _this.$refs["formFlowTypeEl"].resetFields();
            });
        },
        doEditFlow: function () {
            var _this = this;
            if (!this.flowTypeOptions.selectRow.id) {
                this.tipShow('warn', 'IsSelect');
                return;
            }
            this.flowTypeOptions.formDialog = true
            abp.ajax({
                url: '/SysFlowDesigner/GetWorkFlowTypeModel',
                data: JSON.stringify(_this.flowTypeOptions.selectRow.id)
            }).done(function (data, res, e) {
                if (!data) {
                    _this.tipShow('error', '未获取到数据');
                }
                _this.flowTypeFormData = data;
            });
        },
        doDelFlow: function () {
            var _this = this;
            if (this.flowTypeOptions.selectRows.length==0) {
                this.tipShow('warn', 'IsSelect');
                return;
            }
            var ids = [];
            //
            this.flowTypeOptions.selectRows.forEach(function (item, index) {
                ids.push(item.id);
            });

            this.$confirm('确定移除?', '提示', {
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/SysFlowDesigner/DelWorkFlowType',
                    data: JSON.stringify(ids)
                }).done(function (data, res, e) {
                    //判断是否有数据建立关系(后台对重复添加关联用户进行了验证)
                    _this.tipSuccess('del');
                    _this.getWorkFlowTypeDataList();

                });
            }).catch(function (action) {
                //取消操作必须有避免js链式调用报异常
            });
        },
        doFlowTypeSaveForm: function () {
            var _this = this;
            this.$refs["formFlowTypeEl"].validate(
				function (valid) {
				    if (valid) {
				        //
				        abp.ajax({
				            url: '/SysFlowDesigner/SaveWorkFlowType',
				            data: JSON.stringify(_this.flowTypeFormData),
				            type: 'POST'
				        }).done(function (data, res, e) {
				            _this.flowTypeOptions.formDialog = false
				            _this.tipSuccess('save');
				            _this.getWorkFlowTypeDataList();
				        });
				    } else {
				        return false;
				    }
				});
        },

        //审核流程数据源 设置
        doFlowDataSourceRowSelectChange: function (selection) {
            this.flowDataSourceOptions.selectRows = selection
        },
        doFlowDataSourceRowclick: function (row, event, column) {
            //设置选中行
            this.$refs["gridFlowDataSourceEl"].toggleRowSelection(row);
            //
            this.flowDataSourceOptions.selectRow = row;
        },
        getWorkFlowDataSourceList: function () {
            var _this = this;
            abp.ajax({
                url: '/SysFlowDesigner/GetWorkFlowDataSourceList',
                data: JSON.stringify(_this.flowDataSourceOptions.params)
            }).done(function (data) {
                _this.flowDataSourceOptions.tableData = data;
                _this.flowDataSourceOptions.selectRow = {};
                _this.flowDataSourceOptions.selectRows = [];
            });
        },
        //
        doAddFlowDataSource: function () {
            var _this = this;
            this.flowDataSourceOptions.formDialog = true
            this.$nextTick(function () {
                _this.$refs["formFlowDataSourceEl"].resetFields();
            });
        },
        doEditFlowDataSource: function () {
            var _this = this;
            if (!this.flowDataSourceOptions.selectRow.id) {
                this.tipShow('warn', 'IsSelect');
                return;
            }
            this.flowDataSourceOptions.formDialog = true
            abp.ajax({
                url: '/SysFlowDesigner/GetWorkFlowDataSource',
                data: JSON.stringify(_this.flowDataSourceOptions.selectRow.id)
            }).done(function (data, res, e) {
                if (!data) {
                    _this.tipShow('error', '未获取到数据');
                }
                _this.flowDataSourceFormData = data;
            });
        },
        doDelFlowDataSource: function () {
            var _this = this;
            if (this.flowDataSourceOptions.selectRows.length == 0) {
                this.tipShow('warn', 'IsSelect');
                return;
            }
            var ids = [];
            //
            this.flowDataSourceOptions.selectRows.forEach(function (item, index) {
                ids.push(item.id);
            });
            this.$confirm('确定移除?', '提示', {
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/SysFlowDesigner/DelWorkFlowDataSource',
                    data: JSON.stringify(ids)
                }).done(function (data, res, e) {
                    //判断是否有数据建立关系(后台对重复添加关联用户进行了验证)
                    _this.tipSuccess('del');
                    _this.getWorkFlowDataSourceList();

                });
            }).catch(function (action) {
                //取消操作必须有避免js链式调用报异常
            });
        },
        doFlowDataSourceSaveForm: function () {
            var _this = this;
            this.$refs["formFlowDataSourceEl"].validate(
                function (valid) {
                    if (valid) {
                        //
                        abp.ajax({
                            url: '/SysFlowDesigner/SaveWorkFlowDataSource',
                            data: JSON.stringify(_this.flowDataSourceFormData),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            _this.flowDataSourceOptions.formDialog = false
                            _this.tipSuccess('save');
                            _this.getWorkFlowDataSourceList();
                        });
                    } else {
                        return false;
                    }
                });
        },

    }
});
