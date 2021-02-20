
var component = Vue.component('sys-toptoolsmenu', {
    template: Vue.frameTemplate('SysComponents/TopToolsMenu'),
    created: function () {
        //是否启用语言
        this.languages.isMultilingual = abp.setting.getBoolean("IsMultilingual");
        //获取语言信息
        this.languages.currentLanguage = abp.localization.currentLanguage;
        this.languages.languagesData = abp.localization.languages;
        this.languages.isChat = abp.setting.getBoolean("IsChat");
        //注册用户头像刷新事件
        abp.event.on('frame.userimg.ui.event', this.updateUserImg);
        //页面加载同时就刷新推送消息
        abp.event.trigger('frame.userimg.ui.event');

        //注册消息推送窗体的刷新事件
        abp.event.on('frame.received.ui.event', this.refreshNotificationsUI);
        //页面刷新同时就刷新推送消息
        abp.event.trigger('frame.received.ui.event');
    },
    data: function () {
        return {
            userImg:'',
            notifications: {
                items: [],
                total: 0
            },
            languages: {
                languagesData: [],
                currentLanguage: {},
                isMultilingual: false,
                isChat: true
            }
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
        updateUserImg: function () {
            this.userImg = Vue.prototype.GlobalAuthorizedEntity.user.imageUrl_40;
        },
        formatIco: function (severity) {
            var css = "label-primary";
            //Info = 0,
            if (severity === "0")
                css = "label-info";
            //Success = 1,
            if (severity === "1")
                css = "label-success";
            //Warn = 2
            if (severity === "2")
                css = "label-warning";
            //Error = 3
            if (severity === "3")
                css = "label-danger";
            //Fatal = 4
            if (severity === "4")
                css = "label-danger";
            return css;
        },
        doRead: function (details, id) {
            this.$alert(details, '详细信息', { dangerouslyUseHTMLString: true });
            //仅获取id
            var idList = [];
            idList.push({
                id: id,
                state: 1
            });
            //未读state=0 / 已读state=1
            abp.ajax({
                url: '/SysNotifications/UpdateUserNotificationStatus',
                data: JSON.stringify({ params: idList })
            }).done(function (data, res, e) {
                //触发消息提示UI (可以考虑不触发,收到消息signalR进行调用订阅信息获取)
                //abp.event.trigger('frame.received.ui.event');
            })
        },
        refreshNotificationsUI: function () {
            var _this = this;
            var param = {
                params: { state: 0 },
                pagingDto: {
                    pageIndex: 1,
                    maxResultCount: 10,
                    total: 0
                }
            };
            //
            abp.ajax({
                url: '/SysNotifications/GetUserNotifications',
                data: JSON.stringify(param)
            }).done(function (data, res, e) {
                data.items.forEach(function (item, index) {
                    item["formatData"] = JSON.parse(item.data);
                    item["formatPastDate"] = abp.frameCore.format.formatPastDate(item.creationTime);
                    item["formatCss"] = _this.formatIco(item.severity);
                });
                _this.notifications.items = data.items;
                _this.notifications.total = data.totalCount;
            });
        },
        doLogout: function (details, id) {
     
            this.$confirm('确定退出?', '提示', {
                //confirmButtonText: '确定',
                //cancelButtonText: '取消',
                type: 'warning'
            }).then(function () {

                window.location.href = "/SysLogin/Logout";

                //abp.ajax({
                //    url: '/SysLogin/Logout',
                //    type: 'GET'
                //}).done(function (data, res, e) {

                //}).fail(function (data, res, e) {

                //});
            }).catch(function (action) {
                //取消操作必须有避免js链式调用报异常
            });

        },

    }

});
