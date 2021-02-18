// 组织管理
var component = Vue.component('sys-org', {
    template: Vue.frameTemplate('SysOrg/Index'),
    created: function () {
        this.initMenusData();
        this.getOrgType();
    },
    data: function () {
        return {
            treeData: [],
            orgTypeData: [],
            defaultProps: {
                children:'childrenSysOrg',
                label: 'orgName',
                value: 'id'
            },
            formRules: {
                orgName: [
                    { required: true, message: '必填项', trigger: 'blur' }
                ],
                orgCode: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    // 未设置 message 具体错误信息由 validateOrgCode函数返回
                    { validator: this.validateOrgCode, trigger: 'blur' }  
                ]
            },
            formData: {
                id: null,
                parentOrgID: null,
                orgName: null,
                orgCode: null,
                orgType: "1",
                orderBy: 1,
                description: null,
                isActive: true
            },
            pageOptions: {
                selectedParent: []
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
        setNodeIco: function (node) {
            if (node.level == 1) {
                return 'fa fa-institution';
            }
            if (node.data.orgType == 1) {
                return 'fa fa-building';
            }
            if (node.data.orgType == 2) {
                return 'fa fa-group';
            }
            return 'fa fa-th';
        },
        initMenusData: function () {
            var _this = this;
            abp.ajax({
                url: '/SysOrg/GetSysOrgList'
            }).done(function (data, res, e) {
                _this.treeData = data; 
            });
        },
        getOrgType: function () {
            var _this = this;
            abp.ajax({
                url: '/SysDict/GetDictByType',
                data: JSON.stringify('JGLX')
            }).done(function (data, res, e) {
                _this.orgTypeData = data;
            });
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
                    selectList.push(_node.parent.data.id)
                }
                _node = _node.parent
            }
            //设置表单级联选择器显示的列表
            this.pageOptions.selectedParent = selectList.reverse();
        },
        getFormData: function (id) {
            var _this = this;
            this.$refs["formInfoData"].clearValidate();
            abp.ajax({
                url: '/SysOrg/GetSysOrgModel',
                data: JSON.stringify(id),
                type: 'POST'
            }).done(function (data, res, e) {
                _this.formData = data;
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
                //只有选择了当前节点下级节点才给出提示
                if (v.length > (delIndex + 1)) {
                    _this.tipShow('warn','无法使用当前机构下级机构，作为该机构的上级机构。');
                }
                v.splice(delIndex, v.length);
                return;
            }
            //设置父节点ID
            this.formData.parentOrgID = v[v.length - 1];
        },
        doAdd: function () {
            this.$refs["formInfoData"].resetFields();
            this.pageOptions.selectedParent = [];
        },
        doSave: function () {
            var _this = this;
            this.$refs["formInfoData"].validate(
                function (valid) {
                    if (valid) {
                        abp.ajax({
                            url: '/SysOrg/SaveSysOrgModel',
                            data: JSON.stringify(_this.formData),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            //保存成功获取返回id
                            _this.formData.id = data.id; 
                            //重载树菜单
                            _this.initMenusData();
                            _this.tipSuccess('save');
                        });
                    }
                }
            );
        },
        doDelForm: function () {
            var _this = this;
            //验证是否选择了节点
            if (!this.formData.id) {
                this.tipShow('warn', '请选择组织机构');
                return;
            }

            //验证删除的节点
            var currentNode = this.$refs["treeData"].getCurrentNode();
            if (currentNode.childrenSysOrg.length > 0) {
                this.tipShow('warn', '请先移除该机构的子节点机构');
                return;
            }
            //提交删除
            this.$confirm('确定删除 ' + _this.formData.orgName+' ?', '提示', {
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/SysOrg/DelSysOrg',
                    data: JSON.stringify(_this.formData.id),
                    type: 'POST'
                }).done(function (data, res, e) {
                    _this.$refs["formInfoData"].resetFields();
                    _this.pageOptions.selectedParent = [];
                    //重载树菜单
                    _this.initMenusData();
                    _this.tipSuccess('del');
                });
            });
        },
        validateOrgCode: function (rule, value, callback) {
            if (!value) {
                callback();
                return;
            }
            if (!abp.frameCore.utils.checkChars(value)) {
                callback(new Error("机构编码仅限数字或字母组成"));
                return;
            }
            var i = 0;
            var data = {
                id: this.formData.id,
                orgCode: this.formData.orgCode
            };
            abp.ajax({
                url: '/SysOrg/CheckOrgCode',
                data: JSON.stringify(data),
                type: 'POST'
            }).done(function (data, res, e) {
                if (!data) {
                    callback();
                } else {
                    callback(new Error("机构编码重复"));
                }
            });
        }
    }
});
