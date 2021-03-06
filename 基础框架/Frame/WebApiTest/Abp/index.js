﻿new Vue({
    el: '#WebApiApp',
    data: function () {
        return {
            activeTabName: 'webApiTest',
            temp: {
                url: 'http://192.168.1.140:3010',
                url1: 'http://localhost:2018/'
            },
            abpFormData: {
                userCode: 'admin',
                password: '888888',
                token: '',
                forgeToKen: '',
                apiUrl: 'http://192.168.1.128:3010/',
                apiActionUrl: '/api/services/frame/sysMenus/GetMenusList',
                returnData: ''
            },
            oAuthFormData: {
                userCode: 'admin',
                password: '888888',
                token: '',
                resToken: '',
                forgeToKen: '',
                apiUrl: 'http://192.168.1.128:3010/',
                apiActionUrl: '/api/services/frame/sysMenus/GetMenusList',
                returnData: ''
            },
            webApiFormData: {
                userCode: 'admin',
                password: '888888',
                apiActionUrl: 'http://',
                actionType: 'POST',
                returnData: ''
            }
        };
    },
    created: function () {
        //解决ie9以下跨域问题
        jQuery.support.cors = true;
    },
    methods: {
        //获取token(Abp方案)
        doAbpToken: function () {
            var _this = this;

            this.doForgeToKen(_this.abpFormData);

            var formData = {
                userCode: this.abpFormData.userCode,
                userNameCn: 'admin',
                password: this.abpFormData.password,
                isPersistent: false,
            };

            //采用abp的ajax方式获取token
            //abp.ajax({
            //    url: _this.abpFormData.apiUrl + "/api/Account/Authenticate",
            //    type: "POST",
            //    data: JSON.stringify(formData)
            //}).done(function (data, e, ae) {
            //    console.log('11');
            //}).fail(function (data, e, ae) {
            //    console.log('11');
            //});

            //由于是基于abp方式获取token因此需要传递防伪照token
            $.ajax({
                url: _this.abpFormData.apiUrl + "/api/Account/Authenticate",
                type: "POST",
                data: JSON.stringify(formData),
                headers: {
                    "X-XSRF-TOKEN": _this.abpFormData.forgeToKen
                },
                contentType: 'application/json',
                complete: function (e, state) {
                    if (e.status === 200) {
                        _this.abpFormData.token = e.responseJSON.result;
                    } else {
                        _this.abpFormData.returnData = e.statusText
                    }
                }
            });//
        },
        //请求服务获取数据(Abp方案)
        doAbpAction: function () {
            var _this = this;
            $.ajax({
                url: _this.abpFormData.apiUrl + _this.abpFormData.apiActionUrl,
                type: "POST",
                headers: {
                    "Authorization": "Bearer " + _this.abpFormData.token,
                    "X-XSRF-TOKEN": _this.abpFormData.forgeToKen
                },
                //dataType: "json",
                contentType: 'application/json',
                complete: function (e, state) {
                    if (e.status === 200) {
                        _this.abpFormData.returnData = JSON.stringify(e.responseJSON);
                    } else {
                        _this.abpFormData.returnData = e.statusText
                        _this.abpFormData.returnData += '\r\n' + e.responseText

                    }

                }
            });//
        },
        //获取反伪造token
        doForgeToKen: function (formData) {
            //由于在测试环境下可能存在多个入口调取服务端的情况下,
            //因此通过ABP反伪造token的cookie名称一样的特性获取一下反伪造token加载到请求头里面即可.
            formData.forgeToKen = abp.security.antiForgery.getToken();

            $.ajax({
                url: formData.apiUrl + "/api/Account/GetTokenCookie",
                type: "GET",
                async: false,
                //dataType: 'json',
                contentType: 'application/json',
                complete: function (e, state) {
                    if (e.status === 200) {
                        formData.forgeToKen = e.responseJSON[0].value[0].substring(11);
                    } else {
                        formData.returnData = e.statusText;
                    }
                }
            });//
        },
        //获取Token(欧哦服方案)
        doOAuthToken: function () {
            var _this = this;
            //
            this.doForgeToKen(_this.oAuthFormData);
            //采用PassWord的方式,必须组合成url形式的参数集合
            var requestData = "grant_type=password&username=" + this.oAuthFormData.userCode + "&password=" + this.oAuthFormData.password;
           
            $.ajax({
                url: _this.oAuthFormData.apiUrl + "/oauth/token",
                type: "POST",
                data: requestData,
                //dataType: "json",
                contentType: 'application/json',
                headers: {
                    //把用户组账号与key进行Base64_Encode编译后进行传输
                    "Authorization": "Basic " + _this.base64Encode(_this.oAuthFormData.userCode + ":" + _this.oAuthFormData.password)
                },

                complete: function (e, status) {
                    if (e.status !== 200) {
                        _this.oAuthFormData.returnData = e.statusText
                        _this.oAuthFormData.returnData += '\r\n' + e.responseText
                        return;
                    }
                    //保存安全令牌(token)
                    _this.oAuthFormData.token = e.responseJSON.access_token;
                    //保存获取的刷新key
                    _this.oAuthFormData.resToken = e.responseJSON.refresh_token;
                }
            });
        },
        //请求服务(欧哦服方案)
        doOAuthAction: function () {
            var _this = this; 
            $.ajax({
                url: _this.oAuthFormData.apiUrl + _this.oAuthFormData.apiActionUrl,
                type: "POST",
                headers: {
                    "Authorization": "Bearer " + _this.oAuthFormData.token,
                    "X-XSRF-TOKEN": + _this.oAuthFormData.forgeToKen
                },
                //dataType: "json",
                contentType: 'application/json',
                complete: function (e, state) {
                    if (e.status !== 200) {
                        _this.oAuthFormData.returnData = e.statusText;
                        _this.oAuthFormData.returnData += '\r\n' + e.responseText;
                        return;
                    }
                    _this.oAuthFormData.returnData = JSON.stringify(e.responseJSON);
                }
            });//
        },
        //刷新token(欧哦服方案)
        doRefreshToken: function () {
            var _this = this;
            //刷新ToKen
            $.ajax({
                url: _this.oAuthFormData.apiUrl + "/oauth/token",
                type: "post",
                data: { "grant_type": "refresh_token", refresh_token: _this.oAuthFormData.resToken },
                dataType: "json",
                //刷新安全令牌通常不需要传递用户账号与密码
                //headers: {
                //    "Authorization": "Basic " + _this.base64Encode(_this.oAuthFormData.userCode + ":" + _this.oAuthFormData.password)
                //},
                complete: function (e, status) {
                    if (e.status !== 200) {
                        _this.oAuthFormData.returnData = e.statusText
                        _this.oAuthFormData.returnData += '\r\n' + e.responseText
                        return;
                    }
                    //保存安全令牌(token)
                    _this.oAuthFormData.token = e.responseJSON.access_token;
                    //保存获取的刷新key
                    _this.oAuthFormData.resToken = e.responseJSON.refresh_token;
                }
            });
        },
        //简单测试webapi(不包含验证)
        doWebApiAction: function () {
            var _this = this;

            $.ajax({
                url: this.webApiFormData.apiActionUrl,
                type: this.webApiFormData.actionType,
                //headers: {
                //    "Authorization": "Bearer " + _this.abpFormData.token,
                //    "X-XSRF-TOKEN": _this.abpFormData.forgeToKen
                //},
                //dataType: "json",
                //contentType: 'application/json',
                complete: function (e, state) {
                    if (e.status !== 200) {
                        _this.webApiFormData.returnData = e.statusText;
                        _this.webApiFormData.returnData += '\r\n' + e.responseText;
                        return;
                    }
                    _this.webApiFormData.returnData = JSON.stringify(e.responseJSON);
                }
            });//
        },
        //编码格式化
        base64Encode: function (str) {
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

    }
});
