
var component = Vue.component('sys-orgtreeselection', {
    template: Vue.frameTemplate('SysBusinessComponentss/OrgTreeSelection'),
    created: function () {
        this.initMenusData();
    },
    props: {
        value: {
            type: String,
            default: ''
        },
        popoverWidth: {
            type: Number,
            default: 500
        },
        buttonStyle: {
            type: Object,
            default: {}
        },
    },
    data: function () {
        return {
            treeOptions: {
                treeData: [],
                isShowTree: false,
                defaultProps: {
                    children: 'childrenSysOrg',
                    label: 'orgName',
                    value: 'id'
                },
                currentOrgName: '请选择组织机构...'
            },
        };
    },
    watch: {
        //监听
        value: function () {
            this.setOrgName();
        }
    },
    computed: {
    
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
                _this.treeOptions.treeData = data;
                _this.setOrgName();
            });
        },
        doNodeClick: function (data, node, e) {
            this.treeOptions.currentOrgName = data.orgName;
            this.treeOptions.isShowTree = false;
            //更新v-model 数据
            this.$emit('input', data.orgCode);
        },
        setOrgName: function () {
            //设置组织机构的名称转义
            var resData = abp.frameCore.utils.queryRecursive(this.treeOptions.treeData, this.value, 'childrenSysOrg', 'orgCode');
            if (resData) {
                this.treeOptions.currentOrgName = resData.orgName;
            }
        }
    }


});
