
var component = Vue.component('geetest-test', {
    template: Vue.frameTemplate('Geetest/Index'),
    components: {
        "geetest-plug": sampleAssemble.GeetestPlug,
        jsonformat: componentAssemble.SysJsonFormat
    },
    created: function () {
    },
    data: function () {
        return {
            geetest: {
                geetestChallenge: null,
                geetestValidate: null,
                geetestSeccode: null,
            },
            rsaEncryption: [
                { text: 'OaepSHA1' },
                { text: 'OaepSHA256' },
                { text: 'OaepSHA384' },
                { text: 'OaepSHA512' },
                { text: 'Pkcs1' },
            ],
            formData: {
                id: null,
                privateKey: null,
                publicKey: null,
                originalData: "",
                encryptData: "",
                decryptData: "",
                signData: "",
                rsaEncryption: "",
                keySizeInBits: 1024,
                message: ""
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
        doSubmit: function () {
            var _this = this;
            abp.ajax({
                url: '/Geetest/ValidateCaptcha',
                data: JSON.stringify(_this.geetest),
                type: 'POST'
            }).done(function (data, res, e) {
                if (data === 'success') {
                    //自定义提交表单事件
                    abp.message.success('还是验证成功了.');
                } else {
                    //flat
                    alert('验证失败重新验证.');
                }
            });
        },
        doPostCallback: function (geetestdata) {
            //
            /* 
             * 验证完成后返回的验证数据
             * 1. 通常到此处验证已经完成可以调用正式登陆业务
             * 2. 也可以吧该参数传递到后台再次验证,验证的函数同样使用 ValidateCaptcha回调函数处理,可用于手动完成极验证后,很长时间后才点击登陆验证业务时使用.
             */
            this.geetest = geetestdata;
            abp.message.success('居然验证成功了.');
        },
        doCreateAsymmetricAlgorithmSecretKey: function () {
            var _this = this;
            var data = {
                keySizeInBits: this.formData.keySizeInBits,
                rsaEncryption: this.formData.rsaEncryption,
            };
            //初始加载数据
            abp.ajax({
                url: '/Geetest/CreateAsymmetricAlgorithmSecretKey',
                type: 'POST',
                data: JSON.stringify(data)
            }).done(function (data, res, e) {
                if (res.success) {
                    //公钥相对私钥显示内容少,其他内容相同,因此页面仅显示私钥内容作为演示
                    _this.formData.privateKey = data.privateKey;
                    _this.formData.publicKey = data.publicKey;
                } else {
                    abp.message.error('获取秘钥失败咯!');
                }
            });
        },
        doEncryptData: function () {
            var _this = this;
            var data = {
                stringData: this.formData.originalData,
                privateKey: this.formData.privateKey,
                publicKey: this.formData.publicKey,
                keySizeInBits: this.formData.keySizeInBits,
                rsaEncryption: this.formData.rsaEncryption
            };
            abp.ajax({             
                url: '/Geetest/EncryptData',
                type: 'POST',
                data: JSON.stringify(data)
            }).done(function (data, res, e) {
                if (res.success) {
                    //公钥相对私钥显示内容少,其他内容相同,因此页面仅显示私钥内容作为演示
                    _this.formData.encryptData = data;
                } else {
                    abp.message.error('加密失败!');
                }
            });
        },
        doDecryptData: function () {
            var _this = this;
            var data = {
                stringData: this.formData.encryptData,
                privateKey: this.formData.privateKey,
                publicKey: this.formData.publicKey,
                keySizeInBits: this.formData.keySizeInBits,
                rsaEncryption: this.formData.rsaEncryption
            };
            abp.ajax({
                url: '/Geetest/DecryptData',
                type: 'POST',
                data: JSON.stringify(data)
            }).done(function (data, res, e) {
                if (res.success) {
                    //公钥相对私钥显示内容少,其他内容相同,因此页面仅显示私钥内容作为演示
                    _this.formData.decryptData = data;
                } else {
                    abp.message.error('解密失败!');
                }
            });
        },
        doSignData: function () {
            var _this = this;
            var data = {
                stringData: this.formData.originalData,
                privateKey: this.formData.privateKey,
                publicKey: this.formData.publicKey,
                keySizeInBits: this.formData.keySizeInBits,
                rsaEncryption: this.formData.rsaEncryption
            };
            abp.ajax({
                url: '/Geetest/SignData',
                type: 'POST',
                data: JSON.stringify(data)
            }).done(function (data, res, e) {
                if (res.success) {
                    //公钥相对私钥显示内容少,其他内容相同,因此页面仅显示私钥内容作为演示
                    _this.formData.signData = data;
                } else {
                    abp.message.error('签名失败!');
                }
            });
        }
    }
});
