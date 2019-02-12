
var component = Vue.component('j-layoutsample2', {
    template: Vue.frameTemplate('LayoutSample/LayoutSample/Layout2'),
    created: function () {
        this.initMenusData();
    },
    data: function () {
        return {
            treeData: [],
            tableData: [],
            defaultProps: {
                children: 'nodeItems',
                label: 'nodeName',
                value: 'id'
            },
            formRules: {
                InputText: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    { min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                ],
                InputNum: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    { min: 1, max: 30, message: '1-30', trigger: 'blur' } 
                ], 
                InputSelect: [
                    { required: true, message: '请选择一个项目', trigger: 'change' }
                ]
            },
            formData: {
                id: null,
                InputText: null,
                InputSelect: null,
                InputNum: 1,
                InputTextarea: null
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
                url: '/SampleDataSource/DataSource/GetTreeData',
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
          
        }
    }
});
