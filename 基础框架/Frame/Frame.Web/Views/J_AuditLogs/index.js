
var component = Vue.component('j-auditlogs', {
    template: Vue.frameTemplate('J_AuditLogs/index'),
    components: {
        jsonformat: componentAssemble.J_JsonFormat
    },
    created: function () {
        this.getDataList();
    },
    data: function () {
        return {
            formData: {},
            pickerOptions: {
                shortcuts: [{
                    text: '今天',
                    onClick:function(picker) {
                        var end = new Date();
                        var start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 1);
                        picker.$emit('pick', [start, end]);
                    }
                }, {
                    text: '最近一周',
                    onClick: function(picker) {
                        var end = new Date();
                        var start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                        picker.$emit('pick', [start, end]);
                    }
                }, {
                    text: '最近一个月',
                    onClick: function(picker) {
                        var end = new Date();
                        var start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 31);
                        picker.$emit('pick', [start, end]);
                    }
                }]
            },
            tableOptions: {
                tableFiltersData: [
                    {
                        userName: "",
                        serviceName: "",
                        methodName: "",
                        executionTime: [],
                        dateRange:"",
                    }
                ],
                tableData: [],
                selectRows: [],
                selectRow: {}
            },
            pageOptions: {
                pageSizes: [20, 50, 100],
                pageSize: 20,
                pageIndex: 1,
                maxResultCount: 20,
                total : 0
            }
        };
    },
    watch: {
       //监听
    },
    methods: {
        doRowSelectChange: function (selection) {
            this.tableOptions.selectRows = selection;
        },
        doRowclick: function (row, event, column) {
            //设置选中行
            this.$refs.dataGrid.toggleRowSelection(row);
            //
            this.tableOptions.selectRow = row;
            //
            this.formData = row;

            //获取参数字符串进行格式化
            this.formData["formatParameters"] = JSON.parse(row.parameters);
            
        },
        getDataList: function () {
            var _this = this; 
            //
            var parameter = {
                model: this.tableOptions.tableFiltersData[0],
                pagingDto: this.pageOptions
            }
            abp.ajax({
                url: '/J_AuditLogs/GetAuditLogList',
                data: JSON.stringify(parameter)
                //type: 'POST'
            }).done(function (data) {
                _this.pageOptions.total = data.totalCount;
                _this.tableOptions.tableData = data.items;
               
            });
        },
        doDelData: function () {
            var _this = this;
           
            this.$confirm('确定清空所有日志?', '提示', {
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/J_AuditLogs/DelAuditLog',
                }).done(function (data, res, e) {
                    //
                    abp.message.success('全部清空完成');
                    _this.getDataList();
                });
            });
        },
        doSearchLog: function () {
            this.getDataList(); 
        }, 
        doSetDate: function (v) {
            this.tableOptions.tableFiltersData[0].dateRange =
                abp.frameCore.format.formatDate(v[0], "yyyy-MM-dd")
                + "--" +
                abp.frameCore.format.formatDate(v[1], "yyyy-MM-dd");
            this.doSearchLog();
        }, 
        doClear: function (v) {
            this.tableOptions.tableFiltersData[0].userName = "";
            this.tableOptions.tableFiltersData[0].serviceName = "";
            this.tableOptions.tableFiltersData[0].methodName = "";
            this.tableOptions.tableFiltersData[0].executionTime = [];
            this.tableOptions.tableFiltersData[0].dateRange = "";
            this.doSearchLog();
        }, 
        handleSizeChange: function (val) {
            this.pageOptions.maxResultCount = val;
            this.pageOptions.pageSize = val;
            this.getDataList();
        },
        handleCurrentChange: function (val, e) {
            this.pageOptions.pageIndex = val;
            this.getDataList();
        }
    }
});
