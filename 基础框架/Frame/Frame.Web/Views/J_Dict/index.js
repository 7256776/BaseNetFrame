
var component = Vue.component('j-dict',
    {
        template: Vue.frameTemplate('J_Dict/Index'),
        created: function () {
            this.getSysDictTypeDataList(true);
        },
        data: function () {
            return {
                sysDictData: [],
                formData: {
                    id: null,
                    dictType: null,
                    dictTypeName: null,
                    isActive: true
                },
                formRules: {
                    dictType: [
                        { required: true, message: '必填项', trigger: 'blur' }
                    ],
                    dictTypeName: [
                        { required: true, message: '必填项', trigger: 'blur' }
                    ]
                },
                pageOptions: {
                    isDictType: true,
                    tableEditState: false
                },
                tableOptions: {
                    searchTxt: '',
                    tableData: [],
                    selectRows: [],
                    selectRow: {}
                },
                expands:[]
            };
        },
        watch: {
            //监听
        },
        computed: {
            searchGrid: function () {
                var _this = this;
                if (!this.tableOptions.searchTxt) {
                    return this.tableOptions.tableData;
                }
                return this.tableOptions.tableData.filter(function (item, index) {
                    if (item.dictCode.indexOf(_this.tableOptions.searchTxt) > -1 || item.dictContent.indexOf(_this.tableOptions.searchTxt) > -1) {
                        return true;
                    }
                    return false;
                })
            }
        },
        methods: {
            doAddSubscription: function () {
                this.$refs["formSysDictData"].resetFields();
                this.tableOptions.tableData = [];
                this.pageOptions.isDictType = false;
                this.pageOptions.tableEditState = true;
            },
            doDelSubscription: function () {
                var _this = this;

                if (!this.formData.id) {
                    abp.message.warn('请选择字典类型!');
                    return;
                }
                this.$confirm('确定删除?',
                    '提示',
                    {
                        type: 'warning'
                    }).then(function () {
                        abp.ajax({
                            url: '/J_Dict/DelSysDictType',
                            data: JSON.stringify(_this.formData),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            //
                            abp.message.success('删除成功');
                            _this.getSysDictTypeDataList();
                            _this.$refs["formSysDictData"].resetFields();
                        });
                    });
            },
            getSysDictTypeDataList: function (isInit) {
                var _this = this;
                //
                this.tableOptions.tableData = [];
                //
                abp.ajax({
                    url: '/J_Dict/GetSysDictTypeList'
                }).done(function (data) {
                    _this.sysDictData = data;
                    //
                    if (_this.sysDictData.length > 0 && isInit) {
                        //
                        _this.$refs["formSysDictData"].resetFields();
                        _this.doSysDictTypeClick(_this.sysDictData[0].id);
                    }
                });
            },
            doSysDictTypeClick: function (id) {
                this.tableOptions.searchTxt = "";
                this.$refs["formSysDictData"].resetFields();
                this.formData.id = id;
                this.pageOptions.isDictType = true;
                this.pageOptions.tableEditState = false;
                this.getData();
            },
            getData: function () {
                var _this = this;
                abp.ajax({
                    url: '/J_Dict/GetSysDictTypeById',
                    data: JSON.stringify({ id: _this.formData.id })
                }).done(function (data) {
                    _this.formData = data;
                    _this.getDataList();
                });
            },
            getDataList: function () {
                var _this = this;
                abp.ajax({
                    url: '/J_Dict/GetSysDictListByDictType',
                    data: JSON.stringify({ dictType: _this.formData.dictType })
                }).done(function (data) {
                    _this.tableOptions.tableData = data;
                });
            },
            doRowSelectChange: function (selection) {
                this.tableOptions.selectRows = selection;
            },
            doRowCurrentChange: function (currentRow, oldCurrentRow) {
                if (!currentRow) {
                    return;
                }
                if (oldCurrentRow) {
                    oldCurrentRow.showState = false;
                }
                this.$refs.dataGrid.clearSelection(); //清空选择行
                this.tableOptions.selectRow = null; //将当前选中行置空
                this.$refs.dataGrid.toggleRowSelection(currentRow); //设置当前行为选中行
                this.tableOptions.selectRow = currentRow; //设置当前选中行对象
            },
            doRowclick: function (row, event, column) {
                var _this = this;
                row.showState = true; //将行对象设置为可编辑状态
                
                
                //this.$nextTick(function () {
                //    var res = _this.$refs.abc;
                //    //_this.$refs.abc.$refs.input.focus();
                //    _this.$refs.abc.$refs.input.autofocus = true;
                //});

                _this.expands = [];//实现展开当前行的时候，其他行都能收起来,添加前先清空这个数组
                if (_this.expands.indexOf(row.rowKey) < 0) {
                    row.rowKey = _this.tableOptions.tableData.indexOf(row);
                }
                _this.expands.push(row.rowKey);
            },
            doSaveSubscription: function () {
                var _this = this;

                var submitData = _this.tableOptions.tableData.filter(function(item,index) {
                    if (item.editState == "add" || item.editState == "modify") {
                        return true;
                    }
                });
                _this.formData.sysDictInputList = submitData;

                this.$refs["formSysDictData"].validate(
                    function (valid) {
                        if (valid) {
                            abp.ajax({
                                url: '/J_Dict/SaveSysDictTypeModel',
                                data: JSON.stringify({
                                    modelInput: _this.formData
                                })
                            }).done(function (data, res, e) {
                                if (data) {
                                    _this.formData.id = data.id;
                                    _this.getSysDictTypeDataList();
                                    
                                    _this.getDataList();
                                    _this.pageOptions.isDictType = true;
                                    _this.pageOptions.tableEditState = false;
                                    abp.message.success('保存成功');
                                } else {
                                    abp.message.error('保存失败');
                                }
                            });
                        } else {
                            return false;
                        }
                    }
                );
            },
            doSubAdd: function () {
                var _this = this;
                var newRow = {
                    id: null,
                    dictType: _this.formData.dictType,
                    dictContent: null,
                    dictValue: null,
                    dictCode: null,
                    isActive: true,
                    showState: true,
                    editState: "add"
                };
                _this.tableOptions.tableData.push(newRow);
            },
            doSubDel: function () {
                var _this = this;
                if (_this.tableOptions.selectRows.length < 1) {
                    abp.message.warn('请选择要删除的字典编码!');
                    return;
                }
                this.$confirm(
                    '确定要删除数据吗?',
                    '提示',
                    {
                        type: 'warning'
                    }).then(function () {
                        abp.ajax({
                            url: '/J_Dict/DeleteSysDict',
                            data: JSON.stringify(_this.tableOptions.selectRows),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            _this.getDataList();
                            abp.message.success('删除成功');
                        });
                    });
            },
            doSaveRow: function () {
                var _this = this;
                var index = _this.expands[0];
                var row = _this.tableOptions.tableData[index];
                var sysDictInput = [];
                sysDictInput.push(row);
                if (row.editState != "add" && row.editState != "modify") {
                    abp.message.warn('数据没有发生更改');
                    return;
                }
                abp.ajax({
                    url: '/J_Dict/SaveSysDictCodeModel',
                    data: JSON.stringify(sysDictInput),
                    type: 'POST'
                }).done(function (data, res, e) {
                    row.showState = false;
                    row.editState = null;
                    _this.expands = [];
                    abp.message.success('保存成功');
                });

            },
            doCancel: function () {
                var _this = this;
                var index = _this.expands[0];
                var row = _this.tableOptions.tableData[index];
                row.showState = false;
                if (row.editState != "add" && row.editState != "modify") {
                    _this.expands = [];
                    return;
                }
                _this.getDataList();
                _this.expands = [];
                
            },
            fnStyle: function (e) {
                //:row-class-name="tableRowClassName"
                //return { 'border-width': '1px', 'border-color': '#000', 'border-style': 'solid' };
                //return { 'border': '1px solid #ff0000' };
                if (e.row.showState) {
                    return { 'background-color': '#DCDFE6' };
                }
               
            }

        }
    });
