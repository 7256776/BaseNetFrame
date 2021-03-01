
var component = Vue.component('sys-usersetting', {
    template: Vue.frameTemplate('SysAccount/UserSettings'),
    created: function () {
        this.getUserInfo();
    },
    data: function () {
        return {
            userName: '',
            userAvatars: '',
            isShowImageList: false,
            formRules: {
                userNameCn: [
                    { required: true, message: '必填项', trigger: 'change' },
                    { min: 1, max: 30, message: '长度在 1 到 30 个字符', trigger: 'blur' }
                ],
                emailAddress: [
                    { type: 'email', message: '请输入正确的邮箱地址', trigger: 'blur,change' }
                ],
                newPass: [
                    { required: true, message: '必填项', trigger: 'change' },
                    { min: 8,  message: '密码长度在 8 到 30 个字符', trigger: 'blur' },
                    { validator: this.checkPass, message: '密码必须包含 字母、数字、字符 其中的两种组合', trigger: 'change' },
                ],
                oldPass: [
                    { required: true, message: '必填项', trigger: 'change' }
                ],
                checkPass: [
                    { required: true, message: '必填项', trigger: 'change' },
                    { min: 8,  message: '密码长度在 8 到 30 个字符', trigger: 'blur' },
                    { validator: this.checkPass, message: '密码必须包含 字母、数字、字符 其中的两种组合', trigger: 'change' },
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
            formPassData: {
                id: null,
                oldPass: null,
                newPass: null,
                checkPass: null
            }
        };
    },
    watch: {
       //监听
    },
    methods: {
        checkPass: function (rule, value, callback) {
            if (!value) {
                return callback(new Error('密码不能为空'));
            }
            //密码强度分三级 1最高(必须满足字母,字符,数字)  2中等(满足字母,字符,数字其中的两种组合) 3中等(满足大小写字母)
            var isValid = abp.frameCore.utils.passwordStrength(value, 2);
            if (!isValid) {
                callback(new Error('密码必须包含 字母、数字、字符 其中的两种组合'));
            } else {
                callback();
            }
        },
        getUserInfo: function () {
            var _this = this;

            abp.ajax({
                url: '/SysAccount/GetUserModel',
                data: JSON.stringify(abp.session.userId),
                type: 'POST'
            }).done(function (data) {
                _this.formData = data;
                _this.formPassData.id = data.id;
                _this.userName = data.userNameCn;
                _this.userAvatars = data.imageUrl;
                //页面加载完成后执行验证避免出现错误的验证提示
                _this.$nextTick(function () {
                    //_this.$refs["formMyUserEl"].validate();
                });
            });
        },
        doSaveForm: function () {
            var _this = this;
            this.$refs["formMyUserEl"].validate(
                function (valid) {
                    if (valid) {
                        //
                        abp.ajax({
                            url: '/SysAccount/SeetingUserInfo',
                            data: JSON.stringify(_this.formData)
                        }).done(function (data, res, e) {
                            _this.tipSuccess('save');
                            _this.userName = _this.formData.userNameCn;
                        });
                        //基本上可以忽略监控fail,abp已经完成了这部分处理
                        //fail(function (res, e) { });

                    } else {
                        //console.log('error submit!!');
                        return false;
                    }
                }
            );
        },
        doSaveAvatars: function (i) {
            var _this = this;
            abp.ajax({
                url: '/SysAccount/SaveAvatars',
                data: JSON.stringify(i),
                type: 'POST'
            }).done(function (data) {
                Vue.prototype.GlobalAuthorizedEntity.user.refresh(i);
                _this.userAvatars = i;
                //刷新工具栏的头像
                abp.event.trigger('frame.userimg.ui.event');
                _this.tipShow('success', 'OperationComplete');
            });
        },
        doModifyPass: function () {
            var _this = this;
            this.$refs["formPassData"].validate(
                function (valid) {
                    if (valid) {
                        //
                        abp.ajax({
                            url: '/SysLogin/UpdateUserPass',
                            data: JSON.stringify(_this.formPassData)
                        }).done(function (data, res, e) {
                            _this.tipSuccess('edit');
                        });
                        //基本上可以忽略监控fail,abp已经完成了这部分处理
                        //fail(function (res, e) { });

                    } else {
                        //console.log('error submit!!');
                        return false;
                    }
                }
            );
        },
    }
});
