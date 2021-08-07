/*
 * 框架公共Js库.
*  依赖 vue.js
 */
 
//设置ElementUI全局默认组件尺寸
Vue.use(ELEMENT, { size: 'small' })

// 通过ElementUI实现abp消息提示框
var globalVue = new Vue({
    methods: {
        showNotification: function (type, message, title, details) {
            //本地化处理
            message = abp.frameCore.localization.getLocalization(message)

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
                        _this.$alert(details, abp.frameCore.localization.getLocalization('details'), { dangerouslyUseHTMLString: true });
                    }
                   
                }
            });
        },
        showMessage: function (type, message, details) {
            //本地化处理
            details = abp.frameCore.localization.getLocalization(details)
            message = abp.frameCore.localization.getLocalization(message)

            var _this = this;
            var msg = "";
            if (message && details) {
                msg =  "<div style='padding-top:10px;'>" + details + "</div>";
            }
            if (!message) {
                message = details;
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
            this.$alert(details, abp.frameCore.localization.getLocalization('details'), { dangerouslyUseHTMLString: true });
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
    * 输入格式必须是 dateValue = 2020-11-07T22:10:33
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
     
    /**
    * 将数值四舍五入(保留2位小数)后格式化成金额形式
    *(调用方式 : abp.frameCore.format.amountFormat )
    * @param num 数值(Number或者String)
    * @return 金额格式的字符串,如'1,234,567.45'
    * @type String
    */
    frameCore.format.amountFormat = function (num) {
        num = num.toString().replace(/\$|\,/g, '');
        if (isNaN(num))
            num = "0";
        sign = (num == (num = Math.abs(num)));
        num = Math.floor(num * 100 + 0.50000000001);
        cents = num % 100;
        num = Math.floor(num / 100).toString();
        if (cents < 10)
            cents = "0" + cents;
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
            num = num.substring(0, num.length - (4 * i + 3)) + ',' +
                num.substring(num.length - (4 * i + 3));
        return (((sign) ? '' : '-') + num + '.' + cents);
    }

    /**
    * 过滤特殊字符以及汉字
    *(调用方式 : abp.frameCore.format.stringReplace )
    * @param value 需要过滤的字符串
    * @return 完成替换后的字符串
    * @type String
    */
    frameCore.format.stringReplace = function (value) {
        var reg = new RegExp("[`~!@#$^&*()=+|{}':;',\\[\\].<>/?~！@#￥……&*（）——|{}【】‘；：”“'。，、？·]");
        var regZh = /[\u4e00-\u9fa5]/g;
        var str = '';
        for (var i = 0, l = value.length; i < value.length; i++) {
            str = str + value.substr(i, 1).replace(reg, '');
        }
        return str.replace(regZh, "");   
    }

    /*******************************************************通知 公用函数*********************************************************/
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

    /*****************************************************本地化 公用函数***********************************************************/
    frameCore.localization = frameCore.localization || {};
    //本地化资源名称
    frameCore.localization.localizationName = "FrameLocalization";
    //获取框架本地化语言(调用方式 : abp.frameCore.localization.getLocalization )
    frameCore.localization.getLocalization = function (key) {
        //获取本地化资源
        var localizationData = abp.localization.values[frameCore.localization.localizationName];
        //默认本地资源值
        var localizationValue = key;
        //处理大小写忽略的方式获取本地化值
        for (let item in localizationData) {
            if (key && item.toLowerCase() == key.toLowerCase()) {
                localizationValue = localizationData[item];
                return localizationValue;
            }
        } 
        return localizationValue;
    };

    /*******************************************************常规 公用函数*********************************************************/
    frameCore.utils = frameCore.utils || {};

    /*
     * 电话号码验证 (调用方式 : abp.frameCore.utils.checkChars )
     * 验证规则：仅允许包含 数字 字母大小写
        * @param str 验证的字符串
        * @param rule 验证的规则 该参数必须是数组 , 为空为包含全部规则
        *               参数示例:  ['zh', 'en', 'num']
        *                  zh = 允许汉字
        *                  en = 允许字母
        *                  num = 允许数字
    */
    frameCore.utils.checkChars = function (str, rule) {
        if (!rule || rule.length==0) {
            rule= ['zh', 'en', 'num'];
        }
        //
        var checkRule = {
            zh: '\\u4e00-\\u9fa5',      //允许汉字
            en: 'A-Za-z',                     //允许字母
            num: '0-9'                        //允许数字
        };
    
        //初始正则条件
        var tempStr = '';
        rule.forEach(function (item, index) {
            tempStr = tempStr + checkRule[item];
        });
        //拼接最终的正则
        eval("var tempRe = /^[" + tempStr + "]+$/;");
        if (tempRe.test(str)) {
            return true;
        }
        return false;
    };

    /*
     * 电话号码验证 (调用方式 : abp.frameCore.utils.checkPhone )
     * 验证规则：区号+号码，区号以0开头，3位或4位
     * 号码由7位或8位数字组成
     * 区号与号码之间可以无连接符，也可以“-”连接
    */
    frameCore.utils.checkPhone = function (str) {
        var re = /^0\d{2,3}-?\d{7,8}$/;
        if (re.test(str)) {
            return true
        }
        return false;
    };

    /*
     * 手机号码验证 (调用方式 : abp.frameCore.utils.checkMobile )
     * 验证规则：11位数字，以1开头。
    */
    frameCore.utils.checkMobile = function (str) {
        re = /^1\d{10}$/
        if (re.test(str)) {
            return true
        }
        return false;
    };

    /*
     * 检查银行卡号  (调用方式 : abp.frameCore.utils.checkBankNo )
     * 验证规则：长度必须在16到19,全为数字,开头6位符合规范,Luhn校验
    */
    frameCore.utils.checkBankNo = function (bankno) {
        var bankno = bankno.replace(/\s/g, '');
        if (bankno == "") {
            return false;
        }
        //银行卡号长度必须在16到19之间
        if (bankno.length < 16 || bankno.length > 19) {
            return false;
        }

        //银行卡号必须全为数字
        var num = /^\d*$/;
        if (!num.exec(bankno)) {
            return false;
        }

        //开头6位 银行卡号开头6位不符合规范
        var strBin = "10,18,30,35,37,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,58,60,62,65,68,69,84,87,88,94,95,98,99";
        if (strBin.indexOf(bankno.substring(0, 2)) == -1) {
            return false;
        }
        return true;
        //暂取消Luhn校验
        var fn = function (bankno) {
            //取出最后一位（与luhn进行比较）
            var lastNum = bankno.substr(bankno.length - 1, 1);
            //前15或18位
            var first15Num = bankno.substr(0, bankno.length - 1);
            var newArr = new Array();
            //前15或18位倒序存进数组
            for (var i = first15Num.length - 1; i > -1; i--) {
                newArr.push(first15Num.substr(i, 1));

            }
            //奇数位*2的积 <9
            var arrJiShu = new Array();
            //奇数位*2的积 >9
            var arrJiShu2 = new Array();
            //偶数位数组
            var arrOuShu = new Array();
            for (var j = 0; j < newArr.length; j++) {
                //奇数位
                if ((j + 1) % 2 == 1) {
                    if (parseInt(newArr[j]) * 2 < 9)
                        arrJiShu.push(parseInt(newArr[j]) * 2);
                    else
                        arrJiShu2.push(parseInt(newArr[j]) * 2);
                }
                else //偶数位
                    arrOuShu.push(newArr[j]);

            }
            //奇数位*2 >9 的分割之后的数组个位数
            var jishu_child1 = new Array();
            //奇数位*2 >9 的分割之后的数组十位数
            var jishu_child2 = new Array();
            for (var h = 0; h < arrJiShu2.length; h++) {
                jishu_child1.push(parseInt(arrJiShu2[h]) % 10);
                jishu_child2.push(parseInt(arrJiShu2[h]) / 10);
            }
            //奇数位*2 < 9 的数组之和
            var sumJiShu = 0;
            //偶数位数组之和
            var sumOuShu = 0;
            //奇数位*2 >9 的分割之后的数组个位数之和
            var sumJiShuChild1 = 0;
            //奇数位*2 >9 的分割之后的数组十位数之和
            var sumJiShuChild2 = 0;
            var sumTotal = 0;
            for (var m = 0; m < arrJiShu.length; m++) {
                sumJiShu = sumJiShu + parseInt(arrJiShu[m]);
            }

            for (var n = 0; n < arrOuShu.length; n++) {
                sumOuShu = sumOuShu + parseInt(arrOuShu[n]);
            }

            for (var p = 0; p < jishu_child1.length; p++) {
                sumJiShuChild1 = sumJiShuChild1 + parseInt(jishu_child1[p]);
                sumJiShuChild2 = sumJiShuChild2 + parseInt(jishu_child2[p]);
            }
            //计算总和
            sumTotal = parseInt(sumJiShu) + parseInt(sumOuShu) + parseInt(sumJiShuChild1) + parseInt(sumJiShuChild2);

            //计算luhn值
            var k = parseInt(sumTotal) % 10 == 0 ? 10 : parseInt(sumTotal) % 10;
            var luhn = 10 - k;
            //验证通过
            if (lastNum == luhn) {
                return true;
            } else {
                return false;
            }
        };
        return fn(bankno);
    };

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
                if (dataList[i][dataName] === dataValue) {
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
                if (dataList[i][dataName] === dataValue) {
                    resList.push(dataList[i]);
                }
                //递归获取所有子节点菜单路由
                queryRef(dataList[i][childName])
            }
        };
        //
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
                if (nodeItem[id] === item[parentid]) {
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

    /*
     * 创建Guid
     * (调用方式 : abp.frameCore.utils.newGuid )
     * isSymbol = 是否添加guid之间间隔的 '-' 字符
    */
    frameCore.utils.newGuid = function (isSymbol) {
        //递归设置节点的子节点对象
        var symbol = '-';
        if (isSymbol && isSymbol === true) {
            symbol = '';
        }
        var S4 = function () {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1)
        }
        return (S4() + S4() + symbol + S4() + symbol + S4() + symbol + S4() + symbol + S4() + S4() + S4())
    };

    /*
     * 密码验证级别
     * (调用方式 : abp.frameCore.utils.passwordStrength )
     * level = 验证级别 分1、2、3
     * 3 = 大小写字母、数字、至少两个字符，三个条件必须同时满足
     * 2 = 大小写字母、数字、至少两个字符，三个条件满足其中两个
     * 1 = 大小写字母
    */
    frameCore.utils.passwordStrength = function (value, level) {
        // 这个是必须包含 '_' 字符 \W+\D+

        if (/[a-zA-Z]+/.test(value) && /[0-9]+/.test(value) && /\W+/.test(value) && level == 3) {
            return true;
        }
        else if (/[a-zA-Z]+/.test(value) && /[0-9]+/.test(value) && level == 2) {
            return true;
        }
        else if (/[a-zA-Z]+/.test(value) && /\W+/.test(value) && level == 2) {
            return true;
        }
        else if (/[0-9]+/.test(value) && /\W+/.test(value) && level == 2) {
            return true;
        }
        else if (/[a-zA-Z]+/.test(value) && level == 1) {
            return true;
        }
        else {
            return false;
        }
    };

    /*****************************************************初始化自义定页面路由对象***********************************************************/
    frameCore.frameRoutes = frameCore.frameRoutes || {};

    // 框架静态路由(可累加)
    var frameRoutesList = [
        {
            //首页
            path: '/Views/SysHome/DesktopPage',
            name: 'sys-home',
            //meta: {
            //    menuData: [{
            //        url: '/Views/Sys_Home/DesktopPage',
            //        displayName: '首页'
            //    }]
            //}
        },
        {
            //我的设置
            path: '/Views/SysAccount/UserSettings',
            name: 'sys-usersetting',
            meta: {
                menuData: [{
                    url: '/Views/SysAccount/UserSettings',
                    displayName: '我的设置'
                }]
            }
        },
        {
            //我的收件箱
            path: '/Views/SysNotifications/UserInbox',
            name: 'sys-userinbox',
            meta: {
                menuData: [{
                    url: '/Views/SysNotifications/UserInbox',
                    displayName: '我的收件箱'
                }]
            }
        },
        {
            //搜索结果页
            path: '/Views/SysComponents/SearchPage',
            name: 'sys-searchpage',
            meta: {
                menuData: [{
                    url: '/Views/SysComponents/SearchPage',
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
        var tip = abp.frameCore.localization.getLocalization('Tips');
        switch (type) {
            case "success":
                return abp.frameCore.localization.getLocalization('Successful') + tip;
            case "info":
                return abp.frameCore.localization.getLocalization('Info') + tip;
            case "warning":
                return abp.frameCore.localization.getLocalization('Warning') + tip;
            case "error":
                return abp.frameCore.localization.getLocalization('Error') + tip;
            default:
                return abp.frameCore.localization.getLocalization('Info') + tip;
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
        if (notification.data.notificationType === "sms") {
            //直接调用abp的消息弹出ui
            //abp.notifications.showUiNotifyForUserNotification(userNotification);
            //调用自己定义的消息发送(页面显示消息提示窗口)
            abp.frameCore.notifications.showUiNotifications(userNotification);
            //触发所有注册了(frame.received.ui.event)的事件
            abp.event.trigger('frame.received.ui.event');
        } else if (notification.data.notificationType === "chat") {
            //(页面显示消息提示窗口)
            abp.frameCore.notifications.showUiNotifications(userNotification);
            //触发所有注册了(frame.received.chat.event)的事件
            abp.event.trigger('frame.received.chat.event', userNotification);
        } else {
            abp.event.trigger('frame.received.event', userNotification);
        }
    });

    //注册事件,外部项目调用实现该回调事件
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

    /* 通过用户信息获取授权token
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
    };

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
                if (e.status !== 200) {
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
                if (e.status !== 200) {
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
