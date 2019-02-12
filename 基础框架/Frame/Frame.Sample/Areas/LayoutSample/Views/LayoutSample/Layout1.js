
var component = Vue.component('j-layoutsample1', {
    template: Vue.frameTemplate('LayoutSample/LayoutSample/Layout1'),
    created: function () {
        this.getUserList();
    },
    data: function () {
        return {
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
                val:  this.pageOptions.searchTxt
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
        },
        pageRef: function () {
          
            var _this = this;
           
            this.pageRouter({
                name: 'j-layoutsample4',
                path: '/Areas/LayoutSample/Views/LayoutSample/Layout4',
                params: {
                    myParam1: "参数",
                    myParam2: "[{ 'name': '名字' }]"
                },
                displayName: '页面跳转传递参数'
            });

        }
        //, onComplete ?, onAbort ?
    }
});
