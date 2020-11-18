
var component = Vue.component('sys-authorization', {
    template: Vue.frameTemplate('SysAuthorization/Index'),
    components: {
        //userinfoextens: componentAssemble.SysUserInfoExtens
    },
    //updated: function () {
    //    var isShowUserInfo = this.$refs["userFormEx"];
    //},
    //mounted: function () {
    //    var isShowUserInfo = this.$refs["userFormEx"];
    //},
    created: function () {
        this.getApiResourceDataListAll();
    },
    data: function () {
        return {
            formRules: {
                resourceName: [
                    { required: true, message: '请设置资源名称', trigger: 'blur' },
                    { validator: this.validateName, trigger: 'blur' }
                ],
                resourceDisplayName: [
                    { required: true, message: '请设置资源名称', trigger: 'blur' },
                    { min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                ],
                clientId: [
                    { required: true, message: '请设置客户名称', trigger: 'blur' },
                    { validator: this.validateName, trigger: 'blur' }
                ],
                clientSecrets: [
                    { required: true, message: '请设置客户密钥', trigger: 'blur' },
                    { validator: this.validateName, trigger: 'blur' }
                ],
                userName: [
                    { required: true, message: '请设置账号名称', trigger: 'blur' },
                    { validator: this.validateName, trigger: 'blur' }
                ],
                password: [
                    { required: true, message: '请设置账号秘钥', trigger: 'blur' },
                    { min: 6, message: '秘钥长度至少6个字符', trigger: 'blur' },
                    { validator: this.validateName, trigger: 'blur' }
                ],
            },
            
            resourceFormData: {
                id: '',
                resourceName: '',
                resourceDisplayName: '',
                isActive: true,
                description: '',
                extensionData: '',
                creationTime: '',
            },
            resourceOptions: {
                tableData: [],
                tableFormData: [],
                apiResourceCurrentId: '',
                formDialog: false,
            },

            clientFormData: {
                id: null,
                clientId: null,
                clientSecrets: null,
                accessTokenLifetime: "1",
                allowOfflineAccess: true,
                slidingRefreshTokenLifetime: '',
                description: '',
                extensionData: '',
                apiResourceId: '',
                isActive: true
            },
            clientTableOptions: {
                clientTableData: [],
                tableData: [],
                tableFormData: [],
                selectRows: [],
                selectRow: {}
            },
            clientOptions: {
                searchTxt: '',
                formDialog: false,
            },


            accountFormData: {
                id: null,
                userName: null,
                password: null,
                password: null,
                description: "",
                extensionData: '',
                apiClientId: '',
                isActive: true
            },
            accountTableOptions: {
                tableData: [],
            },
            accountOptions: {
                formDialog: false,
                accountCurrentId: '',
                searchTxt: '',
            },

            permissionTableOptions: {
                searchTxt: '',
                tableData: []
            },
        };
    },
    watch: {
        //监听
    },
    methods: {
        doNav: function (page) {
            if (page == 'Resource') {
                this.getApiResourceDataListAll();
            } else if (page == 'Client') {
                this.getClientDataAll();
                this.getApiResourceDataList();
            } else if (page == 'Account') {
                this.getAccountData();
                this.getClientData();
            }
        },
        validateName: function (rule, value, callback) {
            if (!value) {
                callback();
                return;
            }
            if (!abp.frameCore.utils.checkChars(value, ['en', 'num'])) {
                callback(new Error('仅限数字,字母组成'));
                return;
            }
            //验证通过执行 
            callback();
        },
         
        doRowExpandChange: function (row, expanded, e) {
            var isRow = expanded.some(function (item, index) {
                if (row.id == item.id) {
                    return true;
                }
            });
            if (!isRow) {
                return;
            }
            this.getAccountByClient(row);
        },
        getApiResourceDataList: function () {
            var _this = this;
            abp.ajax({
                url: '/SysAuthorization/GetSysApiResourceList',
            }).done(function (data, res, e) {
                _this.resourceOptions.tableFormData = data;
            });
        },
        getApiResourceDataListAll: function () {
            var _this = this;
            abp.ajax({
                url: '/SysAuthorization/GetSysApiResourceAllList',
            }).done(function (data, res, e) {
                _this.resourceOptions.apiResourceCurrentId = null;
                if (data.length > 0) {
                    _this.resourceOptions.apiResourceCurrentId = data[0].id;
                    _this.getClientByResource(_this.resourceOptions.apiResourceCurrentId);
                }
                _this.resourceOptions.tableData = data; 
            });
        },
        getClientByResource: function (id) {
            var _this = this;
            this.resourceOptions.apiResourceCurrentId = id;
            abp.ajax({
                url: '/SysAuthorization/GetApiClientByResource',
                data: JSON.stringify(id)
            }).done(function (data, res, e) {
                _this.clientTableOptions.clientTableData = data;
            });
        },
        getAccountByClient: function (row) {
            var _this = this;
            abp.ajax({
                url: '/SysAuthorization/GetApiAccountByClient',
                data: JSON.stringify(row.id)
            }).done(function (data, res, e) {
              
                    row.sysApiAccountList = data;
             
            });
        },
        doResourceAdd: function () {
            var _this = this;
            this.resourceOptions.formDialog = true;
            this.$nextTick(function () {
                _this.$refs["formResourceData"].resetFields();
            });
        },
        doResourceEdit: function () {
            var _this = this;
            this.resourceOptions.formDialog = true;
            abp.ajax({
                url: '/SysAuthorization/GetSysApiResource',
                data: JSON.stringify(_this.resourceOptions.apiResourceCurrentId)
            }).done(function (data, res, e) {
                if (!data) {
                    _this.tipShow('error', '未获取到数据');
                    return;
                }
                _this.resourceFormData = data;
                _this.$refs["formResourceData"].clearValidate();
            });
        },
        doResourceDel: function () {
            var _this = this;
            if (!this.resourceOptions.apiResourceCurrentId) {
                this.tipShow('error', 'IsSelect');
                return;
            }
            this.$confirm('确定删除?', '提示', {
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/SysAuthorization/DelSysApiResource',
                    data: JSON.stringify(_this.resourceOptions.apiResourceCurrentId)
                }).done(function (data, res, e) {
                    _this.tipSuccess('del');
                    _this.getApiResourceDataListAll();
                });
            }).catch(function () { });
        },
        doResourceSave: function () {
            var _this=this;
            this.$refs["formResourceData"].validate(
             function (valid) {
                 if (!valid) {
                     return false;
                 }
                 abp.ajax({
                     url: '/SysAuthorization/SaveSysApiResource',
                     data: JSON.stringify(_this.resourceFormData)
                 }).done(function (data, res, e) {
                     if (data == true) {
                         _this.tipSuccess('Save');
                         _this.getApiResourceDataListAll();
                         _this.resourceOptions.formDialog = false;
                         return;
                     }
                     _this.tipFail('Save');
                 });
             });
        },

        getClientData: function () {
            var _this = this;
            abp.ajax({
                url: '/SysAuthorization/GetSysApiClientList',
            }).done(function (data, res, e) {
                _this.clientTableOptions.tableFormData = data;
            });
        },
        getClientDataAll: function () {
            var _this = this;
            var client = {
                clientId: this.clientOptions.searchTxt
            }
            abp.ajax({
                url: '/SysAuthorization/GetSysApiClientAllList',
                data: JSON.stringify(client)
            }).done(function (data, res, e) {
                _this.clientTableOptions.tableData = data;
            });
        },
        doRowSelectChange: function (selection) {
            this.clientTableOptions.selectRows = selection
        },
        doRowclick: function (row, event, column) {
            //设置选中行
            this.$refs["clientDataGrid"].toggleRowSelection(row);
            //
            this.clientTableOptions.selectRow = row;
        },
        doSaveClientForm: function () {
            var _this = this;
            this.$refs["formClientData"].validate(
             function (valid) {
                 if (!valid) {
                     return false;
                 }
                 abp.ajax({
                     url: '/SysAuthorization/SaveSysApiClient',
                     data: JSON.stringify(_this.clientFormData)
                 }).done(function (data, res, e) {
                     if (data == true) {
                         _this.tipSuccess('Save');
                         _this.clientOptions.formDialog = false;
                     } else {
                         _this.tipFail('Save');
                     }
                     _this.getClientDataAll();
                 });
             });
        },
        doClientAdd: function () {
            var _this = this;
            this.clientOptions.formDialog = true;
            this.$nextTick(function () {
                _this.$refs.formClientData.resetFields();
            });
        },
        doClientEdit: function () {
            var _this = this;
            this.clientOptions.formDialog = true;
            abp.ajax({
                url: '/SysAuthorization/GetSysApiClient/',
                data: JSON.stringify(_this.clientTableOptions.selectRow.id),
                type: 'POST'
            }).done(function (data) {
                _this.clientFormData = data;
            });
        },
        doClientDel: function () {
            var _this = this;
            if (this.clientTableOptions.selectRows.length === 0) {
                this.tipSuccess('error', 'IsSelect');
                return;
            }
            var ids = [];
            this.clientTableOptions.selectRows.forEach(function (item,index) {
                ids.push(item.id);
            });

            this.$confirm('确定删除?', '提示', {
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/SysAuthorization/DelMultiSysApiClient',
                    data: JSON.stringify(ids)
                }).done(function (data, res, e) {
                    _this.tipSuccess('del');
                    _this.getClientDataAll();
                });
            }).catch(function () { });
        },

        getAccount: function (id) {
            this.accountOptions.accountCurrentId = id;
        },
        getAccountData: function () {
            var _this = this;
            var account = {
                userName: this.accountOptions.searchTxt
            }
            abp.ajax({
                url: '/SysAuthorization/GetSysApiAccountList',
                data: JSON.stringify(account)
            }).done(function (data, res, e) {
                _this.accountOptions.accountCurrentId = null;
                if (data.length > 0) {
                    _this.accountOptions.accountCurrentId = data[0].id;
                }
                _this.accountTableOptions.tableData = data;
            });
        },
        doAccountAdd: function () {
            var _this = this;
            this.accountOptions.formDialog = true;
            this.$nextTick(function () {
                _this.$refs["formAccountData"].resetFields();
            });
        },
        doAccountEdit: function () {
            var _this = this;
            this.accountOptions.formDialog = true;
            abp.ajax({
                url: '/SysAuthorization/GetSysApiAccount/',
                data: JSON.stringify(_this.accountOptions.accountCurrentId),
                type: 'POST'
            }).done(function (data) {
                _this.accountFormData = data;
            });
        },
        doAccountDel: function () {
            var _this = this;
            if (!this.accountOptions.accountCurrentId) {
                this.tipShow('error', 'IsSelect');
                return;
            }
            this.$confirm('确定删除?', '提示', {
                type: 'warning',
                callback: function (action, instance) {
                    if (action == 'cancel') {
                        return;
                    }
                    abp.ajax({
                        url: '/SysAuthorization/DelSysApiAccount',
                        data: JSON.stringify(_this.accountOptions.accountCurrentId)
                    }).done(function (data, res, e) {
                        _this.tipSuccess('del');
                        _this.getAccountData();
                    })
                }
            });
        },
        doSaveAccountForm: function () {
            var _this = this;
            this.$refs["formAccountData"].validate(
             function (valid) {
                 if (!valid) {
                     return false;
                 }
                 abp.ajax({
                     url: '/SysAuthorization/SaveSysApiAccount',
                     data: JSON.stringify(_this.accountFormData)
                 }).done(function (data, res, e) {
                     if (data == true) {
                         _this.tipSuccess('Save');
                         _this.accountOptions.formDialog = false;
                     } else {
                         _this.tipFail('Save');
                     }
                     _this.getAccountData();
                 });
             });
        },

        doResourceToClientDel: function (row) {
            var _this = this;
            var data = {
                apiResourceId: this.resourceOptions.apiResourceCurrentId,
                apiClientId: row.id
            };
          
            this.$confirm('确定移除关联?', '提示', {
                type: 'warning',
                callback: function (action, instance) {
                    if (action == 'cancel') {
                        return;
                    }
                    abp.ajax({
                        url: '/SysAuthorization/DelResourceToClient',
                        data: JSON.stringify(data)
                    }).done(function (data, res, e) {
                        _this.tipSuccess('del');
                        _this.getClientByResource(_this.resourceOptions.apiResourceCurrentId);
                    })
                }
            });
        },
        doClienToAccountDel: function (row, prow) {
            var _this = this;
            var data = {
                apiAccountId: row.id,
                apiClientId: prow.id
            };
          
            this.$confirm('确定移除关联?', '提示', {
                type: 'warning',
                callback: function (action, instance) {
                    if (action == 'cancel') {
                        return;
                    }
                    abp.ajax({
                        url: '/SysAuthorization/DelClienToAccount',
                        data: JSON.stringify(data)
                    }).done(function (data, res, e) {
                        _this.tipSuccess('del');
                        _this.getAccountByClient(prow);
                    })
                }
            });
        },




    }
});
