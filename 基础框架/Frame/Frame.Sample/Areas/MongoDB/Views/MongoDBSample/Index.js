
var component = Vue.component('j-mongodb', {
    template: Vue.frameTemplate('MongoDB/MongoDBSample/Index'),
    created: function () {
        this.getUserList();
        this.initMenusData();

    },
    data: function () {
        return {
            defaultProps: {
                children: 'nodeItems',
                label: 'nodeName',
                value: 'id'
            },
            treeData:[],
            formData: {
                id: null,
                dataString: "",
                dataNum: 1,
                dataBool: true,
                dataTime: '2018-08-10T16:00:00Z',
                dataArray: [],
                dataObject: null
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
        initMenusData: function () {
            var _this = this;
            abp.ajax({
                url: '/SampleDataSource/DataSource/GetTreeData',
            }).done(function (data, res, e) {
                _this.treeData = data;
            })
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
            var parameter = {
                pagingDto: this.pageOptions,
                val: this.pageOptions.searchTxt
            }
            abp.ajax({
                url: '/MongoDB/MongoDBSample/GetMongoList',
                data: JSON.stringify(parameter),
                type: 'POST'
            }).done(function (data) {
                _this.pageOptions.total = data.totalCount;
                _this.tableOptions.tableData = data.items;
                //_this.tableOptions.tableData = data;
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
        doAdd: function () {
            var _this = this;
            //
            this.pageOptions.formDialog = true;
            //
            this.$nextTick(function () {
                _this.$refs.formUserData.resetFields();
            });
        },
        doEdit: function () {
            var _this = this;
            //
            if (!this.tableOptions.selectRow.id) {
                abp.message.warn('请选择一行数据');
                return;
            }
            this.pageOptions.formDialog = true;
            //
            abp.ajax({
                url: '/MongoDB/MongoDBSample/GetMongoModel',
                data: JSON.stringify({ id: _this.tableOptions.selectRow.id }),
                type: 'POST'
            }).done(function (data) {
                _this.formData = data;
            });
        },
        doDel: function () {
            var _this = this;
            //
            if (!this.tableOptions.selectRow.id) {
                abp.message.warn('请选择一行数据');
                return;
            }
            //
            abp.ajax({
                url: '/MongoDB/MongoDBSample/DelMongoModel',
                data: JSON.stringify({ id: _this.tableOptions.selectRow.id }),
                type: 'POST'
            }).done(function (data) {
                _this.getUserList();
            });
        },
        doSaveForm: function () {
            var _this = this;
            
            this.formData.dataObject = JSON.parse(JSON.stringify(this.formData));
            //this.formData.dataArray = this.formData.dataArray.reverse();
            abp.ajax({
                url: '/MongoDB/MongoDBSample/MongoSave',
                data: JSON.stringify(_this.formData),
                type: 'POST'
            }).done(function (data, res, e) {
                console.log(data);
                _this.getUserList();
                _this.pageOptions.formDialog = false;
            });

        }
        //, onComplete ?, onAbort ?
    }
});
