var component = Vue.component('metadata', {
    template: Vue.frameTemplate('Sample/Metadata'),
    created: function () {
        //
        this.initMetaTreeData();
        //加载滚动条样式
        this.$nextTick(function () {
            //height: $('#treeMetaDiv')[0].scrollHeight + 'px'
            $('#treeMetaDiv').slimScroll({});
        });
    },
    data: function () {
        return {
            treeData: [],
            tableData: [],
            defaultProps: {
                children: 'fieldInfoList',
                label: 'tableName',
                value: 'tableCode'
            },
            formRules: {
                tableId: [
                    { required: true, message: '必填项', trigger: 'blur' }
                ],
                fieldCode: [
                    { required: true, message: '必填项', trigger: 'blur' }
                ],
                fieldName: [
                    { required: true, message: '必填项', trigger: 'blur' },
                ],
                isRequired: [
                    { required: true, message: '必填项', trigger: 'blur' }
                ],
                dataType: [
                    { required: true, message: '必选项', trigger: 'blur' }
                ],
                uiType: [
                    { required: true, message: '必选项', trigger: 'blur' }
                ]
            },
            formData: {
                id: null,
                tableId:null,
                fieldCode: null,
                fieldAlias: null,
                fieldName: null,
                isRequired: false,
                dataLength: 50,
                dataType: null,
                dataPrecision: null,
                dateFormat: null,
                uiType: null,
                fieldOrder: null,
                description: null,
                dataSourceType: null,
                dataSourceItems: null,
                dataSourceTable: null,
                dataSourceService: null
              
            },
            pageOptions: { 
                dictText:'',
                dictValue: '',
                searchText: ''
            },
            uiTypeState: {
                textBox: true,
                number: true,
                textArea: true,
                date: true,
                dropdownList: true,
                dropdownMultipleList: true,
            }
        };
    },
    watch: {
        //监听
        'pageOptions.searchText': function (val) {
            this.$refs.treeData.filter(val);
        }
    },
    computed: {
        //设置元数据类型扩展配置
        isShowType: function () {
            this.uiTypeState.textBox = true;
            this.uiTypeState.number = true;
            this.uiTypeState.textArea = true;
            this.uiTypeState.date = true;
            this.uiTypeState.dropdownList = true;
            this.uiTypeState.dropdownMultipleList = true;

            switch (this.formData.dataType) {
                case "String":
                    this.formData.uiType = this.formData.uiType || "TextBox";
                    this.formData.dataLength = this.formData.dataLength || 50;
                    this.uiTypeState.textBox = false;
                    this.uiTypeState.textArea = false;
                    return "1";
                case "Number":
                    this.formData.uiType = this.formData.uiType || "Number";
                    this.uiTypeState.number = false;
                    return "2";
                case "Date":
                    this.formData.uiType = this.formData.uiType || "Date";
                    this.formData.dateFormat = this.formData.dateFormat || "yyyy/MM/dd mm:hh:ss";
                    this.uiTypeState.date = false;
                    return "3";
                case "ObjectDict":
                case "Dict":
                    this.formData.uiType = this.formData.uiType || "DropdownList";
                    this.formData.dataSourceType = this.formData.dataSourceType || "1";
                    this.uiTypeState.dropdownMultipleList = false;
                    this.uiTypeState.dropdownList = false;
                    return "4";
                default:
                    return "0"
            }
        }
    },
    methods: {
        initMetaTreeData: function () {
              //树节点初始加载数据
            var _this = this;

            abp.ajax({
                url: '/Sample/GetTableAndeFieldList',
                type: 'POST'
            }).done(function (data, res, e) {
                //
                _this.treeData = data;
                //_this.$nextTick(function () {});
            });

            //
            abp.ajax({
                url: '/Sample/GetTableList',
                data:"'':''",
                type: 'POST'
            }).done(function (data, res, e) {
                _this.tableData = data;
            });

        },
        doNodeClick: function (data, node, e) {
             //树节点点击事件
            var _this = this;
            //清空表单
            this.doReset();
            //转值类型
            //var tem = JSON.parse(JSON.stringify(data.fieldJson));

            if (!data.isField)
                return;

            //
            abp.ajax({
                url: '/Sample/GetFieldInfoModel',
                data: JSON.stringify(data.id),
                type: 'POST'
            }).done(function (data, res, e) {
                if (!data.fieldJson)
                    return;
                var tem = JSON.parse(data.fieldJson);
                tem.id = data.id;
                _this.setFormData(tem);
            });

        },
        doAddEnum: function () {
               //字典类型添加数据
            var _this = this;
            this.formData.dataSourceItems = this.formData.dataSourceItems || [];
            //
            if (!_this.pageOptions.dictText || !_this.pageOptions.dictValue) {
                abp.message.warn('字典Text和字典Value必填!');
                return;
            }
            //
            var isRepetit = this.formData.dataSourceItems.some(function (item, index) {
                if (item.text == _this.pageOptions.dictText) {
                    abp.message.warn('字典Text:' + _this.pageOptions.dictText+'已存在!');
                    return true;
                }
                if (item.value == _this.pageOptions.dictValue) {
                    abp.message.warn('字典Value:' + _this.pageOptions.dictValue +'已存在!');
                    return true;
                }
            });
            //
            if (!isRepetit ) {
                var item = {
                    "text": this.pageOptions.dictText.trim(),
                    "value": this.pageOptions.dictValue.trim()
                };
                this.formData.dataSourceItems.push(item);
                this.pageOptions.dictText = null;
                this.pageOptions.dictValue = null;
            }
        },
        doDelEnum: function (tag) {
              //移除字典类型
            this.formData.dataSourceItems.splice(this.formData.dataSourceItems.indexOf(tag), 1);
        },
        setFormData: function (data) {
            //设置表单对象
            this.formData = data;
        },
        doReset: function () {
            //新增
            this.$refs["formInfoData"].resetFields();
            this.formData.dataSourceItems = null;
        },
        doSave: function () {
            var _this = this;
            //
            this.dataHandle();
            //
            this.$refs["formInfoData"].validate(
                function (valid) {
                    if (!valid) {
                        abp.message.error('表单信息不完整!');
                        return;
                    }
                    abp.ajax({
                        url: '/Sample/SaveMetaModel',
                        data: JSON.stringify(_this.formData),
                        type: 'POST'
                    }).done(function (data, res, e) {

                        _this.formData.id = data;
                        //
                        abp.message.success('成功');

                        _this.$nextTick(function () {
                            _this.initMetaTreeData();
                        });
                    });
                }
            );
        },
        doDel: function () {
            var _this = this;
            //获取当前选择的节点
            var currentNode = this.$refs["treeData"].getCurrentNode();
            //验证是否选择了节点
            if (!this.formData.id || !currentNode ) {
                abp.message.warn('请选择节点');
                return;
            }

            //验证删除的节点
            if (currentNode.childrenItem && currentNode.childrenItem.length > 0) {
                abp.message.warn('请先移除子节点');
                return;
            }
            //提交删除
            this.$confirm('确定删除?', '提示', {
                type: 'warning'
            }).then(function () {

                abp.ajax({
                    url: '/Sample/DelMetaData',
                    data: JSON.stringify(_this.formData),
                    type: 'POST'
                }).done(function (data, res, e) {
                    //
                    abp.message.success('成功');

                    _this.$nextTick(function () {

                    });
                });

            });
        },
        //保存前移除不需要的数据类型配置信息
        dataHandle: function () {
            switch (this.formData.dataType) {
                case "String":
                    this.formData.dataPrecision = null;
                    this.formData.dateFormat = null;
                    this.formData.dataSourceType = null;
                    this.formData.dataSourceItems = null;
                    this.formData.dataSourceTable = null;
                    this.formData.dataSourceService = null;
                    break;
                case "Number":
                    this.formData.dataLength = null;
                    this.formData.dateFormat = null;
                    this.formData.dataSourceType = null;
                    this.formData.dataSourceItems = null;
                    this.formData.dataSourceTable = null;
                    this.formData.dataSourceService = null;
                    break;
                case "Date":
                    this.formData.dataPrecision = null;
                    this.formData.dataLength = null;
                    this.formData.dataSourceType = null;
                    this.formData.dataSourceItems = null;
                    this.formData.dataSourceTable = null;
                    this.formData.dataSourceService = null;
                    break;
                case "Dict":
                case "ObjectDict":
                    this.formData.dataPrecision = null;
                    this.formData.dataLength = null;
                    this.formData.dateFormat = null;
                    if (this.formData.dataSourceType == '1') {
                        this.formData.dataSourceTable = null;
                        this.formData.dataSourceService = null;
                    }
                    else if (this.formData.dataSourceType == '2') {
                        this.formData.dataSourceItems = null;
                        this.formData.dataSourceService = null;
                    }
                    else if (this.formData.dataSourceType == '3') {
                        this.formData.dataSourceItems = null;
                        this.formData.dataSourceTable = null;
                    }
                    else {
                        this.formData.dataSourceItems = null;
                        this.formData.dataSourceTable = null;
                        this.formData.dataSourceService = null;
                    }
                    break;
                default:
                    break;
            }
        },
        doFilterNode: function (value, data) {
            if (!value) {
                return true;
            }
            return data.title.indexOf(value) !== -1;
        }

    }
});