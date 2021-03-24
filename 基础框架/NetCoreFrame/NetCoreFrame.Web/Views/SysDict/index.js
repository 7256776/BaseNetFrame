var component = Vue.component('sys-dict',
    {
        template: Vue.frameTemplate('SysDict/Index'),
        created: function () {
            this.getSysDictTypeDataList(); 
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
                        { required: true, message: '必填项', trigger: 'blur' },
                        { validator: this.validateDictCode, message: '仅限数字或字母', trigger: 'blur' }
                    ],
                    dictTypeName: [
                        { required: true, message: '必填项', trigger: 'blur' }
                    ]
                },
                pageOptions: {
                    isDictType: true,             //是否禁止编辑
                    tableEditState: false       //表格是否编辑状态
                },
                tableOptions: {
                    searchTxt: '',
                    tableData: [],
                    selectRows: [],
                    selectRow: {},
                    tableDataBak: {},
                },
                expands: []
            };
        },
        watch: {
            //监听
        },
        computed: {
            tableDataRefresh: function () {
                var _this = this;
                if (!this.tableOptions.searchTxt) {
                    return this.tableOptions.tableData;
                }
                return this.tableOptions.tableData.filter(function (item, index) {
                    if (item.dictCode.indexOf(_this.tableOptions.searchTxt) > -1 || item.dictContent.indexOf(_this.tableOptions.searchTxt) > -1) {
                        return true;
                    }
                    return false;
                });
            }
        },
        methods: {
            doAddDictType: function () {
                this.$refs["formDictEl"].resetFields();
                this.tableOptions.tableData = [];
                this.pageOptions.isDictType = false;
                this.pageOptions.tableEditState = true;
            },
            doDelDictType: function () {
                var _this = this;
                if (!this.formData.id) {
                    this.tipShow('warn', '请选择字典类型');
                    return;
                }
                this.$confirm('确定删除 ' + this.formData.dictTypeName + '?', '提示', { type: 'warning' })
                    .then(function () {
                        abp.ajax({
                            url: '/SysDict/DelSysDictType',
                            data: JSON.stringify(_this.formData),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            //
                            _this.tipSuccess('del');
                            _this.getSysDictTypeDataList();
                            _this.$nextTick(function () {
                                _this.$refs["formDictEl"].resetFields();
                            });
                        });
                    }).catch(function (action) {
                        //取消操作必须有避免js链式调用报异常
                    });
            },
            getSysDictTypeDataList: function () {
                var _this = this;
                this.tableOptions.tableData = [];
                //
                abp.ajax({
                    url: '/SysDict/GetSysDictTypeList'
                }).done(function (data) {
                    _this.sysDictData = data;
                    if (_this.sysDictData.length > 0) {
                        //设置表单以及表格状态
                        _this.doSysDictTypeClick(_this.sysDictData[0].id);
                    }
                });
            },
            doSysDictTypeClick: function (id) {
                this.tableOptions.searchTxt = "";
                this.$refs["formDictEl"].resetFields();
                this.formData.id = id;
                this.pageOptions.isDictType = true;
                this.pageOptions.tableEditState = false;
                this.getData();
            },
            getData: function () {
                var _this = this;
                abp.ajax({
                    url: '/SysDict/GetSysDictTypeById',
                    data: JSON.stringify(_this.formData.id)
                }).done(function (data) {
                    _this.formData = data;
                    _this.getDataList();
                });
            },
            getDataList: function () {
                var _this = this;
                abp.ajax({
                    url: '/SysDict/GetSysDictListByDictType',
                    data: JSON.stringify(_this.formData.dictType)
                }).done(function (data) {
                    _this.tableOptions.tableData = data;
                    //备份一次字典数据
                    _this.tableOptions.tableDataBak = JSON.parse(JSON.stringify(data));
                    //
                    _this.doRemoveExpandIcon();
                });
            },
            doRowSelectChange: function (selection) {
                this.tableOptions.selectRows = selection;
            },
            doRowCurrentChange: function (currentRow, oldCurrentRow) {
                var _this = this;
                if (!currentRow) {
                    return;
                }
                if (oldCurrentRow && (oldCurrentRow.editState == "modify" || oldCurrentRow.editState == "add")) {
                    //
                    _this.setExpands(oldCurrentRow);
                    this.$confirm(
                      '当前行数据已变更是否保存?',
                      '提示',
                      {
                          type: 'warning'
                      }).then(function () {
                          _this.doSaveRow();
                      }).catch(function () {
                          //
                          _this.tableOptions.currentEditRow = oldCurrentRow;
                          _this.doCancel();
                      });
                } else {
                    this.setExpands(currentRow);
                    //this.doCancel();
                }
                //
                if (oldCurrentRow) {
                    oldCurrentRow.showState = false;
                }
                this.$refs["gridEl"].clearSelection();                              //清空选择行
                this.tableOptions.selectRow = null;                              //将当前选中行置空
                this.$refs["gridEl"].toggleRowSelection(currentRow);   //设置当前行为选中行
                this.tableOptions.selectRow = currentRow;                 //设置当前选中行对象
                this.doRemoveExpandIcon();                                       //隐藏展开图标
            },
            setExpands: function (row) {
                var _this = this;
                row.showState = true; //将行对象设置为可编辑状态
                //实现展开当前行的时候，其他行都能收起来,添加前先清空这个数组
                _this.expands = [];
                //rowKey 是自定义索引用于记录所选择展开的行
                row.rowKey = _this.tableOptions.tableData.indexOf(row);
                _this.expands.push(row.rowKey);
                //设置当前行为选中行并触发事件(doRowCurrentChange)
                _this.$refs["gridEl"].setCurrentRow(row);
            },
            doSaveDictType: function () {
                var _this = this;
                /*
                    (注释该段业务,保存字典类型的时候不保存对应的字典值明细数据,主要考虑页面操作习惯以及与明细自带的保存冲突问题)
                    //验证字典编码值的必填项
                    var validData = _this.tableOptions.tableData.filter(function (item, index) {
                        if (!item.dictCode || !item.dictContent) {
                            return true;
                        }
                    });
                    if (validData.length > 0) {
                        this.tipShow('warn','请设置字典编码或字典名称');
                        return;
                    }
                    //获取需要保存的字典编码对象
                    _this.formData.sysDictInputList = _this.tableOptions.tableData.filter(function(item,index) {
                        if (item.editState === "add" || item.editState === "modify") {
                            return true;
                        }
                    });
                */
                this.$refs["formDictEl"].validate(
                    function (valid) {
                        if (!valid) {
                            return false;
                        }
                        abp.ajax({
                            url: '/SysDict/SaveSysDictTypeModel',
                            data: JSON.stringify(_this.formData)
                        }).done(function (data, res, e) {
                            if (data) {
                                _this.formData.id = data;
                                _this.getSysDictTypeDataList();
                                //
                                _this.getDataList();
                                //_this.pageOptions.isDictType = true;
                                //_this.pageOptions.tableEditState = false;
                                _this.tipSuccess('save');
                            } else {
                                _this.tipFail('save');
                            }
                        });
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
                this.tableOptions.tableData.push(newRow);
                //同时备份一份原有数据
                this.tableOptions.tableDataBak.push(JSON.parse(JSON.stringify(newRow)));
                this.setExpands(newRow);
            },
            doSubDel: function () {
                var _this = this;
                if (_this.tableOptions.selectRows.length < 1) {
                    this.tipShow('warn', '请选择要删除的字典编码');
                    return;
                }
                this.$confirm(
                    '确定要删除数据吗?',
                    '提示',
                    {
                        type: 'warning'
                    }).then(function () {
                        abp.ajax({
                            url: '/SysDict/DeleteSysDict',
                            data: JSON.stringify(_this.tableOptions.selectRows),
                            type: 'POST'
                        }).done(function (data, res, e) {
                            _this.getDataList();
                            _this.tipSuccess('del');
                        });
                    }).catch(function (action) {
                        //取消操作必须有避免js链式调用报异常
                    });
            },
            doSaveRow: function () {
                var _this = this;
                var index = _this.expands[0];
                var row = _this.tableOptions.tableData[index];
                var sysDictInput = [];
                //验证
                if (!row.dictCode || !row.dictContent) {
                    this.tipShow('warn', '请设置字典编码或字典名称');
                    row.isActive = true;
                    row.showState = true;
                    this.$refs["gridEl"].setCurrentRow(row);
                    return;
                }
                sysDictInput.push(row);
                if (row.editState !== "add" && row.editState !== "modify") {
                    this.tipShow('warn', '数据没有发生更改');
                    return;
                }
                abp.ajax({
                    url: '/SysDict/SaveSysDictCodeModel',
                    data: JSON.stringify(sysDictInput),
                    type: 'POST'
                }).done(function (data, res, e) {
                    row.showState = false;
                    row.editState = null;
                    //修改后保存修改后的值
                    _this.tableOptions.tableDataBak[index] = JSON.parse(JSON.stringify(row));
                    _this.expands = [];
                    _this.$refs["gridEl"].setCurrentRow(null);
                    _this.tipSuccess('save');
                    _this.getDataList();
                });

            },
            doCancel: function () {
                var index = this.expands[0];
                var row = this.tableOptions.tableData[index];
                //取消后还原数据
                this.tableOptions.tableData[index] = JSON.parse(JSON.stringify(this.tableOptions.tableDataBak[index]));
                this.tableOptions.tableData[index].showState = false;
                this.tableOptions.tableData[index].editState = '';
                //取消后如果编码或名称没有填写就移除掉该行
                //if (!row.dictCode && !row.dictContent) {
                if (!row.id) {
                    this.tableOptions.tableData.splice(index, 1)
                }
                this.expands = [];
                this.$refs["gridEl"].setCurrentRow(null);
                this.doRemoveExpandIcon();
            },
            fnStyle: function (e) {
                //设置编辑行的样式
                if (e.row.showState) {
                    return { 'background-color': '#DCDFE6' };
                }
            },
            doGridInputChange: function (e, row) {
                //设置行编辑状态
                if (row.editState != 'add') {
                    row.editState = 'modify';
                }
                if (!row.dictCode) {
                    return;
                }
                var rule = ['en', 'num'];
                if (!abp.frameCore.utils.checkChars(row.dictCode, rule)) {
                    this.tipShow('warn', '字典编码仅限数字或字母');
                    //移除过滤掉特殊字符
                    row.dictCode = abp.frameCore.format.stringReplace(row.dictCode);
                    return;
                }
            },
            validateDictCode: function (rule, value, callback) {
                if (!value) {
                    callback();
                    return;
                }
                var rule = ['en', 'num'];
                if (!abp.frameCore.utils.checkChars(value, rule)) {
                    callback(new Error());
                    return;
                }
                //验证通过执行
                callback();
            },
            doRemoveExpandIcon: function () {
                //设置所有展开的图标不做显示
                this.$nextTick(function () {
                    var expandIcon = document.getElementsByClassName('el-table__expand-icon');
                    for (var i = 0; i < expandIcon.length; i++) {
                        expandIcon[i].style.display = "none";
                        expandIcon[i].style.height = "0px";
                    }
                });
            }


        }
    });
