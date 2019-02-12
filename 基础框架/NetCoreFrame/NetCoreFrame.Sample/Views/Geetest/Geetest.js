
var component = Vue.component('geetest-plug', {
    template: "<div id=\"verification-container\">{{messageLoad}}</div>",
    created: function () {
        this.initGeetestFn();
    },
    data: function () {
        return {
            messageLoad:'正在加载验证信息...',
            formData: {
                id: null,
            },
        };
    },
    watch: {
        //监听
    },
    computed: {
        //计算
    },
    methods: {
        initGeetestFn: function () {
            //初始加载数据
            var _this = this;
            //
            abp.ajax({
                url: '/Geetest/getCaptcha',
                type: 'POST'
            }).done(function (data, res, e) {
                var result = JSON.parse(data);
                initGeetest({
                    gt: result.gt,
                    challenge: result.challenge,
                    offline: !result.success, // 表示用户后台检测极验服务器是否宕机，一般不需要关注
                    new_captcha: result.new_captcha, //用于宕机时表示是新验证码的宕机

                    
                    product: "embed", // 产品形式，包括：float，embed，popup。注意只对PC版验证码有效
                    
                    // 更多配置参数请参见：http://www.geetest.com/install/sections/idx-client-sdk.html#config
                },
                    _this.doBuildGeetest
                );
            });
        },
        //创建验证框
        doBuildGeetest: function (captchaObj) {
            var _this = this;
            //加载验证组件到容器
            captchaObj.appendTo('#verification-container');
     
            //页面验证成功调用后台服务进行验证
            captchaObj.onSuccess(
                function () {
                    _this.doVerification(captchaObj)
                }
            );
            //验证组件加载完成
            captchaObj.onReady(function () {
                _this.messageLoad = '';
                console.log("验证组件加载完成..");
            });
            //验证出现错误
            captchaObj.onError(function () {
                console.log("验证组件出现错误..");
            });
            //关闭
            captchaObj.onClose(function () {
                console.log("验证组件关闭..");
                // 验证码
                //console.log(captchaObj.verify()); 
            });

        },
        //验证回调事件
        doVerification: function (captchaObj) {
            var _this = this;
            var result = captchaObj.getValidate();

            var geetestdata = {
                geetestChallenge: result.geetest_challenge,
                geetestValidate: result.geetest_validate,
                geetestSeccode: result.geetest_seccode,
            };

            abp.ajax({
                url: '/Geetest/ValidateCaptcha',
                data: JSON.stringify(geetestdata),
                type: 'POST'
            }).done(function (data, res, e) {
                if (data === 'success') {
                    //自定义提交表单事件
                    _this.$emit('doPostCallback', geetestdata)
                } else {
                    //flat
                    alert('验证失败重新验证.');
                }
                // 调用该接口进行重置
                captchaObj.reset(); 
            });
        },


    }
});
