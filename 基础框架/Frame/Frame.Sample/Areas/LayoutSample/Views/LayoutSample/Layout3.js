
var component = Vue.component('j-layoutsample3', {
    template: Vue.frameTemplate('LayoutSample/LayoutSample/Layout3'),
    created: function () {
        this.getUserList();
    },
    data: function () {
        return {
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
            },
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
                maxResultCount: 10,
                total: 0
            }
        };
    },
    watch: {
        //监听
    },
    computed: {
    
    },
    methods: {
        doRowSelectChange: function (selection) {
            this.tableOptions.selectRows = selection
            console.log(selection);
        },
        doRowclick: function (row, event, column) {
            //设置选中行
            this.$refs.dataGrid.toggleRowSelection(row);
            //
            this.tableOptions.selectRow = row;
        },
        getUserList: function () {
            var _this = this;
            //
            var parameter = {
                pagingDto: this.pageOptions,
                val: this.pageOptions.searchTxt
            }
            abp.ajax({
                url: '/SampleDataSource/DataSource/GetGridDataList',
                data: JSON.stringify(parameter)
                //type: 'POST'
            }).done(function (data) {
                _this.pageOptions.total = data.totalCount;
                _this.tableOptions.tableData = data.items;
            });
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
