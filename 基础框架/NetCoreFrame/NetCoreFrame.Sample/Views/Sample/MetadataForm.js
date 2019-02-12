
var component = Vue.component('metadata-form', {
    template: Vue.frameTemplate('Sample/MetadataForm'),
    created: function () {
        this.initTableData();
    },
    data: function () {
        return {
            pageSetting: {
                tableId: '',
                tableData: [],
                fieldData: null,
            },
            metadata: [],
            formRules: {},
            formData: {
                id: null,
            },
            pageOptions: {
                dictText: '',
                dictValue: '',
                searchText: ''
            },
            tableOptions: {
                tableData: [],
                selecttableData: {},
                selectRows: [],
                selectRow: {}
            }
        };
    },
    watch: {

    },
    computed: {

    },
    methods: {
        initTableData: function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/Sample/GetTableList',
                data: "1",
                type: 'POST'
            }).done(function (data, res, e) {
                _this.pageSetting.tableData = data;
            });

        },
        initFieldData: function (data) {
            //树节点初始加载数据
            var _this = this;
             
            this.pageSetting.selecttableData = _this.pageSetting.tableData.filter(function (item, index) {
                if (item.id && item.id == data) {
                    return item;
                }
            });

            abp.ajax({
                url: '/Sample/GetFieldInfoByTableModel',
                data: JSON.stringify(data)
            }).done(function (data, res, e) {
                //
                _this.metadata = [];
                data.forEach(function (item, index) {
                    var data = JSON.parse(item.fieldJson)
                    //
                    data.fieldCode = _this.setCamelCase(item.fieldCode);
                    _this.metadata.push(data);
                });

                _this.setFormData();

                _this.$nextTick(function () {
                    _this.getDataTable();
                });
            });

        },
        setFormData: function () {
            var _this = this;
            //this.formData ={};
            this.metadata.forEach(function (item, index) {
                switch (item.uiType) {
                    case "TextBox":
                        Vue.set(_this.formData, item.fieldCode, null);
                        if (item.isRequired) {
                            Vue.set(_this.formRules, item.fieldCode, [{ required: true, message: item.fieldAlias + '必填项', trigger: 'blur' }]);
                        }
                        break;
                    case "TextArea":
                        Vue.set(_this.formData, item.fieldCode, null);
                        if (item.isRequired) {
                            Vue.set(_this.formRules, item.fieldCode, [{ required: true, message: item.fieldAlias + '必填项', trigger: 'blur' }]);
                        }
                        break;
                    case "Number":
                        Vue.set(_this.formData, item.fieldCode, null);
                        if (item.isRequired) {
                            Vue.set(_this.formRules, item.fieldCode, [{ required: true, message: item.fieldAlias + '必填项', trigger: 'blur' }]);
                        }
                        break;
                    case "Date":
                        Vue.set(_this.formData, item.fieldCode, null);
                        if (item.isRequired) {
                            Vue.set(_this.formRules, item.fieldCode, [{ required: true, message: item.fieldAlias + '必填项', trigger: 'blur' }]);
                        }
                        break;
                    case "DropdownList":
                        Vue.set(_this.formData, item.fieldCode, null);
                        if (item.isRequired) {
                            Vue.set(_this.formRules, item.fieldCode, [{ required: true, message: item.fieldAlias + '必填项', trigger: 'change' }]);
                        }
                        break;
                    case "DropdownMultipleList":
                        Vue.set(_this.formData, item.fieldCode, []);
                        if (item.isRequired) {
                            Vue.set(_this.formRules, item.fieldCode, [{ required: true, message: item.fieldAlias + '必填项', trigger: 'change' }]);
                        }
                        break;
                    default:
                        break;
                }
            });

        },
        dateChange: function (data) {

        },
        setDict: function (data) {
            //预设字典值
            if (data.dataSourceType == "1") {
                return data.dataSourceItems;
            }
            else if (data.dataSourceType == "2") {
                //实体表
                //data.dataSourceTable
            }
            else if (data.dataSourceType == "3") {
                //自定义数据源
                // data.dataSourceService
            }
        },
        setCol: function (data) {
            if (data.uiCol == "1") {
                return { width: '175px' };
            }
            else if (data.uiCol == "2") {
                return { width: '460px' };
            }
            else if (data.uiCol == "3") {
                return { width: '745px' };
            }
        },
        setCamelCase: function (data) {
            var field = data.substring(0, 1).toLowerCase() + data.substring(1, data.length);
            return field;
        },
        doRowclick: function (row, event, column) {
            //设置选中行
            this.$refs["dataGrid"].toggleRowSelection(row);
            //
            this.tableOptions.selectRow = row;
            this.formData = JSON.parse(JSON.stringify(row));
        },
        getDataTable: function () {
            var _this = this;
            //
            abp.ajax({
                //url: 'Sample/GetTempTableDapperDto',
                url: 'Sample/GetTempTableDto',
            }).done(function (data, res, e) {
                _this.tableOptions.tableData = data;
            });

        },
        saveForm: function () {
            var _this = this;
            //
            this.$refs["metadataForm"].validate(
                function (valid) {
                    if (!valid) {
                        abp.message.error('表单信息不完整!');
                        return;
                    }
                    abp.ajax({
                        url: 'Sample/SaveFormModel',
                        data: JSON.stringify(_this.formData),
                        type: 'POST'
                    }).done(function (data, res, e) {
                        _this.formData.id = data;
                        _this.getDataTable();
                        abp.message.success('保存成功');
                    });
                }
            );
        },
        saveDapperForm: function () {
            var _this = this;
            //
            this.$refs["metadataForm"].validate(
                function (valid) {
                    if (!valid) {
                        abp.message.error('表单信息不完整!');
                        return;
                    }
                    abp.ajax({
                        url: 'Sample/SaveFormDapperModel',
                        data: JSON.stringify(_this.formData),
                        type: 'POST'
                    }).done(function (data, res, e) {
                        _this.formData.id = data;
                        _this.getDataTable();
                        abp.message.success('保存成功');
                    });
                }
            );
        },
        resetFields: function () {
            this.$refs["metadataForm"].resetFields();
        }

    }
});
