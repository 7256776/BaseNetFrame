﻿/*
 * 框架公共Js库.
*  依赖 vue.js
 */
 
//设置ElementUI全局默认组件尺寸
Vue.use(ELEMENT, { size: 'small' })

// 通过ElementUI实现abp消息提示框
var globalVue = new Vue({
    methods: {
        showNotification: function (type, message, title, details) {
            var _this = this;
          
            title = title || getNotifyTitleBySeverity(type)

            this.$notify({
                title: title,
                message: message,
                type: type,
                onClick: function () {
                    //if (type === 'success' || type === 'info') {
                    //    return;
                    //}
                    if (details) {
                        _this.$alert(details, '详细信息', { dangerouslyUseHTMLString: true });
                    }
                   
                }
            });
        },
        showMessage: function (type, message, details) {
            var _this = this;
            var msg = "";
            if (!message) {
                message = details;
            } else {
                msg =  "<div style='padding-top:10px;'>" + details + "</div>";
            }
            msg = "<strong>" + message + "</strong>" + msg ;

            this.$message({
                dangerouslyUseHTMLString: true,
                showClose:true,
                duration: 4000,
                message: msg,
                type: type
            });
        },
        showDetails: function (details) {
            this.$alert(details, '详细信息', { dangerouslyUseHTMLString: true });
        }
    }
});

/*
    注册公用函数类库 $
*/
(function () {
    //创建框架核心库js命名空间
    var frameCore = abp.utils.createNamespace(abp, 'frameCore');

    /*******************************************************格式化公用函数*********************************************************/
    frameCore.format = frameCore.format || {};
    /*
    * 设置命名空间的对象(调用方式 : abp.frameCore.format.formatDate )
    * 格式化日期
    * yyyy=年 MM=月 dd=日 hh=小时 mm=分 ss=秒
    */
    frameCore.format.formatDate = function (dateValue, formatStr) {
        var date = new Date(dateValue);
        var str = formatStr;
        var Week = ['日', '一', '二', '三', '四', '五', '六'];

        str = str.replace(/yyyy|YYYY/, date.getFullYear());
        str = str.replace(/yy|YY/, (date.getYear() % 100) > 9 ? (date.getYear() % 100).toString() : '0' + (date.getYear() % 100));

        str = str.replace(/MM/, date.getMonth() + 1 > 9 ? (date.getMonth() + 1).toString() : '0' + (date.getMonth() + 1));
        str = str.replace(/M/g, date.getMonth() + 1);

        str = str.replace(/w|W/g, Week[date.getDay()]);

        str = str.replace(/dd|DD/, date.getDate() > 9 ? date.getDate().toString() : '0' + date.getDate());
        str = str.replace(/d|D/g, date.getDate());

        str = str.replace(/hh|HH/, date.getHours() > 9 ? date.getHours().toString() : '0' + date.getHours());
        str = str.replace(/h|H/g, date.getHours());
        str = str.replace(/mm/, date.getMinutes() > 9 ? date.getMinutes().toString() : '0' + date.getMinutes());
        str = str.replace(/m/g, date.getMinutes());

        str = str.replace(/ss|SS/, date.getSeconds() > 9 ? date.getSeconds().toString() : '0' + date.getSeconds());
        str = str.replace(/s|S/g, date.getSeconds());
        return str;
    }

    //日期过去时间格式化
    frameCore.format.formatPastDate = function (dateValue) {
        var date = new Date(dateValue);
        var d = (new Date().getTime() - date.getTime()) / 1000 / 3600;
        var str = "已经很久了";
        if (d < 1) {
            str = "刚刚";
        } else if (d > 1 && d < 24) {
            str = parseInt(d) + " 小时前";
        } else if (d >= 24) {
            str = parseInt(d / 24) + " 天前";
        }
        return str;
    }

    /*******************************************************通知公用函数*********************************************************/
    frameCore.notifications = frameCore.notifications || {};

    //通知ui显示(调用方式 : abp.frameCore.notifications.showUiNotifications )
    frameCore.notifications.showUiNotifications = function (userNotification) {
        var notification = userNotification.notification
        var message = notification.data.message || notification.data.properties.message;
        //获取消息的详细信息
        var detailed = notification.data.notificationDetailed || message;
        //获取消息的类型
        var uiNotifyFunc = abp.notifications.getUiNotifyFuncBySeverity(userNotification.notification.severity);
        //发送消息
        uiNotifyFunc(message, notification.data.title, detailed)
    };

    /*****************************************************本地化公用函数***********************************************************/
    frameCore.localization = frameCore.localization || {};
    //本地化资源名称
    frameCore.localization.localizationName = "J_LOCALIZATION";
    //获取框架本地化语言(调用方式 : abp.frameCore.localization.getLocalization )
    frameCore.localization.getLocalization = function (key) {
        return abp.localization.localize(key, frameCore.localization.localizationName);
    };

    /*******************************************************常规公用函数*********************************************************/
    frameCore.utils = frameCore.utils || {};

    /*
     * 递归查询返回查询单个对象 (调用方式 : abp.frameCore.utils.queryRecursive )
     * data = 数组集合
     * dataValue=过滤值
     * childName=子集合属性名称
     * dataName=过滤值属性名称
    */
    frameCore.utils.queryRecursive = function (data, dataValue, childName, dataName) {

        //
        var queryRef = function (dataList) {
            if (!dataList) {
                return
            }
            for (var i = 0; i < dataList.length; i++) {
                if (dataList[i][dataName] == dataValue) {
                    return dataList[i];
                }
                //递归获取所有子节点菜单路由
                var data = queryRef(dataList[i][childName])
                if (data) {
                    return data;
                }
            }
        };
        //设置路由列表
        return queryRef(data);
    };

    /*
     * 递归查询返回符合条件的集合 (调用方式 : abp.frameCore.utils.queryRecursiveList )
     * data = 数组集合
     * dataValue=过滤值
     * childName=子集合属性名称
     * dataName=过滤值属性名称
    */
    frameCore.utils.queryRecursiveList = function (data, dataValue, childName, dataName) {
        var resList = [];
        //
        var queryRef = function (dataList) {
            if (!dataList) {
                return;
            }
            for (var i = 0; i < dataList.length; i++) {
                if (dataList[i][dataName] == dataValue) {
                    resList.push(dataList[i]);
                }
                //递归获取所有子节点菜单路由
                queryRef(dataList[i][childName])
            }
        };
        //设置路由列表
        queryRef(data);
        return resList;
    };

    /*
 * 根据平行数据处理成内嵌的数据对象,会新增一个children存放子节点对象集合
 * [
 *  属性原有字段...
 *  children;[{},{}]
 * ]
 * (调用方式 : abp.frameCore.utils.buildNestData )
 * dataList = 数组集合
 * id=数据主键id
 * parentid=父节点id
*/
    frameCore.utils.buildNestData = function (dataList, id, parentid) {
        //递归设置节点的子节点对象
        var getTreeChildrenNodes = function (nodeItem, list) {
            list.forEach(function (item, index) {
                if (nodeItem[id] == item[parentid]) {
                    nodeItem.children = nodeItem.children || [];
                    nodeItem.children.push(item);
                    //
                    getTreeChildrenNodes(item, list);
                }
            });
        }

        //获取所有根节点
        var dataRes = [];
        //
        dataList.forEach(function (item, index) {
            if (!item[parentid]) {
                dataRes.push(item)
                //设置根节点的子节点对象
                getTreeChildrenNodes(item, dataList)
            }
        });
        return dataRes;
    };

    /*****************************************************初始化自义定页面路由对象***********************************************************/
    frameCore.frameRoutes = frameCore.frameRoutes || {};

    // 框架静态路由(可累加)
    var frameRoutesList = [
        {
            //首页
            path: '/Views/J_Home/DesktopPage',
            name: 'j-home',
            //meta: {
            //    menuData: [{
            //        url: '/Views/J_Home/DesktopPage',
            //        displayName: '首页'
            //    }]
            //}
        },
        {
            //我的设置
            path: '/Views/J_Account/UserSettings',
            name: 'j-usersetting',
            meta: {
                menuData: [{
                    url: '/Views/J_Account/UserSettings',
                    displayName: '我的设置'
                }]
            }
        },
        {
            //我的收件箱
            path: '/Views/J_Notifications/UserInbox',
            name: 'j-userinbox',
            meta: {
                menuData: [{
                    url: '/Views/J_Notifications/UserInbox',
                    displayName: '我的收件箱'
                }]
            }
        },
        {
            //搜索结果页
            path: '/Views/J_Components/SearchPage',
            name: 'j-searchpage',
            meta: {
                menuData: [{
                    url: '/Views/J_Components/SearchPage',
                    displayName: '搜索结果页'
                }]
            }
        }
    ];

    //设置自定义路由(参数路由集合)
    frameCore.frameRoutes.setFrameRoutes = function (routes) {
        routes.forEach(function (item, index) {
            frameRoutesList.push(item);
        });
    }

    //获取所有自定义页面路由
    frameCore.frameRoutes.getFrameRoutes = function () {
        return frameRoutesList;
    }

    /*****************************************************初始化搜索扩展菜单数据对象***********************************************************/
    frameCore.frameSearch = frameCore.frameSearch || {};

    //设置扩展菜单该示例数据可以删除
    frameCore.frameSearch.menus = [
        {
            text: '自定义搜索项目一',
            value: {
                myParam1: "参数",
                myParam2: "[{ 'name': '对象参数' }]"
            },
            url: '',
        },
        {
            text: '自定义搜索项目二',
            value: {
                myParam1: "自定义搜索项目二参数"
            },
            url: ''
        }
    ];



})();

/*
    通知与消息提示工具接口
*/
$(function () {
    //根据提示类型获取默认的标题文案
    getNotifyTitleBySeverity = function (type) {
        switch (type) {
            case "success":
                return "成功提示";
            case "info":
                return "消息提示";
            case "warning":
                return "警告提示";
            case "error":
                return "错误提示";
            default:
                return "消息提示";
        }
    };

    /* MESSAGE  页面顶部显示的提示 **************************************************/
    abp.message.info = function (details, message) {
        return globalVue.showMessage('info', message, details);
    };

    abp.message.success = function (details, message) {
        return globalVue.showMessage('success', message, details);
    };

    abp.message.warn = function (details, message) {
        return globalVue.showMessage('warning', message, details);
    };

    abp.message.error = function (details, message) {
        return globalVue.showMessage('error', message, details);
    };

    /* NOTIFICATION  右脚上弹出的消息框*********************************************/
    abp.notify.success = function (message, title, details) {
        return globalVue.showNotification('success', message, title, details);
    };

    abp.notify.info = function (message, title, details) {
        globalVue.showNotification('info', message, title, details);
    };

    abp.notify.warn = function (message, title, details) {
        return globalVue.showNotification('warning', message, title, details);
    };

    abp.notify.error = function (message, title, details) {
        return globalVue.showNotification('error', message, title, details);
    };

});

$(function () {
    abp.ajax.defaultError.message = abp.frameCore.localization.getLocalization('DefaultError');
    abp.ajax.defaultError.details = abp.frameCore.localization.getLocalization('DefaultErrorDetail');
    abp.ajax.defaultError401.message = abp.frameCore.localization.getLocalization('DefaultError401');
    abp.ajax.defaultError401.details = abp.frameCore.localization.getLocalization('DefaultErrorDetail401');
    abp.ajax.defaultError403.message = abp.frameCore.localization.getLocalization('DefaultError403');
    abp.ajax.defaultError403.details = abp.frameCore.localization.getLocalization('DefaultErrorDetail403');
    abp.ajax.defaultError404.message = abp.frameCore.localization.getLocalization('DefaultError404');
    abp.ajax.defaultError404.details = abp.frameCore.localization.getLocalization('DefaultErrorDetail404');

});
/*
    通知发送
    聊天发送
    基于signalR
*/
$(function () {
    //注册通知调用事件
    abp.event.on('abp.notifications.received', function (userNotification) {
        if (!userNotification) {
            return;
        }
        //entityTypeName = null
        //获取消息数据对象
        var notification = userNotification.notification
        //判断消息订阅的类型(可以通过notification.type 判断或通过通知订阅名称判断)
        if (notification.data.notificationType == "sms") {
            //直接调用abp的消息弹出ui
            //abp.notifications.showUiNotifyForUserNotification(userNotification);
            //调用自己定义的消息发送
            abp.frameCore.notifications.showUiNotifications(userNotification);

            //触发所有注册了(frame.received.ui.event)的事件
            abp.event.trigger('frame.received.ui.event');
        } else if (notification.data.notificationType == "chat") {
            //触发所有注册了(frame.received.chat.event)的事件
            abp.event.trigger('frame.received.chat.event', userNotification);
        } else {
            abp.event.trigger('frame.received.event', userNotification);
        }
    });


    //外部项目调用实现该回调事件
    abp.event.on('frame.received.event', function (userNotification) {

        /* 
            userNotification 对象的主要参数注释 
            {
                "userId": "用户id",
                "notification": {
                    "notificationName": "订阅的消息名称",
                    "data": {   //通知数据对象根据具体实现类确定属性名称
                        "message": "消息内容",
                        "type": "通知类型",
                        "properties": { //通知包含的自定义信息
                            "属性key": "value值"
                        }
                    },
                    "entityType": "实体通知的对象类型",
                    "entityTypeName": "实体通知的对象名称",
                    "entityId": "实体通知数据对象ID",
                    "severity": "通知的类型    info: 0,  success: 1, warn: 2,  error: 3,  fatal: 4",,
                    "creationTime": "通知创建日期",
                    "id": "通知数据id"
                },
            };
        */

    });



});




//
var serviceConnection = serviceConnection || {};
//webapi授权业务
(function () {

    var apiUrl = "http://192.168.1.140:3010/";
    //apiUrl = "";
    //授权对象
    serviceConnection.oAuthFormData = serviceConnection.oAuthFormData || {
        //access_token: '',           //授权token   
        //token_type: '',              //类型
        //expires_in: '',                //token有效时长(单位:秒)
        //refresh_token: '',          //刷新token
        //issued: '',                     //Token发布日期
        //expires: ''                     //Token失效日期
    };

    /**通过用户信息获取授权token
     *
     *   serviceConnection.authenticationTokenByUser(userCode, password)
     *       .then(function (data) {
     *           //执行登录业务
     *       }).catch(function (reason) {
     *           //返回异常
     *       });
     */
    serviceConnection.authenticationTokenByUser = function (u, p) {
        return new Promise(function (resolve, reject) {
            serviceConnection.handleOAuthToken(resolve, reject, u, p);
        })
    }

    //获取授权token
    serviceConnection.handleOAuthToken = function (resolve, reject, userCode, password) {
        //采用PassWord的方式,必须组合成url形式的参数集合
        var requestData = "grant_type=password&username=" + userCode + "&password=" + password;

        $.ajax({
            url: apiUrl + "/oauth/token",
            type: "POST",
            data: requestData,
            //dataType: "json",
            contentType: 'application/json',
            headers: {
                //把用户组账号与key进行Base64_Encode编译后进行传输
                "Authorization": "Basic " + base64Encode(userCode + ":" + password)
            },
            complete: function (e, status) {
                //获取请求状态
                serviceConnection.oAuthFormData.status = e.status;
                if (e.status != 200) {
                    serviceConnection.oAuthFormData.error = e.responseText;
                    reject();
                }
                //保存安全令牌(token)
                serviceConnection.oAuthFormData = e.responseJSON;
                resolve();
            }
        });
    }

    //
    serviceConnection.getToken = function () {
        //如果刷新key是空 (需重新授权)
        if (!serviceConnection.oAuthFormData.refresh_token) {
            return '';
        }
        //
        if (!serviceConnection.oAuthFormData.access_token) {
            serviceConnection.handleRefreshToken();
        }
        //当前日期
        var nowDate = new Date();
        //有效日期
        var outTime = new Date(Date.parse(serviceConnection.oAuthFormData.expires))
        //验证token是否过期
        if (nowDate > outTime) {
            serviceConnection.handleRefreshToken();
        }
        //执行完同步获取token的方式后如果token还是为空 (需重新授权)
        if (!serviceConnection.oAuthFormData.access_token) {
            return '';
        }
        //返回token
        return serviceConnection.oAuthFormData.access_token;
    }

    //同步执行
    serviceConnection.handleRefreshToken = function () {
        var _this = this;
        //刷新ToKen
        $.ajax({
            url: apiUrl + "/oauth/token",
            type: "post",
            data: { "grant_type": "refresh_token", refresh_token: serviceConnection.oAuthFormData.refresh_token },
            dataType: "json",
            async: false,
            //刷新安全令牌通常不需要传递用户账号与密码
            //headers: {
            //    "Authorization": "Basic " + _this.base64Encode(_this.oAuthFormData.userCode + ":" + _this.oAuthFormData.password)
            //},
            complete: function (e, status) {
                //获取请求状态
                serviceConnection.oAuthFormData.status = e.status;
                if (e.status != 200) {
                    serviceConnection.oAuthFormData.error = e.responseText;
                }
                //保存安全令牌(token)
                serviceConnection.oAuthFormData = e.responseJSON;
            }
        });
    }

    //64编码格式
    function base64Encode(str) {
        //进行编码否则服务端无法获取用户验证信息
        var c1, c2, c3;
        var base64EncodeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        var i = 0, len = str.length, string = '';

        while (i < len) {
            c1 = str.charCodeAt(i++) & 0xff;
            if (i === len) {
                string += base64EncodeChars.charAt(c1 >> 2);
                string += base64EncodeChars.charAt((c1 & 0x3) << 4);
                string += "==";
                break;
            }
            c2 = str.charCodeAt(i++);
            if (i === len) {
                string += base64EncodeChars.charAt(c1 >> 2);
                string += base64EncodeChars.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
                string += base64EncodeChars.charAt((c2 & 0xF) << 2);
                string += "=";
                break;
            }
            c3 = str.charCodeAt(i++);
            string += base64EncodeChars.charAt(c1 >> 2);
            string += base64EncodeChars.charAt(((c1 & 0x3) << 4) | ((c2 & 0xF0) >> 4));
            string += base64EncodeChars.charAt(((c2 & 0xF) << 2) | ((c3 & 0xC0) >> 6));
            string += base64EncodeChars.charAt(c3 & 0x3F);
        }
        return string;
    }

})();

//
//(function (send) {
//    //设置请求头文件添加token
//    XMLHttpRequest.prototype.send = function (data) {
//        //判断参数不需要添加token的请求
//        if (data && data.indexOf("grant_type") != -1 ) {
//            return send.call(this, data);
//        }
//        //获取token
//        var token = serviceConnection.getToken();
//        if (!token) {
//            token = "";
//        }
//        //设置请求头
//        this.setRequestHeader("authorization", "bearer " + token);

//        return send.call(this, data);
//    };
//})(XMLHttpRequest.prototype.send);
