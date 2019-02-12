
var component = Vue.component('j-notifications', {
    template: Vue.frameTemplate('J_Notifications/Index'),
    created: function () {
        this.getNotificationsDataList(true);
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
                    { validator: this.validateUserCode, message: '仅限数字,字母,下划线组成', trigger: 'blur' }
                ],
                notificationDisplayName: [
                    { required: true, message: '必填项', trigger: 'change' },
                    { min: 1, max: 50, message: '长度在 1 到 50 个字符', trigger: 'blur' }
                ]
            }, 
            tableOptions: {
                searchTxt:'',
                tableData: [],
                selectRows: [],
                selectRow: {}
            }
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
                if (item.userCode.indexOf(_this.tableOptions.searchTxt) > -1 || item.userNameCn.indexOf(_this.tableOptions.searchTxt) > -1) {
                    return true;
                }
                return false;
            });
        }
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
                            url: '/J_Notifications/SaveNotificationInfo',
                            data: JSON.stringify(_this.formData)
                        }).done(function (data, res, e) {
                            //_this.getUserList();
                            if (data) {
                                _this.formData.id = data;
                                _this.getNotificationsDataList();
                                _this.getDataList();
                                abp.message.success('保存成功');
                            } else {
                                abp.message.error('保存失败');
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
                abp.message.warn('请选择通知类型!');
                return;
            }
            this.$confirm('确定删除?', '提示', {
                type: 'warning'
            }).then(function () {
                abp.ajax({
                    url: '/J_Notifications/DelNotificationInfo',
                    data: JSON.stringify(_this.formData),
                    type: 'POST'
                }).done(function (data, res, e) {
                    //
                    abp.message.success('删除成功');
                    _this.getNotificationsDataList();
                });
            });
          
        },
        doAddSubscription: function () {
            this.$refs["formNotificationData"].resetFields();
            this.tableOptions.tableData = [];
        },
        getData: function () {
            var _this = this;
            abp.ajax({
                url: '/J_Notifications/GetNotificationInfoById',
                data: JSON.stringify(_this.formData.id)
            }).done(function (data) {
                _this.formData = data;
                _this.getDataList();
            });
        },
        getDataList: function () {
            var _this = this;
            abp.ajax({
                url: '/J_Notifications/GetSubscriptionByName',
                data: JSON.stringify(_this.formData.notificationName)
            }).done(function (data) {
                _this.tableOptions.tableData = data;
            });
        },
        getNotificationsDataList: function (isInit) {
            var _this = this;
            //
            this.tableOptions.tableData = [];
            //
            abp.ajax({
                url: '/J_Notifications/GetNotificationInfoAll'
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
                url: '/J_Notifications/InsertSubscription',
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
                url: '/J_Notifications/DeleteSubscription',
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
                            url: '/J_Notifications/NotificationsSend',
                            data: JSON.stringify(_this.formSendData)
                        }).done(function (data, res, e) {
                            abp.message.success('发送成功');
                            //触发消息提示UI
                            abp.event.trigger('frame.received.ui.event');
                        }).fail(function (data, res, e) {
                            abp.message.error(data.message);
                        });
                    } else {
                        return false;
                    }
                }
            );

        }, 
        validateUserCode: function (rule, value, callback) {
            var reg = /^[0-9a-zA-Z_]+$/;
            if (!value) {
                callback();
                return;
            }
            if (!reg.test(value)) {
                callback(new Error());
                return;
            }
            //验证通过必须执行
            callback();
        }

    }
});
