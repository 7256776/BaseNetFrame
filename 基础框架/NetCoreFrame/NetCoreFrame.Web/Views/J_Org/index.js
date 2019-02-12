// 组织管理
var component = Vue.component('j-org', {
    template: Vue.frameTemplate('J_Org/Index'),
    created: function () {
        this.initMenusData();
    },
    data: function () {
        return {
            treeData: [],
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
                    { validator: this.validateOrgCode, message: '机构编码重复', trigger: 'blur' }
                ]
            },
            formData: {
                id: null,
                parentOrgID: null,
                orgName: null,
                orgCode: null,
                orgType: "2",
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
        initMenusData: function () {
            var _this = this;
            abp.ajax({
                url: '/J_Org/GetSysOrgList'
            }).done(function (data, res, e) {
                _this.treeData = data; 
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
                url: '/J_Org/GetSysOrgModel',
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
                            url: '/J_Org/SaveSysOrgModel',
                            data: JSON.stringify(_this.formData),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            //保存成功获取返回id
                            _this.formData.id = data.id; 
                            //重载树菜单
                            _this.initMenusData();
                            abp.message.success('保存成功');
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
            if (currentNode.childrenSysOrg.length > 0) {
                abp.message.warn('请先移除该机构的子机构节点');
                return;
            }
            //提交删除
            this.$confirm('确定删除?', '提示', {
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/J_Org/DelSysOrg',
                    data: JSON.stringify(_this.formData.id),
                    type: 'POST'
                }).done(function (data, res, e) {
                    _this.$refs["formInfoData"].resetFields();
                    _this.pageOptions.selectedParent = [];
                    //重载树菜单
                    _this.initMenusData();
                    abp.message.success('删除成功');
                });
            });
        },
        validateOrgCode: function (rule, value, callback) {
            if (!value) {
                callback();
                return;
            }
            var i = 0;
            var data = {
                id: this.formData.id,
                orgCode: this.formData.orgCode
            };
            abp.ajax({
                url: '/J_Org/CheckOrgCode',
                data: JSON.stringify(data),
                type: 'POST'
            }).done(function (data, res, e) {
                if (!data) {
                    callback();
                } else {
                    callback(new Error());
                }
            });
        }
    }
});
