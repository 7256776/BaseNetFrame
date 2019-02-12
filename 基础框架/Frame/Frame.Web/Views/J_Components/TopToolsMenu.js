
var component = Vue.component('j-toptoolsmenu', {
    template: Vue.frameTemplate('J_Components/TopToolsMenu'),
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
        updateUserImg: function () {
            this.userImg = Vue.prototype.GlobalAuthorizedEntity.user.imageUrl_40;
        },
        formatIco: function (severity) {
            var css = "label-primary";
            //Info = 0,
            if (severity == "0")
                css = "label-info"
            //Success = 1,
            if (severity == "1")
                css = "label-success"
            //Warn = 2
            if (severity == "2")
                css = "label-warning"
            //Error = 3
            if (severity == "3")
                css = "label-danger"
            //Fatal = 4
            if (severity == "4")
                css = "label-danger"
            return css;
        },
        doRead: function (details, id) {
            this.$alert(details, '详细信息', { dangerouslyUseHTMLString: true });
            //仅获取id
            var idList = [];
            idList.push(id);
            //未读state=0 / 已读state=1
            abp.ajax({
                url: '/J_Notifications/UpdateUserNotificationStatus',
                data: JSON.stringify({ idList: idList, state: 1 })
            }).done(function (data, res, e) {
                //触发消息提示UI
                abp.event.trigger('frame.received.ui.event');
            })
        },
        refreshNotificationsUI: function () {
            var _this = this;
            var param = {
                model: { state: 0 },
                pagingDto: {
                    pageIndex: 1,
                    maxResultCount: 10,
                    total: 0
                }
            };
            //
            abp.ajax({
                url: '/J_Notifications/GetUserNotifications',
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
        }
    }

});
