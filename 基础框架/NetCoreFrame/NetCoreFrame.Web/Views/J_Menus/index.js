
var component = Vue.component('j-menus', {
    template: Vue.frameTemplate('J_Menus/Index'),
    created: function () {
        this.initMenusData();
    },
    data: function () {
        return {
            tabActivatePage:"menu",
            treeData: [], 
            tableData: [],
            defaultProps: {
                children: 'childrenMenus',
                label: 'menuDisplayName',
                value: 'id'
            },
            formRules: {
                menuName: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    { min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                ],
                permissionName: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    { min: 1, max: 30, message: '长度在 1 到 80 个字符', trigger: 'blur' },
                    { validator: this.validatePermission, message: '授权名称重复', trigger: 'blur' }
                ],
                menuDisplayName: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    { min: 1, max: 30, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                ],
                //url: [
                //    { required: true, message: '必填项', trigger: 'change' },
                //    { min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                //],
                requiresAuthModel: [
                    { required: true, message: '请选择一个项目', trigger: 'change' }
                ]
            },
            formData: {
                id: null,
                parentID:null,
                menuName: null,
                menuDisplayName: null,
                orderBy: null,
                icon: "fa-windows",
                url: '/',
                requiresAuthModel: '1',
                permissionName: '',
                customData: null,
                description: null,
                isActive: true
            },
            formSubRules: {
                actionDisplayName: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    { min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                ],
                actionName: [
                    { required: true, message: '必填项', trigger: 'change' },
                    { min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' },
                    { validator: this.validateActionName, message: '动作编码重复',trigger: 'blur' }
                ],
                permissionName: [
                    //{ required: true, message: '必填项', trigger: 'change' },
                    { min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                    //{ validator: this.validateActionName, message: '授权名称重复',trigger: 'blur' }
                ],
                requiresAuthModel: [
                    { required: true, message: '请选择一个项目', trigger: 'change' }
                ]
            },
            formSubData: {
                id: null, 
                menuID: null,
                actionDisplayName: null,
                actionName: null,
                permissionName: null,
                requiresAuthModel: '1',
                description: null,
                isActive: true
            },
            pageOptions: {
                formDialog: false,
                isAdd: true,
                selectedParent: [],
                selectRows:[],
                selectRow: {}
            }
        };
    },
    watch: {
      //监听
    },
    computed: {
      //计算
    },
    methods: {
        initMenusData: function () {
            var _this = this;
            abp.ajax({
                url: '/J_Menus/GetMenusList',
            }).done(function (data, res, e) {
                _this.treeData = data; 
            })
        },
        formatterCol: function (row, column) {
            if (row.requiresAuthModel === "1") {
                return '开放模式'; 
            } else if (row.requiresAuthModel === "2") {
                return '登录模式';
            } else if (row.requiresAuthModel === "3") {
                return '授权模式';
            } else {
                return '';
            }
        },
        doNodeClick: function (data, node, e) {
            this.getFormData(data.id);
            //
            var _node = node;
            var selectList = [];
            while (true) {
                if (_node.level === 1 || !_node.parent) {
                    break;
                }
                if (_node.parent.data) {
                    selectList.push(_node.parent.data.id);
                }
                _node = _node.parent;
            } 
            //设置表单级联选择器显示的列表
            this.pageOptions.selectedParent = selectList.reverse();
        },
        getFormData: function (id) {
            var _this = this;
            abp.ajax({
                url: '/J_Menus/GetMenusModel',
                data: JSON.stringify(id),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.formData = data;
                _this.tableData = data.sysMenuActions;

            });
        },
        doRowSelectChange: function (selection) {
            this.pageOptions.selectRows = selection;
        },
        doRowclick: function (row, event, column) {
            //设置选中行
            this.$refs.dataGrid.toggleRowSelection(row);
            this.pageOptions.selectRow = row;
        },
        validateActionName: function (rule, value, callback) {
            if (!value) {
                callback();
                return;
            }
            //var _this = this;
            //var i = 0;
            //动作明细列表验证 : 授权名称 或 动作名称 是否重复
            var data = this.tableData.filter(function (e, i, d) {
                var oldValue = '';
                //if (rule.field == "permissionName") {
                //    oldValue = e.permissionName.toLowerCase();
                //}
                if (rule.field === "actionName") {
                    oldValue = e.actionName.toLowerCase();
                }
                if (value.toLowerCase() === oldValue) {
                    return true;
                }
            });
            //验证重复,分别针对编辑与新增状态下的验证方式
            if ((!this.pageOptions.isAdd && data.length > 1) || (this.pageOptions.isAdd && data.length> 0)) {
                callback(new Error());
                return;
            }
            //验证通过必须执行
            callback();
        },
        validatePermission: function (rule, value, callback) {
            if (!value) {
                callback();
                return;
            }
            //var _this = this;
            //var i = 0;
            var data = {
                permissionName: this.formData.permissionName,
                id: this.formData.id
            };
            abp.ajax({
                url: '/J_Menus/IsPermissionRepeat',
                data: JSON.stringify(data),
                type: 'POST'
            }).done(function (data, res, e) {
                if (!data && data === false) {
                    callback();
                } else {
                    callback(new Error());
                }
            }).fail(function (data, res, e) {
                //debugger;
                callback(new Error("验证超时"));
            });
        },
        doAdd: function () { 
            this.$refs["formInfoData"].resetFields();
            this.pageOptions.selectedParent = [];
            this.tableData = [];
        },
        doSave: function () { 
            var _this = this;
            //获取动作明细
            this.formData["sysMenuActions"] = this.tableData;
            //
            this.$refs["formInfoData"].validate(
                function (valid) {
                    if (valid) {
                        //
                        abp.ajax({
                            url: '/J_Menus/SaveMenus',
                            data: JSON.stringify(_this.formData),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            //保存成功获取返回id
                            _this.formData.id = data.id;
                            //重载树菜单
                            _this.initMenusData();
                            //重载明细Table
                            _this.getFormData(data.id); 
                            abp.message.success('保存成功');
                        }).fail(function (data, res, e) {
                            //debugger;
                        });
                    }
                }
            );

        },
        doDelForm: function () {
            var _this = this;
            //验证是否选择了节点
            if (!this.formData.id) {
                abp.message.warn('请选择模块节点');
                return;
            }
            //验证删除的节点
            var currentNode = this.$refs["treeData"].getCurrentNode();
            if (currentNode.childrenMenus.length > 0) {
                abp.message.warn('请先移除该模块的子模块节点');
                return;
            }
            //提交删除
            this.$confirm('确定删除?', '提示', {
                //confirmButtonText: '确定',
                //cancelButtonText: '取消',
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/J_Menus/DelMenusModel',
                    data: JSON.stringify(_this.formData.id),
                    type: 'POST'
                }).done(function (data, res, e) {
                    _this.$refs["formInfoData"].resetFields();
                    _this.pageOptions.selectedParent = [];
                    _this.tableData = [];
                    //重载树菜单
                    _this.initMenusData();
                    abp.message.success('删除成功');
                });
            });
        },
        doSubAdd: function () {
            var _this = this;
            this.pageOptions.isAdd = true;
            this.pageOptions.formDialog = true;
            this.$nextTick(function () {
                _this.$refs["formSubData"].resetFields();
            });
        },
        doSubEdit: function () {
            var _this = this;
            this.pageOptions.isAdd = false;
            this.pageOptions.formDialog = true;
            this.$nextTick(function () {
                //获取需要修改的数据,采用复制
                _this.formSubData = JSON.parse(JSON.stringify(this.pageOptions.selectRow));
                //_this.$refs["formSubData"].validate();
            });
        },
        doSubSave: function () {
            var _this = this;
           
            this.$refs["formSubData"].validate(
                function (valid) {
                    if (!valid) {
                        return false;
                    }
                    //设置动作外键ID
                    _this.formSubData.menuID = _this.formData.id;
                    var row = JSON.parse(JSON.stringify(_this.formSubData));
                    //判断新增或编辑
                    if (_this.pageOptions.isAdd) {
                        _this.tableData.push(row);
                    } else {
                        //修改替换原数据
                        _this.tableData.forEach(function (item, index, data) {
                            //
                            if (item.actionName === _this.pageOptions.selectRow.actionName &&
                                item.permissionName === _this.pageOptions.selectRow.permissionName) {
                                _this.tableData[index] = row;
                            }
                        });
                    }
                    _this.pageOptions.formDialog = false;
                    //取消选中状态刷新修改后的数据
                    _this.$refs.dataGrid.clearSelection();
                }
            );
        },
        doSubDel: function () {
            var _this = this;
            if (this.pageOptions.selectRows.length === 0) {
                alert('请选择一行数据.');
                return;
            }
            this.$confirm('确定删除?', '提示', {
                //confirmButtonText: '确定',
                //cancelButtonText: '取消',
                type: 'warning'
            }).then(function () {
                //返回删除后的数据
                var data = _this.tableData.filter(function (item, index, data) {
                    var isAs = _this.pageOptions.selectRows.some(function (e, i, d) {
                        //actionName permissionName
                        if (item.actionName === e.actionName) {
                            return true;
                        }
                    });
                    if (!isAs) {
                        return true;
                    }
                    return false;
                });
                _this.tableData = data;
            });
        },
        doParentTreeChange: function (v) {
            var _this = this;
            var delIndex = v.length;
           //
            var isAs = v.some(function (e, i, d) {
                if (e === _this.formData.id) {
                    delIndex = i;
                    return true;
                }
            });
            //判断不允许调整到当前节点的子节点上
            if (isAs) {
                v.splice(delIndex, v.length);
                return;
            }
            //设置父节点ID
            this.formData.parentID = v[v.length - 1];
        },
        classIcon: function (icon) {
            if (icon == '') {
                return { 'fa fa-th-large': true };
            } else {
                var cs = {};
                cs['fa ' + icon]= true;
                return cs;
            }
        }

    }
});
