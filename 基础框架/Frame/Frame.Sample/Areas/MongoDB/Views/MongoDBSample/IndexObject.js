
var component = Vue.component('j-objectmongodb', {
    template: Vue.frameTemplate('MongoDB/MongoDBSample/IndexObject'),
    created: function () {
        this.getUserList();
        this.formData.dataString = this.initData();
    },
    data: function () {
        return {
            formType:"add",
            txtOption: { minRows: 10 },
            formData: {
                id: null,
                filterData:"",
                dataString: this.initData()
            },
            tableOptions: {
                tableData: [],
                selectRows: [],
                selectRow: {}
            },
            pageOptions: {
                searchTxt: '',
                formDialog: false,
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
        initData: function () {
            var data = {
                "dataString": "字符串",
                "dataNum": 1,
                "dataBool": true,
                "dataArray": ["选项1", "选项2"],
                "dataObject": { "sub1": "项目名称" }
            }
            return JSON.stringify(data);
        },
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
            //var parameter = {
            //    pagingDto: this.pageOptions,
            //    val: this.pageOptions.searchTxt
            //}
            //默认查所有
            var parameter = {  };

            abp.ajax({
                url: '/MongoDB/MongoDBSample/MongoObjectFind',
                data: JSON.stringify(parameter),
                type: 'POST'
            }).done(function (data) {
               // _this.pageOptions.total = data.totalCount;
               // _this.tableOptions.tableData = data.items;
                _this.tableOptions.tableData = data;
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
        doShowForm: function (type) {
            var _this = this;
            //
            this.pageOptions.formDialog = true;
            //
            this.$nextTick(function () {
                _this.$refs.formUserData.resetFields();
            });

            this.formType = type; 
        },
        doDel: function () {
            var _this = this;
           
            var data = this.formData.filterData;

            //删除脚本
            // { "dataString": "字符串1" };

            //
            abp.ajax({
                url: '/MongoDB/MongoDBSample/MongoObjectDel',
                //data: JSON.stringify({ id: _this.tableOptions.selectRow.id }),
                data: data,
                type: 'POST'
            }).done(function (data) {
                _this.getUserList();
                _this.pageOptions.formDialog = false;
            });
        },
        doMongoObjectAdd: function () {
            var _this = this;
            
            abp.ajax({
                url: '/MongoDB/MongoDBSample/MongoObjectAdd',
                data: _this.formData.dataString,
                type: 'POST'
            }).done(function (data, res, e) {
                console.log(data);
                _this.getUserList();
                _this.pageOptions.formDialog = false;
            });

        },
        doMongoObjectEdit: function () {
            var _this = this;
            var data = {
                filterJson: this.formData.filterData,
                updataJson: this.formData.dataString
            };

            //修改数据脚本
            //{ "$set": { "dataString": "字符串1", "dataNum": 11, "dataBool": true, "dataArray": ["选项121", "选项22"], "dataObject": { "sub1": "项1目名称" } } }

            abp.ajax({
                url: '/MongoDB/MongoDBSample/MongoObjectEdit',
                data: JSON.stringify(data),
                type: 'POST'
            }).done(function (data, res, e) {
                console.log(data);
                _this.getUserList();
                _this.pageOptions.formDialog = false;
            });

        },
        doMongoObjectReplace: function () {
            var _this = this;
            var data = {
                filterJson: this.formData.filterData,
                updataJson: this.formData.dataString
            };
            
            //替换数据脚本
            //{ "$set": { "dataString": "字符串替换后的值", "dataNum": 11, "dataBool": true, "dataArray": ["选项121", "选项22"], "dataObject": { "sub1": "项1目名称" } } }

            abp.ajax({
                url: '/MongoDB/MongoDBSample/MongoObjectReplace',
                data: JSON.stringify(data),
                type: 'POST'
            }).done(function (data, res, e) {
                console.log(data);
                _this.getUserList();
                _this.pageOptions.formDialog = false;
            });

        }
    }
});
