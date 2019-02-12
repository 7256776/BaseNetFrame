
var component = Vue.component('j-userinbox', {
    template: Vue.frameTemplate('J_Notifications/UserInbox'),
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
                tabMenu1:true,
                tabMenu2: false,
                tabTitle:"我的通知"
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
                chatSearch:'',
                currentUserId:0,                        //左侧用户列表点击的用户id
                currentUserName: '',                //左侧用户列表点击的用户名称
                currentSendUserImgUrl: '',       //左侧用户列表点击的用户头像
                chatCount: 0,                           //左侧用户列表点击的用户相关的未读聊天数量
                isShowSend:false                    //当点击了左侧用户列表显示头像以及发送按钮
            },
            charSend: {
                chatDetailed: "",
                severity: 0,
                receiveUserId: 0
            },
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
                    return subItem.notificationName == item.notificationName
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
            this.tableOptions.selectRows = selection
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
                url: '/J_Notifications/GetNotificationInfoAll',
            }).done(function (data) {
                _this.subscriptionAll = data;
            });
        },
        getSubscription: function () {
            var _this = this;
            abp.ajax({
                url: '/J_Notifications/GetSubscriptionByUser',
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
                model: {
                    state: null,
                    notificationName: this.tableOptions.searchTxt
                },
                pagingDto: this.pageOptions
            };
            //
            abp.ajax({
                url: '/J_Notifications/GetUserNotifications',
                data: JSON.stringify(param)
            }).done(function (data, res, e) {
                _this.tableOptions.tableData = data.items;
                _this.pageOptions.total = data.totalCount;
            });
        },
        doSelectSubscription: function (val) {
            this.tableOptions.searchTxt = val;
            this.getUserNotifications();
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
            this.tableOptions.gridLoading = true;
            //仅获取id
            var idList = [];
            this.tableOptions.selectRows.forEach(function (item, index) {
                idList.push(item.id);
            })
            //未读state=0 / 已读state=1
            abp.ajax({
                url: '/J_Notifications/UpdateUserNotificationStatus',
                data: JSON.stringify({ idList: idList, state: 1 })
            }).done(function (data, res, e) {
                _this.tableOptions.gridLoading = false;
                _this.getUserNotifications();
                //触发消息提示UI
                abp.event.trigger('frame.received.ui.event');
            }).fail(function (data, res, e) {
                _this.tableOptions.gridLoading = false;
            });
        },
        doClean: function (val, e) {
            var _this = this;
            this.tableOptions.gridLoading = true;
            abp.ajax({
                url: '/J_Notifications/CleanUserNotificationByName',
                data: JSON.stringify({ notificationName: this.tableOptions.searchTxt })
            }).done(function (data, res, e) {
                _this.tableOptions.gridLoading = false;
                _this.getUserNotifications();
                //触发消息提示UI
                abp.event.trigger('frame.received.ui.event');
            }).fail(function (data, res, e) {
                _this.tableOptions.gridLoading = false;
            });
        },
        doDel: function (val, e) {
            var _this = this;
            this.tableOptions.gridLoading = true;
            //仅获取id
            var idList = [];
            this.tableOptions.selectRows.forEach(function (item, index) {
                idList.push(item.id);
            })
            abp.ajax({
                url: '/J_Notifications/DelUserNotification',
                data: JSON.stringify(idList)
            }).done(function (data, res, e) {
                _this.tableOptions.gridLoading = false;
                _this.getUserNotifications();
                //触发消息提示UI
                abp.event.trigger('frame.received.ui.event');
            }).fail(function (data, res, e) {
                _this.tableOptions.gridLoading = false;
            });
        },
        doTab: function (index) {
            this.tabMenu.tabMenu1 = false;
            this.tabMenu.tabMenu2 = false;
            if (index == 0) {
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
                list.push(abp.session.userId);
            } else {
                abp.message.error("未获取到用户信息,请重新登录");
                return
            }
            abp.ajax({
                url: '/J_Notifications/InsertSubscription',
                data: JSON.stringify({ userList: list, notificationName: notificationName })
            }).done(function (data) {
                _this.getSubscription();
            });
        },
        doSubDel: function (notificationName) {
            var _this = this;
            var list = [];
            if (abp.session.userId) {
                list.push(abp.session.userId);
            } else {
                abp.message.error("未获取到用户信息,请重新登录");
                return
            }
            abp.ajax({
                url: '/J_Notifications/DeleteSubscription',
                data: JSON.stringify({ userList: list, notificationName: notificationName })
                //type: 'POST'
            }).done(function (data) {
                _this.getSubscription();
            });
        },
        doShowUserImg: function (item) {
            if (item.senderUserId != this.chatData.currentUserId) {
                return Vue.prototype.GlobalAuthorizedEntity.user.imageUrl_40;
            }
            return this.chatData.currentSendUserImgUrl;
        },
        getAllClients: function () {
            var _this = this;
            abp.ajax({
                url: '/J_Notifications/GetAllClients'
            }).done(function (data) {
                _this.chatData.chatUserAll = data;
                //加载未读的信息数量
                _this.getChatCount();
           
            });
        },
        getChatCount: function () {
            var _this = this;
            abp.ajax({
                url: '/J_Notifications/GetChatRecordSummary',
                data: "{state:0}"
            }).done(function (data) {
                //获取所有未读的消息数量
                _this.chatData.chatUserAll = _this.chatData.chatUserAll.filter(function (itemSub, index) {
                    data.forEach(function (item, index) {
                        if (itemSub.userId == item.senderUserId) {
                            itemSub["chatCount"] = item.chatCount;
                            return; 
                        }
                    });
                    return true;
                });
            });
        },
        formatdate: function (creationTime) {
            return abp.frameCore.format.formatDate(creationTime, 'yyyy-MM-dd hh:mm:ss');
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
                model: {
                    senderUserId: this.chatData.currentUserId,
                    receiveUserId: abp.session.userId,
                    chatDetailed: this.chatData.chatSearch
                },
                pagingDto: this.pagingChatDto
            }
            abp.ajax({
                url: '/J_Notifications/GetChatRecordMutualList',
                data: JSON.stringify(param)
            }).done(function (data) {
                _this.pagingChatDto.total = data.totalCount;
                _this.chatData.chatContentList = data.items;
            });
            if (this.chatData.chatCount > 0) {
                this.chatData.chatUserAll.forEach(function (item, index) {
                    if (item.userId == _this.chatData.currentUserId) {
                        item["chatCount"] = 0;
                    }
                })
                //查看后及设置该消息未已读取
                this.updateChatState(this.chatData.currentUserId);
            }
        },
        updateChatState: function (userId) {
            //批量修改该用户的未读消息
            var param = {
                senderUserId: userId,
                state: 1
            }
            abp.ajax({
                url: '/J_Notifications/UpdateChatRecordStatus',
                data: JSON.stringify(param)
            }).done(function (data) {
                //设置聊天读取状态完成
            });
        },
        doDeleteChatRecord: function (userId) {
            var _this = this;
            //批量修改该用户的未读消息
            var param = {
                senderUserId: userId
            }
            this.$confirm('确定删除与该用户的所有聊天记录么?', '提示', { type: 'warning' })
                .then(function () {
                    abp.ajax({
                        url: '/J_Notifications/DeleteChatRecord',
                        data: JSON.stringify(param)
                    }).done(function (data) {
                        _this.chatData.chatContentList=[]
                        abp.message.success("消息删除完成!")
                        //设置聊天读取状态完成
                    });
                });
        },
        doSendCaht: function () {
            var _this = this;
            //
            this.$refs["txtSms"].focus();
            if (!this.charSend.chatDetailed) {
                abp.message.warn('您还未输入消息!');
                return;
            }
            abp.ajax({
                url: '/J_Notifications/SendChatAsync',
                data: JSON.stringify(this.charSend)
            }).done(function (data) {
                if (data == true) {
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
                abp.message.error('您的消息未发送,请刷新页面或重新登录!');
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
