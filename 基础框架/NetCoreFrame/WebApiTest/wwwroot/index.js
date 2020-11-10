new Vue({
    el: '#WebApiApp',
    data: function () {
        return {
            activeTabName: 'IdentityServer4',
            temp: {
                url: 'http://192.168.1.140:3010',
                url1: 'http://localhost:2018/'
            },
            abpFormData: {
                userCode: 'admin',
                password: '888888',
                token: '',
                encryptedRefreshToken: '',
                encryptedAccessToken: '',
                expireInDate: '',
                forgeToKen: '',
                apiUrl: 'http://localhost:18377',
                apiActionUrl: 'http://localhost:18377/api/services/frame/SysDictExtension/GetDictType',
                returnData: ''
            },
            IdentityServerFormData: {
                clientId: 'clientAll',
                clientSecret: 'secretAll',
                userCode: 'zjf',
                password: 'zjfpass', 
                token: '',
                resToken: '',
                forgeToKen: '',
                apiUrl: 'http://localhost:18377',
                apiActionUrl: 'http://localhost:11726/api/Demo/GetData',
                parameterData: '',
                returnData: '',
                grantTypes: 'password',
                actionType: 'POST'
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
        doJwtToken: function () {
            var _this = this;

            var formData = {
                userCode: this.abpFormData.userCode,
                userNameCn: 'admin',
                password: this.abpFormData.password,
                isPersistent: false,
            }; 

            //由于是基于abp方式获取token因此需要传递防伪照token
            $.ajax({
                url: _this.abpFormData.apiUrl + "/api/TokenAuth/AuthenticateAuth",
                type: "POST",
                data: JSON.stringify(formData),
                contentType: 'application/json',
                complete: function (e, state) {
                    if (e.status === 200) {
                        //未成功获取token
                        if (!e.responseJSON.result.resultState) {
                            _this.abpFormData.returnData = e.responseJSON.result.resultMessage
                            return;
                        }
                        _this.abpFormData.returnData = "";
                        //_this.abpFormData.token = e.responseJSON.result;
                        _this.abpFormData.token = e.responseJSON.result.accessToken;
                        _this.abpFormData.expireInDate = e.responseJSON.result.expireInDate;
                        _this.abpFormData.encryptedAccessToken = e.responseJSON.result.encryptedAccessToken;
                        _this.abpFormData.encryptedRefreshToken = e.responseJSON.result.encryptedRefreshToken;

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
                url: _this.abpFormData.apiActionUrl,
                type: "POST",
                headers: {
                    "Authorization": "Bearer " + _this.abpFormData.token,
                    //"X-XSRF-TOKEN": _this.abpFormData.forgeToKen
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
        //请求服务获取数据(Abp方案)
        doRefreshJwtToken: function () {
            var _this = this;
            $.ajax({
                url: _this.abpFormData.apiUrl  + "/api/TokenAuth/RefreshJwtToken",
                type: "POST",
                headers: {
                    "Authorization": "Bearer " + _this.abpFormData.token,
                    "refresh": _this.abpFormData.encryptedRefreshToken,
                    //"X-XSRF-TOKEN": _this.abpFormData.forgeToKen
                },
                //dataType: "json",
                contentType: 'application/json',
                complete: function (e, state) {
                    if (e.status === 200) {
                        //未成功获取token
                        if (!e.responseJSON.result.resultState) {
                            _this.abpFormData.returnData = e.responseJSON.result.resultMessage
                            return;
                        }
                        _this.abpFormData.returnData = "";

                        _this.abpFormData.token = e.responseJSON.result.accessToken;
                        _this.abpFormData.expireInDate = e.responseJSON.result.expireInDate;
                        _this.abpFormData.encryptedAccessToken = e.responseJSON.result.encryptedAccessToken;
                        _this.abpFormData.encryptedRefreshToken = e.responseJSON.result.encryptedRefreshToken;

                    } else {
                        _this.abpFormData.returnData = e.statusText
                        _this.abpFormData.returnData += '\r\n' + e.responseText
                    }
                }
            });//
        },

        //获取Token(Identity4方案)
        doIdentityServerToken: function () {
            var _this = this;
            //
            //采用PassWord的方式,必须组合成url形式的参数集合
            //var requestData = "grant_type=password&username=" + this.oAuthFormData.userCode + "&password=" + this.oAuthFormData.password;
            var requestData = {
                client_id: this.IdentityServerFormData.clientId,
                client_secret: this.IdentityServerFormData.clientSecret,
                grant_type: this.IdentityServerFormData.grantTypes,     //password  client_credentials  
                username: this.IdentityServerFormData.userCode,
                password: this.IdentityServerFormData.password,
            };
            $.ajax({
                url: _this.IdentityServerFormData.apiUrl + "/connect/token" + "?grant_type=client_credentials",
                type: "POST",
                //contentType: 'application/json',
                data: requestData,
                complete: function (e, status) {
                    //
                    _this.IdentityServerFormData.returnData = e.statusText
                    _this.IdentityServerFormData.returnData += '\r\n' + e.responseText
                    //
                    if (e.status !== 200) {
                        return;
                    }
                    //保存安全令牌(token)
                    _this.IdentityServerFormData.token = e.responseJSON.access_token;
                    //保存获取的刷新key
                    _this.IdentityServerFormData.resToken = e.responseJSON.refresh_token;
                    //e.responseJSON.expires_in
                    //e.responseJSON.token_type
                    //e.responseJSON.scope
                }
            });
        },
        //请求服务(Identity4方案)
        doOAuthAction: function () {
            var _this = this;
           
            $.ajax({
                url: _this.IdentityServerFormData.apiActionUrl,
                type: _this.IdentityServerFormData.actionType,
                headers: {
                    //"X-XSRF-TOKEN": + _this.IdentityServerFormData.forgeToKen,
                    //"Access-Control-Allow-Origin": "*",
                    "Authorization": "Bearer " + _this.IdentityServerFormData.token
                },
                //dataType: "json",
                contentType: 'application/json',
                data: JSON.stringify(_this.IdentityServerFormData.parameterData),
                complete: function (e, state) {
                    if (e.status !== 200) {
                        _this.IdentityServerFormData.returnData = e.statusText;
                        _this.IdentityServerFormData.returnData += '\r\n' + e.responseText;
                        return;
                    }
                    _this.IdentityServerFormData.returnData = JSON.stringify(e.responseJSON);
                }
            });//
        },
        //刷新token(Identity4方案)
        doRefreshToken: function () {
            var _this = this;
            //刷新ToKen
            $.ajax({
                url: _this.IdentityServerFormData.apiUrl + "/connect/token",
                type: "post",
                data: {
                    client_id: _this.IdentityServerFormData.clientId,
                    client_secret: _this.IdentityServerFormData.clientSecret,
                    grant_type: "refresh_token",
                    refresh_token: _this.IdentityServerFormData.resToken
                },
                dataType: "json",
                //刷新安全令牌通常不需要传递用户账号与密码
                //headers: {
                //    "Authorization": "Basic " + _this.base64Encode(_this.oAuthFormData.userCode + ":" + _this.oAuthFormData.password)
                //},
                complete: function (e, status) {
                    //
                    _this.IdentityServerFormData.returnData = e.statusText
                    _this.IdentityServerFormData.returnData += '\r\n' + e.responseText
                    //
                    if (e.status !== 200) {
                        return;
                    }
                    //保存安全令牌(token)
                    _this.IdentityServerFormData.token = e.responseJSON.access_token;
                    //保存获取的刷新key
                    _this.IdentityServerFormData.resToken = e.responseJSON.refresh_token;
                }
            });
        },

        //简单测试webapi(不包含验证)
        doWebApiAction: function () {
            var _this = this;

            $.ajax({
                url: this.webApiFormData.apiActionUrl ,
                type: this.webApiFormData.actionType,
                //contentType: 'application/json',
                //data: data,
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
