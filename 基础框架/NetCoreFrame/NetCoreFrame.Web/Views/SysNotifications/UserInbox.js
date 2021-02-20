
var component = Vue.component('sys-userinbox', {
    template: Vue.frameTemplate('SysNotifications/UserInbox'),
    created: function () {
        this.getSubscription();
        this.getSubscriptionAll(); 
    },
    data: function () {
        return {
            tableOptions: {
                gridLoading: false,
                searchTxt: '',
                tableData: [],
                selectRows: [],
                selectRow: {}
            },
            tabMenu: {
                tabMenu1: true,
                tabMenu2: false,
                tabTitle: "我的通知"
            },
            formRules: {
                userNameCn: [
                    { required: true, message: '必填项', trigger: 'change' },
                    { min: 1, max: 30, message: '长度在 1 到 30 个字符', trigger: 'blur' }
                ]
            },
            formData: {
                id: null,
                userCode: null,
                userNameCn: null,
                sex: "1",
                emailAddress: null,
                phoneNumber: null,
                description: '',
                isActive: true
            },
            subscription: [],
            subscriptionAll: [],
            pageOptions: {
                pageIndex: 1,
                maxResultCount: 20,
                total: 0
            },
            pagingChatDto: {
                pageIndex: 1,
                maxResultCount: 10,
                total: 0
            },
            chatData: {
                chatUserAll: [],
                chatContentList: [],
                chatSearch: '',
                currentUserId: 0,                        //左侧用户列表点击的用户id
                currentUserName: '',                //左侧用户列表点击的用户名称
                currentSendUserImgUrl: '',       //左侧用户列表点击的用户头像
                chatCount: 0,                           //左侧用户列表点击的用户相关的未读聊天数量
                isShowSend: false                    //当点击了左侧用户列表显示头像以及发送按钮
            },
            charSend: {
                chatDetailed: "",
                severity: 0,
                receiveUserId: 0
            },
            btnLoad: false
        };
    },
    watch: {
        //监听
    },
    computed: {
        subscriptionOut: function () {
            var _this = this;
            return this.subscriptionAll.filter(function (item, index) {
                var isdata = _this.subscription.some(function (subItem, index) {
                    return subItem.notificationName === item.notificationName;
                });
                return !isdata;
            });
        },
        formatData: function (data) {
            this.tableOptions.tableData.forEach(function (item, index) {
                item["formatData"] = JSON.parse(item.data);
            });
            return this.tableOptions.tableData;
        },
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
        },
        getSubscriptionAll: function () {
            var _this = this;
            abp.ajax({
                url: '/SysNotifications/GetNotificationInfoAll'
            }).done(function (data) {
                _this.subscriptionAll = data;
            });
        },
        getSubscription: function () {
            var _this = this;
            abp.ajax({
                url: '/SysNotifications/GetSubscriptionByUser'
            }).done(function (data) {
                _this.subscription = data;
                if (data.length>0) {
                    _this.tableOptions.searchTxt = data[0].notificationName;
                    _this.getUserNotifications();
                }
            });
        },
        getUserNotifications: function () {
            var _this = this;
            var param = {
                params: {
                    state: null,
                    notificationName: this.tableOptions.searchTxt
                },
                pagingDto: this.pageOptions
            };
            _this.tableOptions.gridLoading = true;
            //
            abp.ajax({
                url: '/SysNotifications/GetUserNotifications',
                data: JSON.stringify(param)
            }).done(function (data, res, e) {
                _this.tableOptions.tableData = data.items;
                _this.pageOptions.total = data.totalCount;
                _this.tableOptions.gridLoading = false;
            });
        },
        doSelectSubscription: function (val) {
            this.tableOptions.searchTxt = val;
            this.getUserNotifications();
        },
        doSearchChat: function () {
            this.pagingChatDto.pageIndex = 1;
            this.doChatList();
        },
        handleSizeChange: function (val) {
            this.pageOptions.maxResultCount = val;
            this.getUserNotifications();
        },
        handleCurrentChange: function (val, e) {
            this.pageOptions.pageIndex = val;
            this.getUserNotifications();
        },
        doRead: function (val, e) {
            var _this = this;
            //仅获取id
            var idList = [];
            this.tableOptions.selectRows.forEach(function (item, index) {
                idList.push({
                    id: item.id,
                    state: 1
                });
            })
            if (idList.length == 0) {
                _this.tipShow('warn', '请选择需设置的通知消息。');
                return;
            }
            this.tableOptions.gridLoading = true;
            //未读state=0 / 已读state=1
            abp.ajax({
                url: '/SysNotifications/UpdateUserNotificationStatus',
                data: JSON.stringify({ params: idList })
            }).done(function (data, res, e) {
                _this.tableOptions.gridLoading = false; 
                //此处调用了两次获取订阅消息的函数
                //刷新当前页面列表
                _this.getUserNotifications();
                //触发消息提示UI  (刷新消息提示工具栏)
                abp.event.trigger('frame.received.ui.event');
            }).fail(function (data, res, e) {
                _this.tableOptions.gridLoading = false;
            });
        },
        doClean: function (val, e) {
            var _this = this;
            this.$confirm('确定清空所有消息?',
                '提示',
                { type: 'warning' })
                .then(function () {
                    //遮罩
                    _this.tableOptions.gridLoading = true;
                    abp.ajax({
                        url: '/SysNotifications/CleanUserNotificationByName',
                        data: JSON.stringify(_this.tableOptions.searchTxt)
                    }).done(function (data, res, e) {
                        _this.tableOptions.gridLoading = false;
                        _this.getUserNotifications();
                        //触发消息提示UI
                        abp.event.trigger('frame.received.ui.event');
                    }).fail(function (data, res, e) {
                        _this.tableOptions.gridLoading = false;
                    });
                }).catch(function (action) {
                    //取消操作必须有避免js链式调用报异常
                });
        },
        doDel: function (val, e) {
            var _this = this;
            //仅获取id
            var idList = [];
            this.tableOptions.selectRows.forEach(function (item, index) {
                idList.push(item.id);
            });
            if (idList.length==0) {
                this.tipShow('warn', '请选择需删除的通知消息。');
                return;
            }
            this.$confirm('确定删除消息?',
                '提示',
                { type: 'warning' })
                .then(function () {
                    _this.tableOptions.gridLoading = true;
                    abp.ajax({
                        url: '/SysNotifications/DelUserNotification',
                        data: JSON.stringify(idList)
                    }).done(function (data, res, e) {
                        _this.tableOptions.gridLoading = false;
                        _this.getUserNotifications();
                        //触发消息提示UI
                        abp.event.trigger('frame.received.ui.event');
                    }).fail(function (data, res, e) {
                        _this.tableOptions.gridLoading = false;
                    });
                }).catch(function (action) {
                    //取消操作必须有避免js链式调用报异常
                });
        },
        doTab: function (index) {
            this.tabMenu.tabMenu1 = false;
            this.tabMenu.tabMenu2 = false;
            if (index === 0) {
                this.tabMenu.tabMenu1 = true;
                this.tabMenu.tabTitle = "我的通知";
            } else {
                this.tabMenu.tabMenu2 = true;
                this.getAllClients();
                this.tabMenu.tabTitle = "我的消息";
            }
        },
        doSubAdd: function (notificationName) {
            var _this = this;
            var list = [];
            if (abp.session.userId) {
                list.push({
                    userId: abp.session.userId,
                    notificationName: notificationName
                });
            } else {
                this.tipShow('error', '未获取到用户信息,请重新登录');
                return;
            }
            abp.ajax({
                url: '/SysNotifications/InsertSubscription',
                data: JSON.stringify({ params: list })
            }).done(function (data) {
                _this.getSubscription();
            });
        },
        doSubDel: function (notificationName) {
            var _this = this;
            var list = [];
            if (abp.session.userId) {
                list.push({
                    userId: abp.session.userId,
                    notificationName: notificationName
                });
            } else {
                this.tipShow('error', '未获取到用户信息,请重新登录');
                return;
            }
            abp.ajax({
                url: '/SysNotifications/DeleteSubscription',
                data: JSON.stringify({ params: list })
                //type: 'POST'
            }).done(function (data) {
                _this.getSubscription();
            });
        },
        doShowUserImg: function (item) {
            if (item.senderUserId !== this.chatData.currentUserId) {
                return Vue.prototype.GlobalAuthorizedEntity.user.imageUrl_40;
            }
            return this.chatData.currentSendUserImgUrl;
        },
        getAllClients: function () {
            var _this = this;
            abp.ajax({
                url: '/SysNotifications/GetAllClients'
            }).done(function (data) {
                _this.chatData.chatUserAll = data;
                //加载未读的信息数量
                _this.getChatCount();
            });
        },
        getChatCount: function () {
            var _this = this;
            abp.ajax({
                url: '/SysNotifications/GetChatRecordSummary',
                data: "0"
            }).done(function (data) {
                //获取所有未读的消息数量
                _this.chatData.chatUserAll = _this.chatData.chatUserAll.filter(function (itemSub, index) {
                    data.forEach(function (item, index) {
                        if (itemSub.userId === item.senderUserId) {
                            itemSub["chatCount"] = item.chatCount;
                            return; 
                        }
                    });
                    return true;
                });
            });
        },
        doSetCurrentUser: function (item) {
            //获取当前所选择的用户信息
            this.chatData.currentUserId = item.userId;
            this.chatData.currentUserName = item.userNameCn;
            this.chatData.chatCount = item.chatCount;
            this.chatData.currentSendUserImgUrl = Vue.prototype.GlobalAuthorizedEntity.DefaultImgUrl + item.imageUrl + '_40.png';  
            this.chatData.isShowSend = true;
            //
            this.charSend.receiveUserId = item.userId;
            this.doChatList();
        },
        doChatList: function () {
            var _this = this;
            var param = {
                params: {
                    senderUserId: this.chatData.currentUserId,
                    receiveUserId: abp.session.userId,
                    chatDetailed: this.chatData.chatSearch
                },
                pagingDto: this.pagingChatDto
            };
            abp.ajax({
                url: '/SysNotifications/GetChatRecordMutualList',
                data: JSON.stringify(param)
            }).done(function (data) {
                _this.pagingChatDto.total = data.totalCount;
                _this.chatData.chatContentList = data.items;
            });
            if (this.chatData.chatCount > 0) {
                this.chatData.chatUserAll.forEach(function (item, index) {
                    if (item.userId === _this.chatData.currentUserId) {
                        item["chatCount"] = 0;
                    }
                });
                //查看后及设置该消息未已读取
                this.updateChatState(this.chatData.currentUserId);
            }
        },
        updateChatState: function (userId) {
            //批量修改该用户的未读消息
            var param = {
                senderUserId: userId,
                state: 1
            };
            abp.ajax({
                url: '/SysNotifications/UpdateChatRecordStatus',
                data: JSON.stringify(param)
            }).done(function (data) {
                //设置聊天读取状态完成
            });
        },
        doDeleteChatRecord: function (userId) {
            var _this = this;
            this.$confirm('确定删除与该用户的所有聊天记录么?', '提示', { type: 'warning' })
                .then(function () {
                    abp.ajax({
                        url: '/SysNotifications/DeleteChatRecord',
                        data: JSON.stringify(userId)
                    }).done(function (data) {
                        _this.chatData.chatContentList = [];
                        _this.tipSuccess('del');
                    });
                }).catch(function (action) {
                    //取消操作必须有避免js链式调用报异常
                });
        },
        doSendCaht: function () {
            var _this = this;
            _this.btnLoad = true;
            //
            this.$refs["txtSms"].focus();
            if (!this.charSend.chatDetailed) {
                _this.btnLoad = false;
                _this.tipShow('warn', '您还未输入消息。');
                return;
            }
            abp.ajax({
                url: '/SysNotifications/SendChatAsync',
                data: JSON.stringify(this.charSend)
            }).done(function (data) {
                _this.btnLoad = false;
                //返回发送状态，发送成功直接在页面添加信息;
                if (data === true) {
                    var data = {
                        chatDetailed: _this.charSend.chatDetailed,
                        creationTime: new Date(),
                        receiveUserId: _this.charSend.receiveUserId,
                        senderUserId: abp.session.userId
                    };
                    _this.chatData.chatContentList.splice(0, 0, data);
                    _this.charSend.chatDetailed = "";
                }
            }).fail(function (data) {
                _this.btnLoad = false;
                this.tipShow('error', '您的消息未发送，请刷新页面或重新登录。');
            });
        },
        handleChatSizeChange: function (val) {
            this.pagingChatDto.maxResultCount = val;
            this.doChatList();
        },
        handleChatCurrentChange: function (val, e) {
            this.pagingChatDto.pageIndex = val;
            this.doChatList();
        },
    }
});
