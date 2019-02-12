
var component = Vue.component('j-roles', {
    template: $.ajax({ url: abp.appPath + 'J_Role/Index', async: false }).responseText,
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
                formDialog: false
            },
            tableOptions: {
                tableData: [],
                selectRows: [],
                selectRow: {}
            },
            transferOptions: {
                props: { key: 'id', label: 'userName' },
                titles: ['待授权用户', '已授权用户'],
                buttonTexts: ['取消授权', '确定授权'],
                renderFunc: function (h, option) {
                    return h('span',  option.userNameCn + ' [' + option.userCode + ']');
                },
                dataRoleUser: [],
                currentRoleUser: [],
                filterMethod: function (query, item) {
                    if (item.userNameCn.toUpperCase().indexOf(query.toUpperCase()) > -1 || item.userCode.toUpperCase().indexOf(query.toUpperCase()) > -1) {
                        return true;
                    }
                    return false;
                }
            }
        };
    },
    watch: {
    //监听
    },
    methods: {       
        tableRowClassName: function (e) {
            //根节点固定为显示状态
            if (e.row.menuNodeLevel === 1) {
                return '';
            }
            if (!e.row.isExpand) {
                return 'display:none';
            }
        },
        doCell: function (row) {
            //判断是否查询所有子节点
            var isAll = false;
            //设置图标状态
            if (!row["expandState"]) {
                row["expandState"] = "minus";
            } else if (row["expandState"] === "minus") {
                row["expandState"] = "plus";
                isAll = true;
            } else {
                row["expandState"] = "minus";
            }

            this.gridDataMenu.forEach(function (item, index) {
                //var isExpand = false;
                if (row.id === item.parentID) {
                    //设置展开或收缩
                    item.isExpand = !item.isExpand;
                    //设置图标状态
                    item["expandState"] = item.isExpand ? "plus" : "minus";
                }
                //设置所有子节点收缩
                if (isAll && item.menuNode.indexOf(row.menuNode) >= 0) {
                    if (item.menuNode !== row.menuNode) {
                        item.isExpand = false;
                    }
                    item["expandState"] = "plus";
                }
            });
            //
            this.$refs["dataGrid"].doLayout();
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
                url: '/J_Role/GetRoleList' ,
                data: JSON.stringify(_this.formData)
                //type: 'POST'
            }).done(function (data) {
                _this.tableOptions.tableData = data;
                //abp.message.success('created new person with id = ' + data.personId);
            });
        },
        doRoleMenu: function () {
            if (this.tableOptions.selectRows.length === 0) {
                abp.message.warn('请选择一行数据');
                return;
            }
            this.pageOptions.menuDialog = true;

            var _this = this;
            abp.ajax({
                url: '/J_Role/GetMenusListOrderByRole',
                data: JSON.stringify(_this.tableOptions.selectRow.id)
            }).done(function (data, res, e) {
                _this.gridDataMenu = data;
                //设置授权的菜单为选中状态
                _this.$nextTick(function () {
                    _this.gridDataMenu.forEach(function (item, index) {
                        _this.$refs.dataGridMenu.toggleRowSelection(item, item.isCheck);
                    });
                });
            });
        },
        doUserMenu: function () {
            if (this.tableOptions.selectRows.length === 0) {
                abp.message.warn('请选择一行数据');
                return;
            }
            this.pageOptions.userDialog = true;
            var _this = this;
            abp.ajax({
                url: '/J_Role/GetRoleToUser',
                data: JSON.stringify(_this.tableOptions.selectRow.id)
            }).done(function (data, res, e) {
                //获取所有用户
                _this.transferOptions.dataRoleUser = data.roleNotUser;
                //初始设置已授权用户
                _this.transferOptions.currentRoleUser = [];
                //设置已授权用户
                data.roleInUser.forEach(function (item, index) {
                    _this.transferOptions.currentRoleUser.push(item.userId);
                });
            });
        },
        doMenuGridSelectAll: function (selection) {
            var _this = this;
            var isCheck = false;
            if (selection.length > 0) {
                isCheck = true;
            }
            //设置所有子节点被选中
            this.gridDataMenu.forEach(function (item, index) {
                //设置所有子节点的选中状态
                item.isCheck = isCheck;
                _this.$refs.dataGridMenu.toggleRowSelection(item, isCheck);
                //
                item.sysMenuActions.forEach(function (item, index) {
                    item.isCheck = isCheck;
                })
            });
        },
        doMenuGridSelect: function (selection, row) {
            var _this = this;
            //查询是否被选中
            row.isCheck = selection.some(function (item, index) {
                if (item.id === row.id) {
                    return true;
                }
            });
           
            //设置所有子节点被选中
            this.gridDataMenu.forEach(function (item, index) {
                //设置所有子节点的选中状态
                if (item.menuNode.indexOf(row.menuNode) >= 0) {
                    item.isCheck = row.isCheck;
                    _this.$refs.dataGridMenu.toggleRowSelection(item, row.isCheck);
                    //
                    item.sysMenuActions.forEach(function (itemSub, index) {
                        itemSub.isCheck = row.isCheck;
                    });
                }
                //此处只有子节点选中的情况才会设置相关父节点为选中状态,取消子节点不做调整
                if (row.menuNode.indexOf(item.menuNode) >= 0 && row.isCheck) {
                    item.isCheck = row.isCheck;//设置选择值
                    _this.$refs.dataGridMenu.toggleRowSelection(item, row.isCheck);//设置选择状态
                }
            });
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
                url: '/J_Role/SaveRoleToUser',
                data: JSON.stringify(dataJson),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.pageOptions.userDialog = false;
                abp.message.success('用户授权完成');
            }).fail(function (res, e) {
                //console.log(res)
            });

        },
        doSaveRoleMenus: function () {
            var _this = this;
           //
            this.tableOptions.selectRow["menusActionList"] = _this.gridDataMenu;
            abp.ajax({
                url: '/J_Role/SaveRoleToMenu',
                data: JSON.stringify(_this.tableOptions.selectRow),
                type: 'POST'
            }).done(function (data, res, e) {
               
                _this.pageOptions.menuDialog = false;
                abp.message.success('模块授权完成');
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
                abp.message.warn('请选择一行数据');
                return;
            }
            var _this = this;
            this.pageOptions.formDialog = true;
            abp.ajax({
                url: '/J_Role/GetRoleModel',
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
                            url: '/J_Role/SaveRoleModel',
                            data: JSON.stringify(_this.formData),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            _this.getDataList();
                            _this.pageOptions.formDialog = false
                            abp.message.success('保存成功');
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
                abp.message.warn('请选择一行数据');
                return;
            }
            this.$confirm('确定删除?', '提示', {
                //confirmButtonText: '确定',
                //cancelButtonText: '取消',
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/J_Role/DelRoleModel',
                    data: JSON.stringify(_this.tableOptions.selectRows),
                    type: 'POST'
                }).done(function (data, res, e) {
                    //
                    abp.message.success('删除成功');
                    _this.getDataList();
                });
            });
        }

    } 
});
