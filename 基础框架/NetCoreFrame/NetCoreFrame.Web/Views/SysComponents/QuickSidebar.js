
var component = Vue.component('sys-quicksidebar', {
    template: Vue.frameTemplate('SysComponents/QuickSidebar'),
    mounted: function () {
        this.getAllClients(true);
    },
    created: function () {
        //注册聊天消息窗体的刷新事件
        abp.event.on('frame.received.chat.event', this.refreshChatUI);

        var _this = this;
        //注册signalr连接完成后事件
        abp.event.on('abp.signalr.connected', function () {
            _this.$nextTick(function () {
                //signalR的在线客户需要放在页面最后进行获取,放在此处似乎不加载调整到mounted事件中加载
                //_this.getAllClients(true);
            });
        });
    },
    data: function () {
        return {
            currentUser: "",
            currentImageUrl :"",
            charSend: {
                chatDetailed: "",
                severity: 0,
                receiveUserId: 0
            },
            chatDataList: [],
            allClients: []
        };
    },
    watch: {
        //监听
    },
    computed: {
    
    },
    methods: {
        doSidebar: function () {
            //采用jquery方式给body设置,触发打开关闭聊天窗口页面
            $('body').toggleClass('page-quick-sidebar-open'); 
        },
        doCloseChatlist: function () {
            this.charSend.chatDetailed = "";
            this.charSend.severity = 0;
            this.charSend.receiveUserId = 0;
        },
        doShowUserImg: function (type, imageUrl) {
            if (type==='out') {
                return Vue.prototype.GlobalAuthorizedEntity.DefaultImgUrl + imageUrl + '_40.png';
            }
            return Vue.prototype.GlobalAuthorizedEntity.user.imageUrl_40;
        },
        doShowChatlist: function (userId) {
            var _this = this;
            this.charSend.receiveUserId = userId;
            this.charSend.chatDetailed = '';
            this.currentUser = '';
            this.$refs["txtSms"].focus();

            var ourCount = 0;
            var data = this.allClients.filter(function (item, index) {
                if (item.userId === userId) {
                    item["chatList"] = item["chatList"] || []; 
                    //获取当前正在通讯的用户名称
                    _this.currentUser = item.userNameCn;
                    _this.currentImageUrl = item.imageUrl;
                    ourCount = item["chatCount"] || 0;
                    //打开聊天窗口同时清空未读消息状态
                    item["chatCount"] = 0;
                    return true;
                }
            });

            this.chatDataList = [];
            if (data.length <= 0) {
                return;
            }
            //设置接收消息数据的对象
            this.chatDataList = data[0].chatList;
            //如果有未读数量,但是没有获取到消息内容就通过数据库获取
            if (ourCount > 0 && this.chatDataList.length === 0) {
                this.getChat(userId, ourCount);
            }

            //修改该用户的未读消息
            this.updateChatState(data[0].userId);
            //注册js事件
            this.$nextTick(function () {
                QuickSidebar.setChatSlimScroll();
            });

        },
        refreshChatUI: function (data) {
            var _this = this;
            //收到信息
            var message = data.notification.data.notificationDetailed;
            //发件人id
            var userId = data.notification.data.sendId;
            //发件日期
            var creationTime = data.notification.creationTime;
            //
            this.setChat(userId, message, creationTime, 'out');
            //接受到消息给出计数
            this.allClients = this.allClients.filter(function (itemSub, index) {
                //判断信息来自的用户id
                if (itemSub.userId === userId) {
                    //判断聊天窗口正要发送的用户id
                    if (_this.charSend.receiveUserId === userId) {
                        //修改该用户的未读消息
                        _this.updateChatState(userId);
                    } else {
                        itemSub["chatCount"] = itemSub["chatCount"] || 0;
                        //修改该用户的未读消息计数器
                        itemSub["chatCount"] = itemSub["chatCount"] + 1;
                    }
                }
                return true;
            });
        },
        doSendChat: function () {
            var _this = this;
            //
            this.$refs["txtSms"].focus();
            if (!this.charSend.chatDetailed) {
                this.tipShow('warn', '您还未输入消息');
                return;
            }
            abp.ajax({
                url: '/SysNotifications/SendChatAsync',
                data: JSON.stringify(this.charSend)
            }).done(function (data) {
                if (data === true) {
                    //发送信息写入消息到在线用户对象的了解记录集合
                    _this.setChat(_this.charSend.receiveUserId, _this.charSend.chatDetailed, new Date(), 'in');

                    _this.charSend.chatDetailed = "";
                }
            }).fail(function (data) {
                _this.tipShow('error', '您的消息未发送,请刷新页面或重新登录');
            });
        },
        updateChatState: function (userId) {
            //批量修改该用户的未读消息
            var param = {
                senderUserId: userId,
                chatState: 1
            };
            abp.ajax({
                url: '/SysNotifications/UpdateChatRecordStatus',
                data: JSON.stringify(param)
            }).done(function (data) {
                //设置聊天读取状态完成
            });
        },
        getAllClients: function (isInit) {
            var _this = this;
            abp.ajax({
                url: '/SysNotifications/GetAllClients'
            }).done(function (data) {
                _this.allClients = data;
                //加载未读的信息数量
                _this.getChatCount();
                //刷新页面后注册用户列表的点击事件
                _this.$nextTick(function () {
                    //刷新注册事件
                    QuickSidebar.refresh();
                });
                //判断是否属于初始加载
                if (isInit!==true) {
                    _this.tipShow('success', '您的消息未发送,刷新在线用户列表完成');
                } 
             
            });
        },
        getChatCount: function () {
            var _this = this;
            abp.ajax({
                url: '/SysNotifications/GetChatRecordSummary',
                data: "0"
            }).done(function (data) {
                //获取所有未读的消息数量
                _this.allClients = _this.allClients.filter(function (itemSub, index) {
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
        setChat: function (userId, message,date, type) {
            this.allClients.forEach(function (item, index) {
                if (item.userId === userId) {
                    item["chatList"] = item["chatList"] || [];
                    item["chatList"].push({
                        chatDate: abp.frameCore.format.formatDate(date,'yyyy-MM-dd hh:mm:ss'),
                        chatDetailed: message,
                        type: type
                    }); 
                    return;
                }
            });
            this.$nextTick(function () {
                QuickSidebar.setChatSlimScroll();
            });
        },
        getChat: function (senderUserId,count) {
            var _this = this;
            var param = {
                params: {
                    senderUserId: senderUserId,
                    receiveUserId: abp.session.userId
                },
                pagingDto: {
                    pageIndex: 1,
                    maxResultCount: count + 5,          //添加消息数量误差值
                    total: 0
                }
            };
            abp.ajax({
                url: '/SysNotifications/GetChatRecordList',
                data: JSON.stringify(param)
            }).done(function (data) {
                //获取所有未读的消息数量
                data.items.forEach(function (item, index) {
                    _this.setChat(item.senderUserId, item.chatDetailed, item.creationTime, 'out');
                });
            });
        }, 
    }


});
