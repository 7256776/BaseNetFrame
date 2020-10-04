
var component = Vue.component('sys-roles', {
    //template: $.ajax({ url: abp.appPath + 'SysRole/Index', async: false }).responseText,  //等同于 Vue.frameTemplate('SysRole/Index')
    template: Vue.frameTemplate('SysRole/Index'),
    created: function () {
        this.getDataList();
    },
    data: function () {
        return {
            gridDataMenu: [],
            //defaultProps: {
            //    children: 'childrenMenus',
            //    label: 'menuDisplayName',
            //    value: 'id'
            //},
            formRules: {
                roleName: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    { min: 1, max: 30, message: '长度在 1 到 30 个字符', trigger: 'blur' }
                ]
            },
            formData: {
                id: null,
                roleName: null,
                description: null,
                isActive: true
            },
            pageOptions: {
                menuDialog: false,
                userDialog: false,
                formDialog: false,
            },
            tableOptions: {
                tableData: [],
                selectRows: [],
                selectRow: {},
                isCheckAll: false
            },
            transferOptions: {
                props: { key: 'id', label: 'userName' },
                titles: ['待授权用户', '已授权用户'],
                buttonTexts: ['取消授权', '确定授权'],
                renderFunc: function (h, option) {
                    return h('span', option.userNameCn + ' [' + option.userCode + ']');
                },
                dataRoleUser: [],
                currentRoleUser: [],
                filterMethod: function (query, item) {
                    if (item.userNameCn.toUpperCase().indexOf(query.toUpperCase()) > -1 || item.userCode.toUpperCase().indexOf(query.toUpperCase()) > -1) {
                        return true;
                    }
                    return false;
                }
            },
        };
    },
    watch: {
        //监听 
    },
    methods: {
        doActionsCell: function (row) {
            if (!row.sysMenuActions) {
                return;
            }
            //查询授权动作是否被选中
            var isActionsCheck = row.sysMenuActions.some(function (item, index) {
                if (item.isCheck) {
                    return true;
                }
            });
            //动作菜单被选中同时选中相关的父节点对象
            if (isActionsCheck) {
                row.isCheck = isActionsCheck;
                this.doSetMenuGridParentSelect(row, this.gridDataMenu);
            }
        },
        doRowSelectChange: function (selection) {
            this.tableOptions.selectRows = selection;
        },
        doRowclick: function (row, event, column) {
            //this.$refs.dataGrid.clearSelection();
            //设置选中行
            this.$refs.dataGrid.toggleRowSelection(row);
            //
            this.tableOptions.selectRow = row;
        },
        getDataList: function () {
            var _this = this;
            abp.ajax({
                url: '/SysRole/GetRoleList',
                data: JSON.stringify(_this.formData)
                //type: 'POST'
            }).done(function (data) {
                _this.tableOptions.tableData = data;
            });
        },
        doAddRoleMenu: function () {
            if (this.tableOptions.selectRows.length === 0) {
                this.tipShow('error', 'IsSelect');
                return;
            }
            this.pageOptions.menuDialog = true;
        },
        doRoleMenu: function () {
            var _this = this;
            abp.ajax({
                url: '/SysRole/GetMenusListOrderByRole',
                data: JSON.stringify(_this.tableOptions.selectRow.id)
            }).done(function (data, res, e) {
                _this.gridDataMenu = data;
                //特殊处理全选按钮的显示状态
                _this.$nextTick(function () {
                    _this.gridDataMenu.forEach(function (item, index) {
                        _this.$refs.dataGridMenu.toggleRowSelection(item, item.isCheck);
                    });
                });
            });
        },
        doUserMenu: function () {
            if (this.tableOptions.selectRows.length === 0) {
                this.tipShow('error', 'IsSelect');
                return;
            }
            this.pageOptions.userDialog = true;
            var _this = this;
            abp.ajax({
                url: '/SysRole/GetRoleToUser',
                data: JSON.stringify(_this.tableOptions.selectRow.id)
            }).done(function (data, res, e) {
                //获取所有用户
                _this.transferOptions.dataRoleUser = data.roleNotUser;
                //初始设置已授权用户
                _this.transferOptions.currentRoleUser = [];
                //设置已授权用户
                if (data.roleInUser) {
                    data.roleInUser.forEach(function (item, index) {
                        _this.transferOptions.currentRoleUser.push(item.userId);
                    });
                }
            });

        },
        doSetMenuGridChildrenSelect: function (list, isCheck) {
            var _this = this;
            if (!list || list.length == 0) {
                return;
            }
            list.forEach(function (item, index) {
                //设置所有子节点的选中状态
                item.isCheck = isCheck;
                //设置动作节点的选中状态
                if (item.sysMenuActions) {
                    item.sysMenuActions.forEach(function (item, index) {
                        item.isCheck = isCheck;
                    })
                }
                //递归
                _this.doSetMenuGridChildrenSelect(item.childrenMenus, isCheck);
            });
        },
        doSetMenuGridParentSelect: function (row, dataList) {
            var _this = this;
            if (!row || !dataList || dataList.length == 0) {
                return;
            }
            //设置父节点的选中状态
            dataList.forEach(function (item, index) {
                if (item.id == row.parentID) {
                    item.isCheck = row.isCheck;
                    //所有节点中查询父节点
                    _this.doSetMenuGridParentSelect(item, _this.gridDataMenu);
                } else {
                    //当前节点下查询父节点
                    _this.doSetMenuGridParentSelect(row, item.childrenMenus);
                }
                 //特殊处理全选按钮的显示状态
                if (!item.parentID) {
                    _this.$refs.dataGridMenu.toggleRowSelection(item, item.isCheck);
                }
            });
        },
        doMenuGridSelectAll: function (selection) {
            var _this = this;
            //通过查询当前根节点是否选中来确定是否全选状态
            var isCheck = selection.some(function (item, index) {
                if (!item.parentID) {
                    return true;
                }
            })
            //设置根节点模块选择状态
            this.gridDataMenu.forEach(function (item, index) {
                item.isCheck = isCheck;
            });
            //设置所有节点选择状态
            this.doSetMenuGridChildrenSelect(this.gridDataMenu, isCheck);
        },
        doMenuGridSelect: function (row) {
            //特殊处理全选按钮的显示状态
            if (!row.parentID) {
                this.$refs.dataGridMenu.toggleRowSelection(row, row.isCheck);
            }
            var list = [row];
            //
            this.doSetMenuGridChildrenSelect(list, row.isCheck);
            //同时设置当前节点上级节点为选中状态
            if (row.isCheck) {
                this.doSetMenuGridParentSelect(row, this.gridDataMenu);
            }
        },
        doSaveRoleUser: function () {
            var _this = this;
            var dataJson = {
                roleToUserList: [],
                roleID: _this.tableOptions.selectRow.id
            };
            this.transferOptions.currentRoleUser.forEach(function (item, index) {
                dataJson.roleToUserList.push({
                    userID: item,
                    roleID: _this.tableOptions.selectRow.id
                });
            });
            //
            abp.ajax({
                url: '/SysRole/SaveRoleToUser',
                data: JSON.stringify(dataJson),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.pageOptions.userDialog = false;
                _this.tipSuccess('save');
            }).fail(function (res, e) {
                //console.log(res)
            });

        },
        doSaveRoleMenus: function () {
            var _this = this;
           //
            this.tableOptions.selectRow["menusActionList"] = _this.gridDataMenu;
            abp.ajax({
                url: '/SysRole/SaveRoleToMenu',
                data: JSON.stringify(_this.tableOptions.selectRow),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.pageOptions.menuDialog = false;
                _this.tipSuccess('save');
            }).fail(function (res, e) {
                //console.log(res)
            });
        },
        doAdd: function () {
            this.pageOptions.formDialog = true;
            var _this = this;
            this.$nextTick(function () {
                _this.$refs.formInputData.resetFields();
            });
        },
        doEdit: function () {
            if (!this.tableOptions.selectRow.id) {
                this.tipShow('error', 'IsSelect');
                return;
            }
            var _this = this;
            this.pageOptions.formDialog = true;
            abp.ajax({
                url: '/SysRole/GetRoleModel',
                data: JSON.stringify(_this.tableOptions.selectRow.id)
            }).done(function (data) {
                _this.formData = data;
                //页面加载完成后执行验证避免出现错误的验证提示
                _this.$nextTick(function () {
                    _this.$refs.formInputData.validate();
                });
            });
        },
        doSaveForm: function () {
            var _this = this;
            this.$refs["formInputData"].validate(
                function (valid) {
                    if (valid) {
                        //
                        abp.ajax({
                            url: '/SysRole/SaveRoleModel',
                            data: JSON.stringify(_this.formData),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            _this.getDataList();
                            _this.pageOptions.formDialog = false
                            _this.tipSuccess('save');
                            _this.$refs.dataGrid.clearSelection();
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
                this.tipShow('error', 'IsSelect');
                return;
            }
            this.$confirm('确定删除?', '提示', {
                //confirmButtonText: '确定',
                //cancelButtonText: '取消',
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/SysRole/DelRoleModel',
                    data: JSON.stringify(_this.tableOptions.selectRows),
                    type: 'POST'
                }).done(function (data, res, e) {
                    _this.tipSuccess('del');
                    _this.getDataList();
                });
            });
        },

    } 
});
