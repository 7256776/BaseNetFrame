
var component = Vue.component('sys-notifications', {
    template: Vue.frameTemplate('SysNotifications/Index'),
    created: function () {
        this.getNotificationsDataList(true);
        //
        this.getAllUserDataList();
    },
    data: function () {
        return {
            notificationsData: [], 
            formData: {
                id: '',
                notificationName: '',
                notificationDisplayName: '',
                notificationDescribe: '',
                notificationType: 'sms'
            },
            formSendData: {
                notificationNameList: [],
                severity: '0',
                notificationTitle: '',
                notificationContent: '',
                recipient: []
            },
            formRules: {
                notificationNameList: [
                    { required: true, message: '请选择通知类型', trigger: 'change' }
                ],
                notificationTitle: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    { min: 1, max: 50, message: '长度在 1 到 30 个字符', trigger: 'blur' }
                ],
                notificationName: [
                    { required: true, message: '必填项', trigger: 'blur' },
                    { min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' },
                    { validator: this.validateUserCode, message: '仅限数字,字母组成', trigger: 'blur' }
                ],
                notificationDisplayName: [
                    { required: true, message: '必填项', trigger: 'change' },
                    { min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                ]
            }, 
            tableOptions: {
                searchTxt:'',
                tableData: [],
                tableAllData: [],
                selectRows: [],
                selectRow: {}
            },
            pageOptions: {
                formDialog: false,
                isUserCode: false,
                pageIndex: 1,
                maxResultCount: 20,
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
            var _this = this;
            _this.tableOptions.selectRows = [];
            selection.forEach(function (item, index) {
                _this.tableOptions.selectRows.push({
                    notificationName: _this.formData.notificationName,
                    userId: item.userId
                });
            });
        },
        doRowclick: function (row, event, column) {
            //this.$refs.dataGrid.clearSelection();
            //设置选中行
            this.$refs.dataGrid.toggleRowSelection(row);
            //
            this.tableOptions.selectRow = row;
        },
        doSaveSubscription: function () {
            var _this = this;
            this.$refs["formNotificationData"].validate(
                function (valid) {
                    if (valid) {
                        //
                        abp.ajax({
                            url: '/SysNotifications/SaveNotificationInfo',
                            data: JSON.stringify(_this.formData)
                        }).done(function (data, res, e) {
                            if (data) {
                                _this.formData.id = data;
                                _this.getNotificationsDataList();
                                _this.getDataList();
                                _this.tipSuccess('save');
                            } else {
                                _this.tipSuccess('del');
                            }
                        })
                    } else {
                        return false;
                    }
                }
            ); 
        },
        formatUserImg: function (row) {
            return this.doUserImg(row["imageUrl"], 40);
        },
        doDelSubscription: function () {
            var _this = this;

            if (!this.formData.id) {
                this.tipShow('warn', '请选择通知类型。');
                return;
            }
            this.$confirm('确定删除?', '提示', {
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/SysNotifications/DelNotificationInfo',
                    data: JSON.stringify(_this.formData),
                    type: 'POST'
                }).done(function (data, res, e) {
                    //
                    _this.tipSuccess('del');
                });
            });
          
        },
        doAddSubscription: function () {
            this.$refs["formNotificationData"].resetFields();
            //清空用户筛选条件
            this.tableOptions.searchTxt = '';
            this.tableOptions.tableData = [];
        },
        getData: function () {
            var _this = this;
            abp.ajax({
                url: '/SysNotifications/GetNotificationInfoById',
                data: JSON.stringify(_this.formData.id)
            }).done(function (data) {
                _this.formData = data;
                //清空用户筛选条件
                _this.tableOptions.searchTxt = '';
                _this.getDataList();
            });
        },
        getAllUserDataList: function () {
            var _this = this;
            abp.ajax({
                url: '/SysAccount/GetAllUserList',
                data: JSON.stringify({})
            }).done(function (data) {
                _this.tableOptions.tableAllData = data;
            });
        },
        getDataList: function () {
            var _this = this;
           //查询参数
            var parameter = {
                params: {
                    notificationName: this.formData.notificationName,
                    userNameCn: this.tableOptions.searchTxt
                },
                pagingDto: this.pageOptions
            };
            abp.ajax({
                url: '/SysNotifications/GetSubscriptionByName',
                data: JSON.stringify(parameter)
            }).done(function (data) {
                //
                _this.pageOptions.total = data.totalCount;
                _this.tableOptions.tableData = data.items;
                _this.tableOptions.selectRow = {};
                _this.tableOptions.selectRows = [];

            });
        },
        getNotificationsDataList: function (isInit) {
            var _this = this;
            //
            this.tableOptions.tableData = [];
            //
            abp.ajax({
                url: '/SysNotifications/GetNotificationInfoAll'
            }).done(function (data) {
                _this.notificationsData = data;
                //
                if (_this.notificationsData.length > 0 && isInit) {
                    //
                    _this.$refs["formNotificationData"].resetFields();
                    _this.doNotificationsClick(_this.notificationsData[0].id);
                } 
            });
        },
        doNotificationsClick: function (id) {
            this.tableOptions.searchTxt = "";
            this.formData.id = id;
            this.getData();
        },
        doSubAdd: function (dataRow) {
            var _this = this;
            var list = [];
            if (dataRow) {
                list.push({
                    userId: dataRow.userId,
                    notificationName: _this.formData.notificationName
                });
            } else {
                list = this.tableOptions.selectRows;
            }
            abp.ajax({
                url: '/SysNotifications/InsertSubscription',
                data: JSON.stringify({ params: list })
            }).done(function (data) {
                if (!dataRow) {
                    _this.getDataList();
                }
            });
        },
        doSubDel: function (dataRow) {
            var _this = this;
            var list = [];
            if (dataRow) {
                list.push({
                    userId: dataRow.userId,
                    notificationName: _this.formData.notificationName
                });
            } else {
                list = this.tableOptions.selectRows;
            }
            abp.ajax({
                url: '/SysNotifications/DeleteSubscription',
                data: JSON.stringify({ params: list })
                //type: 'POST'
            }).done(function (data) {
                if (!dataRow) {
                    _this.getDataList();
                }
            });
        },
        doSend: function (type) {
            var _this = this;
            this.$refs["formSendNotificationData"].validate(
                function (valid) {
                    if (valid) {
                        //
                        abp.ajax({
                            url: '/SysNotifications/NotificationsSend',
                            data: JSON.stringify(_this.formSendData)
                        }).done(function (data, res, e) {
                            _this.tipShow('success', 'SendSuccess');
                            //触发消息提示UI (可以考虑不触发,收到消息signalR进行调用订阅信息获取)
                            //abp.event.trigger('frame.received.ui.event');
                        }).fail(function (data, res, e) {
                            _this.tipShow('error', data.message);
                        });
                    } else {
                        return false;
                    }
                }
            );

        }, 
        validateUserCode: function (rule, value, callback) {
            if (!value) {
                callback();
                return;
            }
            if (!abp.frameCore.utils.checkChars(value)) {
                callback(new Error());
                return;
            }
            //验证通过执行
            callback();
        },
        handleSizeChange: function (val) {
            this.pageOptions.maxResultCount = val;
            this.getDataList();
        },
        handleCurrentChange: function (val, e) {
            this.pageOptions.pageIndex = val;
            this.getDataList();
        },
        doSearchData: function () {
            this.pageOptions.pageIndex = 1;
            this.getDataList();
        },

    }
});
